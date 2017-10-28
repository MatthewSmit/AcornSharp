using System;
using System.Collections.Generic;

namespace AcornSharp
{
    internal sealed class TokenInformation
    {
        private static readonly Dictionary<TokenType, TokenInformation> types;

        private static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            {"break", TokenType._break},
            {"case", TokenType._case},
            {"catch", TokenType._catch},
            {"continue", TokenType._continue},
            {"debugger", TokenType._debugger},
            {"default", TokenType._default},
            {"do", TokenType._do},
            {"else", TokenType._else},
            {"finally", TokenType._finally},
            {"for", TokenType._for},
            {"function", TokenType._function},
            {"if", TokenType._if},
            {"return", TokenType._return},
            {"switch", TokenType._switch},
            {"throw", TokenType._throw},
            {"try", TokenType._try},
            {"var", TokenType._var},
            {"const", TokenType._const},
            {"while", TokenType._while},
            {"with", TokenType._with},
            {"new", TokenType._new},
            {"this", TokenType._this},
            {"super", TokenType._super},
            {"class", TokenType._class},
            {"extends", TokenType._extends},
            {"export", TokenType._export},
            {"import", TokenType._import},
            {"null", TokenType._null},
            {"true", TokenType._true},
            {"false", TokenType._false},
            {"in", TokenType._in},
            {"instanceof", TokenType._instanceof},
            {"typeof", TokenType._typeof},
            {"void", TokenType._void},
            {"delete", TokenType._delete}
        };

        static TokenInformation()
        {
            var num = new TokenInformation(startsExpr: true);
            var regexp = new TokenInformation(startsExpr: true);
            var @string = new TokenInformation(startsExpr: true);
            var name = new TokenInformation(startsExpr: true);
            var eof = new TokenInformation();

            // Punctuation token types.
            var bracketL = new TokenInformation(beforeExpr: true, startsExpr: true);

            var bracketR = new TokenInformation();
            var braceL = new TokenInformation(beforeExpr: true, startsExpr: true);
            var braceR = new TokenInformation();
            var parenL = new TokenInformation(beforeExpr: true, startsExpr: true);
            var parenR = new TokenInformation();
            var comma = new TokenInformation(beforeExpr: true);
            var semi = new TokenInformation(beforeExpr: true);
            var colon = new TokenInformation(beforeExpr: true);
            var dot = new TokenInformation();
            var question = new TokenInformation(beforeExpr: true);
            var arrow = new TokenInformation(beforeExpr: true);
            var template = new TokenInformation();
            var invalidTemplate = new TokenInformation();
            var ellipsis = new TokenInformation(beforeExpr: true);
            var backQuote = new TokenInformation(startsExpr: true);
            var dollarBraceL = new TokenInformation(beforeExpr: true, startsExpr: true);

            var eq = new TokenInformation(beforeExpr: true);
            var assign = new TokenInformation(beforeExpr: true);
            var incDec = new TokenInformation(prefix: true, postfix: true, startsExpr: true);
            var prefix = new TokenInformation(beforeExpr: true, prefix: true, startsExpr: true);
            var logicalOR = CreateBinaryOperation(1);
            var logicalAND = CreateBinaryOperation(2);
            var bitwiseOR = CreateBinaryOperation(3);
            var bitwiseXOR = CreateBinaryOperation(4);
            var bitwiseAND = CreateBinaryOperation(5);
            var equality = CreateBinaryOperation(6);
            var relational = CreateBinaryOperation(7);
            var bitShift = CreateBinaryOperation(8);
            var plusMin = new TokenInformation(beforeExpr: true, binaryOperation: 9, prefix: true, startsExpr: true);
            var modulo = CreateBinaryOperation(10);
            var star = CreateBinaryOperation(10);
            var slash = CreateBinaryOperation(10);
            var starstar = new TokenInformation(beforeExpr: true);

            // Keyword token types.
            var _break = CreateKeyword("break");

            var _case = CreateKeyword("case", true);
            var _catch = CreateKeyword("catch");
            var _continue = CreateKeyword("continue");
            var _debugger = CreateKeyword("debugger");
            var _default = CreateKeyword("default", true);
            var _do = CreateKeyword("do", isLoop: true, beforeExpr: true);
            var _else = CreateKeyword("else", true);
            var _finally = CreateKeyword("finally");
            var _for = CreateKeyword("for", isLoop: true);
            var _function = CreateKeyword("function", startsExpr: true);
            var _if = CreateKeyword("if");
            var _return = CreateKeyword("return", true);
            var _switch = CreateKeyword("switch");
            var _throw = CreateKeyword("throw", true);
            var _try = CreateKeyword("try");
            var _var = CreateKeyword("var");
            var _const = CreateKeyword("const");
            var _while = CreateKeyword("while", isLoop: true);
            var _with = CreateKeyword("with");
            var _new = CreateKeyword("new", true, true);
            var _this = CreateKeyword("this", startsExpr: true);
            var _super = CreateKeyword("super", startsExpr: true);
            var _class = CreateKeyword("class", startsExpr: true);
            var _extends = CreateKeyword("extends", true);
            var _export = CreateKeyword("export");
            var _import = CreateKeyword("import");
            var _null = CreateKeyword("null", startsExpr: true);
            var _true = CreateKeyword("true", startsExpr: true);
            var _false = CreateKeyword("false", startsExpr: true);
            var _in = CreateKeyword("in", true, binop: 7);
            var _instanceof = CreateKeyword("instanceof", true, binop: 7);
            var _typeof = CreateKeyword("typeof", true, prefix: true, startsExpr: true);
            var _void = CreateKeyword("void", true, prefix: true, startsExpr: true);
            var _delete = CreateKeyword("delete", true, prefix: true, startsExpr: true);

            types = new Dictionary<TokenType, TokenInformation>
            {
                {TokenType.num, num},
                {TokenType.regexp, regexp},
                {TokenType.@string, @string},
                {TokenType.name, name},
                {TokenType.eof, eof},

                // Punctuation token types.
                {TokenType.bracketL, bracketL},
                {TokenType.bracketR, bracketR},
                {TokenType.braceL, braceL},
                {TokenType.braceR, braceR},
                {TokenType.parenL, parenL},
                {TokenType.parenR, parenR},
                {TokenType.comma, comma},
                {TokenType.semi, semi},
                {TokenType.colon, colon},
                {TokenType.dot, dot},
                {TokenType.question, question},
                {TokenType.arrow, arrow},
                {TokenType.template, template},
                {TokenType.invalidTemplate, invalidTemplate},
                {TokenType.ellipsis, ellipsis},
                {TokenType.backQuote, backQuote},
                {TokenType.dollarBraceL, dollarBraceL},

                // Operators. These carry several kinds of properties to help the
                // parser use them properly (the presence of these properties is
                // what categorizes them as operators).
                //
                // `binop`, when present, specifies that this operator is a binary
                // operator, and will refer to its precedence.
                //
                // `prefix` and `postfix` mark the operator as a prefix or postfix
                // unary operator.
                //
                // `isAssign` marks all of `=`, `+=`, `-=` etcetera, which act as
                // binary operators with a very low precedence, that should result
                // in AssignmentExpression nodes.

                {TokenType.eq, eq},
                {TokenType.assign, assign},
                {TokenType.incDec, incDec},
                {TokenType.prefix, prefix},
                {TokenType.logicalOR, logicalOR},
                {TokenType.logicalAND, logicalAND},
                {TokenType.bitwiseOR, bitwiseOR},
                {TokenType.bitwiseXOR, bitwiseXOR},
                {TokenType.bitwiseAND, bitwiseAND},
                {TokenType.equality, equality},
                {TokenType.relational, relational},
                {TokenType.bitShift, bitShift},
                {TokenType.plusMin, plusMin},
                {TokenType.modulo, modulo},
                {TokenType.star, star},
                {TokenType.slash, slash},
                {TokenType.starstar, starstar},

                // Keyword token types.
                {TokenType._break, _break},
                {TokenType._case, _case},
                {TokenType._catch, _catch},
                {TokenType._continue, _continue},
                {TokenType._debugger, _debugger},
                {TokenType._default, _default},
                {TokenType._do, _do},
                {TokenType._else, _else},
                {TokenType._finally, _finally},
                {TokenType._for, _for},
                {TokenType._function, _function},
                {TokenType._if, _if},
                {TokenType._return, _return},
                {TokenType._switch, _switch},
                {TokenType._throw, _throw},
                {TokenType._try, _try},
                {TokenType._var, _var},
                {TokenType._const, _const},
                {TokenType._while, _while},
                {TokenType._with, _with},
                {TokenType._new, _new},
                {TokenType._this, _this},
                {TokenType._super, _super},
                {TokenType._class, _class},
                {TokenType._extends, _extends},
                {TokenType._export, _export},
                {TokenType._import, _import},
                {TokenType._null, _null},
                {TokenType._true, _true},
                {TokenType._false, _false},
                {TokenType._in, _in},
                {TokenType._instanceof, _instanceof},
                {TokenType._typeof, _typeof},
                {TokenType._void, _void},
                {TokenType._delete, _delete}
            };

            // Token-specific context update code
            parenR.UpdateContext = braceR.UpdateContext = Parser.ParenBraceRUpdateContext;
            braceL.UpdateContext = Parser.BraceLUpdateContext;
            dollarBraceL.UpdateContext = Parser.DollarBraceLUpdateContext;
            parenL.UpdateContext = Parser.ParenLUpdateContext;
            incDec.UpdateContext = Parser.IncDecUpdateContext;
            _function.UpdateContext = _class.UpdateContext = Parser.FunctionClassUpdateContext;
            backQuote.UpdateContext = Parser.BackQuoteUpdateContext;
            star.UpdateContext = Parser.StarUpdateContext;
            name.UpdateContext = Parser.NameUpdateContext;
        }

        public TokenInformation(string keyword = null,
            bool beforeExpr = false,
            bool startsExpr = false,
            bool isLoop = false,
            bool prefix = false,
            bool postfix = false,
            int binaryOperation = -1)
        {
            Keyword = keyword;
            BeforeExpression = beforeExpr;
            StartsExpression = startsExpr;
            IsLoop = isLoop;
            Prefix = prefix;
            Postfix = postfix;
            BinaryOperation = binaryOperation;
            UpdateContext = null;
        }

        private static TokenInformation CreateBinaryOperation(int prec)
        {
            return new TokenInformation(beforeExpr: true, binaryOperation: prec);
        }

        // Succinct definitions of keyword token types
        private static TokenInformation CreateKeyword(string keyword,
            bool beforeExpr = false,
            bool startsExpr = false,
            bool isLoop = false,
            bool prefix = false,
            bool postfix = false,
            int binop = -1)
        {
            return new TokenInformation(keyword, beforeExpr, startsExpr, isLoop, prefix, postfix, binop);
        }

        public string Keyword { get; }
        public bool BeforeExpression { get; }
        public bool StartsExpression { get; }
        public bool IsLoop { get; }
        public bool Prefix { get; }
        public bool Postfix { get; }
        public int BinaryOperation { get; }
        public Action<Parser, TokenType> UpdateContext { get; internal set; }

        public static IReadOnlyDictionary<TokenType, TokenInformation> Types => types;
        public static IReadOnlyDictionary<string, TokenType> Keywords => keywords;
    }
}