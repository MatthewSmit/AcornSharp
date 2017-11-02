using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForOfStatementNode : BaseNode
    {
        public BaseNode left;
        public ExpressionNode right;
        public BaseNode body;

        internal ForOfStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}