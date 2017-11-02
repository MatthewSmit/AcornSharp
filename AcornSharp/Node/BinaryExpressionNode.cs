using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode left;
        public ExpressionNode right;
        public Operator @operator;

        internal BinaryExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}