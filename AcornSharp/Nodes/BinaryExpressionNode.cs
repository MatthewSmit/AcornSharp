using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class BinaryExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal BinaryExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode left, Operator @operator, [NotNull] ExpressionNode right)
            : base(parser, start, startLocation)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        [NotNull]
        public ExpressionNode Left { get; }

        public Operator Operator { get; }

        [NotNull]
        public ExpressionNode Right { get; }
    }
}