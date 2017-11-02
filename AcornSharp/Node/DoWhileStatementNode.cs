using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class DoWhileStatementNode : BaseNode
    {
        public BaseNode test;
        public BaseNode body;

        public DoWhileStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}