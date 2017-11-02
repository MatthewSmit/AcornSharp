using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class DoWhileStatementNode : BaseNode
    {
        public ExpressionNode test;
        public BaseNode body;

        internal DoWhileStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}