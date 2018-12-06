using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class TryStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal TryStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] BlockStatementNode block, [CanBeNull] CatchClauseNode handler, [CanBeNull] BlockStatementNode finaliser)
            : base(parser, start, startLocation)
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