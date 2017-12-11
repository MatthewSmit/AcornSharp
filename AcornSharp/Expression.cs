using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    internal sealed partial class Parser
    {
        private sealed class Property
        {
            public bool init;
            public bool get;
            public bool set;

            public bool this[PropertyKind kind]
            {
                get
                {
                    switch (kind)
                    {
                        case PropertyKind.Initialise:
                            return init;
                        case PropertyKind.Get:
                            return get;
                        case PropertyKind.Set:
                            return set;
                        default:
                            throw new InvalidOperationException();
                    }
                }
                set
                {
                    switch (kind)
                    {
                        case PropertyKind.Initialise:
                            init = value;
                            break;
                        case PropertyKind.Get:
                            get = value;
                            break;
                        case PropertyKind.Set:
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
        private void CheckPropertyClash([NotNull] PropertyNode prop, IDictionary<string, Property> propHash)
        {
            if (Options.ecmaVersion >= 6 && (prop.Computed || prop.Method || prop.Shorthand))
                return;
            var key = prop.Key;
            string name;
            if (key is IdentifierNode identifierNode)
                name = identifierNode.Name;
            else if (key is LiteralNode literalNode)
            {
                name = literalNode.Value.ToString();
            }
            else
            {
                return;
            }
            var kind = prop.Kind;
            if (Options.ecmaVersion >= 6)
            {
                if (name == "__proto__" && kind == PropertyKind.Initialise)
                {
                    if (propHash.ContainsKey("proto")) raiseRecoverable(key.Location.Start, "Redefinition of __proto__ property");
                    propHash.Add("proto", new Property());
                }
                return;
            }
            name = "$" + name;
            if (propHash.TryGetValue(name, out var other))
            {
                bool redefinition;
                if (kind == PropertyKind.Initialise)
                {
                    redefinition = strict && other.init || other.get || other.set;
                }
                else
                {
                    redefinition = other.init || other[kind];
                }
                if (redefinition)
                    raiseRecoverable(key.Location.Start, "Redefinition of property");
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
        public ExpressionNode ParseExpression(bool noIn = false, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var startLocation = start;
            var expr = ParseMaybeAssign(noIn, refDestructuringErrors);
            if (type == TokenType.comma)
            {
                var expressions = new List<ExpressionNode>
                {
                    expr
                };
                while (eat(TokenType.comma))
                {
                    expressions.Add(ParseMaybeAssign(noIn, refDestructuringErrors));
                }

                return new SequenceExpressionNode(this, startLocation, lastTokEnd, expressions);
            }
            return expr;
        }

        // Parse an assignment expression. This includes applications of
        // operators like `+=`.
        private ExpressionNode ParseMaybeAssign(bool noIn = false, DestructuringErrors refDestructuringErrors = null, [CanBeNull] Func<Parser, ExpressionNode, int, Position, ExpressionNode> afterLeftParse = null)
        {
            if (inGenerator && isContextual("yield"))
                return parseYield();

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
            var left = ParseMaybeConditional(noIn, refDestructuringErrors);
            if (afterLeftParse != null)
                left = afterLeftParse(this, left, start.Index, startLoc);
            if (type == TokenType.eq || type == TokenType.assign)
            {
                checkPatternErrors(refDestructuringErrors, true);
                if (!ownDestructuringErrors) refDestructuringErrors.Reset();
                var @operator = StringToOperator((string)value);
                var leftNode = type == TokenType.eq ? toAssignable(left) : left;
                refDestructuringErrors.shorthandAssign = default; // reset because shorthand default was used correctly
                checkLVal(leftNode, false, null);
                next();
                var right = ParseMaybeAssign(noIn);
                return new AssignmentExpressionNode(this, startLoc, lastTokEnd, leftNode, right, @operator);
            }
            if (ownDestructuringErrors) checkExpressionErrors(refDestructuringErrors, true);
            if (oldParenAssign.Line > 0) refDestructuringErrors.parenthesizedAssign = oldParenAssign;
            if (oldTrailingComma.Line > 0) refDestructuringErrors.trailingComma = oldTrailingComma;
            return left;
        }

        private static Operator StringToOperator([NotNull] string s)
        {
            switch (s)
            {
                case "+": return Operator.Addition;
                case "-": return Operator.Subtraction;
                case "*": return Operator.Multiplication;
                case "/": return Operator.Division;
                case "%": return Operator.Modulus;
                case "**": return Operator.Power;
                case "<<": return Operator.LeftShift;
                case ">>": return Operator.RightShift;
                case ">>>": return Operator.RightShiftUnsigned;
                case "&": return Operator.BitwiseAnd;
                case "|": return Operator.BitwiseOr;
                case "^": return Operator.BitwiseXOr;

                case "==": return Operator.Equals;
                case "===": return Operator.StrictEquals;
                case "!=": return Operator.NotEquals;
                case "!==": return Operator.StrictNotEquals;
                case "<": return Operator.LessThan;
                case "<=": return Operator.LessEquals;
                case ">": return Operator.GreaterThan;
                case ">=": return Operator.GreaterEquals;
                case "&&": return Operator.LogicalAnd;
                case "||": return Operator.LogicalOr;

                case "=": return Operator.Assignment;
                case "+=": return Operator.AdditionAssignment;
                case "-=": return Operator.SubtractionAssignment;
                case "*=": return Operator.MultiplicationAssignment;
                case "/=": return Operator.DivisionAssignment;
                case "%=": return Operator.ModulusAssignment;
                case "**=": return Operator.PowerAssignment;
                case "<<=": return Operator.LeftShiftAssignment;
                case ">>=": return Operator.RightShiftAssignment;
                case ">>>=": return Operator.RightShiftUnsignedAssignment;
                case "&=": return Operator.BitwiseAndAssignment;
                case "|=": return Operator.BitwiseOrAssignment;
                case "^=": return Operator.BitwiseXOrAssignment;

                case "++": return Operator.Increment;
                case "--": return Operator.Decrement;
                case "~": return Operator.BitwiseNot;
                case "!": return Operator.LogicalNot;
                case "delete": return Operator.Delete;
                case "in": return Operator.In;
                case "instanceof": return Operator.InstanceOf;
                case "void": return Operator.Void;
                case "typeof": return Operator.TypeOf;

                default:
                    throw new ArgumentException();
            }
        }

        // Parse a ternary conditional (`?:`) operator.
        private ExpressionNode ParseMaybeConditional(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            var expr = ParseExpressionOperators(noIn, refDestructuringErrors);
            if (checkExpressionErrors(refDestructuringErrors))
                return expr;
            if (eat(TokenType.question))
            {
                var consequent = ParseMaybeAssign();
                expect(TokenType.colon);
                var alternate = ParseMaybeAssign(noIn);
                return new ConditionalExpressionNode(this, startLoc, lastTokEnd, expr, consequent, alternate);
            }
            return expr;
        }

        // Start the precedence parser.
        private ExpressionNode ParseExpressionOperators(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            var expr = ParseMaybeUnary(refDestructuringErrors, false);
            if (checkExpressionErrors(refDestructuringErrors))
                return expr;
            return expr.Location.Start.Index == startLoc.Index && expr is ArrowFunctionExpressionNode ? expr : ParseExpressionOperator(expr, startLoc, -1, noIn);
        }

        // Parse binary operators with the operator precedence parsing
        // algorithm. `left` is the left-hand side of the operator.
        // `minPrec` provides context that allows the function to stop and
        // defer further parser to one of its callers when it encounters an
        // operator that has a lower precedence than the set it is parsing.
        private ExpressionNode ParseExpressionOperator(ExpressionNode left, Position leftStartLoc, int minPrec, bool noIn)
        {
            var prec = TokenInformation.Types[type].BinaryOperation;
            if (prec >= 0 && (!noIn || type != TokenType._in))
            {
                if (prec > minPrec)
                {
                    var logical = type == TokenType.logicalOR || type == TokenType.logicalAND;
                    var op = StringToOperator((string)value);
                    next();
                    var startLoc = start;
                    var right = ParseExpressionOperator(ParseMaybeUnary(null, false), startLoc, prec, noIn);
                    var node = BuildBinary(leftStartLoc, left, right, op, logical);
                    return ParseExpressionOperator(node, leftStartLoc, minPrec, noIn);
                }
            }
            return left;
        }

        [NotNull]
        private ExpressionNode BuildBinary(Position startLoc, ExpressionNode left, ExpressionNode right, Operator op, bool logical)
        {
            if (logical)
            {
                return new LogicalExpressionNode(this, startLoc, lastTokEnd, left, right, op);
            }

            return new BinaryExpressionNode(this, startLoc, lastTokEnd, left, right, op);
        }

        // Parse unary operators, both prefix and postfix.
        private ExpressionNode ParseMaybeUnary(DestructuringErrors refDestructuringErrors, bool sawUnary)
        {
            var startLoc = start;
            ExpressionNode expr;
            if (inAsync && isContextual("await"))
            {
                expr = parseAwait();
                sawUnary = true;
            }
            else if (TokenInformation.Types[type].Prefix)
            {
                var update = type == TokenType.incDec;
                var @operator = StringToOperator((string)value);
                next();
                var argument = ParseMaybeUnary(null, true);
                checkExpressionErrors(refDestructuringErrors, true);
                if (update) checkLVal(argument, false, null);
                else if (strict && @operator == Operator.Delete &&
                         argument is IdentifierNode)
                    raiseRecoverable(startLoc, "Deleting local variable in strict mode");
                else sawUnary = true;
                if (update)
                {
                    expr = new UpdateExpressionNode(this, startLoc, lastTokEnd, @operator, argument);
                }
                else
                {
                    expr = new UnaryExpressionNode(this, startLoc, lastTokEnd, @operator, argument);
                }
            }
            else
            {
                expr = ParseExpressionSubscripts(refDestructuringErrors);
                if (checkExpressionErrors(refDestructuringErrors))
                    return expr;
                while (TokenInformation.Types[type].Postfix && !canInsertSemicolon())
                {
                    var @operator = StringToOperator((string)value);
                    checkLVal(expr, false, null);
                    next();
                    expr = new UpdateExpressionNode(this, startLoc, lastTokEnd, ToPostfix(@operator), expr);
                }
            }

            if (!sawUnary && eat(TokenType.starstar))
                return BuildBinary(startLoc, expr, ParseMaybeUnary(null, false), Operator.Power, false);
            return expr;
        }

        private static Operator ToPostfix(Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Increment:
                    return Operator.IncrementPostfix;
                case Operator.Decrement:
                    return Operator.DecrementPostfix;
                default:
                    throw new NotImplementedException();
            }
        }

        // Parse call, dot, and `[]`-subscript expressions.
        private ExpressionNode ParseExpressionSubscripts([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var startLoc = start;
            var expr = ParseExpressionAtom(refDestructuringErrors);
            var skipArrowSubscripts = expr is ArrowFunctionExpressionNode && input.Substring(lastTokStart.Index, lastTokEnd.Index - lastTokStart.Index) != ")";
            if (checkExpressionErrors(refDestructuringErrors) || skipArrowSubscripts)
                return expr;
            var result = ParseSubscripts(expr, startLoc);
            if (refDestructuringErrors != null && result is MemberExpressionNode)
            {
                if (refDestructuringErrors.parenthesizedAssign.Index >= result.Location.Start.Index) refDestructuringErrors.parenthesizedAssign = default;
                if (refDestructuringErrors.parenthesizedBind.Index >= result.Location.Start.Index) refDestructuringErrors.parenthesizedBind = default;
            }
            return result;
        }

        [NotNull]
        private ExpressionNode ParseSubscripts([NotNull] ExpressionNode @base, Position startLoc, bool noCalls = false)
        {
            var maybeAsyncArrow = Options.ecmaVersion >= 8 && @base is IdentifierNode identifierNode && identifierNode.Name == "async" &&
                                  lastTokEnd.Index == @base.Location.End.Index && !canInsertSemicolon();
            for (;;)
            {
                bool computed;
                if ((computed = eat(TokenType.bracketL)) || eat(TokenType.dot))
                {
                    var property = computed ? ParseExpression() : parseIdent(true);
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
                    var expressionList = ParseExpressionList(TokenType.parenR, Options.ecmaVersion >= 8, false, refDestructuringErrors);
                    if (maybeAsyncArrow && !canInsertSemicolon() && eat(TokenType.arrow))
                    {
                        checkPatternErrors(refDestructuringErrors, false);
                        checkYieldAwaitInDefaultParams();
                        yieldPos = oldYieldPos;
                        awaitPos = oldAwaitPos;
                        return parseArrowExpression(startLoc, expressionList, true);
                    }
                    checkExpressionErrors(refDestructuringErrors, true);
                    yieldPos = oldYieldPos.Line != 0 ? oldYieldPos : yieldPos;
                    awaitPos = oldAwaitPos.Line != 0 ? oldAwaitPos : awaitPos;
                    @base = new CallExpressionNode(this, startLoc, lastTokEnd, @base, expressionList);
                }
                else if (type == TokenType.backQuote)
                {
                    var quasi = parseTemplate(true);
                    @base = new TaggedTemplateExpressionNode(this, startLoc, lastTokEnd, @base, quasi);
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
        private ExpressionNode ParseExpressionAtom([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var canBeArrow = potentialArrowAt.Index == start.Index;
            var startLoc = start;
            switch (type)
            {
                case TokenType._super:
                    if (!inFunction)
                        raise(startLoc, "'super' outside of function or class");
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
                    return new SuperNode(this, startLoc, lastTokEnd);
                case TokenType._this:
                    next();
                    return new ThisExpressionNode(this, startLoc, lastTokEnd);
                case TokenType.name:
                    var id = parseIdent(type != TokenType.name);
                    if (Options.ecmaVersion >= 8 && id.Name == "async" && !canInsertSemicolon() && eat(TokenType._function))
                        return (ExpressionNode)parseFunction(startLoc, null, false, true);
                    if (canBeArrow && !canInsertSemicolon())
                    {
                        if (eat(TokenType.arrow))
                            return parseArrowExpression(startLoc, new ExpressionNode[] {id});
                        if (Options.ecmaVersion >= 8 && id.Name == "async" && type == TokenType.name)
                        {
                            id = parseIdent();
                            if (canInsertSemicolon() || !eat(TokenType.arrow))
                            {
                                raise(start, "Unexpected token");
                            }
                            return parseArrowExpression(startLoc, new ExpressionNode[] {id}, true);
                        }
                    }
                    return id;
                case TokenType.regexp:
                    return ParseLiteral((RegexNode)this.value);
                case TokenType.num:
                    if (value is int intValue)
                        return ParseLiteral(intValue);
                    return ParseLiteral((double)value);
                case TokenType.@string:
                    return ParseLiteral((string)value);
                case TokenType._null:
                case TokenType._true:
                case TokenType._false:
                {
                    var value = type == TokenType._null ? default : (LiteralValue)(type == TokenType._true);
                    var raw = TokenInformation.Types[type].Keyword;
                    next();
                    return new LiteralNode(this, startLoc, lastTokEnd, value, raw);
                }
                case TokenType.parenL:
                    var expr = parseParenAndDistinguishExpression(canBeArrow);
                    if (refDestructuringErrors != null)
                    {
                        if (refDestructuringErrors.parenthesizedAssign.Line == 0 && !isSimpleAssignTarget(expr))
                            refDestructuringErrors.parenthesizedAssign = startLoc;
                        if (refDestructuringErrors.parenthesizedBind.Line == 0)
                            refDestructuringErrors.parenthesizedBind = startLoc;
                    }
                    return expr;
                case TokenType.bracketL:
                    next();
                    var elements = ParseExpressionList(TokenType.bracketR, true, true, refDestructuringErrors);
                    return new ArrayExpressionNode(this, startLoc, lastTokEnd, elements);
                case TokenType.braceL:
                    return parseObj(false, refDestructuringErrors);
                case TokenType._function:
                    next();
                    return (ExpressionNode)parseFunction(startLoc, null);
                case TokenType._class:
                    return (ExpressionNode)parseClass(startLoc, null);
                case TokenType._new:
                    return parseNew();
                case TokenType.backQuote:
                    return parseTemplate();
            }
            raise(startLoc, "Unexpected token");
            return null;
        }

        [NotNull]
        private ExpressionNode ParseLiteral(LiteralValue value)
        {
            var startLoc = start;
            var raw = input.Substring(start.Index, end.Index - start.Index);
            next();
            return new LiteralNode(this, startLoc, lastTokEnd, value, raw);
        }

        private ExpressionNode parseParenExpression()
        {
            expect(TokenType.parenL);
            var val = ParseExpression();
            expect(TokenType.parenR);
            return val;
        }

        private ExpressionNode parseParenAndDistinguishExpression(bool canBeArrow)
        {
            var startLoc = start;
            ExpressionNode node;
            var allowTrailingComma = Options.ecmaVersion >= 8;
            if (Options.ecmaVersion >= 6)
            {
                next();

                var innerStartLoc = start;
                var exprList = new List<ExpressionNode>();
                var first = true;
                var lastIsComma = false;
                var refDestructuringErrors = new DestructuringErrors();
                var oldYieldPos = yieldPos;
                var oldAwaitPos = awaitPos;
                Position spreadStart = default;
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
                        exprList.Add(parseRestBinding());
                        if (type == TokenType.comma) raise(start, "Comma is not permitted after the rest element");
                        break;
                    }
                    exprList.Add(ParseMaybeAssign(false, refDestructuringErrors, (parser, item, position, location) => item));
                }
                var innerEndLoc = start;
                expect(TokenType.parenR);

                if (canBeArrow && !canInsertSemicolon() && eat(TokenType.arrow))
                {
                    checkPatternErrors(refDestructuringErrors, false);
                    checkYieldAwaitInDefaultParams();
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
                    node = new SequenceExpressionNode(this, innerStartLoc, innerEndLoc, exprList);
                }
                else
                {
                    node = exprList[0];
                }
            }
            else
            {
                node = parseParenExpression();
            }

            if (Options.PreserveParentheses)
            {
                return new ParenthesisedExpressionNode(this, startLoc, lastTokEnd, node);
            }
            return node;
        }

        [NotNull]
        private ExpressionNode parseParenArrowList(Position startLoc, [NotNull] IReadOnlyList<ExpressionNode> exprList)
        {
            return parseArrowExpression(startLoc, exprList);
        }

        // New's precedence is slightly tricky. It must allow its argument to
        // be a `[]` or dot subscript expression, but not a call — at least,
        // not without wrapping it in parentheses. Thus, it uses the noCalls
        // argument to parseSubscripts to prevent it from consuming the
        // argument list
        [NotNull]
        private ExpressionNode parseNew()
        {
            var nodeStart = start;
            var meta = parseIdent(true);
            if (Options.ecmaVersion >= 6 && eat(TokenType.dot))
            {
                var identifierNode = parseIdent(true);
                if (identifierNode.Name != "target")
                    raiseRecoverable(identifierNode.Location.Start, "The only valid meta property for new is new.target");
                if (!inFunction)
                    raiseRecoverable(nodeStart, "new.target can only be used in functions");
                return new MetaPropertyNode(this, nodeStart, lastTokEnd, meta, identifierNode);
            }
            var startLoc = start;
            var callee = ParseSubscripts(ParseExpressionAtom(), startLoc, true);
            IReadOnlyList<ExpressionNode> arguments;
            if (eat(TokenType.parenL)) arguments = ParseExpressionList(TokenType.parenR, Options.ecmaVersion >= 8, false);
            else arguments = Array.Empty<ExpressionNode>();
            return new NewExpressionNode(this, nodeStart, lastTokEnd, callee, arguments);
        }

        private static readonly Regex templateRawRegex = new Regex("\r\n?");

        // Parse template expression.
        [NotNull]
        private TemplateElementNode parseTemplateElement(ref bool isTagged)
        {
            var startLoc = start;
            TemplateNode valueNode;
            if (type == TokenType.invalidTemplate)
            {
                if (!isTagged)
                {
                    raiseRecoverable(start, "Bad escape sequence in untagged template literal");
                }
                valueNode = new TemplateNode((string)value, null);
            }
            else
            {
                valueNode = new TemplateNode(templateRawRegex.Replace(input.Substring(start.Index, end.Index - start.Index), "\n"), (string)value);
            }
            next();
            return new TemplateElementNode(this, startLoc, lastTokEnd, valueNode, type == TokenType.backQuote);
        }

        [NotNull]
        private ExpressionNode parseTemplate(bool isTagged = false)
        {
            var startLoc = start;
            next();
            var expressions = new List<ExpressionNode>();
            var curElt = parseTemplateElement(ref isTagged);
            var quasis = new List<TemplateElementNode> {curElt};
            while (!curElt.Tail)
            {
                expect(TokenType.dollarBraceL);
                expressions.Add(ParseExpression());
                expect(TokenType.braceR);
                quasis.Add(curElt = parseTemplateElement(ref isTagged));
            }
            next();
            return new TemplateLiteralNode(this, startLoc, lastTokEnd, expressions, quasis);
        }

        private bool isAsyncProp(bool computed, [NotNull] BaseNode key)
        {
            return !computed && key is IdentifierNode identifierNode && identifierNode.Name == "async" &&
                   (type == TokenType.name || type == TokenType.num || type == TokenType.@string || type == TokenType.bracketL || TokenInformation.Types[type].Keyword != null) &&
                   !lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index));
        }

        // Parse an object literal or binding pattern.
        [NotNull]
        private ExpressionNode parseObj(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var startLoc = start;
            var first = true;
            var propHash = new Dictionary<string, Property>();
            var properties = new List<PropertyNode>();
            next();
            while (!eat(TokenType.braceR))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (afterTrailingComma(TokenType.braceR)) break;
                }
                else first = false;

                var prop = parseProperty(isPattern, refDestructuringErrors);
                if (!isPattern) CheckPropertyClash(prop, propHash);
                properties.Add(prop);
            }
            if (isPattern)
            {
                return new ObjectPatternNode(this, startLoc, lastTokEnd, properties);
            }
            return new ObjectExpressionNode(this, startLoc, lastTokEnd, properties);
        }

        [NotNull]
        private PropertyNode parseProperty(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors)
        {
            var isGenerator = false;
            bool isAsync;
            Position startLoc = default;
            var nodeStart = start;
            if (Options.ecmaVersion >= 6)
            {
                if (isPattern || refDestructuringErrors != null)
                {
                    startLoc = start;
                }
                if (!isPattern)
                    isGenerator = eat(TokenType.star);
            }
            var (computed, key) = parsePropertyName();
            if (!isPattern && Options.ecmaVersion >= 8 && !isGenerator && isAsyncProp(computed, key))
            {
                isAsync = true;
                (computed, key) = parsePropertyName();
            }
            else
            {
                isAsync = false;
            }

            ExpressionNode value;
            PropertyKind kind;
            bool method;
            bool shorthand;
            (value, kind, method, shorthand, computed, key) = parsePropertyValue(computed, key, isPattern, isGenerator, isAsync, startLoc, refDestructuringErrors);
            return new PropertyNode(this, nodeStart, lastTokEnd, kind, computed, shorthand, method, key, value);
        }

        private (ExpressionNode value, PropertyKind kind, bool method, bool shorthand, bool computed, ExpressionNode key) parsePropertyValue(bool computed, ExpressionNode key, bool isPattern, bool isGenerator, bool isAsync, Position startLoc, [CanBeNull] DestructuringErrors refDestructuringErrors)
        {
            if ((isGenerator || isAsync) && type == TokenType.colon)
            {
                raise(start, "Unexpected token");
            }

            if (eat(TokenType.colon))
            {
                var value = isPattern ? parseMaybeDefault(start) : ParseMaybeAssign(false, refDestructuringErrors);
                return (value, PropertyKind.Initialise, false, false, computed, key);
            }

            if (Options.ecmaVersion >= 6 && type == TokenType.parenL)
            {
                if (isPattern)
                {
                    raise(start, "Unexpected token");
                }
                var value = parseMethod(isGenerator, isAsync);
                return (value, PropertyKind.Initialise, true, false, computed, key);
            }

            if (!isPattern &&
                Options.ecmaVersion >= 5 && !computed && key is IdentifierNode identifierNode &&
                (identifierNode.Name == "get" || identifierNode.Name == "set") &&
                type != TokenType.comma && type != TokenType.braceR)
            {
                if (isGenerator || isAsync)
                {
                    raise(start, "Unexpected token");
                }
                var kind = identifierNode.Name == "get" ? PropertyKind.Get : PropertyKind.Set;
                (computed, key) = parsePropertyName();
                var value = parseMethod(false);
                var paramCount = kind == PropertyKind.Get ? 0 : 1;
                if (value.Parameters.Count != paramCount)
                {
                    var start = value.Location.Start;
                    if (kind == PropertyKind.Get)
                        raiseRecoverable(start, "getter should have no params");
                    else
                        raiseRecoverable(start, "setter should have exactly one param");
                }
                else
                {
                    if (kind == PropertyKind.Set && value.Parameters[0] is RestElementNode)
                        raiseRecoverable(value.Parameters[0].Location.Start, "Setter cannot use rest params");
                }

                return (value, kind, false, false, computed, key);
            }

            if (Options.ecmaVersion >= 6 && !computed && key is IdentifierNode identifierNode2)
            {
                checkUnreserved(key.Location.Start, key.Location.End, identifierNode2.Name);
                ExpressionNode value;
                if (isPattern)
                {
                    value = parseMaybeDefault(startLoc, key);
                }
                else if (type == TokenType.eq && refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.shorthandAssign.Line == 0)
                        refDestructuringErrors.shorthandAssign = start;
                    value = parseMaybeDefault(startLoc, key);
                }
                else
                {
                    value = key;
                }
                return (value, PropertyKind.Initialise, false, true, false, key);
            }

            raise(start, "Unexpected token");
            throw new InvalidOperationException();
        }

        private (bool computed, ExpressionNode key) parsePropertyName()
        {
            if (Options.ecmaVersion >= 6)
            {
                if (eat(TokenType.bracketL))
                {
                    var key = ParseMaybeAssign();
                    expect(TokenType.bracketR);
                    return (true, key);
                }
            }
            return (false, type == TokenType.num || type == TokenType.@string ? ParseExpressionAtom() : parseIdent(true));
        }

        // Parse object or class method.
        [NotNull]
        private FunctionExpressionNode parseMethod(bool isGenerator, bool isAsync = false)
        {
            var startLoc = start;
            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;

            if (Options.ecmaVersion < 6 && isGenerator)
                throw new InvalidOperationException();
            if (Options.ecmaVersion < 8 && isAsync)
                throw new InvalidOperationException();

            inGenerator = isGenerator;
            inAsync = isAsync;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;
            enterFunctionScope();

            expect(TokenType.parenL);
            var parameters = parseBindingList(TokenType.parenR, false, Options.ecmaVersion >= 8);
            checkYieldAwaitInDefaultParams();
            var (body, expression) = parseFunctionBody(parameters, startLoc, null, false);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;

            return new FunctionExpressionNode(this, startLoc, lastTokEnd, expression, isAsync, isGenerator, null, parameters, body);
        }

        // Parse arrow function expression with given parameters.
        [NotNull]
        private ExpressionNode parseArrowExpression(Position startLoc, [NotNull] IReadOnlyList<ExpressionNode> parameters, bool isAsync = false)
        {
            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;

            enterFunctionScope();
            if (Options.ecmaVersion < 8 && isAsync)
                throw new InvalidOperationException();

            inGenerator = false;
            inAsync = isAsync;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;

            parameters = toAssignableList(parameters, true);
            var (body, expression) = parseFunctionBody(parameters, startLoc, null, true);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;
            return new ArrowFunctionExpressionNode(this, startLoc, lastTokEnd, expression, isAsync, parameters, body);
        }

        // Parse function body and check parameters.
        private (BaseNode body, bool expression) parseFunctionBody([NotNull] IReadOnlyList<BaseNode> parameters, Position startLoc, [CanBeNull] BaseNode id, bool isArrowFunction)
        {
            var isExpression = isArrowFunction && type != TokenType.braceL;
            var oldStrict = strict;
            var useStrict = false;

            BaseNode body;
            bool expression;
            if (isExpression)
            {
                body = ParseMaybeAssign();
                expression = true;
                checkParams(parameters, false);
            }
            else
            {
                var nonSimple = Options.ecmaVersion >= 7 && !isSimpleParamList(parameters);
                if (!oldStrict || nonSimple)
                {
                    useStrict = strictDirective(end.Index);
                    // If this is a strict mode function, verify that argument names
                    // are not repeated, and it does not try to bind the words `eval`
                    // or `arguments`.
                    if (useStrict && nonSimple)
                        raiseRecoverable(startLoc, "Illegal 'use strict' directive in function with non-simple parameter list");
                }
                // Start a new scope with regard to labels and the `inFunction`
                // flag (restore them to their old value afterwards).
                var oldLabels = labels;
                labels = new List<Label>();
                if (useStrict) strict = true;

                // Add the params to varDeclaredNames to ensure that an error is thrown
                // if a let/const declaration in the function clashes with one of the params.
                checkParams(parameters, !oldStrict && !useStrict && !isArrowFunction && isSimpleParamList(parameters));
                var block = parseBlock(false);
                body = block;
                expression = false;
                adaptDirectivePrologue(block.Body);
                labels = oldLabels;
            }
            exitFunctionScope();

            if (strict && id != null)
            {
                // Ensure the function name isn't a forbidden identifier in strict mode, e.g. 'eval'
                checkLVal(id, true, null);
            }
            strict = oldStrict;

            return (body, expression);
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
        private void checkParams([NotNull] IEnumerable<BaseNode> parameters, bool allowDuplicates)
        {
            var nameHash = new HashSet<string>();
            foreach (var param in parameters)
                checkLVal(param, true, VariableKind.Var, allowDuplicates ? null : nameHash);
        }

        // Parses a comma-separated list of expressions, and returns them as
        // an array. `close` is the token type that ends the list, and
        // `allowEmpty` can be turned on to allow subsequent commas with
        // nothing in between them to be parsed as `null` (which is needed
        // for array literals).
        [NotNull]
        private IReadOnlyList<ExpressionNode> ParseExpressionList(TokenType close, bool allowTrailingComma, bool allowEmpty, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var elements = new List<ExpressionNode>();
            var first = true;
            while (!eat(close))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (allowTrailingComma && afterTrailingComma(close)) break;
                }
                else first = false;

                ExpressionNode element;
                if (allowEmpty && type == TokenType.comma)
                    element = null;
                else if (type == TokenType.ellipsis)
                {
                    element = parseSpread(refDestructuringErrors);
                    if (refDestructuringErrors != null && type == TokenType.comma && refDestructuringErrors.trailingComma.Line == 0)
                        refDestructuringErrors.trailingComma = start;
                }
                else
                {
                    element = ParseMaybeAssign(false, refDestructuringErrors);
                }
                elements.Add(element);
            }
            return elements;
        }

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

        // Parse the next token as an identifier. If `liberal` is true (used
        // when parsing properties), it will also convert keywords into
        // identifiers.
        [NotNull]
        private IdentifierNode parseIdent(bool liberal = false)
        {
            var startLocation = start;
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
                // If the previous token is a dot, this does not apply because the context-managing code already ignored the keyword
                if ((name == "class" || name == "function") &&
                    (lastTokEnd.Index != lastTokStart.Index + 1 || input.Get(lastTokStart.Index) != 46))
                {
                    context.Pop();
                }
            }
            else
            {
                raise(start, "Unexpected token");
            }
            next();
            var node = new IdentifierNode(this, startLocation, lastTokEnd, name);
            if (!liberal) checkUnreserved(node.Location.Start, node.Location.Start, node.Name);
            return node;
        }

        // Parses yield expression inside generator.
        [NotNull]
        private YieldExpressionNode parseYield()
        {
            if (yieldPos.Line == 0) yieldPos = start;

            var startLoc = start;
            next();
            var @delegate = false;
            ExpressionNode argument = null;
            if (type != TokenType.semi && !canInsertSemicolon() && (type == TokenType.star || TokenInformation.Types[type].StartsExpression))
            {
                @delegate = eat(TokenType.star);
                argument = ParseMaybeAssign();
            }
            return new YieldExpressionNode(this, startLoc, lastTokEnd, @delegate, argument);
        }

        [NotNull]
        private AwaitExpressionNode parseAwait()
        {
            if (awaitPos.Line == 0) awaitPos = start;

            var startLoc = start;
            next();
            var argument = ParseMaybeUnary(null, true);

            return new AwaitExpressionNode(this, startLoc, lastTokEnd, argument);
        }
    }
}
