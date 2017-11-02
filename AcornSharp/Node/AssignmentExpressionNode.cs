using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AssignmentExpressionNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public Operator @operator;

        public AssignmentExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}