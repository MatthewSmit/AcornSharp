using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    public sealed partial class Parser
    {
        private static readonly Label loopLabel = new Label {kind = "loop"};
        private static readonly Label switchLabel = new Label {kind = "switch"};

        // Parse a program. Initializes the parser, reads any number of
        // statements, and wraps them in a Program node.  Optionally takes a
        // `program` argument.  If present, the statements will be appended
        // to its body instead of creating a new node.
        [NotNull]
        private BaseNode parseTopLevel([NotNull] BaseNode node)
        {
            var exports = new Dictionary<string, bool>();
            if (node.body == null) node.body = new List<BaseNode>();
            while (type != TokenType.EOF)
            {
                var stmt = parseStatement(true, true, exports);
                node.body.Add(stmt);
            }
            adaptDirectivePrologue(node.body);
            next();
            if (Options.ecmaVersion >= 6)
            {
                node.sourceType = Options.sourceType;
            }
            node.type = NodeType.Program;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
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
                while (isIdentifierChar(input.Get(pos), true)) ++pos;
                var ident = input.Substring(next, pos - next);
                if (!keywords.IsMatch(ident)) return true;
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
        private BaseNode parseStatement(bool declaration, bool topLevel = false, [CanBeNull] IDictionary<string, bool> exports = null)
        {
            var starttype = type;
            var start = this.start;
            string kind = null;

            if (isLet())
            {
                starttype = TokenType._var;
                kind = "let";
            }

            // Most types of statements are recognized by the keyword they
            // start with. Many are trivial to parse, some require a bit of
            // complexity.

            switch (starttype)
            {
                case TokenType._break:
                case TokenType._continue:
                    return parseBreakContinueStatement(start, TokenInformation.Types[starttype].Keyword);
                case TokenType._debugger:
                    return parseDebuggerStatement(start);
                case TokenType._do:
                    return parseDoStatement(start);
                case TokenType._for:
                    return parseForStatement(start);
                case TokenType._function:
                    if (!declaration && Options.ecmaVersion >= 6)
                    {
                        raise(start, "Unexpected token");
                    }
                    return parseFunctionStatement(start, false);
                case TokenType._class:
                    if (!declaration)
                    {
                        raise(start, "Unexpected token");
                    }
                    return parseClass(start, "true");
                case TokenType._if:
                    return parseIfStatement(start);
                case TokenType._return:
                    return parseReturnStatement(start);
                case TokenType._switch:
                    return parseSwitchStatement(start);
                case TokenType._throw:
                    return parseThrowStatement(start);
                case TokenType._try:
                    return parseTryStatement(start);
                case TokenType._const:
                case TokenType._var:
                    kind = kind ?? (string)value;
                    if (!declaration && kind != "var")
                    {
                        raise(start, "Unexpected token");
                    }
                    return parseVarStatement(start, kind);
                case TokenType._while:
                    return parseWhileStatement(start);
                case TokenType._with:
                    return parseWithStatement(start);
                case TokenType.braceL:
                    return parseBlock();
                case TokenType.semi:
                    return parseEmptyStatement(start);
                case TokenType._export:
                case TokenType._import:
                    if (!Options.allowImportExportEverywhere)
                    {
                        if (!topLevel)
                            raise(start, "'import' and 'export' may only appear at the top level");
                        if (!inModule)
                            raise(start, "'import' and 'export' may appear only with 'sourceType: module'");
                    }
                    return starttype == TokenType._import ? parseImport(start) : parseExport(start, exports);
            }

            if (isAsyncFunction() && declaration)
            {
                next();
                return parseFunctionStatement(start, true);
            }

            var maybeName = value;
            var expr = parseExpression();
            if (starttype == TokenType.name && expr is IdentifierNode identifierNode && eat(TokenType.colon))
                return parseLabeledStatement(start, (string)maybeName, identifierNode);
            return parseExpressionStatement(start, expr);
        }

        [NotNull]
        private BaseNode parseBreakContinueStatement(Position nodeStart, string keyword)
        {
            var node = new BaseNode(this, nodeStart);
            var isBreak = keyword == "break";
            next();
            if (eat(TokenType.semi) || insertSemicolon()) node.label = null;
            else if (type != TokenType.name)
            {
                raise(start, "Unexpected token");
            }
            else
            {
                node.label = parseIdent();
                semicolon();
            }

            // Verify that there is an actual destination to break or
            // continue to.
            var i = 0;
            for (; i < labels.Count; ++i)
            {
                var lab = labels[i];
                if (node.label == null || lab.name == node.label.name)
                {
                    if (lab.kind != null && (isBreak || lab.kind == "loop")) break;
                    if (node.label != null && isBreak) break;
                }
            }
            if (i == labels.Count) raise(node.loc.Start, "Unsyntactic " + keyword);
            node.type = isBreak ? NodeType.BreakStatement : NodeType.ContinueStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseDebuggerStatement(Position nodeStart)
        {
            next();
            semicolon();
            var node = new BaseNode(this, nodeStart);
            node.type = NodeType.DebuggerStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseDoStatement(Position nodeStart)
        {
            next();
            labels.Add(loopLabel);
            var node = new BaseNode(this, nodeStart);
            node.fbody = parseStatement(false);
            labels.Pop();
            expect(TokenType._while);
            node.test = parseParenExpression();
            if (Options.ecmaVersion >= 6)
                eat(TokenType.semi);
            else
                semicolon();
            node.type = NodeType.DoWhileStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Disambiguating between a `for` and a `for`/`in` or `for`/`of`
        // loop is non-trivial. Basically, we have to parse the init `var`
        // statement or expression, disallowing the `in` operator (see
        // the second parameter to `parseExpression`), and then check
        // whether the next token is `in` or `of`. When there is no init
        // part (semicolon immediately after the opening parenthesis), it
        // is a regular `for` loop.
        private BaseNode parseForStatement(Position nodeStart)
        {
            next();
            labels.Add(loopLabel);
            enterLexicalScope();
            expect(TokenType.parenL);
            var node = new BaseNode(this, nodeStart);
            if (type == TokenType.semi) return parseFor(node, null);
            var isLet = this.isLet();
            BaseNode init;
            if (type == TokenType._var || type == TokenType._const || isLet)
            {
                init = new BaseNode(this, start);
                var kind = isLet ? "let" : (string)value;
                next();
                parseVar(init, true, kind);
                init.type = NodeType.VariableDeclaration;
                init.loc = new SourceLocation(init.loc.Start, lastTokEnd, init.loc.Source);
                if ((type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of")) && init.declarations.Count == 1 &&
                    !(kind != "var" && init.declarations[0].init != null))
                    return parseForIn(node, init);
                return parseFor(node, init);
            }
            var refDestructuringErrors = new DestructuringErrors();
            init = parseExpression(true, refDestructuringErrors);
            if (type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of"))
            {
                toAssignable(init);
                checkLVal(init);
                checkPatternErrors(refDestructuringErrors, true);
                return parseForIn(node, init);
            }
            checkExpressionErrors(refDestructuringErrors, true);
            return parseFor(node, init);
        }

        private BaseNode parseFunctionStatement(Position nodeStart, bool isAsync)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            return parseFunction(node, "true", false, isAsync);
        }

        private bool isFunction()
        {
            return type == TokenType._function || isAsyncFunction();
        }

        [NotNull]
        private BaseNode parseIfStatement(Position nodeStart)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            node.test = parseParenExpression();
            // allow function declarations in branches, but only in non-strict mode
            node.consequent = parseStatement(!strict && isFunction());
            node.alternate = eat(TokenType._else) ? parseStatement(!strict && isFunction()) : null;
            node.type = NodeType.IfStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseReturnStatement(Position nodeStart)
        {
            if (!inFunction && !Options.allowReturnOutsideFunction)
                raise(start, "'return' outside of function");
            next();

            // In `return` (and `break`/`continue`), the keywords with
            // optional arguments, we eagerly look for a semicolon or the
            // possibility to insert one.

            var node = new BaseNode(this, nodeStart);
            if (eat(TokenType.semi) || insertSemicolon()) node.argument = null;
            else
            {
                node.argument = parseExpression();
                semicolon();
            }
            node.type = NodeType.ReturnStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseSwitchStatement(Position nodeStart)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            node.discriminant = parseParenExpression();
            node.cases = new List<BaseNode>();
            expect(TokenType.braceL);
            labels.Add(switchLabel);
            enterLexicalScope();

            // Statements under must be grouped (by label) in SwitchCase
            // nodes. `cur` is used to keep the node that we are currently
            // adding statements to.

            BaseNode cur = null;
            for (var sawDefault = false; type != TokenType.braceR;)
            {
                if (type == TokenType._case || type == TokenType._default)
                {
                    var isCase = type == TokenType._case;
                    if (cur != null)
                    {
                        cur.type = NodeType.SwitchCase;
                        cur.loc = new SourceLocation(cur.loc.Start, lastTokEnd, cur.loc.Source);
                    }
                    node.cases.Add(cur = new BaseNode(this, start));
                    cur.sconsequent = new List<BaseNode>();
                    next();
                    if (isCase)
                    {
                        cur.test = parseExpression();
                    }
                    else
                    {
                        if (sawDefault) raiseRecoverable(lastTokStart, "Multiple default clauses");
                        sawDefault = true;
                        cur.test = null;
                    }
                    expect(TokenType.colon);
                }
                else
                {
                    if (cur == null)
                    {
                        raise(start, "Unexpected token");
                    }
                    cur.sconsequent.Add(parseStatement(true));
                }
            }
            exitLexicalScope();
            if (cur != null)
            {
                cur.type = NodeType.SwitchCase;
                cur.loc = new SourceLocation(cur.loc.Start, lastTokEnd, cur.loc.Source);
            }
            next(); // Closing brace
            labels.Pop();
            node.type = NodeType.SwitchStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseThrowStatement(Position nodeStart)
        {
            next();
            if (lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index)))
                raise(lastTokEnd, "Illegal newline after throw");
            var node = new BaseNode(this, nodeStart);
            node.argument = parseExpression();
            semicolon();
            node.type = NodeType.ThrowStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private TryStatementNode parseTryStatement(Position nodeStart)
        {
            next();
            var block = parseBlock();
            BaseNode handler = null;
            if (type == TokenType._catch)
            {
                var start = this.start;
                next();
                expect(TokenType.parenL);
                var param = parseBindingAtom();
                enterLexicalScope();
                checkLVal(param, "let");
                expect(TokenType.parenR);
                var body = parseBlock(false);
                exitLexicalScope();
                var clause = new BaseNode(this, start);
                clause.param = param;
                clause.fbody = body;
                clause.type = NodeType.CatchClause;
                clause.loc = new SourceLocation(clause.loc.Start, lastTokEnd, clause.loc.Source);
                handler = clause;
            }
            var finaliser = eat(TokenType._finally) ? parseBlock() : null;
            if (handler == null && finaliser == null)
                raise(nodeStart, "Missing catch or finally clause");
            return new TryStatementNode(this, nodeStart, lastTokEnd, block, handler, finaliser);
        }

        [NotNull]
        private BaseNode parseVarStatement(Position nodeStart, string kind)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            parseVar(node, false, kind);
            semicolon();
            node.type = NodeType.VariableDeclaration;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseWhileStatement(Position nodeStart)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            node.test = parseParenExpression();
            labels.Add(loopLabel);
            node.fbody = parseStatement(false);
            labels.Pop();
            node.type = NodeType.WhileStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseWithStatement(Position nodeStart)
        {
            if (strict) raise(start, "'with' in strict mode");
            next();
            var node = new BaseNode(this, nodeStart);
            node.@object = parseParenExpression();
            node.fbody = parseStatement(false);
            node.type = NodeType.WithStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseEmptyStatement(Position nodeStart)
        {
            next();
            var node = new BaseNode(this, nodeStart);
            node.type = NodeType.EmptyStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseLabeledStatement(Position nodeStart, string maybeName, IdentifierNode expr)
        {
            foreach (var label in labels)
            {
                if (label.name == maybeName)
                    raise(expr.loc.Start, "Label '" + maybeName + "' is already declared");
            }
            var kind = TokenInformation.Types[type].IsLoop ? "loop" : type == TokenType._switch ? "switch" : null;
            var node = new BaseNode(this, nodeStart);
            for (var i = labels.Count - 1; i >= 0; i--)
            {
                var label = labels[i];
                if (label.statementStart == node.loc.Start.Index)
                {
                    label.statementStart = start.Index;
                    label.kind = kind;
                }
                else break;
            }
            labels.Add(new Label {name = maybeName, kind = kind, statementStart = start.Index });
            node.fbody = parseStatement(true);
            if (node.fbody.type == NodeType.ClassDeclaration ||
                node.fbody.type == NodeType.VariableDeclaration && node.fbody.kind != "var" ||
                node.fbody.type == NodeType.FunctionDeclaration && (strict || node.fbody.generator))
                raiseRecoverable(node.fbody.loc.Start, "Invalid labelled declaration");
            labels.Pop();
            node.label = expr;
            node.type = NodeType.LabeledStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseExpressionStatement(Position nodeStart, BaseNode expr)
        {
            var node = new BaseNode(this, nodeStart);
            node.expression = expr;
            semicolon();
            node.type = NodeType.ExpressionStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parse a semicolon-enclosed block of statements, handling `"use
        // strict"` declarations when `allowStrict` is true (used for
        // function bodies).
        [NotNull]
        private BaseNode parseBlock(bool createNewLexicalScope = true)
        {
            var start = this.start;
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
            var node = new BaseNode(this, start);
            node.body = body;
            node.type = NodeType.BlockStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parse a regular `for` loop. The disambiguation code in
        // `parseStatement` will already have parsed the init statement or
        // expression.
        [NotNull]
        private BaseNode parseFor([NotNull] BaseNode node, BaseNode init)
        {
            node.init = init;
            expect(TokenType.semi);
            node.test = type == TokenType.semi ? null : parseExpression();
            expect(TokenType.semi);
            node.update = type == TokenType.parenR ? null : parseExpression();
            expect(TokenType.parenR);
            exitLexicalScope();
            node.fbody = parseStatement(false);
            labels.Pop();
            node.type = NodeType.ForStatement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parse a `for`/`in` and `for`/`of` loop, which are almost
        // same from parser's perspective.
        [NotNull]
        private BaseNode parseForIn([NotNull] BaseNode node, BaseNode init)
        {
            var type = this.type == TokenType._in ? NodeType.ForInStatement : NodeType.ForOfStatement;
            next();
            node.left = init;
            node.right = parseExpression();
            expect(TokenType.parenR);
            exitLexicalScope();
            node.fbody = parseStatement(false);
            labels.Pop();
            node.type = type;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parse a list of variable declarations.
        private BaseNode parseVar([NotNull] BaseNode node, bool isFor, string kind)
        {
            node.declarations = new List<BaseNode>();
            node.kind = kind;
            for (;;)
            {
                var start = this.start;
                var decl = new BaseNode(this, start);
                parseVarId(decl, kind);
                if (eat(TokenType.eq))
                {
                    decl.init = parseMaybeAssign(isFor);
                }
                else if (kind == "const" && !(type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of")))
                {
                    raise(this.start, "Unexpected token");
                }
                else if (!(decl.id is IdentifierNode) && !(isFor && (type == TokenType._in || isContextual("of"))))
                {
                    raise(lastTokEnd, "Complex binding patterns require an initialization value");
                }
                else
                {
                    decl.init = null;
                }
                decl.type = NodeType.VariableDeclarator;
                decl.loc = new SourceLocation(decl.loc.Start, lastTokEnd, decl.loc.Source);
                node.declarations.Add(decl);
                if (!eat(TokenType.comma)) break;
            }
            return node;
        }

        private void parseVarId([NotNull] BaseNode decl, string kind)
        {
            decl.id = parseBindingAtom();
            checkLVal(decl.id, kind);
        }

        // Parse a function declaration or literal (depending on the
        // `isStatement` parameter).
        [NotNull]
        private BaseNode parseFunction([NotNull] BaseNode node, string isStatement, bool allowExpressionBody = false, bool isAsync = false)
        {
            initFunction(node);
            if (Options.ecmaVersion >= 6 && !isAsync)
                node.generator = eat(TokenType.star);
            if (Options.ecmaVersion >= 8)
                node.async = isAsync;

            if (isStatement != null)
            {
                node.id = isStatement == "nullableID" && type != TokenType.name ? null : parseIdent();
                if (node.id != null)
                {
                    checkLVal(node.id, "var");
                }
            }

            var oldInGen = inGenerator;
            var oldInAsync = inAsync;
            var oldYieldPos = yieldPos;
            var oldAwaitPos = awaitPos;
            var oldInFunc = inFunction;
            inGenerator = node.generator;
            inAsync = node.async;
            yieldPos = default;
            awaitPos = default;
            inFunction = true;
            enterFunctionScope();

            if (isStatement == null)
                node.id = type == TokenType.name ? parseIdent() : null;

            parseFunctionParams(node);
            parseFunctionBody(node, allowExpressionBody);

            inGenerator = oldInGen;
            inAsync = oldInAsync;
            yieldPos = oldYieldPos;
            awaitPos = oldAwaitPos;
            inFunction = oldInFunc;
            node.type = isStatement != null ? NodeType.FunctionDeclaration : NodeType.FunctionExpression;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        private void parseFunctionParams(BaseNode node)
        {
            expect(TokenType.parenL);
            node.@params = parseBindingList(TokenType.parenR, false, Options.ecmaVersion >= 8);
            checkYieldAwaitInDefaultParams();
        }

        // Parse a class declaration or literal (depending on the
        // `isStatement` parameter).
        private BaseNode parseClass(Position nodeStart, string isStatement)
        {
            next();

            var node = new BaseNode(this, nodeStart);
            parseClassId(node, isStatement);
            parseClassSuper(node);
            var classBody = new BaseNode(this, start);
            var hadConstructor = false;
            classBody.body = new List<BaseNode>();
            expect(TokenType.braceL);
            while (!eat(TokenType.braceR))
            {
                if (eat(TokenType.semi)) continue;
                var method = new BaseNode(this, start);
                var isGenerator = eat(TokenType.star);
                var isAsync = false;
                var isMaybeStatic = type == TokenType.name && (string)value == "static";
                parsePropertyName(method);
                method.@static = isMaybeStatic && type != TokenType.parenL;
                if (method.@static)
                {
                    if (isGenerator)
                    {
                        raise(start, "Unexpected token");
                    }
                    isGenerator = eat(TokenType.star);
                    parsePropertyName(method);
                }
                if (Options.ecmaVersion >= 8 && !isGenerator && !method.computed &&
                    method.key is IdentifierNode identifierNode && identifierNode.name == "async" && type != TokenType.parenL &&
                    !canInsertSemicolon())
                {
                    isAsync = true;
                    parsePropertyName(method);
                }
                method.kind = "method";
                var isGetSet = false;
                if (!method.computed)
                {
                    var key = method.key;
                    if (!isGenerator && !isAsync && key is IdentifierNode identifierNode2 && type != TokenType.parenL && (identifierNode2.name == "get" || identifierNode2.name == "set"))
                    {
                        isGetSet = true;
                        method.kind = identifierNode2.name;
                        key = parsePropertyName(method);
                    }
                    if (!method.@static && (key is IdentifierNode identifierNode3 && identifierNode3.name == "constructor" ||
                                            key.type == NodeType.Literal && (string)key.value == "constructor"))
                    {
                        if (hadConstructor) raise(key.loc.Start, "Duplicate constructor in the same class");
                        if (isGetSet) raise(key.loc.Start, "Constructor can't have get/set modifier");
                        if (isGenerator) raise(key.loc.Start, "Constructor can't be a generator");
                        if (isAsync) raise(key.loc.Start, "Constructor can't be an async method");
                        method.kind = "constructor";
                        hadConstructor = true;
                    }
                }
                parseClassMethod(classBody, method, isGenerator, isAsync);
                if (isGetSet)
                {
                    var paramCount = method.kind == "get" ? 0 : 1;
                    var value = (BaseNode)method.value;
                    if (value.@params.Count != paramCount)
                    {
                        var start = value.loc.Start;
                        if (method.kind == "get")
                            raiseRecoverable(start, "getter should have no params");
                        else
                            raiseRecoverable(start, "setter should have exactly one param");
                    }
                    else
                    {
                        if (method.kind == "set" && value.@params[0].type == NodeType.RestElement)
                            raiseRecoverable(value.@params[0].loc.Start, "Setter cannot use rest params");
                    }
                }
            }
            classBody.type = NodeType.ClassBody;
            classBody.loc = new SourceLocation(classBody.loc.Start, lastTokEnd, classBody.loc.Source);
            node.fbody = classBody;
            node.type = isStatement != null ? NodeType.ClassDeclaration : NodeType.ClassExpression;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        private void parseClassMethod(BaseNode classBody, BaseNode method, bool isGenerator, bool isAsync)
        {
            method.value = parseMethod(isGenerator, isAsync);
            method.type = NodeType.MethodDefinition;
            method.loc = new SourceLocation(method.loc.Start, lastTokEnd, method.loc.Source);
            classBody.body.Add(method);
        }

        private void parseClassId(BaseNode node, string isStatement)
        {
            if (type == TokenType.name)
                node.id = parseIdent();
            else
            {
                if (isStatement == "true")
                {
                    raise(start, "Unexpected token");
                }
                else node.id = null;
            }
        }

        private void parseClassSuper(BaseNode node)
        {
            node.superClass = eat(TokenType._extends) ? parseExprSubscripts() : null;
        }

        // Parses module export declaration.
        private BaseNode parseExport(Position nodeStart, IDictionary<string, bool> exports)
        {
            var node = new BaseNode(this, nodeStart);
            next();
            // export * from '...'
            if (eat(TokenType.star))
            {
                expectContextual("from");
                if (type == TokenType.@string) node.source = parseExprAtom();
                else
                {
                    raise(start, "Unexpected token");
                }
                semicolon();
                node.type = NodeType.ExportAllDeclaration;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                return node;
            }
            if (eat(TokenType._default))
            {
                // export default ...
                checkExport(exports, "default", lastTokStart);
                var isAsync = false;
                if (type == TokenType._function || (isAsync = isAsyncFunction()))
                {
                    var fNode = new BaseNode(this, start);
                    next();
                    if (isAsync) next();
                    node.declaration = parseFunction(fNode, "nullableID", false, isAsync);
                }
                else if (type == TokenType._class)
                {
                    node.declaration = parseClass(start, "nullableID");
                }
                else
                {
                    node.declaration = parseMaybeAssign();
                    semicolon();
                }
                node.type = NodeType.ExportDefaultDeclaration;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                return node;
            }
            // export var|const|let|function|class ...
            if (shouldParseExportStatement())
            {
                node.declaration = parseStatement(true);
                if (node.declaration.type == NodeType.VariableDeclaration)
                    checkVariableExport(exports, node.declaration.declarations);
                else
                    checkExport(exports, ((IdentifierNode)node.declaration.id).name, node.declaration.id.loc.Start);
                node.specifiers = new List<BaseNode>();
                node.source = null;
            }
            else
            {
                // export { x, y as z } [from '...']
                node.declaration = null;
                node.specifiers = parseExportSpecifiers(exports);
                if (eatContextual("from"))
                {
                    if (type == TokenType.@string) node.source = parseExprAtom();
                    else
                    {
                        raise(start, "Unexpected token");
                    }
                }
                else
                {
                    // check for keywords used as local names
                    foreach (var spec in node.specifiers)
                    {
                        checkUnreserved(spec.local.loc.Start, spec.local.loc.End, spec.local.name);
                    }

                    node.source = null;
                }
                semicolon();
            }
            node.type = NodeType.ExportNamedDeclaration;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        private static void checkExport(IDictionary<string, bool> exports, string name, Position pos)
        {
            if (exports == null) return;
            if (exports.ContainsKey(name))
                raiseRecoverable(pos, "Duplicate export '" + name + "'");
            exports[name] = true;
        }

        private static void checkPatternExport(IDictionary<string, bool> exports, BaseNode pat)
        {
            switch (pat)
            {
                case IdentifierNode identifierNode:
                    checkExport(exports, identifierNode.name, pat.loc.Start);
                    break;
                default:
                    if (pat.type == NodeType.ObjectPattern)
                    {
                        foreach (var prop in pat.properties)
                            checkPatternExport(exports, (BaseNode)prop.value);
                    }
                    else if (pat.type == NodeType.ArrayPattern)
                    {
                        foreach (var elt in pat.elements)
                        {
                            if (elt != null) checkPatternExport(exports, elt);
                        }
                    }
                    else if (pat.type == NodeType.AssignmentPattern)
                    {
                        checkPatternExport(exports, pat.left);
                    }
                    else if (pat.type == NodeType.ParenthesizedExpression)
                    {
                        checkPatternExport(exports, pat.expression);
                    }
                    break;
            }
        }

        private static void checkVariableExport(IDictionary<string, bool> exports, IEnumerable<BaseNode> decls)
        {
            if (exports == null) return;
            foreach (var decl in decls)
                checkPatternExport(exports, decl.id);
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
        private IList<BaseNode> parseExportSpecifiers(IDictionary<string, bool> exports)
        {
            var nodes = new List<BaseNode>();
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

                var node = new BaseNode(this, start);
                node.local = parseIdent(true);
                node.exported = eatContextual("as") ? parseIdent(true) : node.local;
                checkExport(exports, node.exported.name, node.exported.loc.Start);
                node.type = NodeType.ExportSpecifier;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                nodes.Add(node);
            }
            return nodes;
        }

        // Parses import declaration.
        private BaseNode parseImport(Position nodeStart)
        {
            var node = new BaseNode(this, nodeStart);
            next();
            // import '...'
            if (type == TokenType.@string)
            {
                node.specifiers = new List<BaseNode>();
                node.source = parseExprAtom();
            }
            else
            {
                node.specifiers = parseImportSpecifiers();
                expectContextual("from");
                if (type == TokenType.@string) node.source = parseExprAtom();
                else
                {
                    raise(start, "Unexpected token");
                }
            }
            semicolon();
            node.type = NodeType.ImportDeclaration;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parses a comma-separated list of module imports.
        private IList<BaseNode> parseImportSpecifiers()
        {
            var nodes = new List<BaseNode>();
            var first = true;
            if (type == TokenType.name)
            {
                // import defaultObj, { x, y as z } from '...'
                var node = new BaseNode(this, start);
                node.local = parseIdent();
                checkLVal(node.local, "let");
                node.type = NodeType.ImportDefaultSpecifier;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                nodes.Add(node);
                if (!eat(TokenType.comma)) return nodes;
            }
            if (type == TokenType.star)
            {
                var node = new BaseNode(this, start);
                next();
                expectContextual("as");
                node.local = parseIdent();
                checkLVal(node.local, "let");
                node.type = NodeType.ImportNamespaceSpecifier;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                nodes.Add(node);
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

                var node = new BaseNode(this, start);
                node.imported = parseIdent(true);
                if (eatContextual("as"))
                {
                    node.local = parseIdent();
                }
                else
                {
                    checkUnreserved(node.imported.loc.Start, node.imported.loc.End, node.imported.name);
                    node.local = node.imported;
                }
                checkLVal(node.local, "let");
                node.type = NodeType.ImportSpecifier;
                node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                nodes.Add(node);
            }
            return nodes;
        }

        // Set `ExpressionStatement#directive` property for directive prologues.
        private void adaptDirectivePrologue([NotNull] IList<BaseNode> statements)
        {
            for (var i = 0; i < statements.Count && isDirectiveCandidate(statements[i]); ++i)
            {
                statements[i].directive = statements[i].expression.raw.Substring(1, statements[i].expression.raw.Length - 2);
            }
        }

        private bool isDirectiveCandidate([NotNull] BaseNode statement)
        {
            return statement.type == NodeType.ExpressionStatement &&
                   statement.expression.type == NodeType.Literal &&
                   statement.expression.value is string &&
                   // Reject parenthesized strings.
                   (input[statement.loc.Start.Index] == '\"' || input[statement.loc.Start.Index] == '\'');
        }
    }
}
