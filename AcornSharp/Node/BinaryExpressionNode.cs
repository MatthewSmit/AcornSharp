using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BinaryExpressionNode : ExpressionNode
    {
        internal BinaryExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode left, ExpressionNode right, Operator @operator) :
            base(parser, start, end)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }

        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }
        public Operator Operator { get; }
    }
}