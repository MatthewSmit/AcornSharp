using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CatchClauseNode : BaseNode
    {
        internal CatchClauseNode([NotNull] Parser parser, Position start, Position end, ExpressionNode param, BlockStatementNode body) :
            base(parser, start, end)
        {
            Param = param;
            Body = body;
        }

        public ExpressionNode Param { get; }
        public BlockStatementNode Body { get; }
    }
}