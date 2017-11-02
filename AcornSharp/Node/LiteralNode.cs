using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LiteralNode : ExpressionNode
    {
        public LiteralValue value;
        public RegexNode regex;
        public string raw;

        internal LiteralNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}