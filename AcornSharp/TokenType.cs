using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp
{
    // ## Token types

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
        // Map keyword names to token types.
        private static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();

        public static readonly TokenType Number = new TokenType("num", startsExpression: true);
        public static readonly TokenType RegExp = new TokenType("regexp", startsExpression: true);
        public static readonly TokenType String = new TokenType("string", startsExpression: true);
        public static readonly TokenType Name = new TokenType("name", startsExpression: true);
        public static readonly TokenType Eof = new TokenType("eof");

        // Punctuation token types.
        public static readonly TokenType BracketLeft = new TokenType("[", beforeExpression: true, startsExpression: true);
        public static readonly TokenType BracketRight = new TokenType("]");
        public static readonly TokenType BraceLeft = new TokenType("{", beforeExpression: true, startsExpression: true);
        public static readonly TokenType BraceRight = new TokenType("}");
        public static readonly TokenType ParenLeft = new TokenType("(", beforeExpression: true, startsExpression: true);
        public static readonly TokenType ParenRight = new TokenType(")");
        public static readonly TokenType Comma = new TokenType(",", beforeExpression: true);
        public static readonly TokenType Semicolon = new TokenType(";", beforeExpression: true);
        public static readonly TokenType Colon = new TokenType(":", beforeExpression: true);
        public static readonly TokenType Dot = new TokenType(".");
        public static readonly TokenType Question = new TokenType("?", beforeExpression: true);
        public static readonly TokenType Arrow = new TokenType("=>", beforeExpression: true);
        public static readonly TokenType Template = new TokenType("template");
        public static readonly TokenType InvalidTemplate = new TokenType("invalidTemplate");
        public static readonly TokenType Ellipsis = new TokenType("...", beforeExpression: true);
        public static readonly TokenType BackQuote = new TokenType("`", startsExpression: true);
        public static readonly TokenType DollarBraceLeft = new TokenType("${", beforeExpression: true, startsExpression: true);

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

        public static readonly TokenType Equal = new TokenType("=", beforeExpression: true, isAssign: true);
        public static readonly TokenType Assignment = new TokenType("_=", beforeExpression: true, isAssign: true);
        public static readonly TokenType IncrementDecrement = new TokenType("++/--", prefix: true, postfix: true, startsExpression: true);
        public static readonly TokenType PrefixOperator = new TokenType("!/~", beforeExpression: true, prefix: true, startsExpression: true);
        public static readonly TokenType LogicalOR = MakeBinaryOperator("||", 1);
        public static readonly TokenType LogicalAND = MakeBinaryOperator("&&", 2);
        public static readonly TokenType BitwiseOR = MakeBinaryOperator("|", 3);
        public static readonly TokenType BitwiseXOR = MakeBinaryOperator("^", 4);
        public static readonly TokenType BitwiseAND = MakeBinaryOperator("&", 5);
        public static readonly TokenType Equality = MakeBinaryOperator("==/!=/===/!==", 6);
        public static readonly TokenType Relational = MakeBinaryOperator("</>/<=/>=", 7);
        public static readonly TokenType BitShift = MakeBinaryOperator("<</>>/>>>", 8);
        public static readonly TokenType PlusMinus = new TokenType("+/-", beforeExpression: true, binaryOperator: 9, prefix: true, startsExpression: true);
        public static readonly TokenType Modulo = MakeBinaryOperator("%", 10);
        public static readonly TokenType Star = MakeBinaryOperator("*", 10);
        public static readonly TokenType Slash = MakeBinaryOperator("/", 10);
        public static readonly TokenType StarStar = new TokenType("**", beforeExpression: true);

        // Keyword token types.
        public static readonly TokenType Break = MakeKeyword("break");
        public static readonly TokenType Case = MakeKeyword("case", beforeExpression: true);
        public static readonly TokenType Catch = MakeKeyword("catch");
        public static readonly TokenType Continue = MakeKeyword("continue");
        public static readonly TokenType Debugger = MakeKeyword("debugger");
        public static readonly TokenType Default = MakeKeyword("default", beforeExpression: true);
        public static readonly TokenType Do = MakeKeyword("do", isLoop: true, beforeExpression: true);
        public static readonly TokenType Else = MakeKeyword("else", beforeExpression: true);
        public static readonly TokenType Finally = MakeKeyword("finally");
        public static readonly TokenType For = MakeKeyword("for", isLoop: true);
        public static readonly TokenType Function = MakeKeyword("function", startsExpression: true);
        public static readonly TokenType If = MakeKeyword("if");
        public static readonly TokenType Return = MakeKeyword("return", beforeExpression: true);
        public static readonly TokenType Switch = MakeKeyword("switch");
        public static readonly TokenType Throw = MakeKeyword("throw", beforeExpression: true);
        public static readonly TokenType Try = MakeKeyword("try");
        public static readonly TokenType Var = MakeKeyword("var");
        public static readonly TokenType Const = MakeKeyword("const");
        public static readonly TokenType While = MakeKeyword("while", isLoop: true);
        public static readonly TokenType With = MakeKeyword("with");
        public static readonly TokenType New = MakeKeyword("new", beforeExpression: true, startsExpression: true);
        public static readonly TokenType This = MakeKeyword("this", startsExpression: true);
        public static readonly TokenType Super = MakeKeyword("super", startsExpression: true);
        public static readonly TokenType Class = MakeKeyword("class", startsExpression: true);
        public static readonly TokenType Extends = MakeKeyword("extends", beforeExpression: true);
        public static readonly TokenType Export = MakeKeyword("export");
        public static readonly TokenType Import = MakeKeyword("import");
        public static readonly TokenType Null = MakeKeyword("null", startsExpression: true);
        public static readonly TokenType True = MakeKeyword("true", startsExpression: true);
        public static readonly TokenType False = MakeKeyword("false", startsExpression: true);
        public static readonly TokenType In = MakeKeyword("in", beforeExpression: true, binaryOperator: 7);
        public static readonly TokenType InstanceOf = MakeKeyword("instanceof", beforeExpression: true, binaryOperator: 7);
        public static readonly TokenType TypeOf = MakeKeyword("typeof", beforeExpression: true, prefix: true, startsExpression: true);
        public static readonly TokenType Void = MakeKeyword("void", beforeExpression: true, prefix: true, startsExpression: true);
        public static readonly TokenType Delete = MakeKeyword("delete", beforeExpression: true, prefix: true, startsExpression: true);

        private TokenType(string label, [CanBeNull] string keyword = null, bool beforeExpression = false, bool startsExpression = false, bool isLoop = false, bool isAssign = false, bool prefix = false, bool postfix = false, int binaryOperator = -1, [CanBeNull] Action<Parser, TokenType> updateContext = null)
        {
            Label = label;
            Keyword = keyword;
            BeforeExpression = beforeExpression;
            StartsExpression = startsExpression;
            IsLoop = isLoop;
            IsAssignment = isAssign;
            Prefix = prefix;
            Postfix = postfix;
            BinaryOperator = binaryOperator;
            UpdateContext = updateContext;
        }

        [NotNull]
        private static TokenType MakeBinaryOperator(string name, int precedence)
        {
            return new TokenType(name, beforeExpression: true, binaryOperator: precedence);
        }

        // Succinct definitions of keyword token types
        [NotNull]
        private static TokenType MakeKeyword([NotNull] string name, bool beforeExpression = false, bool startsExpression = false, bool isLoop = false, bool isAssign = false, bool prefix = false, bool postfix = false, int binaryOperator = -1, [CanBeNull] Action<Parser, TokenType> updateContext = null)
        {
            return keywords[name] = new TokenType(name, name, beforeExpression, startsExpression, isLoop, isAssign, prefix, postfix, binaryOperator, updateContext);
        }

        public string Label { get; }
        public string Keyword { get; }
        public bool BeforeExpression { get; }
        public bool StartsExpression { get; }
        public bool IsLoop { get; }
        public bool IsAssignment { get; }
        public bool Prefix { get; }
        public bool Postfix { get; }
        public int BinaryOperator { get; }
        public Action<Parser, TokenType> UpdateContext { get; internal set; }

        public static IReadOnlyDictionary<string, TokenType> Keywords => keywords;
    }
}