using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForInStatementNode : BaseNode
    {
        public BaseNode left;
        public ExpressionNode right;
        public BaseNode body;

        internal ForInStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}