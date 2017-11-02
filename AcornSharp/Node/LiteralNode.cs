using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LiteralNode : ExpressionNode
    {
        internal LiteralNode([NotNull] Parser parser, Position start, Position end, LiteralValue value, string raw) :
            base(parser, start, end)
        {
            Value = value;
            Raw = raw;
        }

        public LiteralValue Value { get; }
        public string Raw { get; }
    }
}