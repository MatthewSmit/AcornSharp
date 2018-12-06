using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class AssignmentPatternNode : ExpressionNode
    {
        /// <inheritdoc />
        internal AssignmentPatternNode([NotNull] Parser parser, int start, Position startLoc, [NotNull] ExpressionNode left, [NotNull] ExpressionNode right)
            : base(parser, start, startLoc)
        {
            Left = left;
            Right = right;
        }

        /// <inheritdoc />
        internal AssignmentPatternNode([NotNull] Parser parser, int start, int end, Position startLoc, Position endLoc, [NotNull] ExpressionNode left, [NotNull] ExpressionNode right)
            : base(parser, start, startLoc)
        {
            Left = left;
            Right = right;
            Finish(parser, end, endLoc);
        }

        [NotNull]
        public ExpressionNode Left { get; }

        [NotNull]
        public ExpressionNode Right { get; }
    }
}