using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

        private Node parseTopLevel(Node node)
        {
            var exports = new Dictionary<string, bool>();
            if (node.body == null) node.body = new List<Node>();
            while (type != TokenType.eof)
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
            return finishNode(node, NodeType.Program);
        }

        private bool isLet()
        {
            if (type != TokenType.name || Options.ecmaVersion < 6 || (string)value != "let") return false;
            var skip = skipWhiteSpace.Match(input, pos);
            var next = this.pos + skip.Groups[0].Length;
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

            var skip = skipWhiteSpace.Match(input, pos);
            var next = pos + skip.Groups[0].Length;
            return !lineBreak.IsMatch(input.Substring(pos, next - pos)) &&
                   input.Length >= next + 8 && input.Substring(next, 8) == "function" &&
                   (next + 8 == input.Length || !isIdentifierChar(input.Get(next + 8)));
        }

        // Parse a single statement.
        //
        // If expecting a statement and finding a slash operator, parse a
        // regular expression literal. This is to handle cases like
        // `if (foo) /blah/.exec(foo)`, where looking at the previous token
        // does not help.
        private Node parseStatement(bool declaration, bool topLevel = false, IDictionary<string, bool> exports = null)
        {
            var starttype = type;
            var node = startNode();
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
                    return parseBreakContinueStatement(node, TokenInformation.Types[starttype].Keyword);
                case TokenType._debugger:
                    return parseDebuggerStatement(node);
                case TokenType._do:
                    return parseDoStatement(node);
                case TokenType._for:
                    return parseForStatement(node);
                case TokenType._function:
                    if (!declaration && Options.ecmaVersion >= 6) unexpected();
                    return parseFunctionStatement(node, false);
                case TokenType._class:
                    if (!declaration) unexpected();
                    return parseClass(node, "true");
                case TokenType._if:
                    return parseIfStatement(node);
                case TokenType._return:
                    return parseReturnStatement(node);
                case TokenType._switch:
                    return parseSwitchStatement(node);
                case TokenType._throw:
                    return parseThrowStatement(node);
                case TokenType._try:
                    return parseTryStatement(node);
                case TokenType._const:
                case TokenType._var:
                    kind = kind ?? (string)value;
                    if (!declaration && kind != "var") unexpected();
                    return parseVarStatement(node, kind);
                case TokenType._while:
                    return parseWhileStatement(node);
                case TokenType._with:
                    return parseWithStatement(node);
                case TokenType.braceL:
                    return parseBlock();
                case TokenType.semi:
                    return parseEmptyStatement(node);
                case TokenType._export:
                case TokenType._import:
                    if (!Options.allowImportExportEverywhere)
                    {
                        if (!topLevel)
                            raise(start, "'import' and 'export' may only appear at the top level");
                        if (!inModule)
                            raise(start, "'import' and 'export' may appear only with 'sourceType: module'");
                    }
                    return starttype == TokenType._import ? parseImport(node) : parseExport(node, exports);
            }

            if (isAsyncFunction() && declaration)
            {
                next();
                return parseFunctionStatement(node, true);
            }

            var maybeName = value;
            var expr = parseExpression();
            if (starttype == TokenType.name && expr.type == NodeType.Identifier && eat(TokenType.colon))
                return parseLabeledStatement(node, (string)maybeName, expr);
            return parseExpressionStatement(node, expr);
        }

        private Node parseBreakContinueStatement(Node node, string keyword)
        {
            var isBreak = keyword == "break";
            next();
            if (eat(TokenType.semi) || insertSemicolon()) node.label = null;
            else if (type != TokenType.name) unexpected();
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
            if (i == labels.Count) raise(node.start, "Unsyntactic " + keyword);
            return finishNode(node, isBreak ? NodeType.BreakStatement : NodeType.ContinueStatement);
        }

        private Node parseDebuggerStatement(Node node)
        {
            next();
            semicolon();
            return finishNode(node, NodeType.DebuggerStatement);
        }

        private Node parseDoStatement(Node node)
        {
            next();
            labels.Add(loopLabel);
            node.fbody = parseStatement(false);
            labels.Pop();
            expect(TokenType._while);
            node.test = parseParenExpression();
            if (Options.ecmaVersion >= 6)
                eat(TokenType.semi);
            else
                semicolon();
            return finishNode(node, NodeType.DoWhileStatement);
        }

        // Disambiguating between a `for` and a `for`/`in` or `for`/`of`
        // loop is non-trivial. Basically, we have to parse the init `var`
        // statement or expression, disallowing the `in` operator (see
        // the second parameter to `parseExpression`), and then check
        // whether the next token is `in` or `of`. When there is no init
        // part (semicolon immediately after the opening parenthesis), it
        // is a regular `for` loop.
        private Node parseForStatement(Node node)
        {
            next();
            labels.Add(loopLabel);
            enterLexicalScope();
            expect(TokenType.parenL);
            if (type == TokenType.semi) return parseFor(node, null);
            var isLet = this.isLet();
            Node init;
            if (type == TokenType._var || type == TokenType._const || isLet)
            {
                init = startNode();
                var kind = isLet ? "let" : (string)value;
                next();
                parseVar(init, true, kind);
                finishNode(init, NodeType.VariableDeclaration);
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

        private Node parseFunctionStatement(Node node, bool isAsync)
        {
            next();
            return parseFunction(node, "true", false, isAsync);
        }

        private bool isFunction()
        {
            return type == TokenType._function || isAsyncFunction();
        }

        private Node parseIfStatement(Node node)
        {
            next();
            node.test = parseParenExpression();
            // allow function declarations in branches, but only in non-strict mode
            node.consequent = parseStatement(!strict && isFunction());
            node.alternate = eat(TokenType._else) ? parseStatement(!strict && isFunction()) : null;
            return finishNode(node, NodeType.IfStatement);
        }

        private Node parseReturnStatement(Node node)
        {
            if (!inFunction && !Options.allowReturnOutsideFunction)
                raise(start, "'return' outside of function");
            next();

            // In `return` (and `break`/`continue`), the keywords with
            // optional arguments, we eagerly look for a semicolon or the
            // possibility to insert one.

            if (eat(TokenType.semi) || insertSemicolon()) node.argument = null;
            else
            {
                node.argument = parseExpression();
                semicolon();
            }
            return finishNode(node, NodeType.ReturnStatement);
        }

        private Node parseSwitchStatement(Node node)
        {
            next();
            node.discriminant = parseParenExpression();
            node.cases = new List<Node>();
            expect(TokenType.braceL);
            labels.Add(switchLabel);
            enterLexicalScope();

            // Statements under must be grouped (by label) in SwitchCase
            // nodes. `cur` is used to keep the node that we are currently
            // adding statements to.

            Node cur = null;
            for (var sawDefault = false; type != TokenType.braceR;)
            {
                if (type == TokenType._case || type == TokenType._default)
                {
                    var isCase = type == TokenType._case;
                    if (cur != null) finishNode(cur, NodeType.SwitchCase);
                    node.cases.Add(cur = startNode());
                    cur.sconsequent = new List<Node>();
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
                    if (cur == null) unexpected();
                    cur.sconsequent.Add(parseStatement(true));
                }
            }
            exitLexicalScope();
            if (cur != null) finishNode(cur, NodeType.SwitchCase);
            next(); // Closing brace
            labels.Pop();
            return finishNode(node, NodeType.SwitchStatement);
        }

        private Node parseThrowStatement(Node node)
        {
            next();
            if (lineBreak.IsMatch(input.Substring(lastTokEnd, start - lastTokEnd)))
                raise(lastTokEnd, "Illegal newline after throw");
            node.argument = parseExpression();
            semicolon();
            return finishNode(node, NodeType.ThrowStatement);
        }

        private Node parseTryStatement(Node node)
        {
            next();
            node.block = parseBlock();
            node.handler = null;
            if (type == TokenType._catch)
            {
                var clause = startNode();
                next();
                expect(TokenType.parenL);
                clause.param = parseBindingAtom();
                enterLexicalScope();
                checkLVal(clause.param, "let");
                expect(TokenType.parenR);
                clause.fbody = parseBlock(false);
                exitLexicalScope();
                node.handler = finishNode(clause, NodeType.CatchClause);
            }
            node.finalizer = eat(TokenType._finally) ? parseBlock() : null;
            if (node.handler == null && node.finalizer == null)
                raise(node.start, "Missing catch or finally clause");
            return finishNode(node, NodeType.TryStatement);
        }

        private Node parseVarStatement(Node node, string kind)
        {
            next();
            parseVar(node, false, kind);
            semicolon();
            return finishNode(node, NodeType.VariableDeclaration);
        }

        private Node parseWhileStatement(Node node)
        {
            next();
            node.test = parseParenExpression();
            labels.Add(loopLabel);
            node.fbody = parseStatement(false);
            labels.Pop();
            return finishNode(node, NodeType.WhileStatement);
        }

        private Node parseWithStatement(Node node)
        {
            if (strict) raise(start, "'with' in strict mode");
            next();
            node.@object = parseParenExpression();
            node.fbody = parseStatement(false);
            return finishNode(node, NodeType.WithStatement);
        }

        private Node parseEmptyStatement(Node node)
        {
            next();
            return finishNode(node, NodeType.EmptyStatement);
        }

        private Node parseLabeledStatement(Node node, string maybeName, Node expr)
        {
            foreach (var label in labels)
            {
                if (label.name == maybeName)
                    raise(expr.start, "Label '" + maybeName + "' is already declared");
            }
            var kind = TokenInformation.Types[type].IsLoop ? "loop" : type == TokenType._switch ? "switch" : null;
            for (var i = labels.Count - 1; i >= 0; i--)
            {
                var label = labels[i];
                if (label.statementStart == node.start)
                {
                    label.statementStart = start;
                    label.kind = kind;
                }
                else break;
            }
            labels.Add(new Label {name = maybeName, kind = kind, statementStart = start});
            node.fbody = parseStatement(true);
            if (node.fbody.type == NodeType.ClassDeclaration ||
                node.fbody.type == NodeType.VariableDeclaration && node.fbody.kind != "var" ||
                node.fbody.type == NodeType.FunctionDeclaration && (strict || node.fbody.generator))
                raiseRecoverable(node.fbody.start, "Invalid labeled declaration");
            labels.Pop();
            node.label = expr;
            return finishNode(node, NodeType.LabeledStatement);
        }

        private Node parseExpressionStatement(Node node, Node expr)
        {
            node.expression = expr;
            semicolon();
            return finishNode(node, NodeType.ExpressionStatement);
        }

        // Parse a semicolon-enclosed block of statements, handling `"use
        // strict"` declarations when `allowStrict` is true (used for
        // function bodies).
        private Node parseBlock(bool createNewLexicalScope = true)
        {
            var node = startNode();
            node.body = new List<Node>();
            expect(TokenType.braceL);
            if (createNewLexicalScope)
            {
                enterLexicalScope();
            }
            while (!eat(TokenType.braceR))
            {
                var stmt = parseStatement(true);
                node.body.Add(stmt);
            }
            if (createNewLexicalScope)
            {
                exitLexicalScope();
            }
            return finishNode(node, NodeType.BlockStatement);
        }

        // Parse a regular `for` loop. The disambiguation code in
        // `parseStatement` will already have parsed the init statement or
        // expression.
        private Node parseFor(Node node, Node init)
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
            return finishNode(node, NodeType.ForStatement);
        }

        // Parse a `for`/`in` and `for`/`of` loop, which are almost
        // same from parser's perspective.
        private Node parseForIn(Node node, Node init)
        {
            var type = this.type == TokenType._in ? NodeType.ForInStatement : NodeType.ForOfStatement;
            next();
            node.left = init;
            node.right = parseExpression();
            expect(TokenType.parenR);
            exitLexicalScope();
            node.fbody = parseStatement(false);
            labels.Pop();
            return finishNode(node, type);
        }

        // Parse a list of variable declarations.
        private Node parseVar(Node node, bool isFor, string kind)
        {
            node.declarations = new List<Node>();
            node.kind = kind;
            for (;;)
            {
                var decl = startNode();
                parseVarId(decl, kind);
                if (eat(TokenType.eq))
                {
                    decl.init = parseMaybeAssign(isFor);
                }
                else if (kind == "const" && !(type == TokenType._in || Options.ecmaVersion >= 6 && isContextual("of")))
                {
                    unexpected();
                }
                else if (decl.id.type != NodeType.Identifier && !(isFor && (type == TokenType._in || isContextual("of"))))
                {
                    raise(lastTokEnd, "Complex binding patterns require an initialization value");
                }
                else
                {
                    decl.init = null;
                }
                node.declarations.Add(finishNode(decl, NodeType.VariableDeclarator));
                if (!eat(TokenType.comma)) break;
            }
            return node;
        }

        private void parseVarId(Node decl, string kind)
        {
            decl.id = parseBindingAtom();
            checkLVal(decl.id, kind);
        }

        // Parse a function declaration or literal (depending on the
        // `isStatement` parameter).
        private Node parseFunction(Node node, string isStatement, bool allowExpressionBody = false, bool isAsync = false)
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
            yieldPos = 0;
            awaitPos = 0;
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
            return finishNode(node, isStatement != null ? NodeType.FunctionDeclaration : NodeType.FunctionExpression);
        }

        private void parseFunctionParams(Node node)
        {
            expect(TokenType.parenL);
            node.@params = parseBindingList(TokenType.parenR, false, Options.ecmaVersion >= 8);
            checkYieldAwaitInDefaultParams();
        }

        // Parse a class declaration or literal (depending on the
        // `isStatement` parameter).
        private Node parseClass(Node node, string isStatement)
        {
            next();

            parseClassId(node, isStatement);
            parseClassSuper(node);
            var classBody = startNode();
            var hadConstructor = false;
            classBody.body = new List<Node>();
            expect(TokenType.braceL);
            while (!eat(TokenType.braceR))
            {
                if (eat(TokenType.semi)) continue;
                var method = startNode();
                var isGenerator = eat(TokenType.star);
                var isAsync = false;
                var isMaybeStatic = type == TokenType.name && (string)value == "static";
                parsePropertyName(method);
                method.@static = isMaybeStatic && type != TokenType.parenL;
                if (method.@static)
                {
                    if (isGenerator) unexpected();
                    isGenerator = eat(TokenType.star);
                    parsePropertyName(method);
                }
                if (Options.ecmaVersion >= 8 && !isGenerator && !method.computed &&
                    method.key.type == NodeType.Identifier && method.key.name == "async" && type != TokenType.parenL &&
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
                    if (!isGenerator && !isAsync && key.type == NodeType.Identifier && type != TokenType.parenL && (key.name == "get" || key.name == "set"))
                    {
                        isGetSet = true;
                        method.kind = key.name;
                        key = parsePropertyName(method);
                    }
                    if (!method.@static && (key.type == NodeType.Identifier && key.name == "constructor" ||
                                            key.type == NodeType.Literal && (string)key.value == "constructor"))
                    {
                        if (hadConstructor) raise(key.start, "Duplicate constructor in the same class");
                        if (isGetSet) raise(key.start, "Constructor can't have get/set modifier");
                        if (isGenerator) raise(key.start, "Constructor can't be a generator");
                        if (isAsync) raise(key.start, "Constructor can't be an async method");
                        method.kind = "constructor";
                        hadConstructor = true;
                    }
                }
                parseClassMethod(classBody, method, isGenerator, isAsync);
                if (isGetSet)
                {
                    var paramCount = method.kind == "get" ? 0 : 1;
                    var value = (Node)method.value;
                    if (value.@params.Count != paramCount)
                    {
                        var start = value.start;
                        if (method.kind == "get")
                            raiseRecoverable(start, "getter should have no params");
                        else
                            raiseRecoverable(start, "setter should have exactly one param");
                    }
                    else
                    {
                        if (method.kind == "set" && value.@params[0].type == NodeType.RestElement)
                            raiseRecoverable(value.@params[0].start, "Setter cannot use rest params");
                    }
                }
            }
            node.fbody = finishNode(classBody, NodeType.ClassBody);
            return finishNode(node, isStatement != null ? NodeType.ClassDeclaration : NodeType.ClassExpression);
        }

        private void parseClassMethod(Node classBody, Node method, bool isGenerator, bool isAsync)
        {
            method.value = parseMethod(isGenerator, isAsync);
            classBody.body.Add(finishNode(method, NodeType.MethodDefinition));
        }

        private void parseClassId(Node node, string isStatement)
        {
            if (type == TokenType.name)
                node.id = parseIdent();
            else
            {
                if (isStatement == "true")
                    unexpected();
                else node.id = null;
            }
        }

        private void parseClassSuper(Node node)
        {
            node.superClass = eat(TokenType._extends) ? parseExprSubscripts() : null;
        }

        // Parses module export declaration.
        private Node parseExport(Node node, IDictionary<string, bool> exports)
        {
            next();
            // export * from '...'
            if (eat(TokenType.star))
            {
                expectContextual("from");
                if (type == TokenType.@string) node.source = parseExprAtom();
                else unexpected();
                semicolon();
                return finishNode(node, NodeType.ExportAllDeclaration);
            }
            if (eat(TokenType._default))
            {
                // export default ...
                checkExport(exports, "default", lastTokStart);
                var isAsync = false;
                if (type == TokenType._function || (isAsync = isAsyncFunction()))
                {
                    var fNode = startNode();
                    next();
                    if (isAsync) next();
                    node.declaration = parseFunction(fNode, "nullableID", false, isAsync);
                }
                else if (type == TokenType._class)
                {
                    var cNode = startNode();
                    node.declaration = parseClass(cNode, "nullableID");
                }
                else
                {
                    node.declaration = parseMaybeAssign();
                    semicolon();
                }
                return finishNode(node, NodeType.ExportDefaultDeclaration);
            }
            // export var|const|let|function|class ...
            if (shouldParseExportStatement())
            {
                node.declaration = parseStatement(true);
                if (node.declaration.type == NodeType.VariableDeclaration)
                    checkVariableExport(exports, node.declaration.declarations);
                else
                    checkExport(exports, node.declaration.id.name, node.declaration.id.start);
                node.specifiers = new List<Node>();
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
                    else unexpected();
                }
                else
                {
                    // check for keywords used as local names
                    foreach (var spec in node.specifiers)
                    {
                        checkUnreserved(spec.local.start, spec.local.end, spec.local.name);
                    }

                    node.source = null;
                }
                semicolon();
            }
            return finishNode(node, NodeType.ExportNamedDeclaration);
        }

        private void checkExport(IDictionary<string, bool> exports, string name, int pos)
        {
            if (exports == null) return;
            if (exports.ContainsKey(name))
                raiseRecoverable(pos, "Duplicate export '" + name + "'");
            exports[name] = true;
        }

        private void checkPatternExport(IDictionary<string, bool> exports, Node pat)
        {
            var type = pat.type;
            switch (type)
            {
                case NodeType.Identifier:
                    checkExport(exports, pat.name, pat.start);
                    break;
                case NodeType.ObjectPattern:
                    foreach (var prop in pat.properties)
                        checkPatternExport(exports, (Node)prop.value);
                    break;
                case NodeType.ArrayPattern:
                    foreach (var elt in pat.elements)
                    {
                        if (elt != null) checkPatternExport(exports, elt);
                    }
                    break;
                case NodeType.AssignmentPattern:
                    checkPatternExport(exports, pat.left);
                    break;
                case NodeType.ParenthesizedExpression:
                    checkPatternExport(exports, pat.expression);
                    break;
            }
        }

        private void checkVariableExport(IDictionary<string, bool> exports, IEnumerable<Node> decls)
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
        private IList<Node> parseExportSpecifiers(IDictionary<string, bool> exports)
        {
            var nodes = new List<Node>();
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

                var node = startNode();
                node.local = parseIdent(true);
                node.exported = eatContextual("as") ? parseIdent(true) : node.local;
                checkExport(exports, node.exported.name, node.exported.start);
                nodes.Add(finishNode(node, NodeType.ExportSpecifier));
            }
            return nodes;
        }

        // Parses import declaration.
        private Node parseImport(Node node)
        {
            next();
            // import '...'
            if (type == TokenType.@string)
            {
                node.specifiers = new List<Node>();
                node.source = parseExprAtom();
            }
            else
            {
                node.specifiers = parseImportSpecifiers();
                expectContextual("from");
                if (type == TokenType.@string) node.source = parseExprAtom();
                else unexpected();
            }
            semicolon();
            return finishNode(node, NodeType.ImportDeclaration);
        }

        // Parses a comma-separated list of module imports.
        private IList<Node> parseImportSpecifiers()
        {
            var nodes = new List<Node>();
            var first = true;
            if (type == TokenType.name)
            {
                // import defaultObj, { x, y as z } from '...'
                var node = startNode();
                node.local = parseIdent();
                checkLVal(node.local, "let");
                nodes.Add(finishNode(node, NodeType.ImportDefaultSpecifier));
                if (!eat(TokenType.comma)) return nodes;
            }
            if (type == TokenType.star)
            {
                var node = startNode();
                next();
                expectContextual("as");
                node.local = parseIdent();
                checkLVal(node.local, "let");
                nodes.Add(finishNode(node, NodeType.ImportNamespaceSpecifier));
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

                var node = startNode();
                node.imported = parseIdent(true);
                if (eatContextual("as"))
                {
                    node.local = parseIdent();
                }
                else
                {
                    checkUnreserved(node.imported.start, node.imported.end, node.imported.name);
                    node.local = node.imported;
                }
                checkLVal(node.local, "let");
                nodes.Add(finishNode(node, NodeType.ImportSpecifier));
            }
            return nodes;
        }

        // Set `ExpressionStatement#directive` property for directive prologues.
        private void adaptDirectivePrologue(IList<Node> statements)
        {
            for (var i = 0; i < statements.Count && isDirectiveCandidate(statements[i]); ++i)
            {
                statements[i].directive = statements[i].expression.raw.Substring(1, statements[i].expression.raw.Length - 2);
            }
        }

        private bool isDirectiveCandidate(Node statement)
        {
            return statement.type == NodeType.ExpressionStatement &&
                   statement.expression.type == NodeType.Literal &&
                   statement.expression.value is string &&
                   // Reject parenthesized strings.
                   (input[statement.start] == '\"' || input[statement.start] == '\'');
        }
    }
}
