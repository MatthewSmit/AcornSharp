using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
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
    public sealed partial class Parser
    {
        // Check if property name clashes with already added.
        // Object/class getters and setters are not allowed to clash —
        // either with each other or with an init property — and in
        // strict mode, init properties are also not allowed to be repeated.
        private void CheckPropertyClash(ExpressionNode node, IDictionary<string, bool[]> propHash, DestructuringErrors refDestructuringErrors)
        {
            if (options.EcmaVersion >= 9 && node is SpreadElementNode)
            {
                return;
            }

            var prop = (PropertyNode)node;

            if (options.EcmaVersion >= 6 && (prop.Computed || prop.Method || prop.Shorthand))
            {
                return;
            }

            var key = prop.Key;
            string name;
            if (key is IdentifierNode identifier)
            {
                name = identifier.Name;
            }
            else if (key is LiteralNode literal)
            {
                name = literal.Value == null ? "null" : literal.Value.ToString();
            }
            else
            {
                return;
            }

            var kind = prop.Kind;
            if (options.EcmaVersion >= 6)
            {
                if (name == "__proto__" && kind == PropertyKind.Init)
                {
                    if (propHash.ContainsKey("proto"))
                    {
                        if (refDestructuringErrors != null && refDestructuringErrors.doubleProto < 0)
                        {
                            refDestructuringErrors.doubleProto = key.Start;
                        }
                        else
                        {
                            // Backwards-compat kludge. Can be removed in version 6.0
                            RaiseRecoverable(key.Start, "Redefinition of __proto__ property");
                        }
                    }
                    propHash["proto"] = Array.Empty<bool>();
                }

                return;
            }

            name = "$" + name;
            if (propHash.TryGetValue(name, out var other))
            {
                bool redefinition;
                if (kind == PropertyKind.Init)
                {
                    redefinition = strict && other[(int)PropertyKind.Init] || other[(int)PropertyKind.Get] || other[(int)PropertyKind.Set];
                }
                else
                {
                    redefinition = other[(int)PropertyKind.Init] || other[(int)kind];
                }

                if (redefinition)
                {
                    RaiseRecoverable(key.Start, "Redefinition of property");
                }
            }
            else
            {
                propHash[name] = other = new bool[(int)PropertyKind.Constructor + 1];
            }

            other[(int)kind] = true;
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
        [NotNull]
        private ExpressionNode ParseExpression(bool noIn = false, [CanBeNull] DestructuringErrors refDestructuringErrors = default)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            var expression = ParseMaybeAssign(noIn, refDestructuringErrors);
            if (type == TokenType.Comma)
            {
                var expressions = new List<ExpressionNode>
                {
                    expression
                };
                while (Eat(TokenType.Comma))
                {
                    expressions.Add(ParseMaybeAssign(noIn, refDestructuringErrors));
                }
                return FinishNode(new SequenceExpressionNode(this, startPos, startLoc, expressions));
            }

            return expression;
        }

        // Parse an assignment expression. This includes applications of
        // operators like `+=`.
        [NotNull]
        private ExpressionNode ParseMaybeAssign(bool noIn = default, DestructuringErrors refDestructuringErrors = default, [CanBeNull] Func<ExpressionNode, int, Position, ExpressionNode> afterLeftParse = default)
        {
            if (IsContextual("yield"))
            {
                if (InGenerator)
                {
                    return ParseYield();
                }

                // The tokenizer will assume an expression is allowed after
                // `yield`, but this isn't that kind of yield
                exprAllowed = false;
            }

            var ownDestructuringErrors = false;
            var oldParenAssign = -1;
            var oldTrailingComma = -1;
            var oldShorthandAssign = -1;
            if (refDestructuringErrors != null)
            {
                oldParenAssign = refDestructuringErrors.parenthesizedAssign;
                oldTrailingComma = refDestructuringErrors.trailingComma;
                oldShorthandAssign = refDestructuringErrors.shorthandAssign;
                refDestructuringErrors.parenthesizedAssign = refDestructuringErrors.trailingComma = refDestructuringErrors.shorthandAssign = -1;
            }
            else
            {
                refDestructuringErrors = new DestructuringErrors();
                ownDestructuringErrors = true;
            }

            var startPos = start;
            var startLoc = this.startLoc;
            if (type == TokenType.ParenLeft || type == TokenType.Name)
            {
                potentialArrowAt = start;
            }

            var left = ParseMaybeConditional(noIn, refDestructuringErrors);
            if (afterLeftParse != null)
            {
                left = afterLeftParse(left, startPos, startLoc);
            }

            if (type.IsAssignment)
            {
                var op = ConvertOperator((string)value);
                var leftNode = type == TokenType.Equal ? ToAssignable(ref left, false, refDestructuringErrors) : left;
                if (!ownDestructuringErrors)
                {
                    refDestructuringErrors.Reset();
                }

                refDestructuringErrors.shorthandAssign = -1;// reset because shorthand default was used correctly
                CheckLeftValue(left);
                Next();
                var rightNode = ParseMaybeAssign(noIn);
                var node = new AssignmentExpressionNode(this, startPos, startLoc, leftNode, op, rightNode);
                return FinishNode(node);
            }
            else
            {
                if (ownDestructuringErrors)
                {
                    CheckExpressionErrors(refDestructuringErrors, true);
                }
            }

            if (oldParenAssign > -1)
            {
                refDestructuringErrors.parenthesizedAssign = oldParenAssign;
            }

            if (oldTrailingComma > -1)
            {
                refDestructuringErrors.trailingComma = oldTrailingComma;
            }

            if (oldShorthandAssign > -1)
            {
                refDestructuringErrors.shorthandAssign = oldShorthandAssign;
            }

            return left;
        }

        // Parse a ternary conditional (`?:`) operator.
        [NotNull]
        private ExpressionNode ParseMaybeConditional(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            var expr = ParseExprOps(noIn, refDestructuringErrors);
            if (CheckExpressionErrors(refDestructuringErrors))
            {
                return expr;
            }

            if (Eat(TokenType.Question))
            {
                var consequent = ParseMaybeAssign();
                Expect(TokenType.Colon);
                var alternate = ParseMaybeAssign(noIn);
                return FinishNode(new ConditionalExpressionNode(this, startPos, startLoc, expr, consequent, alternate));
            }

            return expr;
        }

        // Start the precedence parser.
        private ExpressionNode ParseExprOps(bool noIn, DestructuringErrors refDestructuringErrors)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            var expr = ParseMaybeUnary(refDestructuringErrors, false);
            if (CheckExpressionErrors(refDestructuringErrors))
            {
                return expr;
            }

            return expr.Start == startPos && expr is ArrowFunctionExpressionNode ? expr : ParseExpressionOperator(expr, startPos, startLoc, -1, noIn);
        }

        // Parse binary operators with the operator precedence parsing
        // algorithm. `left` is the left-hand side of the operator.
        // `minPrec` provides context that allows the function to stop and
        // defer further parser to one of its callers when it encounters an
        // operator that has a lower precedence than the set it is parsing.
        private ExpressionNode ParseExpressionOperator(ExpressionNode left, int leftStartPos, Position leftStartLoc, int minPrec, bool noIn)
        {
            var prec = type.BinaryOperator;
            if (prec >= 0 && (!noIn || type != TokenType.In))
            {
                if (prec > minPrec)
                {
                    var logical = type == TokenType.LogicalOR || type == TokenType.LogicalAND;
                    var op = (string)value;
                    Next();
                    var startPos = start;
                    var startLoc = this.startLoc;
                    var right = ParseExpressionOperator(ParseMaybeUnary(null, false), startPos, startLoc, prec, noIn);
                    var node = BuildBinary(leftStartPos, leftStartLoc, left, right, ConvertOperator(op), logical);
                    return ParseExpressionOperator(node, leftStartPos, leftStartLoc, minPrec, noIn);
                }
            }

            return left;
        }

        [NotNull]
        private ExpressionNode BuildBinary(int startPos, Position startLoc, [NotNull] ExpressionNode left, [NotNull] ExpressionNode right, Operator op, bool logical)
        {
            ExpressionNode node;
            if (logical)
            {
                node = new LogicalExpressionNode(this, startPos, startLoc, left, op, right);
            }
            else
            {
                node = new BinaryExpressionNode(this, startPos, startLoc, left, op, right);
            }

            return FinishNode(node);
        }

        internal static Operator ConvertOperator([NotNull] string op)
        {
            switch (op)
            {
                case "void":
                    return Operator.Void;
                case "delete":
                    return Operator.Delete;
                case "typeof":
                    return Operator.TypeOf;
                case "!":
                    return Operator.LogicalNot;
                case "~":
                    return Operator.BitwiseNot;

                case "+":
                    return Operator.Addition;
                case "-":
                    return Operator.Subtraction;
                case "*":
                    return Operator.Multiply;
                case "/":
                    return Operator.Division;
                case "%":
                    return Operator.Modulus;
                case "**":
                    return Operator.Exponent;

                case "||":
                    return Operator.LogicalOr;
                case "&&":
                    return Operator.LogicalAnd;
                case "|":
                    return Operator.BitwiseOr;
                case "^":
                    return Operator.BitwiseXor;
                case "&":
                    return Operator.BitwiseAnd;

                case "==":
                    return Operator.Equal;
                case "!=":
                    return Operator.NotEqual;
                case "===":
                    return Operator.StrictEqual;
                case "!==":
                    return Operator.StrictNotEqual;
                case "in":
                    return Operator.In;
                case "instanceof":
                    return Operator.InstanceOf;

                case "<":
                    return Operator.LessThen;
                case ">":
                    return Operator.GreaterThen;
                case "<=":
                    return Operator.LessThenEquals;
                case ">=":
                    return Operator.GreaterThenEquals;

                case "<<":
                    return Operator.ShiftLeft;
                case ">>":
                    return Operator.ShiftRight;
                case ">>>":
                    return Operator.UnsignedShiftRight;

                case "++":
                    return Operator.Increment;
                case "--":
                    return Operator.Decrement;

                case "=":
                    return Operator.Assignment;
                case "+=":
                    return Operator.AdditionAssignment;
                case "-=":
                    return Operator.SubtractionAssignment;
                case "*=":
                    return Operator.MultiplyAssignment;
                case "/=":
                    return Operator.DivisionAssignment;
                case "%=":
                    return Operator.ModulusAssignment;
                case "**=":
                    return Operator.ExponentAssignment;
                case "|=":
                    return Operator.BitwiseOrAssignment;
                case "^=":
                    return Operator.BitwiseXorAssignment;
                case "&=":
                    return Operator.BitwiseAndAssignment;
                case "<<=":
                    return Operator.ShiftLeftAssignment;
                case ">>=":
                    return Operator.ShiftRightAssignment;
                case ">>>=":
                    return Operator.UnsignedShiftRightAssignment;

                default:
                    throw new NotImplementedException();
            }
        }

        // Parse unary operators, both prefix and postfix.
        [NotNull]
        private ExpressionNode ParseMaybeUnary(DestructuringErrors refDestructuringErrors, bool sawUnary)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            ExpressionNode expr;
            if (IsContextual("await") && (InAsync || !InFunction && options.AllowAwaitOutsideFunction))
            {
                expr = ParseAwait();
                sawUnary = true;
            }
            else if (type.Prefix)
            {
                var update = type == TokenType.IncrementDecrement;
                var op = ConvertOperator((string)value);
                Next();
                var argument = ParseMaybeUnary(null, true);
                CheckExpressionErrors(refDestructuringErrors, true);
                if (update)
                {
                    CheckLeftValue(argument);
                }
                else if (strict && op == Operator.Delete && argument is IdentifierNode)
                {
                    RaiseRecoverable(startPos, "Deleting local variable in strict mode");
                }
                else
                {
                    sawUnary = true;
                }

                ExpressionNode node;
                if (update)
                {
                    node = new UpdateExpressionNode(this, startPos, startLoc, op, true, argument);
                }
                else
                {
                    node = new UnaryExpressionNode(this, startPos, startLoc, op, true, argument);
                }

                expr = FinishNode(node);
            }
            else
            {
                expr = ParseExprSubscripts(refDestructuringErrors);
                if (CheckExpressionErrors(refDestructuringErrors))
                {
                    return expr;
                }

                while (type.Postfix && !CanInsertSemicolon())
                {
                    var op = ConvertOperator((string)value);
                    CheckLeftValue(expr);
                    Next();
                    expr = FinishNode(new UpdateExpressionNode(this, startPos, startLoc, op, false, expr));
                }
            }

            if (!sawUnary && Eat(TokenType.StarStar))
            {
                return BuildBinary(startPos, startLoc, expr, ParseMaybeUnary(null, false), Operator.Exponent, false);
            }

            return expr;
        }

        // Parse call, dot, and `[]`-subscript expressions.
        [NotNull]
        private ExpressionNode ParseExprSubscripts([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            var expr = ParseExprAtom(refDestructuringErrors);
            var skipArrowSubscripts = expr is ArrowFunctionExpressionNode && input.Substring(lastTokStart, lastTokEnd - lastTokStart) != ")";
            if (CheckExpressionErrors(refDestructuringErrors) || skipArrowSubscripts)
            {
                return expr;
            }

            var result = ParseSubscripts(expr, startPos, startLoc);
            if (refDestructuringErrors != null && result is MemberExpressionNode)
            {
                if (refDestructuringErrors.parenthesizedAssign >= result.Start)
                {
                    refDestructuringErrors.parenthesizedAssign = -1;
                }

                if (refDestructuringErrors.parenthesizedBind >= result.Start)
                {
                    refDestructuringErrors.parenthesizedBind = -1;
                }
            }

            return result;
        }

        [NotNull]
        private ExpressionNode ParseSubscripts([NotNull] ExpressionNode @base, int startPos, Position startLoc, bool noCalls = false)
        {
            var maybeAsyncArrow = options.EcmaVersion >= 8 && @base is IdentifierNode identifier && identifier.Name == "async" &&
                                  lastTokEnd == @base.End && !CanInsertSemicolon() && input.Substring(@base.Start, @base.End - @base.Start) == "async";
            for (;;)
            {
                bool computed;
                if ((computed = Eat(TokenType.BracketLeft)) || Eat(TokenType.Dot))
                {
                    var nodeStart = startPos;
                    var nodeStartLoc = startLoc;
                    var property = computed ? ParseExpression() : ParseIdentifier(true);
                    if (computed)
                    {
                        Expect(TokenType.BracketRight);
                    }

                    @base = FinishNode(new MemberExpressionNode(this, nodeStart, nodeStartLoc, @base, property, computed));
                }
                else if (!noCalls && Eat(TokenType.ParenLeft))
                {
                    var refDestructuringErrors = new DestructuringErrors();
                    var oldYieldPos = yieldPos;
                    var oldAwaitPos = awaitPos;
                    yieldPos = 0;
                    awaitPos = 0;
                    var exprList = ParseExpressionList(TokenType.ParenRight, options.EcmaVersion >= 8, false, refDestructuringErrors);
                    if (maybeAsyncArrow && !CanInsertSemicolon() && Eat(TokenType.Arrow))
                    {
                        CheckPatternErrors(refDestructuringErrors, false);
                        CheckYieldAwaitInDefaultParams();
                        yieldPos = oldYieldPos;
                        awaitPos = oldAwaitPos;
                        return ParseArrowExpression(startPos, startLoc, exprList, true);
                    }

                    CheckExpressionErrors(refDestructuringErrors, true);
                    yieldPos = oldYieldPos > 0 ? oldYieldPos : yieldPos;
                    awaitPos = oldAwaitPos > 0 ? oldAwaitPos : awaitPos;
                    @base = FinishNode(new CallExpressionNode(this, startPos, startLoc, @base, exprList));
                }
                else if (type == TokenType.BackQuote)
                {
                    var tag = @base;
                    var quasi = ParseTemplate(true);
                    @base = FinishNode(new TaggedTemplateExpressionNode(this, startPos, startLoc, tag, quasi));
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
        [NotNull]
        private ExpressionNode ParseExprAtom([CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            // If a division operator appears in an expression position, the
            // tokenizer got confused, and we force it to read a regexp instead.
            if (type == TokenType.Slash)
            {
                ReadRegexp();
            }

            var canBeArrow = potentialArrowAt == start;
            if (type == TokenType.Super)
            {
                if (!AllowSuper)
                {
                    Raise(this.start, "'super' keyword outside a method");
                }

                var start = this.start;
                var startLoc = this.startLoc;
                Next();
                if (type == TokenType.ParenLeft && !AllowDirectSuper)
                {
                    Raise(start, "super() call outside constructor of a subclass");
                }

                // The `super` keyword can appear at below:
                // SuperProperty:
                //     super [ Expression ]
                //     super . IdentifierName
                // SuperCall:
                //     super Arguments
                if (type != TokenType.Dot && type != TokenType.BracketLeft && type != TokenType.ParenLeft)
                {
                    Unexpected();
                }

                return FinishNode(new SuperNode(this, start, startLoc));
            }

            if (type == TokenType.This)
            {
                var node = new ThisExpressionNode(this, start, startLoc);
                Next();
                return FinishNode(node);
            }
            if (type == TokenType.Name)
            {
                var startPos = start;
                var startLoc = this.startLoc;
                var containsEsc = this.containsEsc;
                var id = ParseIdentifier(type != TokenType.Name);
                if (options.EcmaVersion >= 8 && !containsEsc && id.Name == "async" && !CanInsertSemicolon() && Eat(TokenType.Function))
                {
                    return (ExpressionNode)ParseFunction(startPos, startLoc, 0, false, true);
                }

                if (canBeArrow && !CanInsertSemicolon())
                {
                    if (Eat(TokenType.Arrow))
                    {
                        return ParseArrowExpression(startPos, startLoc, new ExpressionNode[]
                        {
                            id
                        }, false);
                    }

                    if (options.EcmaVersion >= 8 && id.Name == "async" && type == TokenType.Name && !containsEsc)
                    {
                        id = ParseIdentifier();
                        if (CanInsertSemicolon() || !Eat(TokenType.Arrow))
                        {
                            Unexpected();
                        }

                        return ParseArrowExpression(startPos, startLoc, new ExpressionNode[]
                        {
                            id
                        }, true);
                    }
                }

                return id;
            }
            if (type == TokenType.RegExp || type == TokenType.Number || type == TokenType.String)
            {
                return ParseLiteral(value);
            }
            if (type == TokenType.Null || type == TokenType.True || type == TokenType.False)
            {
                var node = new LiteralNode(this, start, startLoc, type == TokenType.Null ? null : (object)(type == TokenType.True), type.Keyword);
                Next();
                return FinishNode(node);
            }
            if (type == TokenType.ParenLeft)
            {
                var start = this.start;
                var expr = ParseParenAndDistinguishExpression(canBeArrow);
                if (refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.parenthesizedAssign < 0 && !IsSimpleAssignTarget(expr))
                    {
                        refDestructuringErrors.parenthesizedAssign = start;
                    }

                    if (refDestructuringErrors.parenthesizedBind < 0)
                    {
                        refDestructuringErrors.parenthesizedBind = start;
                    }
                }

                return expr;
            }
            if (type == TokenType.BracketLeft)
            {
                var start = this.start;
                var startLoc = this.startLoc;
                Next();
                var elements = ParseExpressionList(TokenType.BracketRight, true, true, refDestructuringErrors);
                var node = new ArrayExpressionNode(this, start, startLoc, elements);
                return FinishNode(node);
            }
            if (type == TokenType.BraceLeft)
            {
                return ParseObject(false, refDestructuringErrors);
            }
            if (type == TokenType.Function)
            {
                var start = this.start;
                var startLoc = this.startLoc;
                Next();
                return (ExpressionNode)ParseFunction(start, startLoc, 0);
            }
            if (type == TokenType.Class)
            {
                return (ExpressionNode)ParseClass(start, startLoc, false, false);
            }
            if (type == TokenType.New)
            {
                return ParseNew();
            }
            if (type == TokenType.BackQuote)
            {
                return ParseTemplate();
            }

            Unexpected();
            throw new InvalidOperationException();
        }

        [NotNull]
        private LiteralNode ParseLiteral(object value)
        {
            var node = new LiteralNode(this, start, startLoc, value, input.Substring(start, end - start));
            Next();
            return FinishNode(node);
        }

        [NotNull]
        private ExpressionNode ParseParenthesisExpression()
        {
            Expect(TokenType.ParenLeft);
            var val = ParseExpression();
            Expect(TokenType.ParenRight);
            return val;
        }

        [NotNull]
        private ExpressionNode ParseParenAndDistinguishExpression(bool canBeArrow)
        {
            var startPos = start;
            var startLoc = this.startLoc;
            ExpressionNode value;
            var allowTrailingComma = options.EcmaVersion >= 8;
            if (options.EcmaVersion >= 6)
            {
                Next();

                var innerStartPos = start;
                var innerStartLoc = this.startLoc;
                var exprList = new List<ExpressionNode>();
                var first = true;
                var lastIsComma = false;
                var refDestructuringErrors = new DestructuringErrors();
                var oldYieldPos = yieldPos;
                var oldAwaitPos = awaitPos;
                var spreadStart = -1;
                yieldPos = 0;
                awaitPos = 0;
                while (type != TokenType.ParenRight)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        Expect(TokenType.Comma);
                    }

                    if (allowTrailingComma && AfterTrailingComma(TokenType.ParenRight, true))
                    {
                        lastIsComma = true;
                        break;
                    }

                    if (type == TokenType.Ellipsis)
                    {
                        spreadStart = start;
                        exprList.Add(ParseParenItem(ParseRestBinding(), default, default));
                        if (type == TokenType.Comma)
                        {
                            Raise(start, "Comma is not permitted after the rest element");
                        }

                        break;
                    }

                    exprList.Add(ParseMaybeAssign(false, refDestructuringErrors, ParseParenItem));
                }

                var innerEndPos = start;
                var innerEndLoc = this.startLoc;
                Expect(TokenType.ParenRight);

                if (canBeArrow && !CanInsertSemicolon() && Eat(TokenType.Arrow))
                {
                    CheckPatternErrors(refDestructuringErrors, false);
                    CheckYieldAwaitInDefaultParams();
                    yieldPos = oldYieldPos;
                    awaitPos = oldAwaitPos;
                    return ParseParenArrowList(startPos, startLoc, exprList);
                }

                if (exprList.Count == 0 || lastIsComma)
                {
                    Unexpected(lastTokStart);
                }

                if (spreadStart >= 0)
                {
                    Unexpected(spreadStart);
                }

                CheckExpressionErrors(refDestructuringErrors, true);
                yieldPos = oldYieldPos > 0 ? oldYieldPos : yieldPos;
                awaitPos = oldAwaitPos > 0 ? oldAwaitPos : awaitPos;

                if (exprList.Count > 1)
                {
                    value = new SequenceExpressionNode(this, innerStartPos, innerStartLoc, exprList);
                    value.Finish(this, innerEndPos, innerEndLoc);
                }
                else
                {
                    value = exprList[0];
                }
            }
            else
            {
                value = ParseParenthesisExpression();
            }

//        
            if (options.PreserveParens)
            {
                var parenthesis = new ParenthesisedExpressionNode(this, startPos, startLoc, value);
                return FinishNode(parenthesis);
            }

            return value;
        }

        private ExpressionNode ParseParenItem(ExpressionNode item, int start, Position startLoc)
        {
            return item;
        }

        [NotNull]
        private ArrowFunctionExpressionNode ParseParenArrowList(int startPos, Position startLoc, IList<ExpressionNode> exprList)
        {
            return ParseArrowExpression(startPos, startLoc, exprList);
        }

        // New's precedence is slightly tricky. It must allow its argument to
        // be a `[]` or dot subscript expression, but not a call — at least,
        // not without wrapping it in parentheses. Thus, it uses the noCalls
        // argument to parseSubscripts to prevent it from consuming the
        // argument list.
        [NotNull]
        private ExpressionNode ParseNew()
        {
            var nodeStart = start;
            var nodeStartLoc = this.startLoc;
            var meta = ParseIdentifier(true);
            IdentifierNode property = null;
            if (options.EcmaVersion >= 6 && Eat(TokenType.Dot))
            {
                var containsEsc = this.containsEsc;
                property = ParseIdentifier(true);
                if (property.Name != "target" || containsEsc)
                {
                    RaiseRecoverable(property.Start, "The only valid meta property for new is new.target");
                }

                if (!InNonArrowFunction)
                {
                    RaiseRecoverable(nodeStart, "new.target can only be used in functions");
                }

                return FinishNode(new MetaPropertyNode(this, nodeStart, nodeStartLoc, meta, property));
            }

            var startPos = start;
            var startLoc = this.startLoc;
            var callee = ParseSubscripts(ParseExprAtom(), startPos, startLoc, true);
            IList<ExpressionNode> arguments;
            if (Eat(TokenType.ParenLeft))
            {
                arguments = ParseExpressionList(TokenType.ParenRight, options.EcmaVersion >= 8, false);
            }
            else
            {
                arguments = Array.Empty<ExpressionNode>();
            }

            var node = new NewExpressionNode(this, nodeStart, nodeStartLoc, callee, arguments);
            return FinishNode(node);
        }

        // Parse template expression.
        [NotNull]
        private TemplateElementNode ParseTemplateElement(bool isTagged)
        {
            var start = this.start;
            var startLoc = this.startLoc;
            TemplateValue value;
            if (type == TokenType.InvalidTemplate)
            {
                if (!isTagged)
                {
                    RaiseRecoverable(this.start, "Bad escape sequence in untagged template literal");
                }

                value = new TemplateValue
                {
                    Raw = (string)this.value,
                    Cooked = null
                };
            }
            else
            {
                value = new TemplateValue
                {
                    Raw = Regex.Replace(input.Substring(this.start, end - this.start), "\r\n?", "\n"),
                    Cooked = (string)this.value
                };
            }

            Next();
            var tail = type == TokenType.BackQuote;
            return FinishNode(new TemplateElementNode(this, start, startLoc, value, tail));
        }

        [NotNull]
        private TemplateLiteralNode ParseTemplate(bool isTagged = false)
        {
            var start = this.start;
            var startLoc = this.startLoc;
            Next();
            var expressions = new List<ExpressionNode>();
            var curElt = ParseTemplateElement(isTagged);
            var quasis = new List<TemplateElementNode>()
            {
                curElt
            };
            while (!curElt.Tail)
            {
                if (type == TokenType.Eof)
                {
                    Raise(pos, "Unterminated template literal");
                }

                Expect(TokenType.DollarBraceLeft);
                expressions.Add(ParseExpression());
                Expect(TokenType.BraceRight);
                quasis.Add(curElt = ParseTemplateElement(isTagged));
            }

            Next();
            return FinishNode(new TemplateLiteralNode(this, start, startLoc, expressions, quasis));
        }

        private bool IsAsyncProperty(bool computed, ExpressionNode key)
        {
            return !computed && key is IdentifierNode identifier && identifier.Name == "async" &&
                   (type == TokenType.Name || type == TokenType.Number || type == TokenType.String || type == TokenType.BracketLeft || type.Keyword != null || options.EcmaVersion >= 9 && type == TokenType.Star) &&
                   !Whitespace.LineBreak.IsMatch(input.Substring(lastTokEnd, start - lastTokEnd));
        }

        // Parse an object literal or binding pattern.
        [NotNull]
        private ExpressionNode ParseObject(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var start = this.start;
            var startLoc = this.startLoc;
            var first = true;
            var propHash = new Dictionary<string, bool[]>();
            var properties = new List<ExpressionNode>();
            Next();

            while (!Eat(TokenType.BraceRight))
            {
                if (!first)
                {
                    Expect(TokenType.Comma);
                    if (AfterTrailingComma(TokenType.BraceRight))
                    {
                        break;
                    }
                }
                else
                {
                    first = false;
                }

                var property = ParseProperty(isPattern, refDestructuringErrors);
                if (!isPattern)
                {
                    CheckPropertyClash(property, propHash, refDestructuringErrors);
                }

                properties.Add(property);
            }

            ExpressionNode node;
            if (isPattern)
            {
                node = new ObjectPatternNode(this, start, startLoc, properties);
            }
            else
            {
                node = new ObjectExpressionNode(this, start, startLoc, properties);
            }

            return FinishNode(node);
        }

        [NotNull]
        private ExpressionNode ParseProperty(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors)
        {
            var originalStartPos = start;
            var originalStartLoc = this.startLoc;
            var isGenerator = false;
            bool isAsync;
            var startPos = 0;
            Position startLoc = default;
            if (options.EcmaVersion >= 9 && Eat(TokenType.Ellipsis))
            {
                if (isPattern)
                {
                    var argument = ParseIdentifier();
                    if (type == TokenType.Comma)
                    {
                        Raise(start, "Comma is not permitted after the rest element");
                    }

                    return FinishNode(new RestElementNode(this, originalStartPos, originalStartLoc, argument));
                }
                else
                {
                    // To disallow parenthesized identifier via `this.toAssignable()`.
                    if (type == TokenType.ParenLeft && refDestructuringErrors != null)
                    {
                        if (refDestructuringErrors.parenthesizedAssign < 0)
                        {
                            refDestructuringErrors.parenthesizedAssign = start;
                        }

                        if (refDestructuringErrors.parenthesizedBind < 0)
                        {
                            refDestructuringErrors.parenthesizedBind = start;
                        }
                    }

                    // Parse argument.
                    var argument = ParseMaybeAssign(false, refDestructuringErrors);
                    // To disallow trailing comma via `this.toAssignable()`.
                    if (type == TokenType.Comma && refDestructuringErrors != null && refDestructuringErrors.trailingComma < 0)
                    {
                        refDestructuringErrors.trailingComma = start;
                    }

                    // Finish
                    return FinishNode(new SpreadElementNode(this, originalStartPos, originalStartLoc, argument));
                }
            }

            if (options.EcmaVersion >= 6)
            {
                if (isPattern || refDestructuringErrors != null)
                {
                    startPos = start;
                    startLoc = this.startLoc;
                }

                if (!isPattern)
                {
                    isGenerator = Eat(TokenType.Star);
                }
            }

            var containsEsc = this.containsEsc;
            var (computed, key) = ParsePropertyName();
            if (!isPattern && !containsEsc && options.EcmaVersion >= 8 && !isGenerator && IsAsyncProperty(computed, key))
            {
                isAsync = true;
                isGenerator = options.EcmaVersion >= 9 && Eat(TokenType.Star);
                (computed, key) = ParsePropertyName();
            }
            else
            {
                isAsync = false;
            }

            var (kind, value, method, shorthand) = ParsePropertyValue(ref computed, ref key, isPattern, isGenerator, isAsync, startPos, startLoc, refDestructuringErrors, containsEsc);
            var property = new PropertyNode(this, originalStartPos, originalStartLoc, computed, key, kind, value, method, shorthand);
            return FinishNode(property);
        }

        private (PropertyKind kind, ExpressionNode value, bool method, bool shorthand) ParsePropertyValue(ref bool computed, ref ExpressionNode key, bool isPattern, bool isGenerator, bool isAsync, int startPos, Position startLoc, DestructuringErrors refDestructuringErrors, bool containsEsc)
        {
            if ((isGenerator || isAsync) && type == TokenType.Colon)
            {
                Unexpected();
                throw new InvalidOperationException();
            }

            if (Eat(TokenType.Colon))
            {
                var value = isPattern ? ParseMaybeDefault(start, this.startLoc) : ParseMaybeAssign(false, refDestructuringErrors);
                return (PropertyKind.Init, value, false, false);
            }

            if (options.EcmaVersion >= 6 && type == TokenType.ParenLeft)
            {
                if (isPattern)
                {
                    Unexpected();
                }

                var value = ParseMethod(isGenerator, isAsync);
                return (PropertyKind.Init, value, true, false);
            }

            if (!isPattern && !containsEsc &&
                options.EcmaVersion >= 5 && !computed && key is IdentifierNode identifier &&
                (identifier.Name == "get" || identifier.Name == "set") && type != TokenType.Comma && type != TokenType.BraceRight)
            {
                if (isGenerator || isAsync)
                {
                    Unexpected();
                }

                var kind = identifier.Name == "get" ? PropertyKind.Get : PropertyKind.Set;
                (computed, key) = ParsePropertyName();
                var value = ParseMethod(false);
                var paramCount = kind == PropertyKind.Get ? 0 : 1;
                if (value.Parameters.Count != paramCount)
                {
                    if (kind == PropertyKind.Get)
                    {
                        RaiseRecoverable(value.Start, "getter should have no params");
                    }
                    else
                    {
                        RaiseRecoverable(value.Start, "setter should have exactly one param");
                    }
                }
                else if (kind == PropertyKind.Set && value.Parameters[0] is RestElementNode)
                {
                    RaiseRecoverable(value.Parameters[0].Start, "Setter cannot use rest params");
                }

                return (kind, value, false, false);
            }

            if (options.EcmaVersion >= 6 && !computed && key is IdentifierNode identifierKey)
            {
                CheckUnreserved(key.Start, key.End, identifierKey.Name);
                var kind = PropertyKind.Init;
                ExpressionNode value;
                if (isPattern)
                {
                    value = ParseMaybeDefault(startPos, startLoc, key);
                }
                else if (type == TokenType.Equal && refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.shorthandAssign < 0)
                    {
                        refDestructuringErrors.shorthandAssign = start;
                    }

                    value = ParseMaybeDefault(startPos, startLoc, key);
                }
                else
                {
                    value = key;
                }

                return (kind, value, false, true);
            }

            Unexpected();
            throw new InvalidOperationException();
        }

        private (bool computed, ExpressionNode key) ParsePropertyName()
        {
            if (options.EcmaVersion >= 6)
            {
                if (Eat(TokenType.BracketLeft))
                {
                    var key = ParseMaybeAssign();
                    Expect(TokenType.BracketRight);
                    return (true, key);
                }
            }

            return (false, type == TokenType.Number || type == TokenType.String ? ParseExprAtom() : ParseIdentifier(true));
        }

        // Parse object or class method.
        [NotNull]
        private FunctionExpressionNode ParseMethod(bool isGenerator, bool isAsync = false, bool allowDirectSuper = false)
        {
            var start = this.start;
            var startLoc = this.startLoc;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;

            var generator = false;
            var async = false;

            if (options.EcmaVersion >= 6)
            {
                generator = isGenerator;
            }

            if (options.EcmaVersion >= 8)
            {
                async = isAsync;
            }

            yieldPos = 0;
            awaitPos = 0;
            EnterScope(FunctionFlags(isAsync, generator) | ScopeFlags.Super | (allowDirectSuper ? ScopeFlags.DirectSuper : 0));

            Expect(TokenType.ParenLeft);
            var parameters = ParseBindingList(TokenType.ParenRight, false, options.EcmaVersion >= 8);
            CheckYieldAwaitInDefaultParams();
            var (expression, body) = ParseFunctionBody(start, null, parameters, false);

            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            var node = new FunctionExpressionNode(this, start, startLoc, null, generator, async, parameters, expression, body);
            return FinishNode(node);
        }

        // Parse arrow function expression with given parameters.
        [NotNull]
        private ArrowFunctionExpressionNode ParseArrowExpression(int start, Position startLoc, IList<ExpressionNode> parameters, bool isAsync = false)
        {
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;

            EnterScope(FunctionFlags(isAsync, false) | ScopeFlags.Arrow);

            yieldPos = 0;
            awaitPos = 0;

            parameters = ToAssignableList(parameters, true);
            var (expression, body) = ParseFunctionBody(start, null, parameters, true);

            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            return FinishNode(new ArrowFunctionExpressionNode(this, start, startLoc, isAsync, parameters, expression, body));
        }

        // Parse function body and check parameters.
        private (bool expression, BaseNode body) ParseFunctionBody(int start, [CanBeNull] BaseNode id, [NotNull] [ItemNotNull] IList<ExpressionNode> parameters, bool isArrowFunction)
        {
            var isExpression = isArrowFunction && type != TokenType.BraceLeft;
            var oldStrict = strict;
            var useStrict = false;

            bool expression;
            BaseNode body;
            if (isExpression)
            {
                body = ParseMaybeAssign();
                expression = true;
                CheckParameters(parameters, false);
            }
            else
            {
                var nonSimple = options.EcmaVersion >= 7 && !IsSimpleParamList(parameters);
                if (!oldStrict || nonSimple)
                {
                    useStrict = StrictDirective(end);
                    // If this is a strict mode function, verify that argument names
                    // are not repeated, and it does not try to bind the words `eval`
                    // or `arguments`.
                    if (useStrict && nonSimple)
                    {
                        RaiseRecoverable(start, "Illegal 'use strict' directive in function with non-simple parameter list");
                    }
                }

                // Start a new scope with regard to labels and the `inFunction`
                // flag (restore them to their old value afterwards).
                var oldLabels = labels;
                labels = new List<Label>();
                if (useStrict)
                {
                    strict = true;
                }

                // Add the params to varDeclaredNames to ensure that an error is thrown
                // if a let/const declaration in the function clashes with one of the params.
                CheckParameters(parameters, !oldStrict && !useStrict && !isArrowFunction && IsSimpleParamList(parameters));
                body = ParseBlock(false);
                expression = false;
                AdaptDirectivePrologue(((BlockStatementNode)body).Body);
                labels = oldLabels;
            }

            ExitScope();

            // Ensure the function name isn't a forbidden identifier in strict mode, e.g. 'eval'
            if (strict && id != null)
            {
                CheckLeftValue(id, BindType.Outside);
            }

            strict = oldStrict;
            return (expression, body);
        }

        private static bool IsSimpleParamList([NotNull] [ItemCanBeNull] IEnumerable<ExpressionNode> parameters)
        {
            foreach (var param in parameters)
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
        private void CheckParameters([NotNull] [ItemCanBeNull] IEnumerable<ExpressionNode> parameters, bool allowDuplicates)
        {
            var nameHash = allowDuplicates ? null : new HashSet<string>();
            foreach (var param in parameters)
            {
                CheckLeftValue(param, BindType.Var, nameHash);
            }
        }

        // Parses a comma-separated list of expressions, and returns them as
        // an array. `close` is the token type that ends the list, and
        // `allowEmpty` can be turned on to allow subsequent commas with
        // nothing in between them to be parsed as `null` (which is needed
        // for array literals).
        [NotNull]
        [ItemCanBeNull]
        private IList<ExpressionNode> ParseExpressionList(TokenType close, bool allowTrailingComma, bool allowEmpty, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var elements = new List<ExpressionNode>();
            var first = true;
            while (!Eat(close))
            {
                if (!first)
                {
                    Expect(TokenType.Comma);
                    if (allowTrailingComma && AfterTrailingComma(close))
                    {
                        break;
                    }
                }
                else
                {
                    first = false;
                }

                ExpressionNode elt;
                if (allowEmpty && type == TokenType.Comma)
                {
                    elt = null;
                }
                else if (type == TokenType.Ellipsis)
                {
                    elt = ParseSpread(refDestructuringErrors);
                    if (refDestructuringErrors != null && type == TokenType.Comma && refDestructuringErrors.trailingComma < 0)
                    {
                        refDestructuringErrors.trailingComma = start;
                    }
                }
                else
                {
                    elt = ParseMaybeAssign(false, refDestructuringErrors);
                }

                elements.Add(elt);
            }

            return elements;
        }

        private void CheckUnreserved(int start, int end, [NotNull] string name)
        {
            if (InGenerator && name == "yield")
            {
                RaiseRecoverable(start, "Can not use 'yield' as identifier inside a generator");
            }

            if (InAsync && name == "await")
            {
                RaiseRecoverable(start, "Can not use 'await' as identifier inside an async function");
            }

            if (keywords.IsMatch(name))
            {
                Raise(start, $"Unexpected keyword '{name}'");
            }

            if (options.EcmaVersion < 6 &&
                input.Substring(start, end - start).IndexOf("\\", StringComparison.Ordinal) != -1)
            {
                return;
            }

            var re = strict ? reservedWordsStrict : reservedWords;
            if (re.IsMatch(name))
            {
                if (!InAsync && name == "await")
                {
                    RaiseRecoverable(start, "Can not use keyword 'await' outside an async function");
                }

                RaiseRecoverable(start, $"The keyword '{name}' is reserved");
            }
        }

        // Parse the next token as an identifier. If `liberal` is true (used
        // when parsing properties), it will also convert keywords into
        // identifiers.
        [NotNull]
        private IdentifierNode ParseIdentifier(bool liberal = false, bool isBinding = false)
        {
            var start = this.start;
            var startLoc = this.startLoc;
            if (liberal && options.AllowReserved == AllowReserved.Never)
            {
                liberal = false;
            }

            string name;
            if (type == TokenType.Name)
            {
                name = (string)value;
            }
            else if (type.Keyword != null)
            {
                name = type.Keyword;

                // To fix https://github.com/acornjs/acorn/issues/575
                // `class` and `function` keywords push new context into this.context.
                // But there is no chance to pop the context if the keyword is consumed as an identifier such as a property name.
                // If the previous token is a dot, this does not apply because the context-managing code already ignored the keyword
                if ((name == "class" || name == "function") &&
                    (lastTokEnd != lastTokStart + 1 || input.CharCodeAt(lastTokStart) != 46))
                {
                    context.RemoveAt(context.Count - 1);
                }
            }
            else
            {
                Unexpected();
                throw new InvalidOperationException();
            }

            Next();
            var node = new IdentifierNode(this, start, startLoc, name);
            FinishNode(node);
            if (!liberal)
            {
                CheckUnreserved(node.Start, node.End, node.Name);
            }

            return node;
        }

        // Parses yield expression inside generator.
        private YieldExpressionNode ParseYield()
        {
            if (yieldPos == 0)
            {
                yieldPos = this.start;
            }

            var start = this.start;
            var startLoc = this.startLoc;
            Next();

            bool @delegate;
            ExpressionNode argument;
            if (type == TokenType.Semicolon || CanInsertSemicolon() || type != TokenType.Star && !type.StartsExpression)
            {
                @delegate = false;
                argument = null;
            }
            else
            {
                @delegate = Eat(TokenType.Star);
                argument = ParseMaybeAssign();
            }

            return FinishNode(new YieldExpressionNode(this, start, startLoc, @delegate, argument));
        }

        [NotNull]
        private AwaitExpressionNode ParseAwait()
        {
            if (awaitPos == 0)
            {
                awaitPos = this.start;
            }

            var start = this.start;
            var startLoc = this.startLoc;
            Next();
            var argument = ParseMaybeUnary(null, true);
            return FinishNode(new AwaitExpressionNode(this, start, startLoc, argument));
        }
    }
}