using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CatchClauseNode : BaseNode
    {
        public ExpressionNode param;
        public BlockStatementNode body;

        internal CatchClauseNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}