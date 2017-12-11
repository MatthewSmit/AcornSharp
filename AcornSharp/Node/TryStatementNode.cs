using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TryStatementNode : BaseNode
    {
        internal TryStatementNode([NotNull] Parser parser, Position start, Position end, [NotNull] BlockStatementNode block, [CanBeNull] CatchClauseNode handler, [CanBeNull] BlockStatementNode finaliser) :
            base(parser, start, end)
        {
            Block = block;
            Handler = handler;
            Finaliser = finaliser;
        }

        [NotNull]
        public BlockStatementNode Block { get; }
        [CanBeNull]
        public CatchClauseNode Handler { get; }
        [CanBeNull]
        public BlockStatementNode Finaliser { get; }
    }
}