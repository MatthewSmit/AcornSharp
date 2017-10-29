using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public sealed partial class Parser
    {
        private sealed class Property
        {
            public bool init;
            public bool get;
            public bool set;

            public bool this[[NotNull] string kind]
            {
                get
                {
                    switch (kind)
                    {
                        case "init":
                            return init;
                        case "get":
                            return get;
                        case "set":
                            return set;
                        default:
                            throw new InvalidOperationException();
                    }
                }
                set
                {
                    switch (kind)
                    {
                        case "init":
                            init = value;
                            break;
                        case "get":
                            get = value;
                            break;
                        case "set":
                            set = value;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }
        }

        // A recursive descent parser operates by defining functions for all
        // syntactic elements, and recursively calling those, each function
        // advancing the input stream and returning an AST node. Precedence
        // of constructs (for example, the fact that `!x[1]` means `!(x[1])`
        // instead of `(!x)[1]` is handled by the fact that the parser
        // function that parses unary prefix operators is called first, and
        // in turn calls the function that parses `[]` subscripts — that
        // way, it'll receive the node for `x[1]` already parsed, and wraps
        // *that* in the unary operator node.
        //
        // Acorn uses an [operator precedence parser][opp] to handle binary
        // operator precedence, because it is much more compact than using
        // the technique outlined above, which uses different, nesting
        // functions to specify precedence, for all of the ten binary
        // precedence levels that JavaScript defines.
        //
        // [opp]: http://en.wikipedia.org/wiki/Operator-precedence_parser

        // Check if property name clashes with already added.
        // Object/class getters and setters are not allowed to clash —
        // either with each other or with an init property — and in
        // strict mode, init properties are also not allowed to be repeated.
        private void checkPropClash([NotNull] BaseNode prop, IDictionary<string, Property> propHash)
        {
            if (Options.ecmaVersion >= 6 && (prop.computed || prop.method || prop.shorthand))
                return;
            var key = prop.key;
            string name;
            if (key is IdentifierNode identifierNode)
                name = identifierNode.name;
            else if (key.type == NodeType.Literal)
            {
                name = key.value.ToString();
            }
            else
            {
                return;
            }
            var kind = prop.kind;
            if (Options.ecmaVersion >= 6)
            {
                if (name == "__proto__" && kind == "init")
                {
                    if (propHash.ContainsKey("proto")) raiseRecoverable(key.loc.Start, "Redefinition of __proto__ property");
                    propHash.Add("proto", new Property());
                }
                return;
            }
            name = "$" + name;
            if (propHash.TryGetValue(name, out var other))
            {
                bool redefinition;
                if (kind == "init")
                {
                    redefinition = strict && other.init || other.get || other.set;
                }
                else
                {
                    redefinition = other.init || other[kind];
                }
                if (redefinition)
                    raiseRecoverable(key.loc.Start, "Redefinition of property");
            }
            else
            {
                other = propHash[name] = new Property();
            }
            other[kind] = true;
        }

        // ### Expression parsing

        // These nest, from the most general expression type at the top to
        // 'atomic', nondivisible expression types at the bottom. Most of
        // the functions will simply let the function(s) below them parse,
        // and, *if* the syntactic construct they handle is present, wrap
        // the AST node that the inner parser gave them in another node.

        // Parse a full expression. The optional arguments are used to
        // forbid the `in` operator (in for loops initalization expressions)
        // and provide reference for storing '=' operator inside shorthand
        // property assignment in contexts where both object expression
        // and object pattern might appear (so it's possible to raise
        // delayed syntax error at correct position).

        private BaseNode parseExpression(bool noIn = false, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var start = this.start;
            var expr = parseMaybeAssign(noIn, refDestructuringErrors);
            if (type == TokenType.comma)
            {
                var expressions = new List<BaseNode> {expr};
                while (eat(TokenType.comma)) expressions.Add(parseMaybeAssign(noIn, refDestructuringErrors));
                var node = new BaseNode(this, start);
                node.expressions = expressions;
                return finishNode(node, NodeType.SequenceExpression);
            }
            return expr;
        }

        // Parse an assignment expression. This includes applications of
        // operators like `+=`.
        private BaseNode parseMaybeAssign(bool noIn = false, DestructuringErrors refDestructuringErrors = null, [CanBeNull] Func<Parser, BaseNode, int, Position, BaseNode> afterLeftParse = null)
        {
            if (inGenerator && isContextual("yield")) return parseYield();

            var ownDestructuringErrors = false;
            Position oldParenAssign = default;
            Position oldTrailingComma = default;
            if (refDestructuringErrors != null)
            {
                oldParenAssign = refDestructuringErrors.parenthesizedAssign;
                oldTrailingComma = refDestructuringErrors.trailingComma;
                refDestructuringErrors.parenthesizedAssign = refDestructuringErrors.trailingComma = default;
            }
            else
            {
                refDestructuringErrors = new DestructuringErrors();
                ownDestructuringErrors = true;
            }

            var startLoc = start;
            if (type == TokenType.parenL || type == TokenType.name)
                potentialArrowAt = start;
            var left = parseMaybeConditional(noIn, refDestructuringErrors);
            if (afterLeftParse != null) left = afterLeftParse(this, left, start.Index, startLoc);
            if (type == TokenType.eq || type == TokenType.assign)
            {
                checkPatternErrors(refDestructuringErrors, true);
                if (!ownDestructuringErrors) refDestructuringErrors.Reset();
                var @operator = (string)value;
                var leftNode = type == TokenType.eq ? toAssignable(left) : left;
                refDestructuringErrors.shorthandAssign = default; // reset because shorthand default was used correctly
                checkLVal(left);
                next();
                var right = parseMaybeAssign(noIn);
                var node = new BaseNode(this, startLoc);
                node.@operator = @operator;
                node.left = leftNode;
                node.right = right;
                return finishNode(node, NodeType.AssignmentExpression);
            }
            if (ownDestructuringErrors) checkExpressionErrors(refDestructuringErrors, true);
            if (oldParenAssign.Line > 0) refDestructuringErrors.parenthesizedAssign = oldParenAssign;
            if (oldTrailingComma.Line > 0) refDestructuringErrors.trailingComma = oldTrailingComma;
            return left;
        }

        // Parse a ternary conditional (`?:`) operator.
        private BaseNode parseMaybeConditional(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            var expr = parseExprOps(noIn, refDestructuringErrors);
            if (checkExpressionErrors(refDestructuringErrors)) return expr;
            if (eat(TokenType.question))
            {
                var consequent = parseMaybeAssign();
                expect(TokenType.colon);
                var alternate = parseMaybeAssign(noIn);
                return new ConditionalExpressionNode(this, startLoc, lastTokEnd, expr, consequent, alternate);
            }
            return expr;
        }

        // Start the precedence parser.
        private BaseNode parseExprOps(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            var expr = parseMaybeUnary(refDestructuringErrors, false);
            if (checkExpressionErrors(refDestructuringErrors)) return expr;
            return expr.loc.Start.Index == startLoc.Index && expr.type == NodeType.ArrowFunctionExpression ? expr : parseExprOp(expr, startLoc, -1, noIn);
        }

        // Parse binary operators with the operator precedence parsing
        // algorithm. `left` is the left-hand side of the operator.
        // `minPrec` provides context that allows the function to stop and
        // defer further parser to one of its callers when it encounters an
        // operator that has a lower precedence than the set it is parsing.
        private BaseNode parseExprOp(BaseNode left, Position leftStartLoc, int minPrec, bool noIn)
        {
            var prec = TokenInformation.Types[type].BinaryOperation;
            if (prec >= 0 && (!noIn || type != TokenType._in))
            {
                if (prec > minPrec)
                {
                    var logical = type == TokenType.logicalOR || type == TokenType.logicalAND;
                    var op = (string)value;
                    next();
                    var startLoc = start;
                    var right = parseExprOp(parseMaybeUnary(null, false), startLoc, prec, noIn);
                    var node = buildBinary(leftStartLoc, left, right, op, logical);
                    return parseExprOp(node, leftStartLoc, minPrec, noIn);
                }
            }
            return left;
        }

        [NotNull]
        private BaseNode buildBinary(Position startLoc, BaseNode left, BaseNode right, string op, bool logical)
        {
            var node = new BaseNode(this, startLoc);
            node.left = left;
            node.@operator = op;
            node.right = right;
            return finishNode(node, logical ? NodeType.LogicalExpression : NodeType.BinaryExpression);
        }

        // Parse unary operators, both prefix and postfix.
        private BaseNode parseMaybeUnary(DestructuringErrors refDestructuringErrors, bool sawUnary)
        {
            var startLoc = start;
            BaseNode expr;
            if (inAsync && isContextual("await"))
            {
                expr = parseAwait();
                sawUnary = true;
            }
            else if (TokenInformation.Types[type].Prefix)
            {
                var node = new BaseNode(this, start);
                var update = type == TokenType.incDec;
                node.@operator = (string)value;
                node.prefix = true;
                next();
                node.argument = parseMaybeUnary(null, true);
                checkExpressionErrors(refDestructuringErrors, true);
                if (update) checkLVal(node.argument);
                else if (strict && node.@operator == "delete" &&
                         node.argument is IdentifierNode)
                    raiseRecoverable(node.loc.Start, "Deleting local variable in strict mode");
                else sawUnary = true;
                expr = finishNode(node, update ? NodeType.UpdateExpression : NodeType.UnaryExpression);
            }
            else
            {
                expr = parseExprSubscripts(refDestructuringErrors);
                if (checkExpressionErrors(refDestructuringErrors)) return expr;
                while (TokenInformation.Types[type].Postfix && !canInsertSemicolon())
                {
                    var node = new BaseNode(this, startLoc);
                    node.@operator = (string)value;
                    node.prefix = false;
                    node.argument = expr;
                    checkLVal(expr);
                    next();
                    expr = finishNode(node, NodeType.UpdateExpression);
                }
            }

            if (!sawUnary && eat(TokenType.starstar))
                return buildBinary(startLoc, expr, parseMaybeUnary(null, false), "**", false);
            return expr;
        }

        // Parse call, dot, and `[]`-subscript expressions.
        private BaseNode parseExprSubscripts([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var startLoc = start;
            var expr = parseExprAtom(refDestructuringErrors);
            var skipArrowSubscripts = expr.type == NodeType.ArrowFunctionExpression && input.Substring(lastTokStart.Index, lastTokEnd.Index - lastTokStart.Index) != ")";
            if (checkExpressionErrors(refDestructuringErrors) || skipArrowSubscripts) return expr;
            var result = parseSubscripts(expr, startLoc);
            if (refDestructuringErrors != null && result is MemberExpressionNode)
            {
                if (refDestructuringErrors.parenthesizedAssign.Index >= result.loc.Start.Index) refDestructuringErrors.parenthesizedAssign = default;
                if (refDestructuringErrors.parenthesizedBind.Index >= result.loc.Start.Index) refDestructuringErrors.parenthesizedBind = default;
            }
            return result;
        }

        private BaseNode parseSubscripts(BaseNode @base, Position startLoc, bool noCalls = false)
        {
            var maybeAsyncArrow = Options.ecmaVersion >= 8 && @base is IdentifierNode identifierNode && identifierNode.name == "async" &&
                                  lastTokEnd.Index == @base.loc.End.Index && !canInsertSemicolon();
            for (;;)
            {
                bool computed;
                if ((computed = eat(TokenType.bracketL)) || eat(TokenType.dot))
                {
                    var property = computed ? parseExpression() : parseIdent(true);
                    if (computed) expect(TokenType.bracketR);
                    @base = new MemberExpressionNode(this, startLoc, lastTokEnd, @base, property, computed);
                }
                else if (!noCalls && eat(TokenType.parenL))
                {
                    var refDestructuringErrors = new DestructuringErrors();
                    var oldYieldPos = yieldPos;
                    var oldAwaitPos = awaitPos;
                    yieldPos = default;
                    awaitPos = default;
                    var exprList = parseExprList(TokenType.parenR, Options.ecmaVersion >= 8, false, refDestructuringErrors);
                    if (maybeAsyncArrow && !canInsertSemicolon() && eat(TokenType.arrow))
                    {
                        checkPatternErrors(refDestructuringErrors, false);
                        checkYieldAwaitInDefaultParams();
                        yieldPos = oldYieldPos;
                        awaitPos = oldAwaitPos;
                        return parseArrowExpression(new BaseNode(this, startLoc), exprList, true);
                    }
                    checkExpressionErrors(refDestructuringErrors, true);
                    yieldPos = oldYieldPos.Line != 0 ? oldYieldPos : yieldPos;
                    awaitPos = oldAwaitPos.Line != 0 ? oldAwaitPos : awaitPos;
                    var node = new BaseNode(this, startLoc);
                    node.callee = @base;
                    node.arguments = exprList;
                    @base = finishNode(node, NodeType.CallExpression);
                }
                else if (type == TokenType.backQuote)
                {
                    var node = new BaseNode(this, startLoc);
                    node.tag = @base;
                    node.quasi = parseTemplate(true);
                    @base = finishNode(node, NodeType.TaggedTemplateExpression);
                }
                else
                {
                    return @base;
                }
            }
        }

        // Parse an atomic expression — either a single token that is an
        // expression, an expression started by a keyword like `function` or
        // `new`, or an expression wrapped in punctuation like `()`, `[]`,
        // or `{}`.
        private BaseNode parseExprAtom([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            BaseNode node;
            var canBeArrow = potentialArrowAt.Index == this.start.Index;
            if (type == TokenType._super)
            {
                if (!inFunction)
                    raise(start, "'super' outside of function or class");
                node = new BaseNode(this, start);
                next();

                // The `super` keyword can appear at below:
                // SuperProperty:
                //     super [ Expression ]
                //     super . IdentifierName
                // SuperCall:
                //     super Arguments
                if (type != TokenType.dot && type != TokenType.bracketL && type != TokenType.parenL)
                {
                    raise(start, "Unexpected token");
                }
                return finishNode(node, NodeType.Super);
            }
            if (type == TokenType._this)
            {
                node = new BaseNode(this, start);
                next();
                return finishNode(node, NodeType.ThisExpression);
            }
            if (type == TokenType.name)
            {
                var startLoc = start;
                var id = parseIdent(type != TokenType.name);
                if (Options.ecmaVersion >= 8 && id.name == "async" && !canInsertSemicolon() && eat(TokenType._function))
                    return parseFunction(new BaseNode(this, startLoc), null, false, true);
                if (canBeArrow && !canInsertSemicolon())
                {
                    if (eat(TokenType.arrow))
                        return parseArrowExpression(new BaseNode(this, startLoc), new BaseNode[] {id});
                    if (Options.ecmaVersion >= 8 && id.name == "async" && type == TokenType.name)
                    {
                        id = parseIdent();
                        if (canInsertSemicolon() || !eat(TokenType.arrow))
                        {
                            raise(start, "Unexpected token");
                        }
                        return parseArrowExpression(new BaseNode(this, startLoc), new BaseNode[] {id}, true);
                    }
                }
                return id;
            }
            if (type == TokenType.regexp)
            {
                var value = this.value;
                node = parseLiteral(((RegexNode)value).value);
                node.regex = new RegexNode {pattern = ((RegexNode)value).pattern, flags = ((RegexNode)value).flags};
                return node;
            }
            if (type == TokenType.num || type == TokenType.@string)
            {
                return parseLiteral(value);
            }
            if (type == TokenType._null || type == TokenType._true || type == TokenType._false)
            {
                node = new BaseNode(this, start);
                node.value = type == TokenType._null ? null : (object)(type == TokenType._true);
                node.raw = TokenInformation.Types[type].Keyword;
                next();
                return finishNode(node, NodeType.Literal);
            }
            if (type == TokenType.parenL)
            {
                var start = this.start;
                var expr = parseParenAndDistinguishExpression(canBeArrow);
                if (refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.parenthesizedAssign.Line == 0 && !isSimpleAssignTarget(expr))
                        refDestructuringErrors.parenthesizedAssign = start;
                    if (refDestructuringErrors.parenthesizedBind.Line == 0)
                        refDestructuringErrors.parenthesizedBind = start;
                }
                return expr;
            }
            if (type == TokenType.bracketL)
            {
                node = new BaseNode(this, start);
                next();
                node.elements = parseExprList(TokenType.bracketR, true, true, refDestructuringErrors);
                return finishNode(node, NodeType.ArrayExpression);
            }
            if (type == TokenType.braceL)
            {
                return parseObj(false, refDestructuringErrors);
            }
            if (type == TokenType._function)
            {
                node = new BaseNode(this, start);
                next();
                return parseFunction(node, null);
            }
            if (type == TokenType._class)
            {
                return parseClass(new BaseNode(this, start), null);
            }
            if (type == TokenType._new)
            {
                return parseNew();
            }
            if (type == TokenType.backQuote)
            {
                return parseTemplate();
            }
            raise(start, "Unexpected token");
            return null;
        }

        [NotNull]
        private BaseNode parseLiteral(object value)
        {
            var node = new BaseNode(this, start);
            node.value = value;
            node.raw = input.Substring(start.Index, end.Index - start.Index);
            next();
            return finishNode(node, NodeType.Literal);
        }

        private BaseNode parseParenExpression()
        {
            expect(TokenType.parenL);
            var val = parseExpression();
            expect(TokenType.parenR);
            return val;
        }

        private BaseNode parseParenAndDistinguishExpression(bool canBeArrow)
        {
            var startLoc = start;
            BaseNode val;
            var allowTrailingComma = Options.ecmaVersion >= 8;
            if (Options.ecmaVersion >= 6)
            {
                next();

                var innerStartLoc = start;
                var exprList = new List<BaseNode>();
                var first = true;
                var lastIsComma = false;
                var refDestructuringErrors = new DestructuringErrors();
                var oldYieldPos = yieldPos;
                var oldAwaitPos = awaitPos;
                Position spreadStart = default;
                Position innerParenStart = default;
                yieldPos = default;
                awaitPos = default;
                while (type != TokenType.parenR)
                {
                    if (first)
                        first = false;
                    else expect(TokenType.comma);
                    if (allowTrailingComma && afterTrailingComma(TokenType.parenR, true))
                    {
                        lastIsComma = true;
                        break;
                    }
                    if (type == TokenType.ellipsis)
                    {
                        spreadStart = start;
                        exprList.Add(parseParenItem(parseRestBinding()));
                        if (type == TokenType.comma) raise(start, "Comma is not permitted after the rest element");
                        break;
                    }
                    if (type == TokenType.parenL && innerParenStart.Line == 0)
                    {
                        innerParenStart = start;
                    }
                    exprList.Add(parseMaybeAssign(false, refDestructuringErrors, parseParenItem));
                }
                var innerEndLoc = start;
                expect(TokenType.parenR);

                if (canBeArrow && !canInsertSemicolon() && eat(TokenType.arrow))
                {
                    checkPatternErrors(refDestructuringErrors, false);
                    checkYieldAwaitInDefaultParams();
                    if (innerParenStart.Line > 0)
                    {
                        raise(innerParenStart, "Unexpected token");
                    }
                    yieldPos = oldYieldPos;
                    awaitPos = oldAwaitPos;
                    return parseParenArrowList(startLoc, exprList);
                }

                if (exprList.Count == 0 || lastIsComma)
                {
                    raise(lastTokStart, "Unexpected token");
                }
                if (spreadStart.Line > 0)
                {
                    raise(spreadStart, "Unexpected token");
                }
                checkExpressionErrors(refDestructuringErrors, true);
                yieldPos = oldYieldPos.Line != 0 ? oldYieldPos : yieldPos;
                awaitPos = oldAwaitPos.Line != 0 ? oldAwaitPos : awaitPos;

                if (exprList.Count > 1)
                {
                    val = new BaseNode(this, innerStartLoc);
                    val.expressions = exprList;
                    finishNodeAt(val, NodeType.SequenceExpression, innerEndLoc);
                }
                else
                {
                    val = exprList[0];
                }
            }
            else
            {
                val = parseParenExpression();
            }

            if (Options.preserveParens)
            {
                var par = new BaseNode(this, startLoc);
                par.expression = val;
                return finishNode(par, NodeType.ParenthesizedExpression);
            }
            return val;
        }

        private static BaseNode parseParenItem(BaseNode item)
        {
            return item;
        }

        private static BaseNode parseParenItem(Parser parser, BaseNode item, int position, Position location)
        {
            return item;
        }

        [NotNull]
        private BaseNode parseParenArrowList(Position startLoc, [NotNull] IList<BaseNode> exprList)
        {
            return parseArrowExpression(new BaseNode(this, startLoc), exprList);
        }

        // New's precedence is slightly tricky. It must allow its argument to
        // be a `[]` or dot subscript expression, but not a call — at least,
        // not without wrapping it in parentheses. Thus, it uses the noCalls
        // argument to parseSubscripts to prevent it from consuming the
        // argument list

        [NotNull]
        private BaseNode parseNew()
        {
            var node = new BaseNode(this, start);
            var meta = parseIdent(true);
            if (Options.ecmaVersion >= 6 && eat(TokenType.dot))
            {
                node.meta = meta;
                var identifierNode = parseIdent(true);
                node.property = identifierNode;
                if (identifierNode.name != "target")
                    raiseRecoverable(node.property.loc.Start, "The only valid meta property for new is new.target");
                if (!inFunction)
                    raiseRecoverable(node.loc.Start, "new.target can only be used in functions");
                return finishNode(node, NodeType.MetaProperty);
            }
            var startLoc = start;
            node.callee = parseSubscripts(parseExprAtom(), startLoc, true);
            if (eat(TokenType.parenL)) node.arguments = parseExprList(TokenType.parenR, Options.ecmaVersion >= 8, false);
            else node.arguments = new List<BaseNode>();
            return finishNode(node, NodeType.NewExpression);
        }

        private static readonly Regex templateRawRegex = new Regex("\r\n?");

        // Parse template expression.
        [NotNull]
        private BaseNode parseTemplateElement(ref bool isTagged)
        {
            var elem = new BaseNode(this, start);
            if (type == TokenType.invalidTemplate)
            {
                if (!isTagged)
                {
                    raiseRecoverable(start, "Bad escape sequence in untagged template literal");
                }
                elem.value = new TemplateNode((string)value, null);
            }
            else
            {
                elem.value = new TemplateNode(templateRawRegex.Replace(input.Substring(start.Index, end.Index - start.Index), "\n"), (string)value);
            }
            next();
            elem.tail = type == TokenType.backQuote;
            return finishNode(elem, NodeType.TemplateElement);
        }

        [NotNull]
        private BaseNode parseTemplate(bool isTagged = false)
        {
            var node = new BaseNode(this, start);
            next();
            node.expressions = new List<BaseNode>();
            var curElt = parseTemplateElement(ref isTagged);
            node.quasis = new List<BaseNode> {curElt};
            while (!curElt.tail)
            {
                expect(TokenType.dollarBraceL);
                node.expressions.Add(parseExpression());
                expect(TokenType.braceR);
                node.quasis.Add(curElt = parseTemplateElement(ref isTagged));
            }
            next();
            return finishNode(node, NodeType.TemplateLiteral);
        }

        // Parse an object literal or binding pattern.
        private bool isAsyncProp([NotNull] BaseNode prop)
        {
            return !prop.computed && prop.key is IdentifierNode identifierNode && identifierNode.name == "async" &&
                   (type == TokenType.name || type == TokenType.num || type == TokenType.@string || type == TokenType.bracketL || TokenInformation.Types[type].Keyword != null) &&
                   !lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index));
        }

        [NotNull]
        private BaseNode parseObj(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var node = new BaseNode(this, start);
            var first = true;
            var propHash = new Dictionary<string, Property>();
            node.properties = new List<BaseNode>();
            next();
            while (!eat(TokenType.braceR))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (afterTrailingComma(TokenType.braceR)) break;
                }
                else first = false;

                var prop = new BaseNode(this, start);
                var isGenerator = false;
                bool isAsync;
                Position startLoc = default;
                if (Options.ecmaVersion >= 6)
                {
                    prop.method = false;
                    prop.shorthand = false;
                    if (isPattern || refDestructuringErrors != null)
                    {
                        startLoc = start;
                    }
                    if (!isPattern)
                        isGenerator = eat(TokenType.star);
                }
                parsePropertyName(prop);
                if (!isPattern && Options.ecmaVersion >= 8 && !isGenerator && isAsyncProp(prop))
                {
                    isAsync = true;
                    parsePropertyName(prop);
                }
                else
                {
                    isAsync = false;
                }
                parsePropertyValue(prop, isPattern, isGenerator, isAsync, startLoc, refDestructuringErrors);
                checkPropClash(prop, propHash);
                node.properties.Add(finishNode(prop, NodeType.Property));
            }
            return finishNode(node, isPattern ? NodeType.ObjectPattern : NodeType.ObjectExpression);
        }

        private void parsePropertyValue([NotNull] BaseNode prop, bool isPattern, bool isGenerator, bool isAsync, Position startLoc, [CanBeNull] DestructuringErrors refDestructuringErrors)
        {
            if ((isGenerator || isAsync) && type == TokenType.colon)
            {
                raise(start, "Unexpected token");
            }

            if (eat(TokenType.colon))
            {
                prop.value = isPattern ? parseMaybeDefault(start) : parseMaybeAssign(false, refDestructuringErrors);
                prop.kind = "init";
            }
            else if (Options.ecmaVersion >= 6 && type == TokenType.parenL)
            {
                if (isPattern)
                {
                    raise(start, "Unexpected token");
                }
                prop.kind = "init";
                prop.method = true;
                prop.value = parseMethod(isGenerator, isAsync);
            }
            else if (!isPattern &&
                     Options.ecmaVersion >= 5 && !prop.computed && prop.key is IdentifierNode identifierNode &&
                     (identifierNode.name == "get" || identifierNode.name == "set") &&
                     type != TokenType.comma && type != TokenType.braceR)
            {
                if (isGenerator || isAsync)
                {
                    raise(start, "Unexpected token");
                }
                prop.kind = identifierNode.name;
                parsePropertyName(prop);
                prop.value = parseMethod(false);
                var paramCount = prop.kind == "get" ? 0 : 1;
                var value = (BaseNode)prop.value;
                if (value.@params.Count != paramCount)
                {
                    var start = value.loc.Start;
                    if (prop.kind == "get")
                        raiseRecoverable(start, "getter should have no params");
                    else
                        raiseRecoverable(start, "setter should have exactly one param");
                }
                else
                {
                    if (prop.kind == "set" && value.@params[0].type == NodeType.RestElement)
                        raiseRecoverable(value.@params[0].loc.Start, "Setter cannot use rest params");
                }
            }
            else if (Options.ecmaVersion >= 6 && !prop.computed && prop.key is IdentifierNode identifierNode2)
            {
                checkUnreserved(prop.key.loc.Start, prop.key.loc.End, identifierNode2.name);
                prop.kind = "init";
                if (isPattern)
                {
                    prop.value = parseMaybeDefault(startLoc, prop.key);
                }
                else if (type == TokenType.eq && refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.shorthandAssign.Line == 0)
                        refDestructuringErrors.shorthandAssign = start;
                    prop.value = parseMaybeDefault(startLoc, prop.key);
                }
                else
                {
                    prop.value = prop.key;
                }
                prop.shorthand = true;
            }
            else
            {
                raise(start, "Unexpected token");
            }
        }

        private BaseNode parsePropertyName([NotNull] BaseNode prop)
        {
            if (Options.ecmaVersion >= 6)
            {
                if (eat(TokenType.bracketL))
                {
                    prop.computed = true;
                    prop.key = parseMaybeAssign();
                    expect(TokenType.bracketR);
                    return prop.key;
                }
                prop.computed = false;
            }
            return prop.key = type == TokenType.num || type == TokenType.@string ? parseExprAtom() : parseIdent(true);
        }

        // Initialize empty function node.
        private void initFunction([NotNull] BaseNode node)
        {
            node.id = null;
            if (Options.ecmaVersion >= 6)
            {
                node.generator = false;
                node.expression = null;
            }
            if (Options.ecmaVersion >= 8)
                node.async = false;
        }

        // Parse object or class method.
        [NotNull]
        private BaseNode parseMethod(bool isGenerator, bool isAsync = false)
        {
            var node = new BaseNode(this, start);
            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;

            initFunction(node);
            if (Options.ecmaVersion >= 6)
                node.generator = isGenerator;
            if (Options.ecmaVersion >= 8)
                node.async = isAsync;

            inGenerator = node.generator;
            inAsync = node.async;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;
            enterFunctionScope();

            expect(TokenType.parenL);
            node.@params = parseBindingList(TokenType.parenR, false, Options.ecmaVersion >= 8);
            checkYieldAwaitInDefaultParams();
            parseFunctionBody(node, false);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;
            return finishNode(node, NodeType.FunctionExpression);
        }

        // Parse arrow function expression with given parameters.
        [NotNull]
        private BaseNode parseArrowExpression([NotNull] BaseNode node, [NotNull] IList<BaseNode> @params, bool isAsync = false)
        {
            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;

            enterFunctionScope();
            initFunction(node);
            if (Options.ecmaVersion >= 8)
                node.async = isAsync;

            inGenerator = false;
            inAsync = node.async;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;

            node.@params = toAssignableList(@params, true);
            parseFunctionBody(node, true);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;
            return finishNode(node, NodeType.ArrowFunctionExpression);
        }

        // Parse function body and check parameters.
        private void parseFunctionBody([NotNull] BaseNode node, bool isArrowFunction)
        {
            var isExpression = isArrowFunction && type != TokenType.braceL;
            var oldStrict = strict;
            var useStrict = false;

            if (isExpression)
            {
                node.fbody = parseMaybeAssign();
                node.bexpression = true;
                checkParams(node, false);
            }
            else
            {
                var nonSimple = Options.ecmaVersion >= 7 && !isSimpleParamList(node.@params);
                if (!oldStrict || nonSimple)
                {
                    useStrict = strictDirective(end.Index);
                    // If this is a strict mode function, verify that argument names
                    // are not repeated, and it does not try to bind the words `eval`
                    // or `arguments`.
                    if (useStrict && nonSimple)
                        raiseRecoverable(node.loc.Start, "Illegal 'use strict' directive in function with non-simple parameter list");
                }
                // Start a new scope with regard to labels and the `inFunction`
                // flag (restore them to their old value afterwards).
                var oldLabels = labels;
                labels = new List<Label>();
                if (useStrict) strict = true;

                // Add the params to varDeclaredNames to ensure that an error is thrown
                // if a let/const declaration in the function clashes with one of the params.
                checkParams(node, !oldStrict && !useStrict && !isArrowFunction && isSimpleParamList(node.@params));
                node.fbody = parseBlock(false);
                node.bexpression = false;
                adaptDirectivePrologue(node.fbody.body);
                labels = oldLabels;
            }
            exitFunctionScope();

            if (strict && node.id != null)
            {
                // Ensure the function name isn't a forbidden identifier in strict mode, e.g. 'eval'
                checkLVal(node.id, "none");
            }
            strict = oldStrict;
        }

        private static bool isSimpleParamList([NotNull] IEnumerable<BaseNode> @params)
        {
            foreach (var param in @params)
            {
                if (!(param is IdentifierNode))
                {
                    return false;
                }
            }
            return true;
        }

        // Checks function params for various disallowed patterns such as using "eval"
        // or "arguments" and duplicate parameters.
        private void checkParams([NotNull] BaseNode node, bool allowDuplicates)
        {
            var nameHash = new HashSet<string>();
            foreach (var param in node.@params)
                checkLVal(param, "var", allowDuplicates ? null : nameHash);
        }

        // Parses a comma-separated list of expressions, and returns them as
        // an array. `close` is the token type that ends the list, and
        // `allowEmpty` can be turned on to allow subsequent commas with
        // nothing in between them to be parsed as `null` (which is needed
        // for array literals).
        [NotNull]
        private IList<BaseNode> parseExprList(TokenType close, bool allowTrailingComma, bool allowEmpty, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var elts = new List<BaseNode>();
            var first = true;
            while (!eat(close))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (allowTrailingComma && afterTrailingComma(close)) break;
                }
                else first = false;

                BaseNode elt;
                if (allowEmpty && type == TokenType.comma)
                    elt = null;
                else if (type == TokenType.ellipsis)
                {
                    elt = parseSpread(refDestructuringErrors);
                    if (refDestructuringErrors != null && type == TokenType.comma && refDestructuringErrors.trailingComma.Line == 0)
                        refDestructuringErrors.trailingComma = start;
                }
                else
                {
                    elt = parseMaybeAssign(false, refDestructuringErrors);
                }
                elts.Add(elt);
            }
            return elts;
        }

        // Parse the next token as an identifier. If `liberal` is true (used
        // when parsing properties), it will also convert keywords into
        // identifiers.
        private void checkUnreserved(Position start, Position end, string name)
        {
            if (inGenerator && name == "yield")
                raiseRecoverable(start, "Can not use 'yield' as identifier inside a generator");
            if (inAsync && name == "await")
                raiseRecoverable(start, "Can not use 'await' as identifier inside an async function");
            if (keywords.IsMatch(name))
                raise(start, $"Unexpected keyword '{name}'");
            if (Options.ecmaVersion < 6 && input.Substring(start.Index, end - start).IndexOf("\\", StringComparison.Ordinal) != -1)
                return;
            var re = strict ? reservedWordsStrict : reservedWords;
            if (re.IsMatch(name))
                raiseRecoverable(start, $"The keyword '{name}' is reserved");
        }

        [NotNull]
        private IdentifierNode parseIdent(bool liberal = false)
        {
            var start = this.start;
            if (liberal && "never".Equals(Options.allowReserved)) liberal = false;

            string name = null;
            if (type == TokenType.name)
            {
                name = (string)value;
            }
            else if (TokenInformation.Types[type].Keyword != null)
            {
                name = TokenInformation.Types[type].Keyword;

                // To fix https://github.com/ternjs/acorn/issues/575
                // `class` and `function` keywords push new context into this.context.
                // But there is no chance to pop the context if the keyword is consumed as an identifier such as a property name.
                if (name == "class" || name == "function")
                {
                    context.Pop();
                }
            }
            else
            {
                raise(this.start, "Unexpected token");
            }
            next();
            var node = new IdentifierNode(this, start, lastTokEnd, name);
            if (!liberal) checkUnreserved(node.loc.Start, node.loc.Start, node.name);
            return node;
        }

        // Parses yield expression inside generator.
        [NotNull]
        private BaseNode parseYield()
        {
            if (yieldPos.Line == 0) yieldPos = start;

            var node = new BaseNode(this, start);
            next();
            if (type == TokenType.semi || canInsertSemicolon() || type != TokenType.star && !TokenInformation.Types[type].StartsExpression)
            {
                node.@delegate = false;
                node.argument = null;
            }
            else
            {
                node.@delegate = eat(TokenType.star);
                node.argument = parseMaybeAssign();
            }
            return finishNode(node, NodeType.YieldExpression);
        }

        [NotNull]
        private BaseNode parseAwait()
        {
            if (awaitPos.Line == 0) awaitPos = start;

            var node = new BaseNode(this, start);
            next();
            node.argument = parseMaybeUnary(null, true);
            return finishNode(node, NodeType.AwaitExpression);
        }
    }
}
