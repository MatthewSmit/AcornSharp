using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SpreadElementNode : ExpressionNode
    {
        public ExpressionNode argument;

        internal SpreadElementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}