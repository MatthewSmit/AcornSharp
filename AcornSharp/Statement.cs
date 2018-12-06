using System;
using System.Collections.Generic;
using System.Diagnostics;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        // ### Statement parsing

        // Parse a program. Initializes the parser, reads any number of
        // statements, and wraps them in a Program node.  Optionally takes a
        // `program` argument.  If present, the statements will be appended
        // to its body instead of creating a new node.
        [NotNull]
        private ProgramNode ParseTopLevel([NotNull] ProgramNode node)
        {
            var exports = new HashSet<string>();
            while (type != TokenType.Eof)
            {
                var statement = ParseStatement(null, true, exports);
                node.Body.Add(statement);
            }

            AdaptDirectivePrologue(node.Body);
            Next();
            if (options.EcmaVersion >= 6)
            {
                node.SourceType = options.SourceType;
            }

            return FinishNode(node);
        }

        private bool IsLet()
        {
            if (options.EcmaVersion < 6 || !IsContextual("let"))
            {
                return false;
            }

            var skip = Whitespace.SkipWhiteSpace.Match(input, pos);
            var next = this.pos + skip.Length;
            var nextCh = input[next];
            if (nextCh == '{' && !Whitespace.LineBreak.IsMatch(input.Substring(end, next - end))
                || nextCh == '[')
            {
                return true;
            }

            if (Identifier.IsIdentifierStart(nextCh, true))
            {
                var pos = next + 1;
                while (Identifier.IsIdentifierChar(input.CharCodeAt(pos), true))
                {
                    ++pos;
                }

                var ident = input.Substring(next, pos - next);
                if (!Identifier.KeywordRelationalOperator.IsMatch(ident))
                {
                    return true;
                }
            }

            return false;
        }

        // check 'async [no LineTerminator here] function'
        // - 'async /*foo*/ function' is OK.
        // - 'async /*\n*/ function' is invalid.
        private bool IsAsyncFunction()
        {
            if (options.EcmaVersion < 8 || !IsContextual("async"))
            {
                return false;
            }

            var skip = Whitespace.SkipWhiteSpace.Match(input, pos);
            var next = pos + skip.Length;
            return !Whitespace.LineBreak.IsMatch(input.Substring(pos, next - pos)) &&
                   next + 8 <= input.Length &&
                   input.Substring(next, 8) == "function" &&
                   (next + 8 == input.Length || !Identifier.IsIdentifierChar(input[next + 8]));
        }

        // Parse a single statement.
        //
        // If expecting a statement and finding a slash operator, parse a
        // regular expression literal. This is to handle cases like
        // `if (foo) /blah/.exec(foo)`, where looking at the previous token
        // does not help.
        [NotNull]
        private StatementNode ParseStatement([CanBeNull] string context, bool topLevel = false, [CanBeNull] ISet<string> exports = null)
        {
            var startType = type;
            var start = this.start;
            var startLoc = this.startLoc;
            var kind = PropertyKind.Var;

            if (IsLet())
            {
                startType = TokenType.Var;
                kind = PropertyKind.Let;
            }

            // Most types of statements are recognized by the keyword they
            // start with. Many are trivial to parse, some require a bit of
            // complexity.

            if (startType == TokenType.Break || startType == TokenType.Continue)
            {
                return ParseBreakContinueStatement(start, startLoc, startType.Keyword);
            }

            if (startType == TokenType.Debugger)
            {
                return ParseDebuggerStatement(start, startLoc);
            }

            if (startType == TokenType.Do)
            {
                return ParseDoStatement(start, startLoc);
            }

            if (startType == TokenType.For)
            {
                return ParseForStatement(start, startLoc);
            }

            if (startType == TokenType.Function)
            {
                if (context != null && (strict || context != "if") && options.EcmaVersion >= 6)
                {
                    Unexpected();
                }

                return ParseFunctionStatement(start, startLoc, false, context == null);
            }

            if (startType == TokenType.Class)
            {
                if (context != null)
                {
                    Unexpected();
                }

                return (StatementNode)ParseClass(start, startLoc, true, true);
            }

            if (startType == TokenType.If)
            {
                return ParseIfStatement(start, startLoc);
            }

            if (startType == TokenType.Return)
            {
                return ParseReturnStatement(start, startLoc);
            }

            if (startType == TokenType.Switch)
            {
                return ParseSwitchStatement(start, startLoc);
            }

            if (startType == TokenType.Throw)
            {
                return ParseThrowStatement(start, startLoc);
            }

            if (startType == TokenType.Try)
            {
                return ParseTryStatement(start, startLoc);
            }

            if (startType == TokenType.Const || startType == TokenType.Var)
            {
                if (kind == PropertyKind.Var)
                {
                    switch (value)
                    {
                        case "let":
                            kind = PropertyKind.Let;
                            break;

                        case "const":
                            kind = PropertyKind.Const;
                            break;
                    }
                }

                if (context != null && kind != PropertyKind.Var)
                {
                    Unexpected();
                }

                return ParseVarStatement(start, startLoc, kind);
            }

            if (startType == TokenType.While)
            {
                return ParseWhileStatement(start, startLoc);
            }

            if (startType == TokenType.With)
            {
                return ParseWithStatement(start, startLoc);
            }

            if (startType == TokenType.BraceLeft)
            {
                return ParseBlock(true, start, startLoc);
            }

            if (startType == TokenType.Semicolon)
            {
                return ParseEmptyStatement(start, startLoc);
            }

            if (startType == TokenType.Export || startType == TokenType.Import)
            {
                if (!options.AllowImportExportEverywhere)
                {
                    if (!topLevel)
                    {
                        Raise(this.start, "'import' and 'export' may only appear at the top level");
                    }

                    if (!inModule)
                    {
                        Raise(this.start, "'import' and 'export' may appear only with 'sourceType: module'");
                    }
                }

                return startType == TokenType.Import ? ParseImport(start, startLoc) : (StatementNode)ParseExport(start, startLoc, exports);
            }

            if (IsAsyncFunction())
            {
                if (context != null)
                {
                    Unexpected();
                }

                Next();
                return ParseFunctionStatement(start, startLoc, true, context == null);
            }

            var maybeName = value;
            var expr = ParseExpression();
            if (startType == TokenType.Name && expr is IdentifierNode && Eat(TokenType.Colon))
            {
                return ParseLabelledStatement(start, startLoc, (string)maybeName, expr, context);
            }

            return ParseExpressionStatement(start, startLoc, expr);
        }

        [NotNull]
        private StatementNode ParseBreakContinueStatement(int start, Position startLoc, string keyword)
        {
            var isBreak = keyword == "break";
            Next();
            IdentifierNode label = null;
            if (Eat(TokenType.Semicolon) || InsertSemicolon())
            {
            }
            else if (type != TokenType.Name)
            {
                Unexpected();
            }
            else
            {
                label = ParseIdentifier();
                Semicolon();
            }

            // Verify that there is an actual destination to break or
            // continue to.
            var i = 0;
            for (; i < labels.Count; ++i)
            {
                var lab = labels[i];
                if (label == null || lab.Name == label.Name)
                {
                    if (lab.Kind != LabelKind.None && (isBreak || lab.Kind == LabelKind.Loop))
                    {
                        break;
                    }

                    if (label != null && isBreak)
                    {
                        break;
                    }
                }
            }

            if (i == labels.Count)
            {
                Raise(start, "Unsyntactic " + keyword);
            }

            StatementNode node;
            if (isBreak)
            {
                node = new BreakStatementNode(this, start, startLoc, label);
            }
            else
            {
                node = new ContinueStatementNode(this, start, startLoc, label);
            }

            return FinishNode(node);
        }

        [NotNull]
        private DebuggerStatementNode ParseDebuggerStatement(int start, Position startLoc)
        {
            Next();
            Semicolon();
            return FinishNode(new DebuggerStatementNode(this, start, startLoc));
        }

        [NotNull]
        private DoWhileStatementNode ParseDoStatement(int start, Position startLoc)
        {
            Next();
            labels.Add(loopLabel);
            var body = ParseStatement("do");
            labels.RemoveAt(labels.Count - 1);
            Expect(TokenType.While);
            var test = ParseParenthesisExpression();
            if (options.EcmaVersion >= 6)
            {
                Eat(TokenType.Semicolon);
            }
            else
            {
                Semicolon();
            }

            var node = new DoWhileStatementNode(this, start, startLoc, body, test);
            return FinishNode(node);
        }

        // Disambiguating between a `for` and a `for`/`in` or `for`/`of`
        // loop is non-trivial. Basically, we have to parse the init `var`
        // statement or expression, disallowing the `in` operator (see
        // the second parameter to `parseExpression`), and then check
        // whether the next token is `in` or `of`. When there is no init
        // part (semicolon immediately after the opening parenthesis), it
        // is a regular `for` loop.
        [NotNull]
        private StatementNode ParseForStatement(int start, Position startLoc)
        {
            Next();
            var awaitAt = options.EcmaVersion >= 9 && (InAsync || !InFunction && options.AllowAwaitOutsideFunction) && EatContextual("await") ? lastTokStart : -1;
            labels.Add(loopLabel);
            EnterScope(0);
            Expect(TokenType.ParenLeft);
            if (type == TokenType.Semicolon)
            {
                if (awaitAt > -1)
                {
                    Unexpected(awaitAt);
                }

                return ParseFor(start, startLoc, null);
            }

            var isLet = IsLet();
            if (type == TokenType.Var || type == TokenType.Const || isLet)
            {
                var initStart = this.start;
                var initStartLoc = this.startLoc;

                PropertyKind kind;
                if (isLet)
                {
                    kind = PropertyKind.Let;
                }
                else
                {
                    switch (value)
                    {
                        case "var":
                            kind = PropertyKind.Var;
                            break;

                        case "let":
                            kind = PropertyKind.Let;
                            break;

                        case "const":
                            kind = PropertyKind.Const;
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }

                Next();
                var declarations = ParseVar(true, kind);
                var init = new VariableDeclarationNode(this, initStart, initStartLoc, kind, declarations);
                FinishNode(init);
                if ((type == TokenType.In || options.EcmaVersion >= 6 && IsContextual("of")) && init.Declarations.Count == 1 &&
                    !(kind != PropertyKind.Var && init.Declarations[0].Init != null))
                {
                    var isAwait = false;
                    if (options.EcmaVersion >= 9)
                    {
                        if (type == TokenType.In)
                        {
                            if (awaitAt > -1)
                            {
                                Unexpected(awaitAt);
                            }
                        }
                        else
                        {
                            isAwait = awaitAt > -1;
                        }
                    }

                    return ParseForIn(start, startLoc, isAwait, init);
                }

                if (awaitAt > -1)
                {
                    Unexpected(awaitAt);
                }

                return ParseFor(start, startLoc, init);
            }
            else
            {
                var refDestructuringErrors = new DestructuringErrors();
                var init = ParseExpression(true, refDestructuringErrors);
                if (type == TokenType.In || options.EcmaVersion >= 6 && IsContextual("of"))
                {
                    var isAwait = false;
                    if (options.EcmaVersion >= 9)
                    {
                        if (type == TokenType.In)
                        {
                            if (awaitAt > -1)
                            {
                                Unexpected(awaitAt);
                            }
                        }
                        else
                        {
                            isAwait = awaitAt > -1;
                        }
                    }

                    ToAssignable(ref init, false, refDestructuringErrors);
                    CheckLeftValue(init);
                    return ParseForIn(start, startLoc, isAwait, init);
                }

                CheckExpressionErrors(refDestructuringErrors, true);

                if (awaitAt > -1)
                {
                    Unexpected(awaitAt);
                }

                return ParseFor(start, startLoc, init);
            }
        }

        [NotNull]
        private FunctionDeclarationNode ParseFunctionStatement(int start, Position startLoc, bool isAsync, bool declarationPosition)
        {
            Next();
            return (FunctionDeclarationNode)ParseFunction(start, startLoc, FUNC_STATEMENT | (declarationPosition ? 0 : FUNC_HANGING_STATEMENT), false, isAsync);
        }

        [NotNull]
        private IfStatementNode ParseIfStatement(int start, Position startLoc)
        {
            Next();
            var test = ParseParenthesisExpression();
            // allow function declarations in branches, but only in non-strict mode
            var consequent = ParseStatement("if");
            var alternate = Eat(TokenType.Else) ? ParseStatement("if") : null;
            var node = new IfStatementNode(this, start, startLoc, test, consequent, alternate);
            return FinishNode(node);
        }

        [NotNull]
        private ReturnStatementNode ParseReturnStatement(int start, Position startLoc)
        {
            if (!InFunction && !options.AllowReturnOutsideFunction)
            {
                Raise(this.start, "'return' outside of function");
            }

            Next();

            // In `return` (and `break`/`continue`), the keywords with
            // optional arguments, we eagerly look for a semicolon or the
            // possibility to insert one.

            ExpressionNode argument;
            if (Eat(TokenType.Semicolon) || InsertSemicolon())
            {
                argument = null;
            }
            else
            {
                argument = ParseExpression();
                Semicolon();
            }

            var node = new ReturnStatementNode(this, start, startLoc, argument);
            return FinishNode(node);
        }

        [NotNull]
        private SwitchStatementNode ParseSwitchStatement(int start, Position startLoc)
        {
            Next();
            var discriminant = ParseParenthesisExpression();
            var cases = new List<SwitchCaseNode>();
            Expect(TokenType.BraceLeft);
            labels.Add(switchLabel);
            EnterScope(0);

            // Statements under must be grouped (by label) in SwitchCase
            // nodes. `cur` is used to keep the node that we are currently
            // adding statements to.

            SwitchCaseNode current = null;
            for (var sawDefault = false; type != TokenType.BraceRight;)
            {
                if (type == TokenType.Case || type == TokenType.Default)
                {
                    var isCase = type == TokenType.Case;
                    if (current != null)
                    {
                        FinishNode(current);
                    }

                    var caseStart = this.start;
                    var caseStartLoc = this.startLoc;
                    Next();
                    ExpressionNode test;
                    if (isCase)
                    {
                        test = ParseExpression();
                    }
                    else
                    {
                        if (sawDefault)
                        {
                            RaiseRecoverable(lastTokStart, "Multiple default clauses");
                        }

                        sawDefault = true;
                        test = null;
                    }

                    current = new SwitchCaseNode(this, caseStart, caseStartLoc, test, new List<StatementNode>());
                    cases.Add(current);

                    Expect(TokenType.Colon);
                }
                else
                {
                    if (current == null)
                    {
                        Unexpected();
                        throw new InvalidOperationException();
                    }

                    current.Consequent.Add(ParseStatement(null));
                }
            }

            ExitScope();
            if (current != null)
            {
                FinishNode(current);
            }

            Next();
            labels.RemoveAt(labels.Count - 1);

            var node = new SwitchStatementNode(this, start, startLoc, discriminant, cases);
            return FinishNode(node);
        }

        [NotNull]
        private ThrowStatementNode ParseThrowStatement(int start, Position startLoc)
        {
            Next();
            if (Whitespace.LineBreak.IsMatch(input.Substring(lastTokEnd, this.start - lastTokEnd)))
            {
                Raise(lastTokEnd, "Illegal newline after throw");
            }

            var argument = ParseExpression();
            Semicolon();
            var node = new ThrowStatementNode(this, start, startLoc, argument);
            return FinishNode(node);
        }

        [NotNull]
        private TryStatementNode ParseTryStatement(int start, Position startLoc)
        {
            Next();
            var block = ParseBlock();
            CatchClauseNode handler = null;
            if (type == TokenType.Catch)
            {
                var clauseStart = this.start;
                var clauseStartLoc = this.startLoc;
                Next();
                ExpressionNode param;
                if (Eat(TokenType.ParenLeft))
                {
                    param = ParseBindingAtom();
                    var simple = param is IdentifierNode;
                    EnterScope(simple ? ScopeFlags.SimpleCatch : 0);
                    CheckLeftValue(param, simple ? BindType.SimpleCatch : BindType.Lexical);
                    Expect(TokenType.ParenRight);
                }
                else
                {
                    if (options.EcmaVersion < 10)
                    {
                        Unexpected();
                    }

                    param = null;
                    EnterScope(0);
                }

                var body = ParseBlock(false);
                ExitScope();
                handler = FinishNode(new CatchClauseNode(this, clauseStart, clauseStartLoc, param, body));
            }

            var finaliser = Eat(TokenType.Finally) ? ParseBlock() : null;
            if (handler == null && finaliser == null)
            {
                Raise(start, "Missing catch or finally clause");
            }

            return FinishNode(new TryStatementNode(this, start, startLoc, block, handler, finaliser));
        }

        [NotNull]
        private VariableDeclarationNode ParseVarStatement(int start, Position startLoc, PropertyKind kind)
        {
            Next();
            var declarations = ParseVar(false, kind);
            Semicolon();
            var node = new VariableDeclarationNode(this, start, startLoc, kind, declarations);
            return FinishNode(node);
        }

        [NotNull]
        private WhileStatementNode ParseWhileStatement(int start, Position startLoc)
        {
            Next();
            var test = ParseParenthesisExpression();
            labels.Add(loopLabel);
            var body = ParseStatement("while");
            labels.RemoveAt(labels.Count - 1);
            var node = new WhileStatementNode(this, start, startLoc, test, body);
            return FinishNode(node);
        }

        [NotNull]
        private WithStatementNode ParseWithStatement(int start, Position startLoc)
        {
            if (strict)
            {
                Raise(this.start, "'with' in strict mode");
            }

            Next();
            var @object = ParseParenthesisExpression();
            var body = ParseStatement("with");
            var node = new WithStatementNode(this, start, startLoc, @object, body);
            return FinishNode(node);
        }

        [NotNull]
        private EmptyStatementNode ParseEmptyStatement(int start, Position startLoc)
        {
            Next();
            return FinishNode(new EmptyStatementNode(this, start, startLoc));
        }

        [NotNull]
        private LabelledStatementNode ParseLabelledStatement(int start, Position startLoc, string maybeName, [NotNull] ExpressionNode expr, string context)
        {
            foreach (var label in labels)
            {
                if (label.Name == maybeName)
                {
                    Raise(expr.Start, "Label '" + maybeName + "' is already declared");
                }
            }

            var kind = type.IsLoop ? LabelKind.Loop : type == TokenType.Switch ? LabelKind.Switch : LabelKind.None;
            for (var i = labels.Count - 1; i >= 0; i--)
            {
                var label = labels[i];
                if (label.StatementStart == start)
                {
                    // Update information about previous labels on this node
                    label.StatementStart = this.start;
                    label.Kind = kind;
                }
                else
                {
                    break;
                }
            }

            labels.Add(new Label(kind, maybeName, this.start));
            var body = ParseStatement(context);
            if (body is ClassDeclarationNode ||
                body is VariableDeclarationNode variable && variable.Kind != PropertyKind.Var ||
                body is FunctionDeclarationNode function && (strict || function.Generator || function.Async))
            {
                RaiseRecoverable(body.Start, "Invalid labeled declaration");
            }

            labels.RemoveAt(labels.Count - 1);
            return FinishNode(new LabelledStatementNode(this, start, startLoc, body, expr));
        }

        [NotNull]
        private ExpressionStatementNode ParseExpressionStatement(int start, Position startLocation, [NotNull] ExpressionNode expressionNode)
        {
            var node = new ExpressionStatementNode(this, start, startLocation, expressionNode);
            Semicolon();
            return FinishNode(node);
        }

        // Parse a semicolon-enclosed block of statements, handling `"use
        // strict"` declarations when `allowStrict` is true (used for
        // function bodies).
        [NotNull]
        private BlockStatementNode ParseBlock(bool createNewLexicalScope = true, int start = -1, Position startLoc = default)
        {
            if (start < 0)
            {
                start = this.start;
                startLoc = this.startLoc;
            }

            var body = new List<StatementNode>();
            Expect(TokenType.BraceLeft);
            if (createNewLexicalScope)
            {
                EnterScope(0);
            }

            while (!Eat(TokenType.BraceRight))
            {
                var statement = ParseStatement(null);
                body.Add(statement);
            }

            if (createNewLexicalScope)
            {
                ExitScope();
            }

            var node = new BlockStatementNode(this, start, startLoc, body);
            return FinishNode(node);
        }

        // Parse a regular `for` loop. The disambiguation code in
        // `parseStatement` will already have parsed the init statement or
        // expression.
        [NotNull]
        private ForStatementNode ParseFor(int start, Position startLoc, [CanBeNull] BaseNode init)
        {
            Expect(TokenType.Semicolon);
            var test = type == TokenType.Semicolon ? null : ParseExpression();
            Expect(TokenType.Semicolon);
            var update = type == TokenType.ParenRight ? null : ParseExpression();
            Expect(TokenType.ParenRight);
            ExitScope();
            var body = ParseStatement("for");
            labels.RemoveAt(labels.Count - 1);

            var node = new ForStatementNode(this, start, startLoc, init, test, update, body);
            return FinishNode(node);
        }

        // Parse a `for`/`in` and `for`/`of` loop, which are almost
        // same from parser's perspective.
        [NotNull]
        private StatementNode ParseForIn(int start, Position startLoc, bool isAwait, [NotNull] BaseNode init)
        {
            var isIn = type == TokenType.In;
            Next();
            if (isIn)
            {
                if (init is AssignmentPatternNode ||
                    init is VariableDeclarationNode declarationNode && declarationNode.Declarations[0].Init != null &&
                    (strict || !(declarationNode.Declarations[0].Id is IdentifierNode)))
                {
                    Raise(init.Start, "Invalid assignment in for-in loop head");
                }
            }

            var left = init;
            var right = isIn ? ParseExpression() : ParseMaybeAssign();
            Expect(TokenType.ParenRight);
            ExitScope();
            var body = ParseStatement("for");
            labels.RemoveAt(labels.Count - 1);

            StatementNode node;
            if (isIn)
            {
                node = new ForInStatementNode(this, start, startLoc, isAwait, left, right, body);
            }
            else
            {
                node = new ForOfStatementNode(this, start, startLoc, isAwait, left, right, body);
            }

            return FinishNode(node);
        }

        // Parse a list of variable declarations.
        [NotNull]
        [ItemNotNull]
        private List<VariableDeclaratorNode> ParseVar(bool isFor, PropertyKind kind)
        {
            var declarations = new List<VariableDeclaratorNode>();
            for (;;)
            {
                var start = this.start;
                var startLoc = this.startLoc;
                var id = ParseVarId(kind);
                ExpressionNode init = null;
                if (Eat(TokenType.Equal))
                {
                    init = ParseMaybeAssign(isFor);
                }
                else if (kind == PropertyKind.Const && !(type == TokenType.In || options.EcmaVersion >= 6 && IsContextual("of")))
                {
                    Unexpected();
                }
                else if (!(id is IdentifierNode) && !(isFor && (type == TokenType.In || IsContextual("of"))))
                {
                    Raise(lastTokEnd, "Complex binding patterns require an initialization value");
                }

                var decl = new VariableDeclaratorNode(this, start, startLoc, id, init);
                declarations.Add(FinishNode(decl));
                if (!Eat(TokenType.Comma))
                {
                    break;
                }
            }

            return declarations;
        }

        [NotNull]
        private ExpressionNode ParseVarId(PropertyKind kind)
        {
            var id = ParseBindingAtom();
            CheckLeftValue(id, kind == PropertyKind.Var ? BindType.Var : BindType.Lexical);
            return id;
        }

        private const int FUNC_STATEMENT = 1;
        private const int FUNC_HANGING_STATEMENT = 2;
        private const int FUNC_NULLABLE_ID = 4;

        // Parse a function declaration or literal (depending on the
        // `isStatement` parameter).
        [NotNull]
        private BaseNode ParseFunction(int startPos, Position startLoc, int statement, bool allowExpressionBody = false, bool isAsync = false)
        {
            var generator = false;
            var async = false;
            if (options.EcmaVersion >= 9 || options.EcmaVersion >= 6 && !isAsync)
            {
                generator = Eat(TokenType.Star);
            }

            if (options.EcmaVersion >= 8)
            {
                async = isAsync;
            }

            IdentifierNode id = null;
            if ((statement & FUNC_STATEMENT) != 0)
            {
                id = (statement & FUNC_NULLABLE_ID) != 0 && type != TokenType.Name ? null : ParseIdentifier();
                if (id != null && (statement & FUNC_HANGING_STATEMENT) == 0)
                {
                    CheckLeftValue(id, inModule && !InFunction ? BindType.Lexical : BindType.Function);
                }
            }

            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            yieldPos = 0;
            awaitPos = 0;
            EnterScope(FunctionFlags(async, generator));

            if ((statement & FUNC_STATEMENT) == 0)
            {
                id = type == TokenType.Name ? ParseIdentifier() : null;
            }

            var parameters = ParseFunctionParams();
            var (expression, body) = ParseFunctionBody(startPos, id, parameters, allowExpressionBody);

            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;

            BaseNode node;
            if ((statement & FUNC_STATEMENT) != 0)
            {
                node = new FunctionDeclarationNode(this, startPos, startLoc, id, generator, async, parameters, expression, body);
            }
            else
            {
                node = new FunctionExpressionNode(this, startPos, startLoc, id, generator, async, parameters, expression, body);
            }

            return FinishNode(node);
        }

        [NotNull]
        private IList<ExpressionNode> ParseFunctionParams()
        {
            Expect(TokenType.ParenLeft);
            var parameters = ParseBindingList(TokenType.ParenRight, false, options.EcmaVersion >= 8);
            CheckYieldAwaitInDefaultParams();
            return parameters;
        }

        // Parse a class declaration or literal (depending on the
        // `isStatement` parameter).
        [NotNull]
        private BaseNode ParseClass(int start, Position startLoc, bool isStatement, bool needsId)
        {
            Next();

            var id = ParseClassId(needsId);
            var superClass = ParseClassSuper();
            var classBodyStart = this.start;
            var classBodyStartLoc = this.startLoc;
            var hadConstructor = false;
            var body = new List<MethodDefinitionNode>();
            Expect(TokenType.BraceLeft);
            while (!Eat(TokenType.BraceRight))
            {
                var element = ParseClassElement(superClass != null);
                if (element != null)
                {
                    body.Add(element);
                    if (element is MethodDefinitionNode methodDefinition && methodDefinition.Kind == PropertyKind.Constructor)
                    {
                        if (hadConstructor)
                        {
                            Raise(element.Start, "Duplicate constructor in the same class");
                        }

                        hadConstructor = true;
                    }
                }
            }

            var bodyNode = FinishNode(new ClassBodyNode(this, classBodyStart, classBodyStartLoc, body));
            BaseNode node;
            if (isStatement)
            {
                node = new ClassDeclarationNode(this, start, startLoc, id, superClass, bodyNode);
            }
            else
            {
                node = new ClassExpressionNode(this, start, startLoc, id, superClass, bodyNode);
            }

            return FinishNode(node);
        }

        [CanBeNull]
        private MethodDefinitionNode ParseClassElement(bool constructorAllowsSuper)
        {
            if (Eat(TokenType.Semicolon))
            {
                return null;
            }

            var start = this.start;
            var startLoc = this.startLoc;
            ExpressionNode key = null;

            bool TryContextual(string k, bool noLineBreak)
            {
                var cstart = this.start;
                var cstartLoc = this.startLoc;
                if (!EatContextual(k))
                {
                    return false;
                }

                if (type != TokenType.ParenLeft && (!noLineBreak || !CanInsertSemicolon()))
                {
                    return true;
                }

                if (key != null)
                {
                    Unexpected();
                }

                key = new IdentifierNode(this, cstart, cstartLoc, k);
                FinishNode(key);
                return false;
            }

            var kind = PropertyKind.Method;
            var @static = TryContextual("static", false);
            var isGenerator = Eat(TokenType.Star);
            var computed = false;
            var isAsync = false;
            if (!isGenerator)
            {
                if (options.EcmaVersion >= 8 && TryContextual("async", true))
                {
                    isAsync = true;
                    isGenerator = options.EcmaVersion >= 9 && Eat(TokenType.Star);
                }
                else if (TryContextual("get", false))
                {
                    kind = PropertyKind.Get;
                }
                else if (TryContextual("set", false))
                {
                    kind = PropertyKind.Set;
                }
            }

            if (key == null)
            {
                (computed, key) = ParsePropertyName();
            }

            var allowsDirectSuper = false;
            if (!computed && !@static && (key is IdentifierNode identifier && identifier.Name == "constructor" ||
                                          key is LiteralNode literal && literal.IsString && (string)literal.Value == "constructor"))
            {
                if (kind != PropertyKind.Method)
                {
                    Raise(key.Start, "Constructor can't have get/set modifier");
                }

                if (isGenerator)
                {
                    Raise(key.Start, "Constructor can't be a generator");
                }

                if (isAsync)
                {
                    Raise(key.Start, "Constructor can't be an async method");
                }

                kind = PropertyKind.Constructor;
                allowsDirectSuper = constructorAllowsSuper;
            }
            else if (@static && key is IdentifierNode node && node.Name == "prototype")
            {
                Raise(node.Start, "Classes may not have a static property named prototype");
            }

            var method = ParseClassMethod(start, startLoc, key, computed, kind, @static, isGenerator, isAsync, allowsDirectSuper);
            if (method.Kind == PropertyKind.Get && method.Value.Parameters.Count != 0)
            {
                RaiseRecoverable(method.Value.Start, "getter should have no params");
            }

            if (method.Kind == PropertyKind.Set && method.Value.Parameters.Count != 1)
            {
                RaiseRecoverable(method.Value.Start, "setter should have exactly one param");
            }

            if (method.Kind == PropertyKind.Set && method.Value.Parameters[0] is RestElementNode)
            {
                RaiseRecoverable(method.Value.Parameters[0].Start, "Setter cannot use rest params");
            }

            return method;
        }

        [NotNull]
        private MethodDefinitionNode ParseClassMethod(int start, Position startLoc, [NotNull] ExpressionNode key, bool computed, PropertyKind kind, bool @static, bool isGenerator, bool isAsync, bool allowsDirectSuper)
        {
            var value = ParseMethod(isGenerator, isAsync, allowsDirectSuper);
            return FinishNode(new MethodDefinitionNode(this, start, startLoc, key, computed, kind, @static, value));
        }

        [CanBeNull]
        private IdentifierNode ParseClassId(bool isStatement)
        {
            if (type == TokenType.Name)
            {
                return ParseIdentifier();
            }

            if (isStatement)
            {
                Unexpected();
            }

            return null;
        }

        [CanBeNull]
        private ExpressionNode ParseClassSuper()
        {
            return Eat(TokenType.Extends) ? ParseExprSubscripts() : null;
        }

        // Parses module export declaration.
        [NotNull]
        private BaseExportDeclarationNode ParseExport(int start, Position startLoc, ISet<string> exports)
        {
            Next();
            // export * from '...'
            if (Eat(TokenType.Star))
            {
                ExpectContextual("from");
                if (type != TokenType.String)
                {
                    Unexpected();
                }

                var source = ParseExprAtom();
                Semicolon();
                return FinishNode(new ExportAllDeclarationNode(this, start, startLoc, source));
            }

            if (Eat(TokenType.Default))
            {
                // export default ...
                CheckExport(exports, "default", lastTokStart);
                var isAsync = false;
                BaseNode declaration;
                if (type == TokenType.Function || (isAsync = IsAsyncFunction()))
                {
                    var functionStart = this.start;
                    var functionStartLoc = this.startLoc;
                    Next();
                    if (isAsync)
                    {
                        Next();
                    }

                    declaration = ParseFunction(functionStart, functionStartLoc, FUNC_STATEMENT | FUNC_NULLABLE_ID, false, isAsync);
                }
                else if (type == TokenType.Class)
                {
                    declaration = ParseClass(this.start, this.startLoc, true, false);
                }
                else
                {
                    declaration = ParseMaybeAssign();
                    Semicolon();
                }

                return FinishNode(new ExportDefaultDeclarationNode(this, start, startLoc, declaration));
            }
            else
            {
                StatementNode declaration;
                IList<ExportSpecifierNode> specifiers;
                ExpressionNode source;
                if (ShouldParseExportStatement())
                {
                    // export var|const|let|function|class ...

                    declaration = ParseStatement(null);
                    if (declaration is VariableDeclarationNode variableDeclaration)
                    {
                        CheckVariableExport(exports, variableDeclaration.Declarations);
                    }
                    else if (declaration is FunctionDeclarationNode functionDeclaration)
                    {
                        Debug.Assert(functionDeclaration.Id != null, "functionDeclaration.Id != null");
                        CheckExport(exports, functionDeclaration.Id.Name, functionDeclaration.Id.Start);
                    }
                    else if (declaration is ClassDeclarationNode classDeclaration)
                    {
                        CheckExport(exports, classDeclaration.Id.Name, classDeclaration.Id.Start);
                    }
                    else
                    {
//                        CheckExport(exports, declaration.id.name, declaration.id.start);
                        throw new NotImplementedException();
                    }

                    specifiers = Array.Empty<ExportSpecifierNode>();
                    source = null;
                }
                else
                {
                    // export { x, y as z } [from '...']

                    declaration = null;
                    specifiers = ParseExportSpecifiers(exports);
                    if (EatContextual("from"))
                    {
                        if (type != TokenType.String)
                        {
                            Unexpected();
                        }

                        source = ParseExprAtom();
                    }
                    else
                    {
                        // check for keywords used as local names
                        foreach (var spec in specifiers)
                        {
                            CheckUnreserved(spec.Local.Start, spec.Local.End, spec.Local.Name);
                        }

                        source = null;
                    }

                    Semicolon();
                }

                return FinishNode(new ExportNamedDeclarationNode(this, start, startLoc, declaration, specifiers, source));
            }
        }

        private void CheckExport([CanBeNull] ISet<string> exports, string name, int pos)
        {
            if (exports == null)
            {
                return;
            }

            if (exports.Contains(name))
            {
                RaiseRecoverable(pos, "Duplicate export '" + name + "'");
            }

            exports.Add(name);
        }

        private void CheckPatternExport(ISet<string> exports, ExpressionNode pattern)
        {
            if (pattern is IdentifierNode identifier)
            {
                CheckExport(exports, identifier.Name, identifier.Start);
            }
            else if (pattern is ObjectPatternNode objectPattern)
            {
                foreach (var prop in objectPattern.Properties)
                {
                    CheckPatternExport(exports, prop);
                }
            }
            else if (pattern is ArrayPatternNode arrayPattern)
            {
                foreach (var elt in arrayPattern.Elements)
                {
                    if (elt != null)
                    {
                        CheckPatternExport(exports, elt);
                    }
                }
            }
            else if (pattern is PropertyNode property)
            {
                CheckPatternExport(exports, property.Value);
            }
            else if (pattern is AssignmentPatternNode assignment)
            {
                CheckPatternExport(exports, assignment.Left);
            }
            else if (pattern is RestElementNode restElement)
            {
                CheckPatternExport(exports, restElement.Argument);
            }
            else if (pattern is ParenthesisedExpressionNode parenthesised)
            {
                CheckPatternExport(exports, parenthesised.Expression);
            }
        }

        private void CheckVariableExport([CanBeNull] ISet<string> exports, [NotNull] IEnumerable<VariableDeclaratorNode> decls)
        {
            if (exports == null)
            {
                return;
            }

            foreach (var decl in decls)
            {
                CheckPatternExport(exports, decl.Id);
            }
        }

        private bool ShouldParseExportStatement()
        {
            return type.Keyword == "var" ||
                   type.Keyword == "const" ||
                   type.Keyword == "class" ||
                   type.Keyword == "function" ||
                   IsLet() ||
                   IsAsyncFunction();
        }

        // Parses a comma-separated list of module exports.
        [NotNull]
        [ItemNotNull]
        private IList<ExportSpecifierNode> ParseExportSpecifiers(ISet<string> exports)
        {
            var nodes = new List<ExportSpecifierNode>();
            var first = true;
            // export { x, y as z } [from '...']
            Expect(TokenType.BraceLeft);
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

                var start = this.start;
                var startLoc = this.startLoc;
                var local = ParseIdentifier(true);
                var exported = EatContextual("as") ? ParseIdentifier(true) : local;
                CheckExport(exports, exported.Name, exported.Start);
                nodes.Add(FinishNode(new ExportSpecifierNode(this, start, startLoc, local, exported)));
            }

            return nodes;
        }

        // Parses import declaration.
        [NotNull]
        private ImportDeclarationNode ParseImport(int start, Position startLoc)
        {
            Next();
            // import '...'
            IList<BaseImportSpecifierNode> specifiers;
            ExpressionNode source;
            if (type == TokenType.String)
            {
                specifiers = Array.Empty<BaseImportSpecifierNode>();
                source = ParseExprAtom();
            }
            else
            {
                specifiers = ParseImportSpecifiers();
                ExpectContextual("from");
                if (type != TokenType.String)
                {
                    Unexpected();
                }

                source = ParseExprAtom();
            }

            Semicolon();
            return FinishNode(new ImportDeclarationNode(this, start, startLoc, specifiers, source));
        }

        // Parses a comma-separated list of module imports.
        [NotNull]
        private IList<BaseImportSpecifierNode> ParseImportSpecifiers()
        {
            var nodes = new List<BaseImportSpecifierNode>();
            var first = true;
            var start = this.start;
            var startLoc = this.startLoc;

            if (type == TokenType.Name)
            {
                // import defaultObj, { x, y as z } from '...'

                var local = ParseIdentifier();
                CheckLeftValue(local, BindType.Lexical);
                nodes.Add(FinishNode(new ImportDefaultSpecifierNode(this, start, startLoc, local)));
                if (!Eat(TokenType.Comma))
                {
                    return nodes;
                }
            }

            if (type == TokenType.Star)
            {
                Next();
                ExpectContextual("as");
                var local = ParseIdentifier();
                CheckLeftValue(local, BindType.Lexical);
                nodes.Add(FinishNode(new ImportNamespaceSpecifierNode(this, start, startLoc, local)));
                return nodes;
            }

            Expect(TokenType.BraceLeft);
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

                var nstart = this.start;
                var nstartLoc = this.startLoc;
                var imported = ParseIdentifier(true);
                IdentifierNode local;
                if (EatContextual("as"))
                {
                    local = ParseIdentifier();
                }
                else
                {
                    CheckUnreserved(imported.Start, imported.End, imported.Name);
                    local = imported;
                }

                CheckLeftValue(local, BindType.Lexical);
                nodes.Add(FinishNode(new ImportSpecifierNode(this, nstart, nstartLoc, imported, local)));
            }

            return nodes;
        }

        // Set `ExpressionStatement#directive` property for directive prologues.
        private void AdaptDirectivePrologue([NotNull] IList<StatementNode> statements)
        {
            for (var i = 0; i < statements.Count && IsDirectiveCandidate(statements[i]); ++i)
            {
                var raw = ((LiteralNode)((ExpressionStatementNode)statements[i]).Expression).Raw;
                ((ExpressionStatementNode)statements[i]).Directive = raw.Substring(1, raw.Length - 2);
            }
        }

        private bool IsDirectiveCandidate(BaseNode statement)
        {
            return statement is ExpressionStatementNode expressionStatement &&
                   expressionStatement.Expression is LiteralNode literalNode &&
                   literalNode.IsString &&
                   // Reject parenthesized strings.
                   (input[statement.Start] == '"' || input[statement.Start] == '\'');
        }
    }
}