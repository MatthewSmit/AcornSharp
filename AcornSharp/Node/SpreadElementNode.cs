using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SpreadElementNode : ExpressionNode
    {
        public SpreadElementNode(SourceLocation sourceLocation, ExpressionNode argument) :
            base(sourceLocation)
        {
            Argument = argument;
        }

        internal SpreadElementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode argument) :
            base(parser, start, end)
        {
            Argument = argument;
        }

        public ExpressionNode Argument { get; }
    }
}