using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AssignmentPatternNode : ExpressionNode
    {
        internal AssignmentPatternNode([NotNull] Parser parser, Position start, Position end, ExpressionNode left, ExpressionNode right) :
            base(parser, start, end)
        {
            Left = left;
            Right = right;
        }

        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }
    }
}