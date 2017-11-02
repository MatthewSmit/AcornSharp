using System;
using System.Collections.Generic;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    internal sealed partial class Parser
    {
        private static readonly Label loopLabel = new Label {kind = "loop"};
        private static readonly Label switchLabel = new Label {kind = "switch"};

        // Parse a program. Initializes the parser, reads any number of
        // statements, and wraps them in a Program node.  Optionally takes a
        // `program` argument.  If present, the statements will be appended
        // to its body instead of creating a new node.
        private void parseTopLevel([NotNull] ProgramNode node)
        {
            var exports = new Dictionary<string, bool>();
            while (type != TokenType.EOF)
            {
                var stmt = parseStatement(true, true, exports);
                node.Body.Add(stmt);
            }
            adaptDirectivePrologue(node.Body);
            next();
            node.Location = new SourceLocation(node.Location.Start, lastTokEnd, node.Location.Source);
        }

        private bool isLet()
        {
            if (type != TokenType.name || Options.ecmaVersion < 6 || (string)value != "let") return false;
            var skip = skipWhiteSpace.Match(input, pos.Index);
            var next = this.pos.Index + skip.Groups[0].Length;
            var nextCh = input[next];
            if (nextCh == 91 || nextCh == 123) return true; // '{' and '['
            if (isIdentifierStart(nextCh, true))
            {
                var pos = next + 1;
                while (isIdentifierChar(input.Get(pos), true))
                    ++pos;
                var ident = input.Substring(next, pos - next);
                if (!keywords.IsMatch(ident))
                    return true;
            }
            return false;
        }

        // check 'async [no LineTerminator here] function'
        // - 'async /*foo*/ function' is OK.
        // - 'async /*\n*/ function' is invalid.
        private bool isAsyncFunction()
        {
            if (type != TokenType.name || Options.ecmaVersion < 8 || (string)value != "async")
                return false;

            var skip = skipWhiteSpace.Match(input, pos.Index);
            var next = pos.Index + skip.Groups[0].Length;
            return !lineBreak.IsMatch(input.Substring(pos.Index, next - pos.Index)) &&
                   input.Length >= next + 8 && input.Substring(next, 8) == "function" &&
                   (next + 8 == input.Length || !isIdentifierChar(input.Get(next + 8)));
        }

        // Parse a single statement.
        //
        // If expecting a statement and finding a slash operator, parse a
        // regular expression literal. This is to handle cases like
        // `if (foo) /blah/.exec(foo)`, where looking at the previous token
        // does not help.
        [NotNull]
        private BaseNode parseStatement(bool declaration, bool topLevel = false, [CanBeNull] IDictionary<string, bool> exports = null)
        {
            var starttype = type;
            var startLocation = start;
            VariableKind? kind = null;

            if (isLet())
            {
                starttype = TokenType._var;
                kind = VariableKind.Let;
            }

            // Most types of statements are recognized by the keyword they
            // start with. Many are trivial to parse, some require a bit of
            // complexity.

            switch (starttype)
            {
                case TokenType._break:
                case TokenType._continue:
                    return parseBreakContinueStatement(startLocation, TokenInformation.Types[starttype].Keyword);
                case TokenType._debugger:
                    return parseDebuggerStatement(startLocation);
                case TokenType._do:
                    return parseDoStatement(startLocation);
                case TokenType._for:
                    return parseForStatement(startLocation);
                case TokenType._function:
                    if (!declaration && Options.ecmaVersion >= 6)
                    {
                        raise(startLocation, "Unexpected token");
                    }
                    return parseFunctionStatement(startLocation, false);
                case TokenType._class:
                    if (!declaration)
                    {
                        raise(startLocation, "Unexpected token");
                    }
                    return parseClass(startLocation, "true");
                case TokenType._if:
                    return parseIfStatement(startLocation);
                case TokenType._return:
                    return parseReturnStatement(startLocation);
                case TokenType._switch:
                    return parseSwitchStatement(startLocation);
                case TokenType._throw:
                    return parseThrowStatement(startLocation);
                case TokenType._try:
                    return parseTryStatement(startLocation);
                case TokenType._const:
                case TokenType._var:
                    var realKind = kind ?? ToVariableKind((string)value);
                    if (!declaration && realKind != VariableKind.Var)
                    {
                        raise(startLocation, "Unexpected token");
                    }
                    return parseVarStatement(startLocation, realKind);
                case TokenType._while:
                    return parseWhileStatement(startLocation);
                case TokenType._with:
                    return parseWithStatement(startLocation);
                case TokenType.braceL:
                    return parseBlock();
                case TokenType.semi:
                    return parseEmptyStatement(startLocation);
                case TokenType._export:
                case TokenType._import:
                    if (!Options.allowImportExportEverywhere)
                    {
                        if (!topLevel)
                            raise(startLocation, "'import' and 'export' may only appear at the top level");
                        if (!inModule)
                            raise(startLocation, "'import' and 'export' may appear only with 'sourceType: module'");
                    }
                    return starttype == TokenType._import ? parseImport(startLocation) : parseExport(startLocation, exports);
            }

            if (isAsyncFunction() && declaration)
            {
                next();
                return parseFunctionStatement(startLocation, true);
            }

            var maybeName = value;
            var expr = ParseExpression();
            if (starttype == TokenType.name && expr is IdentifierNode identifierNode && eat(TokenType.colon))
                return parseLabelledStatement(startLocation, (string)maybeName, identifierNode);
            return parseExpressionStatement(startLocation, expr);
        }

        private static VariableKind ToVariableKind([NotNull] string s)
        {
            switch (s)
            {
                case "var":
                    return VariableKind.Var;
                case "let":
                    return VariableKind.Let;
                case "const":
                    return VariableKind.Const;
                default:
                    throw new ArgumentException();
            }
        }

        [NotNull]
        private BaseNode parseBreakContinueStatement(Position nodeStart, string keyword)
        {
            var isBreak = keyword == "break";
            IdentifierNode label = null;
            next();
            if (eat(TokenType.semi) || insertSemicolon())
            {
            }
            else if (type != TokenType.name)
            {
                raise(start, "Unexpected token");
            }
            else
            {
                label = parseIdent();
                semicolon();
            }

            // Verify that there is an actual destination to break or
            // continue to.
            var i = 0;
            for (; i < labels.Count; ++i)
            {
                var lab = labels[i];
                if (label == null || lab.name == label.Name)
                {
                    if (lab.kind != null && (isBreak || lab.kind == "loop")) break;
                    if (label != null && isBreak) break;
                }
            }
            if (i == labels.Count) raise(nodeStart, "Unsyntactic " + keyword);

            if (isBreak)
            {
                return new BreakStatementNode(this, nodeStart, lastTokEnd, label);
            }
            return new ContinueStatementNode(this, nodeStart, lastTokEnd, label);
        }

        [NotNull]
        private DebuggerStatementNode parseDebuggerStatement(Position nodeStart)
        {
            next();
            semicolon();
            return new DebuggerStatementNode(this, nodeStart, lastTokEnd);
        }

        [NotNull]
        private DoWhileStatementNode parseDoStatement(Position nodeStart)
        {
            next();
            labels.Add(loopLabel);
            var body = parseStatement(false);
            labels.Pop();
            expect(TokenType._while);
            var test = parseParenExpression();
            if (Options.ecmaVersion >= 6)
                eat(TokenType.semi);
            else
                semicolon();

            return new DoWhileStatementNode(this, nodeStart, lastTokEnd, test, body);
        }

        // Disambiguating between a `for` and a `for`/`in` or `for`/`of`
        // loop is non-trivial. Basically, we have to parse the init `var`
        // statement or expression, disallowing the `in` operator (see
        // the second parameter to `parseExpression`), and then check
        // whether the next token is `in` or `of`. When there is no init
        // part (semicolon immediately after the opening parenthesis), it
        // is a regular `for` loop.
        [NotNull]
        private BaseNode parseForStatement(Position nodeStart)
        {
            next();
            labels.Add(loopLabel);
            enterLexicalScope();
            expect(TokenType.parenL);
            if (type == TokenType.semi) return parseFor(nodeStart, null);
            var isLet = this.isLet();
            if (type == TokenType._var || type == TokenType._const || isLet)
            {
                var startLoc = start;
                var kind = isLet ? VariableKind.Let : ToVariableKind((string)value);
                next();
                var declarations = parseVar(true, kind);
                var init = new VariableDeclarationNode(this, startLoc, lastTokEnd, kind, declarations);
                if ((type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of")) && init.Declarations.Count == 1 &&
                    !(kind != VariableKind.Var && init.Declarations[0].Init != null))
                    return parseForIn(nodeStart, init);
                return parseFor(nodeStart, init);
            }
            else
            {
                var refDestructuringErrors = new DestructuringErrors();
                var init = ParseExpression(true, refDestructuringErrors);
                if (type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of"))
                {
                    init = toAssignable(init);
                    checkLVal(init, false, null);
                    checkPatternErrors(refDestructuringErrors, true);
                    return parseForIn(nodeStart, init);
                }
                checkExpressionErrors(refDestructuringErrors, true);
                return parseFor(nodeStart, init);
            }
        }

        [NotNull]
        private BaseNode parseFunctionStatement(Position nodeStart, bool isAsync)
        {
            next();
            return parseFunction(nodeStart, "true", false, isAsync);
        }

        private bool isFunction()
        {
            return type == TokenType._function || isAsyncFunction();
        }

        [NotNull]
        private IfStatementNode parseIfStatement(Position nodeStart)
        {
            next();
            var test = parseParenExpression();
            // allow function declarations in branches, but only in non-strict mode
            var consequent = parseStatement(!strict && isFunction());
            var alternate = eat(TokenType._else) ? parseStatement(!strict && isFunction()) : null;
            return new IfStatementNode(this, nodeStart, lastTokEnd, test, consequent, alternate);
        }

        [NotNull]
        private ReturnStatementNode parseReturnStatement(Position nodeStart)
        {
            if (!inFunction && !Options.allowReturnOutsideFunction)
                raise(start, "'return' outside of function");
            next();

            // In `return` (and `break`/`continue`), the keywords with
            // optional arguments, we eagerly look for a semicolon or the
            // possibility to insert one.

            ExpressionNode argument = null;
            if (!eat(TokenType.semi) && !insertSemicolon())
            {
                argument = ParseExpression();
                semicolon();
            }
            return new ReturnStatementNode(this, nodeStart, lastTokEnd, argument);
        }

        [NotNull]
        private SwitchStatementNode parseSwitchStatement(Position nodeStart)
        {
            next();
            var discriminant = parseParenExpression();
            var cases = new List<SwitchCaseNode>();
            expect(TokenType.braceL);
            labels.Add(switchLabel);
            enterLexicalScope();

            // Statements under must be grouped (by label) in SwitchCase
            // nodes. `cur` is used to keep the node that we are currently
            // adding statements to.

            var startLoc = start;
            List<BaseNode> consequent = null;
            BaseNode test = null;
            for (var sawDefault = false; type != TokenType.braceR;)
            {
                if (type == TokenType._case || type == TokenType._default)
                {
                    var isCase = type == TokenType._case;
                    if (consequent != null)
                    {
                        var current = new SwitchCaseNode(this, startLoc, lastTokEnd, test, consequent);
                        cases.Add(current);
                    }

                    startLoc = start;
                    consequent = new List<BaseNode>();
                    next();
                    if (isCase)
                    {
                        test = ParseExpression();
                    }
                    else
                    {
                        if (sawDefault) raiseRecoverable(lastTokStart, "Multiple default clauses");
                        sawDefault = true;
                        test = null;
                    }
                    expect(TokenType.colon);
                }
                else
                {
                    if (consequent == null)
                    {
                        raise(start, "Unexpected token");
                    }
                    consequent.Add(parseStatement(true));
                }
            }
            exitLexicalScope();
            if (consequent != null)
            {
                var current = new SwitchCaseNode(this, startLoc, lastTokEnd, test, consequent);
                cases.Add(current);
            }
            next(); // Closing brace
            labels.Pop();
            return new SwitchStatementNode(this, nodeStart, lastTokEnd, discriminant, cases);
        }

        [NotNull]
        private ThrowStatementNode parseThrowStatement(Position nodeStart)
        {
            next();
            if (lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index)))
                raise(lastTokEnd, "Illegal newline after throw");
            var argument = ParseExpression();
            semicolon();
            return new ThrowStatementNode(this, nodeStart, lastTokEnd, argument);
        }

        [NotNull]
        private TryStatementNode parseTryStatement(Position nodeStart)
        {
            next();
            var block = parseBlock();
            CatchClauseNode handler = null;
            if (type == TokenType._catch)
            {
                var startLocation = start;
                next();
                expect(TokenType.parenL);
                var param = parseBindingAtom();
                enterLexicalScope();
                checkLVal(param, true, VariableKind.Let);
                expect(TokenType.parenR);
                var body = parseBlock(false);
                exitLexicalScope();
                handler = new CatchClauseNode(this, startLocation, lastTokEnd, param, body);
            }
            var finaliser = eat(TokenType._finally) ? parseBlock() : null;
            if (handler == null && finaliser == null)
                raise(nodeStart, "Missing catch or finally clause");
            return new TryStatementNode(this, nodeStart, lastTokEnd, block, handler, finaliser);
        }

        [NotNull]
        private VariableDeclarationNode parseVarStatement(Position nodeStart, VariableKind kind)
        {
            next();
            var declarations = parseVar(false, kind);
            semicolon();
            return new VariableDeclarationNode(this, nodeStart, lastTokEnd, kind, declarations);
        }

        [NotNull]
        private WhileStatementNode parseWhileStatement(Position nodeStart)
        {
            next();
            var test = parseParenExpression();
            labels.Add(loopLabel);
            var body = parseStatement(false);
            labels.Pop();
            return new WhileStatementNode(this, nodeStart, lastTokEnd, test, body);
        }

        [NotNull]
        private WithStatementNode parseWithStatement(Position nodeStart)
        {
            if (strict) raise(start, "'with' in strict mode");
            next();
            var @object = parseParenExpression();
            var body = parseStatement(false);
            return new WithStatementNode(this, nodeStart, lastTokEnd, @object, body);
        }

        [NotNull]
        private EmptyStatementNode parseEmptyStatement(Position nodeStart)
        {
            next();
            return new EmptyStatementNode(this, nodeStart, lastTokEnd);
        }

        [NotNull]
        private BaseNode parseLabelledStatement(Position nodeStart, string maybeName, IdentifierNode expr)
        {
            foreach (var label in labels)
            {
                if (label.name == maybeName)
                    raise(expr.Location.Start, "Label '" + maybeName + "' is already declared");
            }
            var kind = TokenInformation.Types[type].IsLoop ? "loop" : type == TokenType._switch ? "switch" : null;
            for (var i = labels.Count - 1; i >= 0; i--)
            {
                var label = labels[i];
                if (label.statementStart == nodeStart.Index)
                {
                    label.statementStart = start.Index;
                    label.kind = kind;
                }
                else break;
            }
            labels.Add(new Label {name = maybeName, kind = kind, statementStart = start.Index });
            var body = parseStatement(true);
            if (body is ClassDeclarationNode ||
                body is VariableDeclarationNode variableDeclaration && variableDeclaration.Kind != VariableKind.Var ||
                body is FunctionDeclarationNode functionDeclaration && (strict || functionDeclaration.Generator))
                raiseRecoverable(body.Location.Start, "Invalid labelled declaration");
            labels.Pop();

            return new LabelledStatementNode(this, nodeStart, lastTokEnd, expr, body);
        }

        [NotNull]
        private ExpressionStatementNode parseExpressionStatement(Position nodeStart, ExpressionNode expr)
        {
            semicolon();
            return new ExpressionStatementNode(this, nodeStart, lastTokEnd, expr);
        }

        // Parse a semicolon-enclosed block of statements, handling `"use
        // strict"` declarations when `allowStrict` is true (used for
        // function bodies).
        [NotNull]
        private BlockStatementNode parseBlock(bool createNewLexicalScope = true)
        {
            var startLocation = start;
            var body = new List<BaseNode>();
            expect(TokenType.braceL);
            if (createNewLexicalScope)
            {
                enterLexicalScope();
            }
            while (!eat(TokenType.braceR))
            {
                var stmt = parseStatement(true);
                body.Add(stmt);
            }
            if (createNewLexicalScope)
            {
                exitLexicalScope();
            }
            return new BlockStatementNode(this, startLocation, lastTokEnd, body);
        }

        // Parse a regular `for` loop. The disambiguation code in
        // `parseStatement` will already have parsed the init statement or
        // expression.
        [NotNull]
        private BaseNode parseFor(Position nodeStart, BaseNode init)
        {
            expect(TokenType.semi);
            var test = type == TokenType.semi ? null : ParseExpression();
            expect(TokenType.semi);
            var update = type == TokenType.parenR ? null : ParseExpression();
            expect(TokenType.parenR);
            exitLexicalScope();
            var body = parseStatement(false);
            labels.Pop();
            return new ForStatementNode(this, nodeStart, lastTokEnd, init, test, update, body);
        }

        // Parse a `for`/`in` and `for`/`of` loop, which are almost
        // same from parser's perspective.
        [NotNull]
        private BaseNode parseForIn(Position nodeStart, BaseNode init)
        {
            var isIn = type == TokenType._in;
            next();
            var right = ParseExpression();
            expect(TokenType.parenR);
            exitLexicalScope();
            var body = parseStatement(false);
            labels.Pop();

            if (isIn)
            {
                return new ForInStatementNode(this, nodeStart, lastTokEnd, init, right, body);
            }
            return new ForOfStatementNode(this, nodeStart, lastTokEnd, init, right, body);
        }

        // Parse a list of variable declarations.
        [NotNull]
        private List<VariableDeclaratorNode> parseVar(bool isFor, VariableKind kind)
        {
            var declarations = new List<VariableDeclaratorNode>();
            for (;;)
            {
                var startLocation = start;
                var id = parseVarId(kind);
                ExpressionNode init = null;
                if (eat(TokenType.eq))
                {
                    init = ParseMaybeAssign(isFor);
                }
                else if (kind == VariableKind.Const && !(type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of")))
                {
                    raise(start, "Unexpected token");
                }
                else if (!(id is IdentifierNode) && !(isFor && (type == TokenType._in || isContextual("of"))))
                {
                    raise(lastTokEnd, "Complex binding patterns require an initialization value");
                }
                var decl = new VariableDeclaratorNode(this, startLocation, lastTokEnd, kind, id, init);
                declarations.Add(decl);
                if (!eat(TokenType.comma)) break;
            }
            return declarations;
        }

        [NotNull]
        private BaseNode parseVarId(VariableKind kind)
        {
            var id = parseBindingAtom();
            checkLVal(id, true, kind);
            return id;
        }

        // Parse a function declaration or literal (depending on the
        // `isStatement` parameter).
        [NotNull]
        private BaseNode parseFunction(Position startLoc, [CanBeNull] string isStatement, bool allowExpressionBody = false, bool isAsync = false)
        {
            var generator = false;
            if (Options.ecmaVersion >= 6 && !isAsync)
                generator = eat(TokenType.star);
            if (Options.ecmaVersion < 8 && isAsync)
                throw new InvalidOperationException();

            IdentifierNode id = null;
            if (isStatement != null)
            {
                id = isStatement == "nullableID" && type != TokenType.name ? null : parseIdent();
                if (id != null)
                {
                    checkLVal(id, true, VariableKind.Var);
                }
            }

            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;
            inGenerator = generator;
            inAsync = isAsync;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;
            enterFunctionScope();

            if (isStatement == null)
                id = type == TokenType.name ? parseIdent() : null;

            var parameters = parseFunctionParams();
            var (body, expression) = parseFunctionBody(parameters, startLoc, id, allowExpressionBody);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;

            if (isStatement != null)
            {
                return new FunctionDeclarationNode(this, startLoc, lastTokEnd, expression, isAsync, generator, id, parameters, body);
            }
            return new FunctionExpressionNode(this, startLoc, lastTokEnd, expression, isAsync, generator, id, parameters, body);
        }

        [NotNull]
        private IReadOnlyList<ExpressionNode> parseFunctionParams()
        {
            expect(TokenType.parenL);
            var parameters = parseBindingList(TokenType.parenR, false, Options.ecmaVersion >= 8);
            checkYieldAwaitInDefaultParams();
            return parameters;
        }

        // Parse a class declaration or literal (depending on the
        // `isStatement` parameter).
        [NotNull]
        private BaseNode parseClass(Position nodeStart, [CanBeNull] string isStatement)
        {
            next();

            var id = parseClassId(isStatement);
            var superClass = parseClassSuper();
            var classBodyStart = start;
            var hadConstructor = false;
            var body = new List<BaseNode>();
            expect(TokenType.braceL);
            while (!eat(TokenType.braceR))
            {
                if (eat(TokenType.semi)) continue;
                var methodStart = start;
                var isGenerator = eat(TokenType.star);
                var isAsync = false;
                var isMaybeStatic = type == TokenType.name && (string)value == "static";
                var (computed, key) = parsePropertyName();
                var @static = isMaybeStatic && type != TokenType.parenL;
                if (@static)
                {
                    if (isGenerator)
                    {
                        raise(start, "Unexpected token");
                    }
                    isGenerator = eat(TokenType.star);
                    (computed, key) = parsePropertyName();
                }
                if (Options.ecmaVersion >= 8 && !isGenerator && !computed &&
                    key is IdentifierNode identifierNode && identifierNode.Name == "async" && type != TokenType.parenL &&
                    !canInsertSemicolon())
                {
                    isAsync = true;
                    (computed, key) = parsePropertyName();
                }
                var kind = PropertyKind.Method;
                var isGetSet = false;
                if (!computed)
                {
                    if (!isGenerator && !isAsync && key is IdentifierNode identifierNode2 && type != TokenType.parenL && (identifierNode2.Name == "get" || identifierNode2.Name == "set"))
                    {
                        isGetSet = true;
                        kind = identifierNode2.Name == "get" ? PropertyKind.Get : PropertyKind.Set;
                        (computed, key) = parsePropertyName();
                    }
                    if (!@static && (key is IdentifierNode identifierNode3 && identifierNode3.Name == "constructor" ||
                                            key is LiteralNode literal && literal.Value.ToString() == "constructor"))
                    {
                        if (hadConstructor) raise(key.Location.Start, "Duplicate constructor in the same class");
                        if (isGetSet) raise(key.Location.Start, "Constructor can't have get/set modifier");
                        if (isGenerator) raise(key.Location.Start, "Constructor can't be a generator");
                        if (isAsync) raise(key.Location.Start, "Constructor can't be an async method");
                        kind = PropertyKind.Constructor;
                        hadConstructor = true;
                    }
                }
                var methodValue = parseMethod(isGenerator, isAsync);

                if (isGetSet)
                {
                    var paramCount = kind == PropertyKind.Get ? 0 : 1;
                    if (methodValue.Parameters.Count != paramCount)
                    {
                        var startLocation = methodValue.Location.Start;
                        if (kind == PropertyKind.Get)
                            raiseRecoverable(startLocation, "getter should have no params");
                        else
                            raiseRecoverable(startLocation, "setter should have exactly one param");
                    }
                    else
                    {
                        if (kind == PropertyKind.Set && methodValue.Parameters[0] is RestElementNode)
                            raiseRecoverable(methodValue.Parameters[0].Location.Start, "Setter cannot use rest params");
                    }
                }

                body.Add(new MethodDefinitionNode(this, methodStart, lastTokEnd, kind, computed, @static, key, methodValue));
            }
            var classBody = new ClassBodyNode(this, classBodyStart, lastTokEnd, body);

            if (isStatement != null)
            {
                return new ClassDeclarationNode(this, nodeStart, lastTokEnd, id, superClass, classBody);
            }
            return new ClassExpressionNode(this, nodeStart, lastTokEnd, id, superClass, classBody);
        }

        [CanBeNull]
        private IdentifierNode parseClassId(string isStatement)
        {
            if (type == TokenType.name)
                return parseIdent();
            if (isStatement == "true")
            {
                raise(start, "Unexpected token");
            }
            return null;
        }

        [CanBeNull]
        private ExpressionNode parseClassSuper()
        {
            return eat(TokenType._extends) ? ParseExpressionSubscripts() : null;
        }

        // Parses module export declaration.
        [NotNull]
        private BaseNode parseExport(Position nodeStart, IDictionary<string, bool> exports)
        {
            next();
            // export * from '...'
            if (eat(TokenType.star))
            {
                expectContextual("from");
                ExpressionNode source = null;
                if (type == TokenType.@string)
                {
                    source = ParseExpressionAtom();
                }
                else
                {
                    raise(start, "Unexpected token");
                }
                semicolon();
                return new ExportAllDeclarationNode(this, nodeStart, lastTokEnd, source);
            }
            if (eat(TokenType._default))
            {
                // export default ...
                checkExport(exports, "default", lastTokStart);
                var isAsync = false;
                BaseNode declaration;
                if (type == TokenType._function || (isAsync = isAsyncFunction()))
                {
                    var startLoc = start;
                    next();
                    if (isAsync) next();
                    declaration = parseFunction(startLoc, "nullableID", false, isAsync);
                }
                else if (type == TokenType._class)
                {
                    declaration = parseClass(start, "nullableID");
                }
                else
                {
                    declaration = ParseMaybeAssign();
                    semicolon();
                }
                return new ExportDefaultDeclarationNode(this, nodeStart, lastTokEnd, declaration);
            }
            else
            {
                // export var|const|let|function|class ...
                BaseNode declaration;
                IReadOnlyList<ExportSpecifierNode> specifiers;
                ExpressionNode source = null;
                if (shouldParseExportStatement())
                {
                    declaration = parseStatement(true);
                    if (declaration is VariableDeclarationNode variableDeclaration)
                    {
                        checkVariableExport(exports, variableDeclaration.Declarations);
                    }
                    else
                    {
                        var declarationNode = (IDeclarationNode)declaration;
                        checkExport(exports, declarationNode.Id.Name, declarationNode.Id.Location.Start);
                    }
                    specifiers = Array.Empty<ExportSpecifierNode>();
                }
                else
                {
                    // export { x, y as z } [from '...']
                    declaration = null;
                    specifiers = parseExportSpecifiers(exports);
                    if (eatContextual("from"))
                    {
                        if (type == TokenType.@string)
                        {
                            source = ParseExpressionAtom();
                        }
                        else
                        {
                            raise(start, "Unexpected token");
                        }
                    }
                    else
                    {
                        // check for keywords used as local names
                        foreach (var spec in specifiers)
                        {
                            checkUnreserved(spec.Local.Location.Start, spec.Local.Location.End, spec.Local.Name);
                        }
                    }
                    semicolon();
                }
                return new ExportNamedDeclarationNode(this, nodeStart, lastTokEnd, source, declaration, specifiers);
            }
        }

        private static void checkExport([CanBeNull] IDictionary<string, bool> exports, string name, Position pos)
        {
            if (exports == null) return;
            if (exports.ContainsKey(name))
                raiseRecoverable(pos, "Duplicate export '" + name + "'");
            exports[name] = true;
        }

        private static void checkPatternExport(IDictionary<string, bool> exports, BaseNode pattern)
        {
            switch (pattern)
            {
                case IdentifierNode identifierNode:
                    checkExport(exports, identifierNode.Name, pattern.Location.Start);
                    break;
                case ObjectPatternNode objectPattern:
                    foreach (var prop in objectPattern.Properties)
                    {
                        checkPatternExport(exports, prop.Value);
                    }
                    break;
                case ArrayPatternNode arrayPattern:
                    foreach (var elt in arrayPattern.Elements)
                    {
                        if (elt != null) checkPatternExport(exports, elt);
                    }
                    break;
                case AssignmentPatternNode assignmentPattern:
                    checkPatternExport(exports, assignmentPattern.Left);
                    break;
                case ParenthesisedExpressionNode parenthesisedExpressionNode:
                    checkPatternExport(exports, parenthesisedExpressionNode.Expression);
                    break;
            }
        }

        private static void checkVariableExport([CanBeNull] IDictionary<string, bool> exports, IEnumerable<BaseNode> decls)
        {
            if (exports == null)
                return;
            foreach (var decl in decls)
            {
                checkPatternExport(exports, ((IDeclarationNode)decl).Id);
            }
        }

        private bool shouldParseExportStatement()
        {
            return TokenInformation.Types[type].Keyword == "var" ||
                   TokenInformation.Types[type].Keyword == "const" ||
                   TokenInformation.Types[type].Keyword == "class" ||
                   TokenInformation.Types[type].Keyword == "function" ||
                   isLet() ||
                   isAsyncFunction();
        }

        // Parses a comma-separated list of module exports.
        [NotNull]
        private IReadOnlyList<ExportSpecifierNode> parseExportSpecifiers(IDictionary<string, bool> exports)
        {
            var nodes = new List<ExportSpecifierNode>();
            var first = true;
            // export { x, y as z } [from '...']
            expect(TokenType.braceL);
            while (!eat(TokenType.braceR))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (afterTrailingComma(TokenType.braceR)) break;
                }
                else first = false;

                var startLoc = start;
                var local = parseIdent(true);
                var exported = eatContextual("as") ? parseIdent(true) : local;
                checkExport(exports, exported.Name, exported.Location.Start);
                nodes.Add(new ExportSpecifierNode(this, startLoc, lastTokEnd, local, exported));
            }
            return nodes;
        }

        // Parses import declaration.
        [NotNull]
        private ImportDeclarationNode parseImport(Position nodeStart)
        {
            next();
            // import '...'
            IReadOnlyList<BaseNode> specifiers;
            ExpressionNode source = null;
            if (type == TokenType.@string)
            {
                specifiers = new List<BaseNode>();
                source = ParseExpressionAtom();
            }
            else
            {
                specifiers = parseImportSpecifiers();
                expectContextual("from");
                if (type == TokenType.@string)
                {
                    source = ParseExpressionAtom();
                }
                else
                {
                    raise(start, "Unexpected token");
                }
            }
            semicolon();
            return new ImportDeclarationNode(this, nodeStart, lastTokEnd, source, specifiers);
        }

        // Parses a comma-separated list of module imports.
        [NotNull]
        private IReadOnlyList<BaseNode> parseImportSpecifiers()
        {
            var nodes = new List<BaseNode>();
            var first = true;
            if (type == TokenType.name)
            {
                // import defaultObj, { x, y as z } from '...'
                var startLoc = start;
                var local = parseIdent();
                checkLVal(local, true, VariableKind.Let);
                nodes.Add(new ImportDefaultSpecifierNode(this, startLoc, lastTokEnd, local));
                if (!eat(TokenType.comma))
                    return nodes;
            }
            if (type == TokenType.star)
            {
                var startLoc = start;
                next();
                expectContextual("as");
                var local = parseIdent();
                checkLVal(local, true, VariableKind.Let);
                nodes.Add(new ImportNamespaceSpecifierNode(this, startLoc, lastTokEnd, local));
                return nodes;
            }
            expect(TokenType.braceL);
            while (!eat(TokenType.braceR))
            {
                if (!first)
                {
                    expect(TokenType.comma);
                    if (afterTrailingComma(TokenType.braceR)) break;
                }
                else first = false;

                var startLoc = start;
                var imported = parseIdent(true);
                IdentifierNode local;
                if (eatContextual("as"))
                {
                    local = parseIdent();
                }
                else
                {
                    checkUnreserved(imported.Location.Start, imported.Location.End, imported.Name);
                    local = imported;
                }
                checkLVal(local, true, VariableKind.Let);
                nodes.Add(new ImportSpecifierNode(this, startLoc, lastTokEnd, local, imported));
            }
            return nodes;
        }

        // Set `ExpressionStatement#directive` property for directive prologues.
        private void adaptDirectivePrologue([NotNull] IReadOnlyList<BaseNode> statements)
        {
            for (var i = 0; i < statements.Count && isDirectiveCandidate(statements[i]); ++i)
            {
                var expressionStatement = (ExpressionStatementNode)statements[i];
                var literal = (LiteralNode)expressionStatement.Expression;
                expressionStatement.Directive = literal.Raw.Substring(1, literal.Raw.Length - 2);
            }
        }

        private bool isDirectiveCandidate([NotNull] BaseNode statement)
        {
            return statement is ExpressionStatementNode expressionStatementNode &&
                   expressionStatementNode.Expression is LiteralNode literal &&
                   literal.Value.IsString &&
                   // Reject parenthesized strings.
                   (input[statement.Location.Start.Index] == '\"' || input[statement.Location.Start.Index] == '\'');
        }
    }
}
