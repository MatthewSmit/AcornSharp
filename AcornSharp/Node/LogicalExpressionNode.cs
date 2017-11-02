using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LogicalExpressionNode : ExpressionNode
    {
        public BaseNode left;
        public BaseNode right;
        public Operator @operator;

        internal LogicalExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}