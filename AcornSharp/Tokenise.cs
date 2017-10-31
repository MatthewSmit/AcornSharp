using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AcornSharp
{
    // Object type used to represent tokens. Note that normally, tokens
    // simply exist as properties on the parser object. This is only
    // used for the onToken callback and the external tokenizer.

    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public sealed partial class Parser : IEnumerable<Token>
    {
        // Move to the next token
        private void next()
        {
            Options.onToken?.Invoke(new Token(type, value, new SourceLocation(start, end, sourceFile)));

            lastTokEnd = end;
            lastTokStart = start;
            nextToken();
        }

        private Token getToken()
        {
            next();
            return new Token(type, value, new SourceLocation(start, end, sourceFile));
        }

        public IEnumerator<Token> GetEnumerator()
        {
            Token token;
            do
            {
                token = getToken();
                yield return token;
            } while (token.Type != TokenType.EOF);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Toggle strict mode. Re-reads the next number or string to please
        // pedantic tests (`"use strict"; 010;` should fail).
        private TokContext curContext()
        {
            return context[context.Count - 1];
        }

        // Read a single token, updating the parser object's token-related
        // properties.
        internal void nextToken()
        {
            var curContext = this.curContext();
            if (curContext == null || curContext.PreserveSpace != true) skipSpace();

            start = curPosition();
            if (pos.Index >= input.Length)
            {
                finishToken(TokenType.EOF);
                return;
            }

            if (curContext?.Override != null) curContext.Override(this);
            else readToken(fullCharCodeAtPos());
        }

        private void readToken(int code)
        {
            // Identifier or keyword. '\uXXXX' sequences are allowed in
            // identifiers, so '\' also dispatches to that.
            if (isIdentifierStart(code, Options.ecmaVersion >= 6) || code == 92 /* '\' */)
            {
                readWord();
                return;
            }

            getTokenFromCode(code);
        }

        private int fullCharCodeAtPos()
        {
            if (pos.Index >= input.Length)
                return 0;

            return char.ConvertToUtf32(input, pos.Index);
        }

        private void skipBlockComment()
        {
            var start = pos;
            pos = pos.Increment(2);
            var end = input.IndexOf("*/", pos.Index, StringComparison.Ordinal);
            if (end == -1) raise(pos.Increment(-2), "Unterminated comment");
            pos = new Position(pos.Line, pos.Column + (end - start.Index), end + 2);
            var lastIndex = start.Index;
            while (true)
            {
                var match = lineBreak.Match(input, lastIndex);
                if (!match.Success || match.Index >= pos.Index)
                    break;

                var lineStart = match.Index + match.Length;
                pos = new Position(pos.Line + 1, pos.Index - lineStart, pos.Index);
                lastIndex = lineStart;
            }
            Options.onComment?.Invoke(true, input.Substring(start.Index + 2, end - (start.Index + 2)), new SourceLocation(start, curPosition(), sourceFile));
        }

        private void skipLineComment(int startSkip)
        {
            var start = pos;
            pos = pos.Increment(startSkip);
            var ch = input.Get(pos.Index);
            while (pos.Index < input.Length && !isNewLine(ch))
            {
                pos = pos.Increment(1);
                ch = input.Get(pos.Index);
            }
            Options.onComment?.Invoke(false, input.Substring(start.Index + startSkip, pos.Index - (start.Index + startSkip)), new SourceLocation(start, curPosition(), sourceFile));
        }

        // Called at the start of the parse and after every token. Skips
        // whitespace and comments, and.
        private void skipSpace()
        {
            while (pos.Index < input.Length)
            {
                var ch = (int)input[pos.Index];
                switch (ch)
                {
                    case 32:
                    case 160: // ' '
                        pos = pos.Increment(1);
                        break;
                    case 13:
                        if (input[pos.Index + 1] == 10)
                            pos = pos.Increment(1);
                        goto case 10;
                    case 10:
                    case 8232:
                    case 8233:
                        pos = new Position(pos.Line + 1, 0, pos.Index + 1);
                        break;
                    case 47: // '/'
                        switch ((int)input.Get(pos.Index + 1))
                        {
                            case 42: // '*'
                                skipBlockComment();
                                break;
                            case 47:
                                skipLineComment(2);
                                break;
                            default:
                                return;
                        }
                        break;
                    default:
                        if (ch > 8 && ch < 14 || ch >= 5760 && nonASCIIwhitespace.IsMatch(((char)ch).ToString()))
                        {
                            pos = pos.Increment(1);
                            break;
                        }
                        else
                        {
                            return;
                        }
                }
            }
        }

        // Called at the end of every token. Sets `end`, `val`, and
        // maintains `context` and `exprAllowed`, and skips the space after
        // the token, so that the next one's `start` will point at the
        // right position.
        private void finishToken(TokenType type, object val = null)
        {
            end = curPosition();
            var prevType = this.type;
            this.type = type;
            value = val;

            updateContext(prevType);
        }

        // ### Token reading

        // This is the function that is called to fetch the next token. It
        // is somewhat obscure, because it works in character codes rather
        // than characters, and because operator parsing has been inlined
        // into it.
        //
        // All in the name of speed.
        //
        private void readToken_dot()
        {
            var next = input[pos.Index + 1];
            if (next >= 48 && next <= 57)
            {
                readNumber(true);
                return;
            }
            var next2 = input[pos.Index + 2];
            if (Options.ecmaVersion >= 6 && next == 46 && next2 == 46)
            {
                // 46 = dot '.'
                pos = pos.Increment(3);
                finishToken(TokenType.ellipsis);
            }
            else
            {
                pos = pos.Increment(1);
                finishToken(TokenType.dot);
            }
        }

        private void readToken_slash() { // '/'
            var next = input.Get(pos.Index + 1);
            if (exprAllowed)
            {
                pos = pos.Increment(1);
                readRegexp();
                return;
            }
            if (next == 61) finishOp(TokenType.assign, 2);
            else finishOp(TokenType.slash, 1);
        }

        private void readToken_mult_modulo_exp(int code)
        {
            // '%*'
            var next = input.Get(pos.Index + 1);
            var size = 1;
            var tokentype = code == 42 ? TokenType.star : TokenType.modulo;

            // exponentiation operator ** and **=
            if (Options.ecmaVersion >= 7 && code == 42 && next == 42)
            {
                ++size;
                tokentype = TokenType.starstar;
                next = input[pos.Index + 2];
            }

            if (next == 61) finishOp(TokenType.assign, size + 1);
            else finishOp(tokentype, size);
        }

        private void readToken_pipe_amp(int code)
        {
            // '|&'
            var next = input[pos.Index + 1];
            if (next == code) finishOp(code == 124 ? TokenType.logicalOR : TokenType.logicalAND, 2);
            else if (next == 61) finishOp(TokenType.assign, 2);
            else finishOp(code == 124 ? TokenType.bitwiseOR : TokenType.bitwiseAND, 1);
        }

        private void readToken_caret()
        {
            // '^'
            var next = input[pos.Index + 1];
            if (next == 61) finishOp(TokenType.assign, 2);
            else finishOp(TokenType.bitwiseXOR, 1);
        }

        private void readToken_plus_min(int code)
        {
            // '+-'
            var next = input[pos.Index + 1];
            if (next == code)
            {
                if (next == 45 && !inModule && input.Get(pos.Index + 2) == 62 &&
                    (lastTokEnd.Index == 0 || lineBreak.IsMatch(input.Substring(lastTokEnd.Index, pos - lastTokEnd))))
                {
                    // A `-->` line comment
                    skipLineComment(3);
                    skipSpace();
                    nextToken();
                    return;
                }
                finishOp(TokenType.incDec, 2);
            }
            else if (next == 61) finishOp(TokenType.assign, 2);
            else finishOp(TokenType.plusMin, 1);
        }

        private void readToken_lt_gt(int code)
        {
            // '<>'
            var next = input[pos.Index + 1];
            var size = 1;
            if (next == code)
            {
                size = code == 62 && input[pos.Index + 2] == 62 ? 3 : 2;
                if (input[pos.Index + size] == 61) finishOp(TokenType.assign, size + 1);
                else finishOp(TokenType.bitShift, size);
                return;
            }
            if (next == 33 && code == 60 && !inModule && input[pos.Index + 2] == 45 &&
                input[pos.Index + 3] == 45)
            {
                // `<!--`, an XML-style comment that should be interpreted as a line comment
                skipLineComment(4);
                skipSpace();
                nextToken();
                return;
            }
            if (next == 61) size = 2;
            finishOp(TokenType.relational, size);
        }

        private void readToken_eq_excl(int code)
        {
            // '=!'
            var next = input[pos.Index + 1];
            if (next == 61) finishOp(TokenType.equality, input[pos.Index + 2] == 61 ? 3 : 2);
            else if (code == 61 && next == 62 && Options.ecmaVersion >= 6)
            {
                // '=>'
                pos = pos.Increment(2);
                finishToken(TokenType.arrow);
            }
            else finishOp(code == 61 ? TokenType.eq : TokenType.prefix, 1);
        }

        private void getTokenFromCode(int code)
        {
            switch (code)
            {
                // The interpretation of a dot depends on whether it is followed
                // by a digit or another two dots.
                case 46: // '.'
                    readToken_dot();
                    return;

                // Punctuation tokens.
                case 40:
                    pos = pos.Increment(1);
                    finishToken(TokenType.parenL);
                    return;
                case 41:
                    pos = pos.Increment(1);
                    finishToken(TokenType.parenR);
                    return;
                case 59:
                    pos = pos.Increment(1);
                    finishToken(TokenType.semi);
                    return;
                case 44:
                    pos = pos.Increment(1);
                    finishToken(TokenType.comma);
                    return;
                case 91:
                    pos = pos.Increment(1);
                    finishToken(TokenType.bracketL);
                    return;
                case 93:
                    pos = pos.Increment(1);
                    finishToken(TokenType.bracketR);
                    return;
                case 123:
                    pos = pos.Increment(1);
                    finishToken(TokenType.braceL);
                    return;
                case 125:
                    pos = pos.Increment(1);
                    finishToken(TokenType.braceR);
                    return;
                case 58:
                    pos = pos.Increment(1);
                    finishToken(TokenType.colon);
                    return;
                case 63:
                    pos = pos.Increment(1);
                    finishToken(TokenType.question);
                    return;

                case 96: // '`'
                    if (Options.ecmaVersion < 6) break;
                    pos = pos.Increment(1);
                    finishToken(TokenType.backQuote);
                    return;

                case 48: // '0'
                    var next = input.Get(pos.Index + 1);
                    if (next == 120 || next == 88)
                    {
                        readRadixNumber(16); // '0x', '0X' - hex number
                        return;
                    }
                    if (Options.ecmaVersion >= 6)
                    {
                        if (next == 111 || next == 79) {
                            readRadixNumber(8); // '0o', '0O' - octal number
                            return;
                        }
                        if (next == 98 || next == 66) {
                            readRadixNumber(2); // '0b', '0B' - binary number
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
                    readNumber(false);
                    return;

                // Quotes produce strings.
                case 34:
                case 39: // '"', "'"
                    readString(code);
                    return;

                // Operators are parsed inline in tiny state machines. '=' (61) is
                // often referred to. `finishOp` simply skips the amount of
                // characters it is given as second argument, and returns a token
                // of the type given by its first argument.

                case 47: // '/'
                    readToken_slash();
                    return;

                case 37:
                case 42: // '%*'
                    readToken_mult_modulo_exp(code);
                    return;

                case 124:
                case 38: // '|&'
                    readToken_pipe_amp(code);
                    return;

                case 94: // '^'
                    readToken_caret();
                    return;

                case 43:
                case 45: // '+-'
                    readToken_plus_min(code);
                    return;

                case 60:
                case 62: // '<>'
                    readToken_lt_gt(code);
                    return;

                case 61:
                case 33: // '=!'
                    readToken_eq_excl(code);
                    return;

                case 126: // '~'
                    finishOp(TokenType.prefix, 1);
                    return;
            }

            raise(pos, "Unexpected character '" + codePointToString(code) + "'");
        }

        private void finishOp(TokenType type, int size)
        {
            var str = input.Substring(pos.Index, size);
            pos = pos.Increment(size);
            finishToken(type, str);
        }

        private void readRegexp()
        {
            var escaped = false;
            var inClass = false;
            var start = pos;
            while (true)
            {
                if (pos.Index >= input.Length) raise(start, "Unterminated regular expression");
                var ch = input[pos.Index];
                if (lineBreak.IsMatch(ch.ToString())) raise(start, "Unterminated regular expression");
                if (!escaped)
                {
                    if (ch == '[') inClass = true;
                    else if (ch == ']' && inClass) inClass = false;
                    else if (ch == '/' && !inClass) break;
                    escaped = ch == '\\';
                }
                else escaped = false;
                pos = pos.Increment(1);
            }

            var content = input.Substring(start.Index, pos - start);
            pos = pos.Increment(1);
            // Need to use `readWord1` because '\uXXXX' sequences are allowed
            // here (don't ask).
            var mods = readWord1();
            if (!string.IsNullOrEmpty(mods))
            {
                var validFlags = new Regex("^[gim]*$");
                if (Options.ecmaVersion >= 6) validFlags = new Regex("^[gimuy]*$");
                if (!validFlags.IsMatch(mods)) raise(start, "Invalid regular expression flag");
            }
            finishToken(TokenType.regexp, new RegexNode
            {
                pattern = content,
                flags = mods
            });
        }

        // Read an integer in the given radix. Return null if zero digits
        // were read, the integer value otherwise. When `len` is given, this
        // will return `null` unless the integer has exactly `len` digits.
        private int? readInt(int radix, int? len = null)
        {
            var total = 0;
            var start = pos;
            for (var i = 0; !len.HasValue || i < len; ++i)
            {
                var code = input.Get(pos.Index);
                int val;
                if (code >= 97) val = code - 97 + 10; // a
                else if (code >= 65) val = code - 65 + 10; // A
                else if (code >= 48 && code <= 57) val = code - 48; // 0-9
                else val = int.MaxValue;
                if (val >= radix) break;
                pos = pos.Increment(1);
                total = total * radix + val;
            }

            if (pos == start || len != null && pos - start != len) return null;
            return total;
        }

        private void readRadixNumber(int radix)
        {
            pos = pos.Increment(2); // 0x
            var val = readInt(radix);
            if (!val.HasValue)
            {
                raise(start.Increment(2), "Expected number in radix " + radix);
            }
            if (isIdentifierStart(fullCharCodeAtPos())) raise(pos, "Identifier directly after number");
            finishToken(TokenType.num, val.Value);
        }

        // Read an integer, octal integer, or floating-point number.
        private void readNumber(bool startsWithDot)
        {
            var start = pos;
            var isFloat = false;
            var octal = input[pos.Index] == 48;
            if (!startsWithDot && readInt(10) == null) raise(start, "Invalid number");
            if (octal && pos.Index == start.Index + 1) octal = false;
            var next = input.Get(pos.Index);

            if (next == 46 && !octal)
            {
                // '.'
                pos = pos.Increment(1);
                readInt(10);
                isFloat = true;
                next = input.Get(pos.Index);
            }

            if ((next == 69 || next == 101) && !octal)
            {
                // 'eE'
                pos = pos.Increment(1);
                next = input.Get(pos.Index);
                if (next == 43 || next == 45) pos = pos.Increment(1);// '+-'
                if (readInt(10) == null) raise(start, "Invalid number");
                isFloat = true;
            }
            if (isIdentifierStart(fullCharCodeAtPos())) raise(pos, "Identifier directly after number");

            var str = input.Substring(start.Index, pos - start);
            object val = null;
            if (isFloat) val = double.Parse(str);
            else if (!octal || str.Length == 1) val = parseInt(str, 10);
            else if (strict)
            {
                raise(start, "Invalid number");
            }
            else if (Regex.IsMatch(str, "[89]")) val = parseInt(str, 10);
            else val = parseInt(str, 8);
            finishToken(TokenType.num, val);
        }

        // Read a string value, interpreting backslash-escapes.
        private int readCodePoint()
        {
            var ch = input.Get(pos.Index);
            int code;

            if (ch == 123)
            {
                // '{'
                if (Options.ecmaVersion < 6)
                {
                    raise(start, "Unexpected token");
                }
                var codePos = pos = pos.Increment(1);
                code = readHexChar(input.IndexOf("}", pos.Index, StringComparison.Ordinal) - pos.Index);
                pos = pos.Increment(1);
                if (code > 0x10FFFF) invalidStringToken(codePos, "Code point out of bounds");
            }
            else
            {
                code = readHexChar(4);
            }
            return code;
        }

        private static string codePointToString(int code)
        {
            return char.ConvertFromUtf32(code);
        }

        private void readString(int quote)
        {
            var @out = "";
            var chunkStart = pos = pos.Increment(1);
            while (true)
            {
                if (pos.Index >= input.Length) raise(start, "Unterminated string constant");
                var ch = input[pos.Index];
                if (ch == quote) break;
                if (ch == 92)
                {
                    // '\'
                    @out += input.Substring(chunkStart.Index, pos - chunkStart);
                    @out += readEscapedChar(false);
                    chunkStart = pos;
                }
                else
                {
                    if (isNewLine(ch)) raise(start, "Unterminated string constant");
                    pos = pos.Increment(1);
                }
            }
            @out += input.Substring(chunkStart.Index, pos - chunkStart);
            pos = pos.Increment(1);
            finishToken(TokenType.@string, @out);
        }

        // Reads template string tokens.
        private sealed class INVALID_TEMPLATE_ESCAPE_ERROR : Exception
        {
        }

        public void tryReadTemplateToken()
        {
            inTemplateElement = true;
            try
            {
                readTmplToken();
            }
            catch (INVALID_TEMPLATE_ESCAPE_ERROR)
            {
                readInvalidTemplateToken();
            }

            inTemplateElement = false;
        }

        private void invalidStringToken(Position position, string message)
        {
            if (inTemplateElement && Options.ecmaVersion >= 9)
            {
                throw new INVALID_TEMPLATE_ESCAPE_ERROR();
            }

            raise(position, message);
        }

        private void readTmplToken()
        {
            var @out = "";
            var chunkStart = pos;
            while (true)
            {
                if (pos.Index >= input.Length) raise(start, "Unterminated template");
                var ch = input[pos.Index];
                if (ch == 96 || ch == 36 && input[pos.Index + 1] == 123)
                {
                    // '`', '${'
                    if (pos.Index == start.Index && (type == TokenType.template || type == TokenType.invalidTemplate))
                    {
                        if (ch == 36)
                        {
                            pos = pos.Increment(2);
                            finishToken(TokenType.dollarBraceL);
                            return;
                        }

                        pos = pos.Increment(1);
                        finishToken(TokenType.backQuote);
                        return;
                    }
                    @out += input.Substring(chunkStart.Index, pos - chunkStart);
                    finishToken(TokenType.template, @out);
                    return;
                }
                if (ch == 92)
                {
                    // '\'
                    @out += input.Substring(chunkStart.Index, pos - chunkStart);
                    @out += readEscapedChar(true);
                    chunkStart = pos;
                }
                else if (isNewLine(ch))
                {
                    @out += input.Substring(chunkStart.Index, pos - chunkStart);
                    pos = pos.Increment(1);
                    switch ((int)ch)
                    {
                        case 13:
                            if (input[pos.Index] == 10)
                                pos = pos.Increment(1);
                            goto case 10;
                        case 10:
                            @out += "\n";
                            break;
                        default:
                            @out += ch.ToString();
                            break;
                    }
                    pos = new Position(pos.Line + 1, 0, pos.Index);
                    chunkStart = pos;
                }
                else
                {
                    pos = pos.Increment(1);
                }
            }
        }
        // Reads a template token to search for the end, without validating any escape sequences
        private void readInvalidTemplateToken()
        {
            for (; pos.Index < input.Length; pos = pos.Increment(1))
            {
                switch (input[pos.Index])
                {
                    case '\\':
                        pos = pos.Increment(1);
                        break;

                    case '$':
                        if (input[pos.Index + 1] != '{')
                        {
                            break;
                        }
                        goto case '`';
                    // falls through

                    case '`':
                        finishToken(TokenType.invalidTemplate, input.Substring(start.Index, pos - start));
                        return;

                    // no default
                }
            }
            raise(start, "Unterminated template");
        }

        // Used to read escaped characters
        private string readEscapedChar(bool inTemplate)
        {
            pos = pos.Increment(1);
            var ch = input.Get(pos.Index);
            pos = pos.Increment(1);
            switch ((int)ch)
            {
                case 110: return "\n"; // 'n' -> '\n'
                case 114: return "\r"; // 'r' -> '\r'
                case 120: return ((char)readHexChar(2)).ToString(); // 'x'
                case 117: return codePointToString(readCodePoint()); // 'u'
                case 116: return "\t"; // 't' -> '\t'
                case 98: return "\b"; // 'b' -> '\b'
                case 118: return "\u000b"; // 'v' -> '\u000b'
                case 102: return "\f"; // 'f' -> '\f'
                case 13:
                    if (input[pos.Index] == 10)
                        pos = pos.Increment(1); // '\r\n'
                    goto case 10;
                case 10: // ' \n'
                    pos = new Position(pos.Line + 1, 0, pos.Index);
                    return "";
                default:
                    if (ch >= 48 && ch <= 55)
                    {
                        var octalStr = new Regex("^[0-7]+").Match(input.Substring(pos.Index - 1, Math.Min(3, input.Length - pos.Index + 1))).Groups[0].Value;
                        var octal = parseInt(octalStr, 8);
                        if (octal > 255)
                        {
                            octalStr = octalStr.Substring(0, octalStr.Length - 1);
                            octal = parseInt(octalStr, 8);
                        }
                        if (octalStr != "0" && (strict || inTemplate))
                        {
                            invalidStringToken(pos.Increment(-2), "Octal literal in strict mode");
                        }
                        pos = pos.Increment(octalStr.Length - 1);
                        return ((char)octal).ToString();
                    }
                    return ch.ToString();
            }
        }

        private static int parseInt(string str, int @base)
        {
            if (@base > 10)
                throw new NotImplementedException();

            const string numbers = "0123456789";
            var number = 0;
            foreach (var c in str)
            {
                number *= @base;
                var index = numbers.IndexOf(c, 0, @base);
                if (index < 0)
                    throw new NotImplementedException();
                number += index;
            }

            return number;
        }

        // Used to read character escape sequences ('\x', '\u', '\U').
        private int readHexChar(int len)
        {
            var codePos = pos;
            var n = readInt(16, len);
            if (!n.HasValue)
            {
                invalidStringToken(codePos, "Bad character escape sequence");
                return 0;
            }
            return n.Value;
        }

        // Read an identifier, and return it as a string. Sets `this.containsEsc`
        // to whether the word contained a '\u' escape.
        //
        // Incrementally adds only escaped chars, adding other chunks as-is
        // as a micro-optimization.
        private string readWord1()
        {
            containsEsc = false;
            var word = "";
            var first = true;
            var chunkStart = pos;
            var astral = Options.ecmaVersion >= 6;
            while (pos.Index < input.Length)
            {
                var ch = fullCharCodeAtPos();
                if (isIdentifierChar(ch, astral))
                {
                    pos = pos.Increment(ch <= 0xffff ? 1 : 2);
                }
                else if (ch == 92)
                {
                    // "\"
                    containsEsc = true;
                    word += input.Substring(chunkStart.Index, pos - chunkStart);
                    var escStart = pos;
                    pos = pos.Increment(1);
                    if (input.Get(pos.Index) != 117) // "u"
                        invalidStringToken(pos, "Expecting Unicode escape sequence \\uXXXX");
                    pos = pos.Increment(1);
                    var esc = readCodePoint();
                    if (!(first ? (Func<int, bool, bool>)isIdentifierStart : isIdentifierChar)(esc, astral))
                        invalidStringToken(escStart, "Invalid Unicode escape");
                    word += codePointToString(esc);
                    chunkStart = pos;
                }
                else
                {
                    break;
                }
                first = false;
            }
            return word + input.Substring(chunkStart.Index, pos - chunkStart);
        }

        // Read an identifier or keyword token. Will check for reserved
        // words when necessary.
        private void readWord()
        {
            var word = readWord1();
            var type = TokenType.name;
            if (keywords.IsMatch(word))
            {
                if (containsEsc) raiseRecoverable(start, "Escape sequence in keyword " + word);
                type = TokenInformation.Keywords[word];
            }
            finishToken(type, word);
        }
    }
}
