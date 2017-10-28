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
    public enum TokenType
    {
        num,
        regexp,
        @string,
        name,
        eof,

        bracketL,

        bracketR,
        braceL,
        braceR,
        parenL,
        parenR,
        comma,
        semi,
        colon,
        dot,
        question,
        arrow,
        template,
        invalidTemplate,
        ellipsis,
        backQuote,
        dollarBraceL,

        eq,
        assign,
        incDec,
        prefix,
        logicalOR,
        logicalAND,
        bitwiseOR,
        bitwiseXOR,
        bitwiseAND,
        equality,
        relational,
        bitShift,
        plusMin,
        modulo,
        star,
        slash,
        starstar,

        _break,

        _case,
        _catch,
        _continue,
        _debugger,
        _default,
        _do,
        _else,
        _finally,
        _for,
        _function,
        _if,
        _return,
        _switch,
        _throw,
        _try,
        _var,
        _const,
        _while,
        _with,
        _new,
        _this,
        _super,
        _class,
        _extends,
        _export,
        _import,
        _null,
        _true,
        _false,
        _in,
        _instanceof,
        _typeof,
        _void,
        _delete
    }
}