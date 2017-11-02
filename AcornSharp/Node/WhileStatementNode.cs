using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class WhileStatementNode : BaseNode
    {
        public BaseNode test;
        public BaseNode body;

        internal WhileStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}