using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SpreadElementNode : ExpressionNode
    {
        internal SpreadElementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode argument) :
            base(parser, start, end)
        {
            Argument = argument;
        }

        public ExpressionNode Argument { get; }
    }
}