using System;
using System.Collections.Generic;

namespace AcornSharp
{
    // The assignment of fine-grained, information-carrying type objects
    // allows the tokenizer to store the information it has about a
    // token in a way that is very cheap for the parser to look up.

    // All token type variables start with an underscore, to make them
    // easy to recognize.

    // The `beforeExpr` property is used to disambiguate between regular
    // expressions and divisions. It is set on all token types that can
    // be followed by an expression (thus, a slash after them would be a
    // regular expression).
    //
    // The `startsExpr` property is used to check if the token ends a
    // `yield` expression. It is set on all token types that either can
    // directly start an expression (like a quotation mark) or can
    // continue an expression (like the body of a string).
    //
    // `isLoop` marks a keyword as starting a loop, which is important
    // to know when parsing a label, in order to allow or disallow
    // continue jumps to that label.
    public sealed class TokenType
    {
        public static readonly TokenType num;
        public static readonly TokenType regexp;
        public static readonly TokenType @string;
        public static readonly TokenType name;
        public static readonly TokenType eof;

        // Punctuation token types.
        public static readonly TokenType bracketL;

        public static readonly TokenType bracketR;
        public static readonly TokenType braceL;
        public static readonly TokenType braceR;
        public static readonly TokenType parenL;
        public static readonly TokenType parenR;
        public static readonly TokenType comma;
        public static readonly TokenType semi;
        public static readonly TokenType colon;
        public static readonly TokenType dot;
        public static readonly TokenType question;
        public static readonly TokenType arrow;
        public static readonly TokenType template;
        public static readonly TokenType invalidTemplate;
        public static readonly TokenType ellipsis;
        public static readonly TokenType backQuote;
        public static readonly TokenType dollarBraceL;

        public static readonly TokenType eq;
        public static readonly TokenType assign;
        public static readonly TokenType incDec;
        public static readonly TokenType prefix;
        public static readonly TokenType logicalOR;
        public static readonly TokenType logicalAND;
        public static readonly TokenType bitwiseOR;
        public static readonly TokenType bitwiseXOR;
        public static readonly TokenType bitwiseAND;
        public static readonly TokenType equality;
        public static readonly TokenType relational;
        public static readonly TokenType bitShift;
        public static readonly TokenType plusMin;
        public static readonly TokenType modulo;
        public static readonly TokenType star;
        public static readonly TokenType slash;
        public static readonly TokenType starstar;

        // Keyword token types.
        public static readonly TokenType _break;

        public static readonly TokenType _case;
        public static readonly TokenType _catch;
        public static readonly TokenType _continue;
        public static readonly TokenType _debugger;
        public static readonly TokenType _default;
        public static readonly TokenType _do;
        public static readonly TokenType _else;
        public static readonly TokenType _finally;
        public static readonly TokenType _for;
        public static readonly TokenType _function;
        public static readonly TokenType _if;
        public static readonly TokenType _return;
        public static readonly TokenType _switch;
        public static readonly TokenType _throw;
        public static readonly TokenType _try;
        public static readonly TokenType _var;
        public static readonly TokenType _const;
        public static readonly TokenType _while;
        public static readonly TokenType _with;
        public static readonly TokenType _new;
        public static readonly TokenType _this;
        public static readonly TokenType _super;
        public static readonly TokenType _class;
        public static readonly TokenType _extends;
        public static readonly TokenType _export;
        public static readonly TokenType _import;
        public static readonly TokenType _null;
        public static readonly TokenType _true;
        public static readonly TokenType _false;
        public static readonly TokenType _in;
        public static readonly TokenType _instanceof;
        public static readonly TokenType _typeof;
        public static readonly TokenType _void;
        public static readonly TokenType _delete;

        // Map keyword names to token types.
        private static readonly Dictionary<string, TokenType> keywords;

        private static readonly Dictionary<string, TokenType> types;

        static TokenType()
        {
            keywords = new Dictionary<string, TokenType>();

            num = new TokenType("num", startsExpr: true);
            regexp = new TokenType("regexp", startsExpr: true);
            @string = new TokenType("string", startsExpr: true);
            name = new TokenType("name", startsExpr: true);
            eof = new TokenType("eof");

            // Punctuation token types.
            bracketL = new TokenType("[", beforeExpr: true, startsExpr: true);

            bracketR = new TokenType("]");
            braceL = new TokenType("{", beforeExpr: true, startsExpr: true);
            braceR = new TokenType("}");
            parenL = new TokenType("(", beforeExpr: true, startsExpr: true);
            parenR = new TokenType(")");
            comma = new TokenType(",", beforeExpr: true);
            semi = new TokenType(";", beforeExpr: true);
            colon = new TokenType(":", beforeExpr: true);
            dot = new TokenType(".");
            question = new TokenType("?", beforeExpr: true);
            arrow = new TokenType("=>", beforeExpr: true);
            template = new TokenType("template");
            invalidTemplate = new TokenType("invalidTemplate");
            ellipsis = new TokenType("...", beforeExpr: true);
            backQuote = new TokenType("`", startsExpr: true);
            dollarBraceL = new TokenType("${", beforeExpr: true, startsExpr: true);

            eq = new TokenType("=", beforeExpr: true, isAssign: true);
            assign = new TokenType("_=", beforeExpr: true, isAssign: true);
            incDec = new TokenType("++/--", prefix: true, postfix: true, startsExpr: true);
            prefix = new TokenType("!/~", beforeExpr: true, prefix: true, startsExpr: true);
            logicalOR = CreateBinaryOperation("||", 1);
            logicalAND = CreateBinaryOperation("&&", 2);
            bitwiseOR = CreateBinaryOperation("|", 3);
            bitwiseXOR = CreateBinaryOperation("^", 4);
            bitwiseAND = CreateBinaryOperation("&", 5);
            equality = CreateBinaryOperation("==/!=/===/!==", 6);
            relational = CreateBinaryOperation("</>/<=/>=", 7);
            bitShift = CreateBinaryOperation("<</>>/>>>", 8);
            plusMin = new TokenType("+/-", beforeExpr: true, binaryOperation: 9, prefix: true, startsExpr: true);
            modulo = CreateBinaryOperation("%", 10);
            star = CreateBinaryOperation("*", 10);
            slash = CreateBinaryOperation("/", 10);
            starstar = new TokenType("**", beforeExpr: true);

            // Keyword token types.
            _break = CreateKeyword("break");

            _case = CreateKeyword("case", true);
            _catch = CreateKeyword("catch");
            _continue = CreateKeyword("continue");
            _debugger = CreateKeyword("debugger");
            _default = CreateKeyword("default", true);
            _do = CreateKeyword("do", isLoop: true, beforeExpr: true);
            _else = CreateKeyword("else", true);
            _finally = CreateKeyword("finally");
            _for = CreateKeyword("for", isLoop: true);
            _function = CreateKeyword("function", startsExpr: true);
            _if = CreateKeyword("if");
            _return = CreateKeyword("return", true);
            _switch = CreateKeyword("switch");
            _throw = CreateKeyword("throw", true);
            _try = CreateKeyword("try");
            _var = CreateKeyword("var");
            _const = CreateKeyword("const");
            _while = CreateKeyword("while", isLoop: true);
            _with = CreateKeyword("with");
            _new = CreateKeyword("new", true, true);
            _this = CreateKeyword("this", startsExpr: true);
            _super = CreateKeyword("super", startsExpr: true);
            _class = CreateKeyword("class", startsExpr: true);
            _extends = CreateKeyword("extends", true);
            _export = CreateKeyword("export");
            _import = CreateKeyword("import");
            _null = CreateKeyword("null", startsExpr: true);
            _true = CreateKeyword("true", startsExpr: true);
            _false = CreateKeyword("false", startsExpr: true);
            _in = CreateKeyword("in", true, binop: 7);
            _instanceof = CreateKeyword("instanceof", true, binop: 7);
            _typeof = CreateKeyword("typeof", true, prefix: true, startsExpr: true);
            _void = CreateKeyword("void", true, prefix: true, startsExpr: true);
            _delete = CreateKeyword("delete", true, prefix: true, startsExpr: true);

            types = new Dictionary<string, TokenType>
            {
                {"num", num},
                {"regexp", regexp},
                {"string", @string},
                {"name", name},
                {"eof", eof},

                // Punctuation token types.
                {"bracketL", bracketL},
                {"bracketR", bracketR},
                {"braceL", braceL},
                {"braceR", braceR},
                {"parenL", parenL},
                {"parenR", parenR},
                {"comma", comma},
                {"semi", semi},
                {"colon", colon},
                {"dot", dot},
                {"question", question},
                {"arrow", arrow},
                {"template", template},
                {"invalidTemplate", invalidTemplate},
                {"ellipsis", ellipsis},
                {"backQuote", backQuote},
                {"dollarBraceL", dollarBraceL},

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

                {"eq", eq},
                {"assign", assign},
                {"incDec", incDec},
                {"prefix", prefix},
                {"logicalOR", logicalOR},
                {"logicalAND", logicalAND},
                {"bitwiseOR", bitwiseOR},
                {"bitwiseXOR", bitwiseXOR},
                {"bitwiseAND", bitwiseAND},
                {"equality", equality},
                {"relational", relational},
                {"bitShift", bitShift},
                {"plusMin", plusMin},
                {"modulo", modulo},
                {"star", star},
                {"slash", slash},
                {"starstar", starstar},

                // Keyword token types.
                {"_break", _break},
                {"_case", _case},
                {"_catch", _catch},
                {"_continue", _continue},
                {"_debugger", _debugger},
                {"_default", _default},
                {"_do", _do},
                {"_else", _else},
                {"_finally", _finally},
                {"_for", _for},
                {"_function", _function},
                {"_if", _if},
                {"_return", _return},
                {"_switch", _switch},
                {"_throw", _throw},
                {"_try", _try},
                {"_var", _var},
                {"_const", _const},
                {"_while", _while},
                {"_with", _with},
                {"_new", _new},
                {"_this", _this},
                {"_super", _super},
                {"_class", _class},
                {"_extends", _extends},
                {"_export", _export},
                {"_import", _import},
                {"_null", _null},
                {"_true", _true},
                {"_false", _false},
                {"_in", _in},
                {"_instanceof", _instanceof},
                {"_typeof", _typeof},
                {"_void", _void},
                {"_delete", _delete}
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

        public TokenType(string label,
            string keyword = null,
            bool beforeExpr = false,
            bool startsExpr = false,
            bool isLoop = false,
            bool isAssign = false,
            bool prefix = false,
            bool postfix = false,
            int binaryOperation = -1)
        {
            Label = label;
            Keyword = keyword;
            BeforeExpression = beforeExpr;
            StartsExpression = startsExpr;
            IsLoop = isLoop;
            IsAssign = isAssign;
            Prefix = prefix;
            Postfix = postfix;
            BinaryOperation = binaryOperation;
            UpdateContext = null;
        }

        private static TokenType CreateBinaryOperation(string name, int prec)
        {
            return new TokenType(name, beforeExpr: true, binaryOperation: prec);
        }

        // Succinct definitions of keyword token types
        private static TokenType CreateKeyword(string name,
            bool beforeExpr = false,
            bool startsExpr = false,
            bool isLoop = false,
            bool isAssign = false,
            bool prefix = false,
            bool postfix = false,
            int binop = -1)
        {
            return keywords[name] = new TokenType(name, name, beforeExpr, startsExpr, isLoop, isAssign, prefix, postfix, binop);
        }

        public string Label { get; }
        public string Keyword { get; }
        public bool BeforeExpression { get; }
        public bool StartsExpression { get; }
        public bool IsLoop { get; }
        public bool IsAssign { get; }
        public bool Prefix { get; }
        public bool Postfix { get; }
        public int BinaryOperation { get; }
        public Action<Parser, TokenType> UpdateContext { get; internal set; }

        public static IReadOnlyDictionary<string, TokenType> Types => types;
        public static IReadOnlyDictionary<string, TokenType> Keywords => keywords;
    }
}