using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TryStatementNode : BaseNode
    {
        internal TryStatementNode([NotNull] Parser parser, Position start, Position end, BlockStatementNode block, CatchClauseNode handler, BlockStatementNode finaliser) :
            base(parser, start, end)
        {
            Block = block;
            Handler = handler;
            Finaliser = finaliser;
        }

        public BlockStatementNode Block { get; }
        public CatchClauseNode Handler { get; }
        public BlockStatementNode Finaliser { get; }
    }
}