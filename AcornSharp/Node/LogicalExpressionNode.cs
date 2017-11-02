using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LogicalExpressionNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public Operator @operator;

        public LogicalExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}