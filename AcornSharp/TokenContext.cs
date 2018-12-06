using System;
using JetBrains.Annotations;

namespace AcornSharp
{
    // The algorithm used to determine whether a regexp can appear at a
    // given point in the program is loosely based on sweet.js' approach.
    // See https://github.com/mozilla/sweet.js/wiki/design
    internal sealed class TokenContext
    {
        public static readonly TokenContext BasicStatement = new TokenContext("{", false);
        public static readonly TokenContext BasicExpression = new TokenContext("{", true);
        public static readonly TokenContext BasicTemplate = new TokenContext("${", false);
        public static readonly TokenContext ParenthesesStatatement = new TokenContext("(", false);
        public static readonly TokenContext ParenthesesExpression = new TokenContext("(", true);
        public static readonly TokenContext QuoteTemplate = new TokenContext("`", true, true, parser => parser.TryReadTemplateToken());
        public static readonly TokenContext FunctionStatement = new TokenContext("function", false);
        public static readonly TokenContext FunctionExpression = new TokenContext("function", true);
        public static readonly TokenContext FunctionExpressionGenerator = new TokenContext("function", true, false, null, true);
        public static readonly TokenContext FunctionGenerator = new TokenContext("function", false, false, null, true);

        private TokenContext(string token, bool isExpression, bool preserveSpace = false, [CanBeNull] Action<Parser> @override = null, bool generator = false)
        {
            Token = token;
            IsExpression = isExpression;
            PreserveSpace = preserveSpace;
            Override = @override;
            Generator = generator;
        }

        public string Token { get; }
        public bool IsExpression { get; }
        public bool PreserveSpace { get; }
        public Action<Parser> Override { get; }
        public bool Generator { get; }
    }
}