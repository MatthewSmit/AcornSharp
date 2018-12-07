using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (Options.EcmaVersion >= 9 && node is SpreadElementNode)
            {
                return;
            }

            var prop = (PropertyNode)node;

            if (Options.EcmaVersion >= 6 && (prop.Computed || prop.Method || prop.Shorthand))
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
            if (Options.EcmaVersion >= 6)
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
            var startPos = Start;
            var startLoc = StartLocation;
            var expression = ParseMaybeAssign(noIn, refDestructuringErrors);
            if (Type == TokenType.Comma)
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
                expressionAllowed = false;
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

            var startPos = Start;
            var startLoc = StartLocation;
            if (Type == TokenType.ParenLeft || Type == TokenType.Name)
            {
                potentialArrowAt = Start;
            }

            var left = ParseMaybeConditional(noIn, refDestructuringErrors);
            if (afterLeftParse != null)
            {
                left = afterLeftParse(left, startPos, startLoc);
            }

            if (Type.IsAssignment)
            {
                var op = ConvertOperator((string)Value);
                var leftNode = Type == TokenType.Equal ? ToAssignable(ref left, false, refDestructuringErrors) : left;
                if (!ownDestructuringErrors)
                {
                    refDestructuringErrors.Reset();
                }

                refDestructuringErrors.shorthandAssign = -1;// reset because shorthand default was used correctly
                CheckLeftValue(left);
                Next();
                var rightNode = ParseMaybeAssign(noIn);
                Debug.Assert(leftNode != null, nameof(leftNode) + " != null");
                var node = new AssignmentExpressionNode(this, startPos, startLoc, leftNode, op, rightNode);
                return FinishNode(node);
            }

            if (ownDestructuringErrors)
            {
                CheckExpressionErrors(refDestructuringErrors, true);
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
            var startPos = Start;
            var startLoc = StartLocation;
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
            var startPos = Start;
            var startLoc = StartLocation;
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
            var prec = Type.BinaryOperator;
            if (prec >= 0 && (!noIn || Type != TokenType.In))
            {
                if (prec > minPrec)
                {
                    var logical = Type == TokenType.LogicalOR || Type == TokenType.LogicalAND;
                    var op = (string)Value;
                    Next();
                    var startPos = Start;
                    var startLoc = StartLocation;
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
                    throw new ArgumentOutOfRangeException(nameof(op));
            }
        }

        // Parse unary operators, both prefix and postfix.
        [NotNull]
        private ExpressionNode ParseMaybeUnary(DestructuringErrors refDestructuringErrors, bool sawUnary)
        {
            var startPos = Start;
            var startLoc = StartLocation;
            ExpressionNode expr;
            if (IsContextual("await") && (InAsync || !InFunction && Options.AllowAwaitOutsideFunction))
            {
                expr = ParseAwait();
                sawUnary = true;
            }
            else if (Type.Prefix)
            {
                var update = Type == TokenType.IncrementDecrement;
                var op = ConvertOperator((string)Value);
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

                while (Type.Postfix && !CanInsertSemicolon())
                {
                    var op = ConvertOperator((string)Value);
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
            var startPos = Start;
            var startLoc = StartLocation;
            var expr = ParseExprAtom(refDestructuringErrors);
            var skipArrowSubscripts = expr is ArrowFunctionExpressionNode && Input.Substring(lastTokenStart, lastTokenEnd - lastTokenStart) != ")";
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
            var maybeAsyncArrow = Options.EcmaVersion >= 8 && @base is IdentifierNode identifier && identifier.Name == "async" &&
                                  lastTokenEnd == @base.End && !CanInsertSemicolon() && Input.Substring(@base.Start, @base.End - @base.Start) == "async";
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
                    var oldYieldPos = yieldPosition;
                    var oldAwaitPos = awaitPosition;
                    yieldPosition = 0;
                    awaitPosition = 0;
                    var exprList = ParseExpressionList(TokenType.ParenRight, Options.EcmaVersion >= 8, false, refDestructuringErrors);
                    if (maybeAsyncArrow && !CanInsertSemicolon() && Eat(TokenType.Arrow))
                    {
                        CheckPatternErrors(refDestructuringErrors, false);
                        CheckYieldAwaitInDefaultParams();
                        yieldPosition = oldYieldPos;
                        awaitPosition = oldAwaitPos;
                        return ParseArrowExpression(startPos, startLoc, exprList, true);
                    }

                    CheckExpressionErrors(refDestructuringErrors, true);
                    yieldPosition = oldYieldPos > 0 ? oldYieldPos : yieldPosition;
                    awaitPosition = oldAwaitPos > 0 ? oldAwaitPos : awaitPosition;
                    @base = FinishNode(new CallExpressionNode(this, startPos, startLoc, @base, exprList));
                }
                else if (Type == TokenType.BackQuote)
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
            if (Type == TokenType.Slash)
            {
                ReadRegexp();
            }

            var canBeArrow = potentialArrowAt == Start;
            if (Type == TokenType.Super)
            {
                if (!AllowSuper)
                {
                    Raise(Start, "'super' keyword outside a method");
                }

                var start = Start;
                var startLoc = StartLocation;
                Next();
                if (Type == TokenType.ParenLeft && !AllowDirectSuper)
                {
                    Raise(start, "super() call outside constructor of a subclass");
                }

                // The `super` keyword can appear at below:
                // SuperProperty:
                //     super [ Expression ]
                //     super . IdentifierName
                // SuperCall:
                //     super Arguments
                if (Type != TokenType.Dot && Type != TokenType.BracketLeft && Type != TokenType.ParenLeft)
                {
                    Unexpected();
                }

                return FinishNode(new SuperNode(this, start, startLoc));
            }

            if (Type == TokenType.This)
            {
                var node = new ThisExpressionNode(this, Start, StartLocation);
                Next();
                return FinishNode(node);
            }
            if (Type == TokenType.Name)
            {
                var startPos = Start;
                var startLoc = StartLocation;
                var containsEsc = this.containsEscape;
                var id = ParseIdentifier(Type != TokenType.Name);
                if (Options.EcmaVersion >= 8 && !containsEsc && id.Name == "async" && !CanInsertSemicolon() && Eat(TokenType.Function))
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
                        });
                    }

                    if (Options.EcmaVersion >= 8 && id.Name == "async" && Type == TokenType.Name && !containsEsc)
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
            if (Type == TokenType.RegExp || Type == TokenType.Number || Type == TokenType.String)
            {
                return ParseLiteral(Value);
            }
            if (Type == TokenType.Null || Type == TokenType.True || Type == TokenType.False)
            {
                var node = new LiteralNode(this, Start, StartLocation, Type == TokenType.Null ? null : (object)(Type == TokenType.True), Type.Keyword);
                Next();
                return FinishNode(node);
            }
            if (Type == TokenType.ParenLeft)
            {
                var start = Start;
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
            if (Type == TokenType.BracketLeft)
            {
                var start = Start;
                var startLoc = StartLocation;
                Next();
                var elements = ParseExpressionList(TokenType.BracketRight, true, true, refDestructuringErrors);
                var node = new ArrayExpressionNode(this, start, startLoc, elements);
                return FinishNode(node);
            }
            if (Type == TokenType.BraceLeft)
            {
                return ParseObject(false, refDestructuringErrors);
            }
            if (Type == TokenType.Function)
            {
                var start = Start;
                var startLoc = StartLocation;
                Next();
                return (ExpressionNode)ParseFunction(start, startLoc, 0);
            }
            if (Type == TokenType.Class)
            {
                return (ExpressionNode)ParseClass(Start, StartLocation, false, false);
            }
            if (Type == TokenType.New)
            {
                return ParseNew();
            }
            if (Type == TokenType.BackQuote)
            {
                return ParseTemplate();
            }

            Unexpected();
            throw new InvalidOperationException();
        }

        [NotNull]
        private LiteralNode ParseLiteral(object value)
        {
            var node = new LiteralNode(this, Start, StartLocation, value, Input.Substring(Start, End - Start));
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
            var startPos = Start;
            var startLoc = StartLocation;
            ExpressionNode value;
            var allowTrailingComma = Options.EcmaVersion >= 8;
            if (Options.EcmaVersion >= 6)
            {
                Next();

                var innerStartPos = Start;
                var innerStartLoc = StartLocation;
                var exprList = new List<ExpressionNode>();
                var first = true;
                var lastIsComma = false;
                var refDestructuringErrors = new DestructuringErrors();
                var oldYieldPos = yieldPosition;
                var oldAwaitPos = awaitPosition;
                var spreadStart = -1;
                yieldPosition = 0;
                awaitPosition = 0;
                while (Type != TokenType.ParenRight)
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

                    if (Type == TokenType.Ellipsis)
                    {
                        spreadStart = Start;
                        exprList.Add(ParseParenItem(ParseRestBinding(), default, default));
                        if (Type == TokenType.Comma)
                        {
                            Raise(Start, "Comma is not permitted after the rest element");
                        }

                        break;
                    }

                    exprList.Add(ParseMaybeAssign(false, refDestructuringErrors, ParseParenItem));
                }

                var innerEndPos = Start;
                var innerEndLoc = StartLocation;
                Expect(TokenType.ParenRight);

                if (canBeArrow && !CanInsertSemicolon() && Eat(TokenType.Arrow))
                {
                    CheckPatternErrors(refDestructuringErrors, false);
                    CheckYieldAwaitInDefaultParams();
                    yieldPosition = oldYieldPos;
                    awaitPosition = oldAwaitPos;
                    return ParseParenArrowList(startPos, startLoc, exprList);
                }

                if (exprList.Count == 0 || lastIsComma)
                {
                    Unexpected(lastTokenStart);
                }

                if (spreadStart >= 0)
                {
                    Unexpected(spreadStart);
                }

                CheckExpressionErrors(refDestructuringErrors, true);
                yieldPosition = oldYieldPos > 0 ? oldYieldPos : yieldPosition;
                awaitPosition = oldAwaitPos > 0 ? oldAwaitPos : awaitPosition;

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
            if (Options.PreserveParens)
            {
                var parenthesis = new ParenthesisedExpressionNode(this, startPos, startLoc, value);
                return FinishNode(parenthesis);
            }

            return value;
        }

        private static ExpressionNode ParseParenItem(ExpressionNode item, int start, Position startLoc)
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
            var nodeStart = Start;
            var nodeStartLoc = StartLocation;
            var meta = ParseIdentifier(true);
            if (Options.EcmaVersion >= 6 && Eat(TokenType.Dot))
            {
                var containsEsc = this.containsEscape;
                var property = ParseIdentifier(true);
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

            var startPos = Start;
            var startLoc = StartLocation;
            var callee = ParseSubscripts(ParseExprAtom(), startPos, startLoc, true);
            IList<ExpressionNode> arguments;
            if (Eat(TokenType.ParenLeft))
            {
                arguments = ParseExpressionList(TokenType.ParenRight, Options.EcmaVersion >= 8, false);
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
            var start = Start;
            var startLoc = StartLocation;
            TemplateValue value;
            if (Type == TokenType.InvalidTemplate)
            {
                if (!isTagged)
                {
                    RaiseRecoverable(Start, "Bad escape sequence in untagged template literal");
                }

                value = new TemplateValue
                {
                    Raw = (string)Value,
                    Cooked = null
                };
            }
            else
            {
                value = new TemplateValue
                {
                    Raw = Regex.Replace(Input.Substring(Start, End - Start), "\r\n?", "\n"),
                    Cooked = (string)Value
                };
            }

            Next();
            var tail = Type == TokenType.BackQuote;
            return FinishNode(new TemplateElementNode(this, start, startLoc, value, tail));
        }

        [NotNull]
        private TemplateLiteralNode ParseTemplate(bool isTagged = false)
        {
            var start = Start;
            var startLoc = StartLocation;
            Next();
            var expressions = new List<ExpressionNode>();
            var curElt = ParseTemplateElement(isTagged);
            var quasis = new List<TemplateElementNode>
            {
                curElt
            };
            while (!curElt.Tail)
            {
                if (Type == TokenType.Eof)
                {
                    Raise(position, "Unterminated template literal");
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
                   (Type == TokenType.Name || Type == TokenType.Number || Type == TokenType.String || Type == TokenType.BracketLeft || Type.Keyword != null || Options.EcmaVersion >= 9 && Type == TokenType.Star) &&
                   !Whitespace.LineBreak.IsMatch(Input.Substring(lastTokenEnd, Start - lastTokenEnd));
        }

        // Parse an object literal or binding pattern.
        [NotNull]
        private ExpressionNode ParseObject(bool isPattern, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            var start = Start;
            var startLoc = StartLocation;
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
            var originalStartPos = Start;
            var originalStartLoc = StartLocation;
            var isGenerator = false;
            bool isAsync;
            var startPos = 0;
            Position startLoc = default;
            if (Options.EcmaVersion >= 9 && Eat(TokenType.Ellipsis))
            {
                if (isPattern)
                {
                    var argument = ParseIdentifier();
                    if (Type == TokenType.Comma)
                    {
                        Raise(Start, "Comma is not permitted after the rest element");
                    }

                    return FinishNode(new RestElementNode(this, originalStartPos, originalStartLoc, argument));
                }
                else
                {
                    // To disallow parenthesized identifier via `this.toAssignable()`.
                    if (Type == TokenType.ParenLeft && refDestructuringErrors != null)
                    {
                        if (refDestructuringErrors.parenthesizedAssign < 0)
                        {
                            refDestructuringErrors.parenthesizedAssign = Start;
                        }

                        if (refDestructuringErrors.parenthesizedBind < 0)
                        {
                            refDestructuringErrors.parenthesizedBind = Start;
                        }
                    }

                    // Parse argument.
                    var argument = ParseMaybeAssign(false, refDestructuringErrors);
                    // To disallow trailing comma via `this.toAssignable()`.
                    if (Type == TokenType.Comma && refDestructuringErrors != null && refDestructuringErrors.trailingComma < 0)
                    {
                        refDestructuringErrors.trailingComma = Start;
                    }

                    // Finish
                    return FinishNode(new SpreadElementNode(this, originalStartPos, originalStartLoc, argument));
                }
            }

            if (Options.EcmaVersion >= 6)
            {
                if (isPattern || refDestructuringErrors != null)
                {
                    startPos = Start;
                    startLoc = StartLocation;
                }

                if (!isPattern)
                {
                    isGenerator = Eat(TokenType.Star);
                }
            }

            var containsEsc = this.containsEscape;
            var (computed, key) = ParsePropertyName();
            if (!isPattern && !containsEsc && Options.EcmaVersion >= 8 && !isGenerator && IsAsyncProperty(computed, key))
            {
                isAsync = true;
                isGenerator = Options.EcmaVersion >= 9 && Eat(TokenType.Star);
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
            if ((isGenerator || isAsync) && Type == TokenType.Colon)
            {
                Unexpected();
                throw new InvalidOperationException();
            }

            if (Eat(TokenType.Colon))
            {
                var value = isPattern ? ParseMaybeDefault(Start, StartLocation) : ParseMaybeAssign(false, refDestructuringErrors);
                return (PropertyKind.Init, value, false, false);
            }

            if (Options.EcmaVersion >= 6 && Type == TokenType.ParenLeft)
            {
                if (isPattern)
                {
                    Unexpected();
                }

                var value = ParseMethod(isGenerator, isAsync);
                return (PropertyKind.Init, value, true, false);
            }

            if (!isPattern && !containsEsc &&
                Options.EcmaVersion >= 5 && !computed && key is IdentifierNode identifier &&
                (identifier.Name == "get" || identifier.Name == "set") && Type != TokenType.Comma && Type != TokenType.BraceRight)
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

            if (Options.EcmaVersion >= 6 && !computed && key is IdentifierNode identifierKey)
            {
                CheckUnreserved(key.Start, key.End, identifierKey.Name);
                var kind = PropertyKind.Init;
                ExpressionNode value;
                if (isPattern)
                {
                    value = ParseMaybeDefault(startPos, startLoc, key);
                }
                else if (Type == TokenType.Equal && refDestructuringErrors != null)
                {
                    if (refDestructuringErrors.shorthandAssign < 0)
                    {
                        refDestructuringErrors.shorthandAssign = Start;
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
            if (Options.EcmaVersion >= 6)
            {
                if (Eat(TokenType.BracketLeft))
                {
                    var key = ParseMaybeAssign();
                    Expect(TokenType.BracketRight);
                    return (true, key);
                }
            }

            return (false, Type == TokenType.Number || Type == TokenType.String ? ParseExprAtom() : ParseIdentifier(true));
        }

        // Parse object or class method.
        [NotNull]
        private FunctionExpressionNode ParseMethod(bool isGenerator, bool isAsync = false, bool allowDirectSuper = false)
        {
            var start = Start;
            var startLoc = StartLocation;
            var oldYieldPos = yieldPosition;
            var oldAwaitPos = awaitPosition;

            var generator = false;
            var async = false;

            if (Options.EcmaVersion >= 6)
            {
                generator = isGenerator;
            }

            if (Options.EcmaVersion >= 8)
            {
                async = isAsync;
            }

            yieldPosition = 0;
            awaitPosition = 0;
            EnterScope(FunctionFlags(isAsync, generator) | ScopeFlags.Super | (allowDirectSuper ? ScopeFlags.DirectSuper : 0));

            Expect(TokenType.ParenLeft);
            var parameters = ParseBindingList(TokenType.ParenRight, false, Options.EcmaVersion >= 8);
            CheckYieldAwaitInDefaultParams();
            var (expression, body) = ParseFunctionBody(start, null, parameters, false);

            yieldPosition = oldYieldPos;
            awaitPosition = oldAwaitPos;
            var node = new FunctionExpressionNode(this, start, startLoc, null, generator, async, parameters, expression, body);
            return FinishNode(node);
        }

        // Parse arrow function expression with given parameters.
        [NotNull]
        private ArrowFunctionExpressionNode ParseArrowExpression(int start, Position startLoc, IList<ExpressionNode> parameters, bool isAsync = false)
        {
            var oldYieldPos = yieldPosition;
            var oldAwaitPos = awaitPosition;

            EnterScope(FunctionFlags(isAsync, false) | ScopeFlags.Arrow);

            yieldPosition = 0;
            awaitPosition = 0;

            parameters = ToAssignableList(parameters, true);
            var (expression, body) = ParseFunctionBody(start, null, parameters, true);

            yieldPosition = oldYieldPos;
            awaitPosition = oldAwaitPos;
            return FinishNode(new ArrowFunctionExpressionNode(this, start, startLoc, isAsync, parameters, expression, body));
        }

        // Parse function body and check parameters.
        private (bool expression, BaseNode body) ParseFunctionBody(int start, [CanBeNull] BaseNode id, [NotNull] [ItemNotNull] IList<ExpressionNode> parameters, bool isArrowFunction)
        {
            var isExpression = isArrowFunction && Type != TokenType.BraceLeft;
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
                var nonSimple = Options.EcmaVersion >= 7 && !IsSimpleParamList(parameters);
                if (!oldStrict || nonSimple)
                {
                    useStrict = StrictDirective(End);
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
                Debug.Assert(param != null, nameof(param) + " != null");
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
                if (allowEmpty && Type == TokenType.Comma)
                {
                    elt = null;
                }
                else if (Type == TokenType.Ellipsis)
                {
                    elt = ParseSpread(refDestructuringErrors);
                    if (refDestructuringErrors != null && Type == TokenType.Comma && refDestructuringErrors.trailingComma < 0)
                    {
                        refDestructuringErrors.trailingComma = Start;
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

            if (Options.EcmaVersion < 6 &&
                Input.Substring(start, end - start).IndexOf("\\", StringComparison.Ordinal) != -1)
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
        private IdentifierNode ParseIdentifier(bool liberal = false)
        {
            var start = Start;
            var startLoc = StartLocation;
            if (liberal && Options.AllowReserved == AllowReserved.Never)
            {
                liberal = false;
            }

            string name;
            if (Type == TokenType.Name)
            {
                name = (string)Value;
            }
            else if (Type.Keyword != null)
            {
                name = Type.Keyword;

                // To fix https://github.com/acornjs/acorn/issues/575
                // `class` and `function` keywords push new context into this.context.
                // But there is no chance to pop the context if the keyword is consumed as an identifier such as a property name.
                // If the previous token is a dot, this does not apply because the context-managing code already ignored the keyword
                if ((name == "class" || name == "function") &&
                    (lastTokenEnd != lastTokenStart + 1 || Input.CharCodeAt(lastTokenStart) != 46))
                {
                    contextStack.RemoveAt(contextStack.Count - 1);
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
        [NotNull]
        private YieldExpressionNode ParseYield()
        {
            if (yieldPosition == 0)
            {
                yieldPosition = Start;
            }

            var start = Start;
            var startLoc = StartLocation;
            Next();

            bool @delegate;
            ExpressionNode argument;
            if (Type == TokenType.Semicolon || CanInsertSemicolon() || Type != TokenType.Star && !Type.StartsExpression)
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
            if (awaitPosition == 0)
            {
                awaitPosition = Start;
            }

            var start = Start;
            var startLoc = StartLocation;
            Next();
            var argument = ParseMaybeUnary(null, true);
            return FinishNode(new AwaitExpressionNode(this, start, startLoc, argument));
        }
    }
}