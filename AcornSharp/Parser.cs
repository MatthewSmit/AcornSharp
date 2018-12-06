using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        private sealed class Label
        {
            public Label(LabelKind kind)
            {
                Kind = kind;
            }

            public Label(LabelKind kind, string name, int statementStart)
            {
                Kind = kind;
                Name = name;
                StatementStart = statementStart;
            }

            public LabelKind Kind { get; set; }

            public string Name { get; }

            public int StatementStart { get; set; }
        }

        private enum LabelKind
        {
            None,
            Loop,
            Switch
        }

        private static readonly Label loopLabel = new Label(LabelKind.Loop);
        private static readonly Label switchLabel = new Label(LabelKind.Switch);

        private Options options;
        private Regex keywords;
        private Regex reservedWords;
        private Regex reservedWordsStrict;
        private Regex reservedWordsStrictBind;
        private string input;
        private bool containsEsc; // TODO: rename to containsEscape
        private int pos; // TODO: rename to position
        private int lineStart;
        private int curLine; // TODO: rename to currentLine
        private TokenType type;
        private object value;
        private int start;
        private int end;
        private Position startLoc; // TODO: rename startLocation
        private Position endLoc; // TODO: rename endLocation
        private Position? lastTokStartLoc; // TODO: rename lastTokenStartLocation
        private Position? lastTokEndLoc; // TODO: rename lastTokenEndLocation
        private int lastTokStart; // TODO: rename lastTokenStart
        private int lastTokEnd; // TODO: rename lastTokenEnd
        private IList<TokenContext> context;
        private bool exprAllowed; // TODO: rename expressionAllowed
        private bool inModule;
        private bool strict;
        private int potentialArrowAt;
        private int awaitPos; // TODO: rename awaitPosition
        private int yieldPos; // TODO: rename yieldPosition
        private List<Scope> scopeStack;
        private RegExpValidationState regexpState;
        private List<Label> labels;
        private bool inTemplateElement;

        static Parser()
        {
            // Token-specific context update code

            TokenType.ParenRight.UpdateContext = TokenType.BraceRight.UpdateContext = (parser, previousType) =>
            {
                if (parser.context.Count == 1)
                {
                    parser.exprAllowed = true;
                    return;
                }

                var @out = parser.context[parser.context.Count - 1];
                parser.context.RemoveAt(parser.context.Count - 1);
                if (@out == TokenContext.BasicStatement && parser.CurrentContext().Token == "function")
                {
                    @out = parser.context[parser.context.Count - 1];
                    parser.context.RemoveAt(parser.context.Count - 1);
                }

                parser.exprAllowed = !@out.IsExpression;
            };

            TokenType.BraceLeft.UpdateContext = (parser, previousType) =>
            {
                parser.context.Add(parser.BraceIsBlock(previousType) ? TokenContext.BasicStatement : TokenContext.BasicExpression);
                parser.exprAllowed = true;
            };

            TokenType.DollarBraceLeft.UpdateContext = (parser, previousType) =>
            {
                parser.context.Add(TokenContext.BasicTemplate);
                parser.exprAllowed = true;
            };

            TokenType.ParenLeft.UpdateContext = (parser, previousType) =>
            {
                var statementParens = previousType == TokenType.If || previousType == TokenType.For || previousType == TokenType.With || previousType == TokenType.While;
                parser.context.Add(statementParens ? TokenContext.ParenthesesStatatement : TokenContext.ParenthesesExpression);
                parser.exprAllowed = true;
            };

            TokenType.IncrementDecrement.UpdateContext = (parser, previousType) =>
            {
                // tokExprAllowed stays unchanged
            };

            TokenType.Function.UpdateContext = TokenType.Class.UpdateContext = (parser, previousType) =>
            {
                if (previousType.BeforeExpression && previousType != TokenType.Semicolon && previousType != TokenType.Else &&
                    !(previousType == TokenType.Return && Whitespace.LineBreak.IsMatch(parser.input.Substring(parser.lastTokEnd, parser.start - parser.lastTokEnd))) &&
                    !((previousType == TokenType.Colon || previousType == TokenType.BraceLeft) && parser.CurrentContext() == TokenContext.BasicStatement))
                {
                    parser.context.Add(TokenContext.FunctionExpression);
                }
                else
                {
                    parser.context.Add(TokenContext.FunctionStatement);
                }

                parser.exprAllowed = false;
            };

            TokenType.BackQuote.UpdateContext = (parser, previousType) =>
            {
                if (parser.CurrentContext() == TokenContext.QuoteTemplate)
                {
                    parser.context.RemoveAt(parser.context.Count - 1);
                }
                else
                {
                    parser.context.Add(TokenContext.QuoteTemplate);
                }

                parser.exprAllowed = false;
            };

            TokenType.Star.UpdateContext = (parser, previousType) =>
            {
                if (previousType == TokenType.Function)
                {
                    var index = parser.context.Count - 1;
                    if (parser.context[index] == TokenContext.FunctionExpression)
                    {
                        parser.context[index] = TokenContext.FunctionExpressionGenerator;
                    }
                    else
                    {
                        parser.context[index] = TokenContext.FunctionGenerator;
                    }
                }

                parser.exprAllowed = true;
            };

            TokenType.Name.UpdateContext = (parser, previousType) =>
            {
                var allowed = false;
                if (parser.options.EcmaVersion >= 6 && previousType != TokenType.Dot && parser.value is string value)
                {
                    if (value == "of" && !parser.exprAllowed ||
                        value == "yield" && parser.InGeneratorContext())
                    {
                        allowed = true;
                    }
                }

                parser.exprAllowed = allowed;
            };
        }

        public Parser(Options options, string input, int startPosition = 0)
        {
            this.options = options = Options.GetOptions(options);
            SourceFile = options.SourceFile;
            keywords = options.EcmaVersion >= 6 ? Identifier.Ecma6Keywords : Identifier.Ecma5Keywords;

            if (options.AllowReserved == AllowReserved.No)
            {
                switch (options.EcmaVersion)
                {
                    case 3:
                        reservedWords = options.SourceType == SourceType.Module ? Identifier.ReservedWords3Module : Identifier.ReservedWords3;
                        reservedWordsStrict = options.SourceType == SourceType.Module ? Identifier.ReservedWords3ModuleStrict : Identifier.ReservedWords3Strict;
                        reservedWordsStrictBind = options.SourceType == SourceType.Module ? Identifier.ReservedWords3ModuleStrictBind : Identifier.ReservedWords3StrictBind;
                        break;

                    case 5:
                        reservedWords = options.SourceType == SourceType.Module ? Identifier.ReservedWords5Module : Identifier.ReservedWords5;
                        reservedWordsStrict = options.SourceType == SourceType.Module ? Identifier.ReservedWords5ModuleStrict : Identifier.ReservedWords5Strict;
                        reservedWordsStrictBind = options.SourceType == SourceType.Module ? Identifier.ReservedWords5ModuleStrictBind : Identifier.ReservedWords5StrictBind;
                        break;

                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        reservedWords = options.SourceType == SourceType.Module ? Identifier.ReservedWords6Module : Identifier.ReservedWords6;
                        reservedWordsStrict = options.SourceType == SourceType.Module ? Identifier.ReservedWords6ModuleStrict : Identifier.ReservedWords6Strict;
                        reservedWordsStrictBind = options.SourceType == SourceType.Module ? Identifier.ReservedWords6ModuleStrictBind : Identifier.ReservedWords6StrictBind;
                        break;

                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            else
            {
                reservedWords = Identifier.ReservedWordsEmpty;
                reservedWordsStrict = Identifier.ReservedWordsEmptyStrict;
                reservedWordsStrictBind = Identifier.ReservedWordsEmptyStrictBind;
            }

            this.input = input;

            // Used to signal to callers of `ReadWord1` whether the word
            // contained any escape sequences. This is needed because words with
            // escape sequences must not be interpreted as keywords.
            containsEsc = false;

            // Set up token state

            // The current position of the tokenizer in the input.
            if (startPosition != 0)
            {
                pos = startPosition;
                lineStart = this.input.LastIndexOf('\n', startPosition - 1) + 1;
                curLine = Whitespace.LineBreak.Split(this.input.Substring(0, lineStart)).Length;
            }
            else
            {
                pos = lineStart = 0;
                curLine = 1;
            }

            // Properties of the current token:
            // Its type
            type = TokenType.Eof;
            // For tokens that include more information than their type, the value
            value = null;
            // Its start and end offset
            start = end = pos;
            // And, if locations are used, the {line, column} object
            // corresponding to those offsets
            startLoc = endLoc = CurrentPosition();

            // Position information for the previous token
            lastTokEndLoc = lastTokStartLoc = null;
            lastTokStart = lastTokEnd = pos;

            // The context stack is used to superficially track syntactic
            // context to predict whether a regular expression is allowed in a
            // given position.
            context = InitialContext();
            exprAllowed = true;

            // Figure out if it's a module code.
            inModule = options.SourceType == SourceType.Module;
            strict = inModule || StrictDirective(pos);

            // Used to signify the start of a potential arrow function
            potentialArrowAt = -1;

            // Positions to delayed-check that yield/await does not exist in default parameters.
            yieldPos = awaitPos = 0;
            // Labels in scope.
            labels = new List<Label>();

            // If enabled, skip leading hashbang line.
            if (pos == 0 && options.AllowHashBang && this.input.Substring(0, 2) == "#!")
            {
                SkipLineComment(2);
            }

            // Scope tracking for duplicate variable names (see scope.js)
            scopeStack = new List<Scope>();
            EnterScope(ScopeFlags.Top);
        }

        [NotNull]
        private ProgramNode Parse()
        {
            var node = options.Program ?? new ProgramNode(this, start, startLoc);
            NextToken();
            return ParseTopLevel(node);
        }

        private bool InFunction => (CurrentVarScope.Flags & ScopeFlags.Function) > 0;
        private bool InGenerator => (CurrentVarScope.Flags & ScopeFlags.Generator) > 0;
        private bool InAsync => (CurrentVarScope.Flags & ScopeFlags.Async) > 0;
        private bool AllowSuper => (CurrentThisScope.Flags & ScopeFlags.Super) > 0;
        private bool AllowDirectSuper => (CurrentThisScope.Flags & ScopeFlags.DirectSuper) > 0;
        private bool InNonArrowFunction => (CurrentThisScope.Flags & ScopeFlags.Function) > 0;

        //static extend(...plugins) {
        //  let cls = this
        //  for (let i = 0; i < plugins.length; i++) cls = plugins[i](cls)
        //  return cls
        //}

        [NotNull]
        public static ProgramNode Parse(string input, Options options)
        {
            return new Parser(options, input).Parse();
        }

        //static parseExpressionAt(input, pos, options) {
        //  let parser = new this(options, input, pos)
        //  parser.nextToken()
        //  return parser.parseExpression()
        //}

        //static tokenizer(input, options) {
        //  return new this(options, input)
        //}

        // This function is used to raise exceptions on parse errors. It
        // takes an offset integer (into the current `input`) to indicate
        // the location of the error, attaches the position to the end
        // of the error message, and then raises a `SyntaxError` with that
        // message.
        private void Raise(int position, string message)
        {
            var loc = GetLineInfo(input, position);
            message += " (" + loc.Line + ":" + loc.Column + ")";
            throw new SyntaxException(message, position, loc);
        }

        public void RaiseRecoverable(int position, string message)
        {
            Raise(position, message);
        }

        private Position CurrentPosition()
        {
            if (options.Locations)
            {
                return new Position(curLine, pos - lineStart);
            }

            return default;
        }

        [NotNull]
        private static IList<TokenContext> InitialContext()
        {
            return new List<TokenContext>
            {
                TokenContext.BasicStatement
            };
        }

        private bool BraceIsBlock(TokenType previousType)
        {
            var parent = CurrentContext();
            if (parent == TokenContext.FunctionExpression || parent == TokenContext.FunctionStatement)
            {
                return true;
            }

            if (previousType == TokenType.Colon && (parent == TokenContext.BasicStatement || parent == TokenContext.BasicExpression))
            {
                return !parent.IsExpression;
            }

            // The check for `tt.name && exprAllowed` detects whether we are
            // after a `yield` or `of` construct. See the `updateContext` for
            // `tt.name`.
            if (previousType == TokenType.Return || previousType == TokenType.Name && exprAllowed)
            {
                return Whitespace.LineBreak.IsMatch(input.Substring(lastTokEnd, start - lastTokEnd));
            }

            if (previousType == TokenType.Else || previousType == TokenType.Semicolon || previousType == TokenType.Eof || previousType == TokenType.ParenRight || previousType == TokenType.Arrow)
            {
                return true;
            }

            if (previousType == TokenType.BraceLeft)
            {
                return parent == TokenContext.BasicStatement;
            }

            if (previousType == TokenType.Var || previousType == TokenType.Const || previousType == TokenType.Name)
            {
                return false;
            }

            return !exprAllowed;
        }

        private bool InGeneratorContext()
        {
            for (var i = this.context.Count - 1; i >= 1; i--)
            {
                var context = this.context[i];
                if (context.Token == "function")
                {
                    return context.Generator;
                }
            }

            return false;
        }

        private void UpdateContext(TokenType previousType)
        {
            Action<Parser, TokenType> update;
            var currentType = type;
            if (currentType.Keyword != null && previousType == TokenType.Dot)
            {
                exprAllowed = false;
            }
            else if ((update = currentType.UpdateContext) != null)
            {
                update(this, previousType);
            }
            else
            {
                exprAllowed = currentType.BeforeExpression;
            }
        }

        // ## Parser utilities

        private static readonly Regex literal = new Regex(@"\G(?:'((?:\\.|[^'])*?)'|""((?:\\.|[^""])*?)""|;)");

        private bool StrictDirective(int start)
        {
            for (; ; )
            {
                start += Whitespace.SkipWhiteSpace.Match(input, start).Length;
                var match = literal.Match(input, start);
                if (!match.Success)
                {
                    return false;
                }

                if (match.Groups[1].Success && match.Groups[1].Value == "use strict")
                {
                    return true;
                }
                if (match.Groups[2].Success && match.Groups[2].Value == "use strict")
                {
                    return true;
                }

                start += match.Length;
            }
        }

        // Predicate that tests whether the next token is of the given
        // type, and if yes, consumes it as a side effect.
        private bool Eat(TokenType type)
        {
            if (this.type == type)
            {
                Next();
                return true;
            }

            return false;
        }

        // Tests whether parsed token is a contextual keyword.
        private bool IsContextual(string name)
        {
            return type == TokenType.Name && value is string valueString && string.Equals(valueString, name, StringComparison.Ordinal) && !containsEsc;
        }

        // Consumes contextual keyword if possible.
        private bool EatContextual(string name)
        {
            if (!IsContextual(name))
            {
                return false;
            }

            Next();
            return true;
        }

        // Asserts that following token is given contextual keyword.
        private void ExpectContextual(string name)
        {
            if (!EatContextual(name))
            {
                Unexpected();
            }
        }

        // Test whether a semicolon can be inserted at the current position.
        private bool CanInsertSemicolon()
        {
            return type == TokenType.Eof ||
                   type == TokenType.BraceRight ||
                   Whitespace.LineBreak.IsMatch(input.Substring(lastTokEnd, start - lastTokEnd));
        }

        public bool InsertSemicolon()
        {
            if (CanInsertSemicolon())
            {
                options.OnInsertedSemicolon?.Invoke(this, lastTokEnd, lastTokEndLoc.GetValueOrDefault());
                return true;
            }

            return false;
        }

        // Consume a semicolon, or, failing that, see if we are allowed to
        // pretend that there is a semicolon at this position.
        public void Semicolon()
        {
            if (!Eat(TokenType.Semicolon) && !InsertSemicolon())
            {
                Unexpected();
            }
        }

        private bool AfterTrailingComma(TokenType tokType, bool notNext = false)
        {
            if (type == tokType)
            {
                options.OnTrailingComma?.Invoke(this, lastTokStart, lastTokStartLoc.GetValueOrDefault());

                if (!notNext)
                {
                    Next();
                }

                return true;
            }

            return false;
        }

        // Expect a token of a given type. If found, consume it, otherwise,
        // raise an unexpected token error.
        private void Expect(TokenType type)
        {
            if (!Eat(type))
            {
                Unexpected();
            }
        }

        // Raise an unexpected token error.
        private void Unexpected(int? pos = null)
        {
            Raise(pos ?? start, "Unexpected token");
        }

        //export function DestructuringErrors() {
        //  this.shorthandAssign =
        //  this.trailingComma =
        //  this.parenthesizedAssign =
        //  this.parenthesizedBind =
        //  this.doubleProto =
        //    -1
        //}

        private void CheckPatternErrors([CanBeNull] DestructuringErrors refDestructuringErrors, bool isAssign)
        {
            if (refDestructuringErrors == null)
            {
                return;
            }

            if (refDestructuringErrors.trailingComma > -1)
            {
                RaiseRecoverable(refDestructuringErrors.trailingComma, "Comma is not permitted after the rest element");
            }

            var parens = isAssign ? refDestructuringErrors.parenthesizedAssign : refDestructuringErrors.parenthesizedBind;
            if (parens > -1)
            {
                RaiseRecoverable(parens, "Parenthesized pattern");
            }
        }

        private bool CheckExpressionErrors([CanBeNull] DestructuringErrors refDestructuringErrors, bool andThrow = false)
        {
            if (refDestructuringErrors == null)
            {
                return false;
            }

            if (!andThrow)
            {
                return refDestructuringErrors.shorthandAssign >= 0 || refDestructuringErrors.doubleProto >= 0;
            }

            if (refDestructuringErrors.shorthandAssign >= 0)
            {
                Raise(refDestructuringErrors.shorthandAssign, "Shorthand property assignments are valid only in destructuring patterns");
            }

            if (refDestructuringErrors.doubleProto >= 0)
            {
                RaiseRecoverable(refDestructuringErrors.doubleProto, "Redefinition of __proto__ property");
            }

            return false;
        }

        private void CheckYieldAwaitInDefaultParams()
        {
            if (yieldPos != 0 && (awaitPos == 0 || yieldPos < awaitPos))
            {
                Raise(yieldPos, "Yield expression cannot be a default value");
            }

            if (awaitPos != 0)
            {
                Raise(awaitPos, "Await expression cannot be a default value");
            }
        }

        private static bool IsSimpleAssignTarget(ExpressionNode expr)
        {
            if (expr is ParenthesisedExpressionNode parenthesised)
            {
                return IsSimpleAssignTarget(parenthesised.Expression);
            }

            return expr is IdentifierNode || expr is MemberExpressionNode;
        }

        // ## Tokenizer

        // Move to the next token
        private void Next()
        {
            if (options.OnToken != null)
            {
                //                this.options.OnToken(new Token(this));
                throw new NotImplementedException();
            }

            lastTokEnd = end;
            lastTokStart = start;
            lastTokEndLoc = endLoc;
            lastTokStartLoc = startLoc;
            NextToken();
        }

        //pp.getToken = function() {
        //  this.next()
        //  return new Token(this)
        //}
        //
        //// If we're in an ES6 environment, make parsers iterable
        //if (typeof Symbol !== "undefined")
        //  pp[Symbol.iterator] = function() {
        //    return {
        //      next: () => {
        //        let token = this.getToken()
        //        return {
        //          done: token.type === tt.eof,
        //          value: token
        //        }
        //      }
        //    }
        //  }

        // Toggle strict mode. Re-reads the next number or string to please
        // pedantic tests (`"use strict"; 010;` should fail).
        private TokenContext CurrentContext()
        {
            return context[context.Count - 1];
        }

        // Read a single token, updating the parser object's token-related
        // properties.
        private void NextToken()
        {
            var currentContext = CurrentContext();
            if (currentContext == null || !currentContext.PreserveSpace)
            {
                SkipSpace();
            }

            start = pos;
            if (options.Locations)
            {
                startLoc = CurrentPosition();
            }

            if (pos >= input.Length)
            {
                FinishToken(TokenType.Eof);
                return;
            }

            Debug.Assert(currentContext != null, nameof(currentContext) + " != null");
            if (currentContext.Override != null)
            {
                currentContext.Override(this);
            }
            else
            {
                ReadToken(FullCharCodeAtPosition());
            }
        }

        private void ReadToken(int code)
        {
            // Identifier or keyword. '\uXXXX' sequences are allowed in
            // identifiers, so '\' also dispatches to that.
            if (Identifier.IsIdentifierStart(code, options.EcmaVersion >= 6) || code == '\\')
            {
                ReadWord();
                return;
            }

            GetTokenFromCode(code);
        }

        private int FullCharCodeAtPosition()
        {
            var code = input.CharCodeAt(pos);
            if (code <= 0xd7ff || code >= 0xe000)
            {
                return code;
            }

            var next = input.CharCodeAt(pos + 1);
            return (code << 10) + next - 0x35fdc00;
        }

        private void SkipBlockComment()
        {
            var startLoc = options.OnComment != null ? CurrentPosition() : default;
            var start = pos;
            var end = input.IndexOf("*/", pos += 2, StringComparison.Ordinal);
            if (end == -1)
            {
                Raise(pos - 2, "Unterminated comment");
            }

            pos = end + 2;
            if (options.Locations)
            {
                var index = start;
                while (true)
                {
                    var match = Whitespace.LineBreak.Match(input, index);
                    if (match.Success && match.Index < pos)
                    {
                        ++curLine;
                        index = lineStart = match.Index + match.Length;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            options.OnComment?.Invoke(this, true, input.Substring(start + 2, end - (start + 2)), start, pos, startLoc, CurrentPosition());
        }

        private void SkipLineComment(int startSkip)
        {
            var start = pos;
            var startLoc = options.OnComment != null ? CurrentPosition() : default;
            var ch = input.CharCodeAt(pos += startSkip);
            while (pos < input.Length && !Whitespace.IsNewLine(ch))
            {
                ch = input.CharCodeAt(++pos);
            }

            options.OnComment?.Invoke(this, false, input.Substring(start + startSkip, pos - (start + startSkip)), start, pos, startLoc, CurrentPosition());
        }

        // Called at the start of the parse and after every token. Skips
        // whitespace and comments, and.
        private void SkipSpace()
        {
            while (pos < input.Length)
            {
                var ch = input[pos];
                switch (ch)
                {
                    case ' ':
                    case (char)160:
                        ++pos;
                        break;

                    case '\r':
                        if (input[pos + 1] == 10)
                        {
                            ++pos;
                        }

                        goto case '\n';

                    case '\n':
                    case (char)8232:
                    case (char)8233:
                        ++pos;
                        if (options.Locations)
                        {
                            ++curLine;
                            lineStart = pos;
                        }
                        break;

                    case '/':
                        switch (input.CharCodeAt(pos + 1))
                        {
                            case '*':
                                SkipBlockComment();
                                break;

                            case '/':
                                SkipLineComment(2);
                                break;

                            default:
                                return;
                        }
                        break;

                    default:
                        if (ch > 8 && ch < 14 || ch >= 5760 && Whitespace.NonASCIIwhitespace.IsMatch(ch.ToString()))
                        {
                            ++pos;
                        }
                        else
                        {
                            return;
                        }
                        break;
                }
            }
        }

        // Called at the end of every token. Sets `end`, `val`, and
        // maintains `context` and `exprAllowed`, and skips the space after
        // the token, so that the next one's `start` will point at the
        // right position.
        private void FinishToken(TokenType type, [CanBeNull] object value = null)
        {
            end = pos;
            if (options.Locations)
            {
                endLoc = CurrentPosition();
            }

            var prevType = this.type;
            this.type = type;
            this.value = value;

            UpdateContext(prevType);
        }

        // ### Token reading

        // This is the function that is called to fetch the next token. It
        // is somewhat obscure, because it works in character codes rather
        // than characters, and because operator parsing has been inlined
        // into it.
        //
        // All in the name of speed.
        private void ReadTokenDot()
        {
            var next = input.CharCodeAt(pos + 1);
            if (next >= 48 && next <= 57)
            {
                ReadNumber(true);
            }
            else
            {
                var next2 = input.CharCodeAt(pos + 2);
                if (options.EcmaVersion >= 6 && next == 46 && next2 == 46)
                {
                    // 46 = dot '.'
                    pos += 3;
                    FinishToken(TokenType.Ellipsis);
                }
                else
                {
                    ++pos;
                    FinishToken(TokenType.Dot);
                }
            }
        }

        private void ReadTokenSlash()
        {
            var next = input.CharCodeAt(pos + 1);
            if (exprAllowed)
            {
                ++pos;
                ReadRegexp();
            }
            else if (next == 61)
            {
                FinishOperand(TokenType.Assignment, 2);
            }
            else
            {
                FinishOperand(TokenType.Slash, 1);
            }
        }

        private void ReadTokenMultiplyModuloExponent(int code)
        {
            // '%*'
            var next = input.CharCodeAt(pos + 1);
            var size = 1;
            var tokentype = code == 42 ? TokenType.Star : TokenType.Modulo;

            // exponentiation operator ** and **=
            if (options.EcmaVersion >= 7 && code == 42 && next == 42)
            {
                ++size;
                tokentype = TokenType.StarStar;
                next = input[pos + 2];
            }

            if (next == 61)
            {
                FinishOperand(TokenType.Assignment, size + 1);
            }
            else
            {
                FinishOperand(tokentype, size);
            }
        }

        private void ReadTokenPipeAmp(int code)
        {
            // '|&'
            var next = input.CharCodeAt(pos + 1);
            if (next == code)
            {
                FinishOperand(code == 124 ? TokenType.LogicalOR : TokenType.LogicalAND, 2);
            }
            else if (next == 61)
            {
                FinishOperand(TokenType.Assignment, 2);
            }
            else
            {
                FinishOperand(code == 124 ? TokenType.BitwiseOR : TokenType.BitwiseAND, 1);
            }
        }

        private void ReadTokenCaret()
        {
            // '^'
            var next = input.CharCodeAt(pos + 1);
            if (next == 61)
            {
                FinishOperand(TokenType.Assignment, 2);
            }
            else
            {
                FinishOperand(TokenType.BitwiseXOR, 1);
            }
        }

        private void ReadTokenPlusMinus(int code)
        {
            // '+-'
            var next = input[pos + 1];
            if (next == code)
            {
                if (next == 45 && !inModule && input.CharCodeAt(pos + 2) == 62 &&
                    (lastTokEnd == 0 || Whitespace.LineBreak.IsMatch(input.Substring(lastTokEnd, pos - lastTokEnd))))
                {
                    // A `-->` line comment
                    SkipLineComment(3);
                    SkipSpace();
                    NextToken();
                }
                else
                {
                    FinishOperand(TokenType.IncrementDecrement, 2);
                }
            }
            else if (next == 61)
            {
                FinishOperand(TokenType.Assignment, 2);
            }
            else
            {
                FinishOperand(TokenType.PlusMinus, 1);
            }
        }

        private void ReadTokenLtGt(int code)
        {
            // '<>'
            var next = input.CharCodeAt(pos + 1);
            var size = 1;
            if (next == code)
            {
                size = code == 62 && input.CharCodeAt(pos + 2) == 62 ? 3 : 2;
                if (input.CharCodeAt(pos + size) == 61)
                {
                    FinishOperand(TokenType.Assignment, size + 1);
                }
                else
                {
                    FinishOperand(TokenType.BitShift, size);
                }
            }
            else if (next == 33 && code == 60 && !inModule && input.CharCodeAt(pos + 2) == 45 &&
                input.CharCodeAt(pos + 3) == 45)
            {
                // `<!--`, an XML-style comment that should be interpreted as a line comment
                SkipLineComment(4);
                SkipSpace();
                NextToken();
            }
            else if (next == 61)
            {
                size = 2;
                FinishOperand(TokenType.Relational, size);
            }
            else
            {
                FinishOperand(TokenType.Relational, size);
            }
        }

        private void ReadTokenEqExcl(int code)
        {
            // '=!'
            var next = input.CharCodeAt(pos + 1);
            if (next == 61)
            {
                FinishOperand(TokenType.Equality, input.CharCodeAt(pos + 2) == 61 ? 3 : 2);
            }
            else if (code == 61 && next == 62 && options.EcmaVersion >= 6)
            {
                // '=>'
                pos += 2;
                FinishToken(TokenType.Arrow);
            }
            else
            {
                FinishOperand(code == 61 ? TokenType.Equal : TokenType.PrefixOperator, 1);
            }
        }

        private void GetTokenFromCode(int code)
        {
            switch (code)
            {
                // The interpretation of a dot depends on whether it is followed
                // by a digit or another two dots.
                case 46: // '.'
                    ReadTokenDot();
                    return;

                // Punctuation tokens.
                case 40:
                    ++pos;
                    FinishToken(TokenType.ParenLeft);
                    return;

                case 41:
                    ++pos;
                    FinishToken(TokenType.ParenRight);
                    return;

                case 59:
                    ++pos;
                    FinishToken(TokenType.Semicolon);
                    return;

                case 44:
                    ++pos;
                    FinishToken(TokenType.Comma);
                    return;

                case 91:
                    ++pos;
                    FinishToken(TokenType.BracketLeft);
                    return;

                case 93:
                    ++pos;
                    FinishToken(TokenType.BracketRight);
                    return;

                case 123:
                    ++pos;
                    FinishToken(TokenType.BraceLeft);
                    return;

                case 125:
                    ++pos;
                    FinishToken(TokenType.BraceRight);
                    return;

                case 58:
                    ++pos;
                    FinishToken(TokenType.Colon);
                    return;

                case 63:
                    ++pos;
                    FinishToken(TokenType.Question);
                    return;

                case 96: // '`'
                    if (options.EcmaVersion < 6)
                    {
                        break;
                    }

                    ++pos;
                    FinishToken(TokenType.BackQuote);
                    return;

                case 48: // '0'
                    var next = input.CharCodeAt(pos + 1);
                    if (next == 120 || next == 88)
                    {
                        ReadRadixNumber(16); // '0x', '0X' - hex number
                        return;
                    }

                    if (options.EcmaVersion >= 6)
                    {
                        if (next == 111 || next == 79)
                        {
                            ReadRadixNumber(8); // '0o', '0O' - octal number
                            return;
                        }

                        if (next == 98 || next == 66)
                        {
                            ReadRadixNumber(2); // '0b', '0B' - binary number
                            return;
                        }
                    }

                    goto case 49;

                // Anything else beginning with a digit is an integer, octal
                // number, or float.
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57: // 1-9
                    ReadNumber(false);
                    return;

                // Quotes produce strings.
                case 34:
                case 39: // '"', "'"
                    ReadString(code);
                    return;

                // Operators are parsed inline in tiny state machines. '=' (61) is
                // often referred to. `finishOp` simply skips the amount of
                // characters it is given as second argument, and returns a token
                // of the type given by its first argument.

                case '/':
                    ReadTokenSlash();
                    return;

                case 37:
                case 42: // '%*'
                    ReadTokenMultiplyModuloExponent(code);
                    return;

                case 124:
                case 38: // '|&'
                    ReadTokenPipeAmp(code);
                    return;

                case 94: // '^'
                    ReadTokenCaret();
                    return;

                case 43:
                case 45: // '+-'
                    ReadTokenPlusMinus(code);
                    return;

                case 60:
                case 62: // '<>'
                    ReadTokenLtGt(code);
                    return;

                case 61:
                case 33: // '=!'
                    ReadTokenEqExcl(code);
                    return;

                case 126: // '~'
                    FinishOperand(TokenType.PrefixOperator, 1);
                    return;
            }

            Raise(pos, "Unexpected character '" + CodePointToString(code) + "'");
        }


        private void FinishOperand(TokenType type, int size)
        {
            var str = input.Substring(pos, size);
            pos += size;
            FinishToken(type, str);
        }

        private void ReadRegexp()
        {
            var escaped = false;
            var inClass = false;
            var start = pos;
            for (;;)
            {
                if (pos >= input.Length)
                {
                    Raise(start, "Unterminated regular expression");
                }

                var ch = input[pos];
                if (Whitespace.LineBreak.IsMatch(ch.ToString()))
                {
                    Raise(start, "Unterminated regular expression");
                }

                if (!escaped)
                {
                    if (ch == '[')
                    {
                        inClass = true;
                    }
                    else if (ch == ']' && inClass)
                    {
                        inClass = false;
                    }
                    else if (ch == '/' && !inClass)
                    {
                        break;
                    }

                    escaped = ch == '\\';
                }
                else
                {
                    escaped = false;
                }

                ++pos;
            }

            var pattern = input.Substring(start, pos - start);
            ++pos;
            var flagsStart = pos;
            var flags = ReadWord1();
            if (containsEsc)
            {
                Unexpected(flagsStart);
            }

            // Validate pattern
            var state = regexpState ?? (regexpState = new RegExpValidationState(this));
            state.Reset(start, pattern, flags);
            ValidateRegExpFlags(state);
            ValidateRegExpPattern(state);

            FinishToken(TokenType.RegExp, new RegExpValue(pattern, flags));
        }

        // Read an integer in the given radix. Return null if zero digits
        // were read, the integer value otherwise. When `len` is given, this
        // will return `null` unless the integer has exactly `len` digits.
        private int? ReadInt(int radix, int? len = null)
        {
            var start = pos;
            var total = 0;
            for (int i = 0, e = len ?? int.MaxValue; i < e; ++i)
            {
                var code = input.CharCodeAt(pos);
                int val;
                if (code >= 97)
                {
                    val = code - 97 + 10; // a
                }
                else if (code >= 65)
                {
                    val = code - 65 + 10; // A
                }
                else if (code >= 48 && code <= 57)
                {
                    val = code - 48;// 0-9
                }
                else
                {
                    break;
                }

                if (val >= radix)
                {
                    break;
                }

                ++pos;
                total = total * radix + val;
            }

            if (pos == start || len != null && pos - start != len)
            {
                return null;
            }

            return total;
        }

        private void ReadRadixNumber(int radix)
        {
            pos += 2;// 0x
            var val = ReadInt(radix);
            if (val == null)
            {
                Raise(start + 2, "Expected number in radix " + radix);
            }

            if (Identifier.IsIdentifierStart(FullCharCodeAtPosition()))
            {
                Raise(pos, "Identifier directly after number");
            }

            FinishToken(TokenType.Number, val);
        }

        // Read an integer, octal integer, or floating-point number.
        private void ReadNumber(bool startsWithDot)
        {
            var start = pos;
            if (!startsWithDot && ReadInt(10) == null)
            {
                Raise(start, "Invalid number");
            }

            var octal = pos - start >= 2 && input[start] == 48;
            if (octal && strict)
            {
                Raise(start, "Invalid number");
            }

            if (octal && Regex.IsMatch(input.Substring(start, pos - start), "[89]"))
            {
                octal = false;
            }

            var next = input.CharCodeAt(pos);
            if (next == 46 && !octal)
            {
                // '.'
                ++pos;
                ReadInt(10);
                next = input.CharCodeAt(pos);
            }

            if ((next == 69 || next == 101) && !octal)
            {
                // 'eE'
                next = input.CharCodeAt(++pos);
                if (next == 43 || next == 45)
                {
                    ++pos; // '+-'
                }

                if (ReadInt(10) == null)
                {
                    Raise(start, "Invalid number");
                }
            }

            if (Identifier.IsIdentifierStart(FullCharCodeAtPosition()))
            {
                Raise(pos, "Identifier directly after number");
            }

            var str = input.Substring(start, pos - start);
            var val = octal ? ParseInt(str, 8) : double.Parse(str);
            FinishToken(TokenType.Number, val);
        }

        private static int ParseInt([NotNull] string str, int radix)
        {
            var value = 0;
            foreach (var c in str)
            {
                int digit;
                if (c >= '0' && c <= '9')
                {
                    digit = c - '0';
                }
                else if (c >= 'a' && c <= 'f')
                {
                    digit = c - 'a' + 10;
                }
                else if (c >= 'A' && c <= 'F')
                {
                    digit = c - 'A' + 10;
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (digit >= radix)
                {
                    throw new NotImplementedException();
                }

                value *= radix;
                value += digit;
            }

            return value;
        }

        // Read a string value, interpreting backslash-escapes.
        private int ReadCodePoint()
        {
            var ch = input.CharCodeAt(pos);
            int code;

            if (ch == 123)
            {
                // '{'
                if (options.EcmaVersion < 6)
                {
                    Unexpected();
                }

                var codePos = ++pos;
                code = ReadHexChar(input.IndexOf("}", pos, StringComparison.Ordinal) - pos);
                ++pos;
                if (code > 0x10FFFF)
                {
                    InvalidStringToken(codePos, "Code point out of bounds");
                }
            }
            else
            {
                code = ReadHexChar(4);
            }

            return code;
        }

        [NotNull]
        private static string CodePointToString(int code)
        {
            // UTF-16 Decoding
            if (code <= 0xFFFF)
            {
                return ((char)code).ToString();
            }

            code -= 0x10000;
            return new string(new[]
            {
                (char)((code >> 10) + 0xD800),
                (char)((code & 1023) + 0xDC00)
            });
        }

        private void ReadString(int quote)
        {
            var @out = "";
            var chunkStart = ++pos;
            for (;;)
            {
                if (pos >= input.Length)
                {
                    Raise(start, "Unterminated string constant");
                }

                var ch = input.CharCodeAt(pos);
                if (ch == quote)
                {
                    break;
                }

                if (ch == 92)
                {
                    // '\'
                    @out += input.Substring(chunkStart, pos - chunkStart);
                    @out += ReadEscapedChar(false);
                    chunkStart = pos;
                }
                else
                {
                    if (Whitespace.IsNewLine(ch, options.EcmaVersion >= 10))
                    {
                        Raise(start, "Unterminated string constant");
                    }

                    ++pos;
                }
            }

            @out += input.Substring(chunkStart, pos++ - chunkStart);
            FinishToken(TokenType.String, @out);
        }

        // Reads template string tokens.
        private sealed class InvalidTemplateEscapeException : Exception
        {
        }

        internal void TryReadTemplateToken()
        {
            inTemplateElement = true;
            try
            {
                ReadTmplToken();
            }
            catch (InvalidTemplateEscapeException)
            {
                ReadInvalidTemplateToken();
            }

            inTemplateElement = false;
        }

        private void InvalidStringToken(int position, string message)
        {
            if (inTemplateElement && options.EcmaVersion >= 9)
            {
                throw new InvalidTemplateEscapeException();
            }

            Raise(position, message);
        }

        private void ReadTmplToken()
        {
            var @out = "";
            var chunkStart = pos;
            for (;;)
            {
                if (pos >= input.Length)
                {
                    Raise(start, "Unterminated template");
                }

                var ch = input.CharCodeAt(pos);
                if (ch == 96 || ch == 36 && input.CharCodeAt(pos + 1) == 123)
                {
                    // '`', '${'

                    if (pos == start && (type == TokenType.Template || type == TokenType.InvalidTemplate))
                    {
                        if (ch == 36)
                        {
                            pos += 2;
                            FinishToken(TokenType.DollarBraceLeft);
                        }
                        else
                        {
                            ++pos;
                            FinishToken(TokenType.BackQuote);
                        }

                        return;
                    }

                    @out += input.Substring(chunkStart, pos - chunkStart);
                    FinishToken(TokenType.Template, @out);
                    return;
                }

                if (ch == 92)
                {
                    // '\'

                    @out += input.Substring(chunkStart, pos - chunkStart);
                    @out += ReadEscapedChar(true);
                    chunkStart = pos;
                }
                else if (Whitespace.IsNewLine(ch))
                {
                    @out += input.Substring(chunkStart, pos - chunkStart);
                    ++pos;
                    switch (ch)
                    {
                        case 13:
                            if (input.CharCodeAt(pos) == 10)
                            {
                                ++pos;
                            }

                            goto case 10;
                        case 10:
                            @out += "\n";
                            break;
                        default:
                            @out += (char)ch;
                            break;
                    }

                    if (options.Locations)
                    {
                        ++curLine;
                        lineStart = pos;
                    }

                    chunkStart = pos;
                }
                else
                {
                    ++pos;
                }
            }
        }

        // Reads a template token to search for the end, without validating any escape sequences
        private void ReadInvalidTemplateToken()
        {
            for (; pos < input.Length; pos++)
            {
                switch (input[pos])
                {
                    case '\\':
                        ++pos;
                        break;

                    case '$':
                        if (input[pos + 1] != '{')
                        {
                            break;
                        }

                        // falls through
                        goto case '`';

                    case '`':
                        FinishToken(TokenType.InvalidTemplate, input.Substring(start, pos - start));
                        return;
                }
            }

            Raise(start, "Unterminated template");
        }

        // Used to read escaped characters
        [NotNull]
        private string ReadEscapedChar(bool inTemplate)
        {
            var ch = input.CharCodeAt(++pos);
            ++pos;
            switch (ch)
            {
                case 110: return "\n"; // 'n' -> '\n'
                case 114: return "\r"; // 'r' -> '\r'
                case 120: return ((char)ReadHexChar(2)).ToString(); // 'x'
                case 117: return CodePointToString(ReadCodePoint()); // 'u'
                case 116: return "\t"; // 't' -> '\t'
                case 98: return "\b"; // 'b' -> '\b'
                case 118: return "\u000b"; // 'v' -> '\u000b'
                case 102: return "\f"; // 'f' -> '\f'
                case 13:
                    if (input.CharCodeAt(pos) == 10)
                    {
                        ++pos; // '\r\n'
                    }

                    goto case 10;

                case 10: // ' \n'
                    if (options.Locations)
                    {
                        lineStart = pos;
                        ++curLine;
                    }

                    return "";

                default:
                    if (ch >= 48 && ch <= 55)
                    {
                        var octalStr = Regex.Match(input.Substring(pos - 1, Math.Min(3, input.Length - pos + 1)), "^[0-7]+").Value;
                        var octal = ParseInt(octalStr, 8);
                        if (octal > 255)
                        {
                            octalStr = octalStr.Substring(0, octalStr.Length - 1);
                            octal = ParseInt(octalStr, 8);
                        }

                        pos += octalStr.Length - 1;
                        ch = input.CharCodeAt(pos);
                        if ((octalStr != "0" || ch == 56 || ch == 57) && (strict || inTemplate))
                        {
                            InvalidStringToken(
                                pos - 1 - octalStr.Length,
                                inTemplate
                                    ? "Octal literal in template string"
                                    : "Octal literal in strict mode"
                            );
                        }

                        return ((char)octal).ToString();
                    }

                    return ((char)ch).ToString();
            }
        }

        // Used to read character escape sequences ('\x', '\u', '\U').
        private int ReadHexChar(int len)
        {
            var codePos = pos;
            var n = ReadInt(16, len);
            if (n == null)
            {
                InvalidStringToken(codePos, "Bad character escape sequence");
                throw new InvalidOperationException();
            }

            return n.Value;
        }

        // Read an identifier, and return it as a string. Sets `this.containsEsc`
        // to whether the word contained a '\u' escape.
        //
        // Incrementally adds only escaped chars, adding other chunks as-is
        // as a micro-optimization.
        [NotNull]
        private string ReadWord1()
        {
            containsEsc = false;
            var word = "";
            var first = true;
            var chunkStart = pos;
            var astral = options.EcmaVersion >= 6;

            while (pos < input.Length)
            {
                var ch = FullCharCodeAtPosition();
                if (Identifier.IsIdentifierChar(ch, astral))
                {
                    pos += ch <= 0xffff ? 1 : 2;
                }
                else if (ch == '\\')
                {
                    containsEsc = true;
                    word += input.Substring(chunkStart, pos - chunkStart);
                    var escStart = pos;
                    if (input.CharCodeAt(++pos) != 'u')
                    {
                        InvalidStringToken(pos, "Expecting Unicode escape sequence \\uXXXX");
                    }

                    ++pos;
                    var esc = ReadCodePoint();
                    if (first && !Identifier.IsIdentifierStart(esc, astral) ||
                        !first && !Identifier.IsIdentifierChar(esc, astral))
                    {
                        InvalidStringToken(escStart, "Invalid Unicode escape");
                    }

                    word += CodePointToString(esc);
                    chunkStart = pos;
                }
                else
                {
                    break;
                }

                first = false;
            }

            return word + input.Substring(chunkStart, pos - chunkStart);
        }

        // Read an identifier or keyword token. Will check for reserved
        // words when necessary.
        private void ReadWord()
        {
            var word = ReadWord1();
            var tokenType = TokenType.Name;
            if (keywords.IsMatch(word))
            {
                if (containsEsc)
                {
                    RaiseRecoverable(start, "Escape sequence in keyword " + word);
                }

                tokenType = TokenType.Keywords[word];
            }

            FinishToken(tokenType, word);
        }

        // The functions in this module keep track of declared variables in the current scope in order to detect duplicate variable names.
        private void EnterScope(ScopeFlags flags)
        {
            scopeStack.Add(new Scope(flags));
        }

        private void ExitScope()
        {
            scopeStack.RemoveAt(scopeStack.Count - 1);
        }

        private void DeclareName(string name, BindType bindingType, int pos)
        {
            var redeclared = false;
            if (bindingType == BindType.Lexical)
            {
                var scope = CurrentScope;
                redeclared = scope.Lexical.IndexOf(name) > -1 || scope.Var.IndexOf(name) > -1;
                scope.Lexical.Add(name);
            }
            else if (bindingType == BindType.SimpleCatch)
            {
                var scope = CurrentScope;
                scope.Lexical.Add(name);
            }
            else if (bindingType == BindType.Function)
            {
                var scope = CurrentScope;
                redeclared = scope.Lexical.IndexOf(name) > -1;
                scope.Var.Add(name);
            }
            else
            {
                for (var i = scopeStack.Count - 1; i >= 0; --i)
                {
                    var scope = scopeStack[i];
                    if (scope.Lexical.IndexOf(name) > -1 && (scope.Flags & ScopeFlags.SimpleCatch) == 0 && scope.Lexical[0] == name)
                    {
                        redeclared = true;
                    }

                    scope.Var.Add(name);
                    if ((scope.Flags & ScopeFlags.Var) != 0)
                    {
                        break;
                    }
                }
            }

            if (redeclared)
            {
                RaiseRecoverable(pos,  $"Identifier '{name}' has already been declared");
            }
        }

        [NotNull]
        private Scope CurrentScope => scopeStack[scopeStack.Count - 1];

        [NotNull]
        private Scope CurrentVarScope
        {
            get
            {
                for (var i = scopeStack.Count - 1; ; i--)
                {
                    var scope = scopeStack[i];
                    if ((scope.Flags & ScopeFlags.Var) != 0)
                    {
                        return scope;
                    }
                }
            }
        }

        // Could be useful for `this`, `new.target`, `super()`, `super.property`, and `super[property]`.
        [NotNull]
        private Scope CurrentThisScope
        {
            get
            {
                for (var i = scopeStack.Count - 1;; i--)
                {
                    var scope = scopeStack[i];
                    if ((scope.Flags & ScopeFlags.Var) != 0 && (scope.Flags & ScopeFlags.Arrow) == 0)
                    {
                        return scope;
                    }
                }
            }
        }

        //pp.startNodeAt = function(pos, loc) {
        //  return new Node(this, pos, loc)
        //}

        // Finish an AST node, adding `type` and `end` properties.
        [NotNull]
        private T FinishNode<T>([NotNull] T node)
            where T : BaseNode
        {
            node.Finish(this, lastTokEnd, lastTokEndLoc);
            return node;
        }

        private static ScopeFlags FunctionFlags(bool async, bool generator)
        {
            return ScopeFlags.Function | (async ? ScopeFlags.Async : 0) | (generator ? ScopeFlags.Generator : 0);
        }

        // The `getLineInfo` function is mostly useful when the
        // `locations` option is off (for performance reasons) and you
        // want to find the line/column position for a given character
        // offset. `input` should be the code string that the offset refers
        // into.
        private static Position GetLineInfo([NotNull] string input, int offset)
        {
            for (int line = 1, cur = 0;;)
            {
                var match = Whitespace.LineBreak.Match(input, cur);
                if (match.Success && match.Index < offset)
                {
                    ++line;
                    cur = match.Index + match.Length;
                }
                else
                {
                    return new Position(line, offset - cur);
                }
            }
        }

        public Options Options => options;
        public string SourceFile { get; }
    }
}