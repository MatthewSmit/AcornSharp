using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class BreakStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal BreakStatementNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] IdentifierNode label)
            : base(parser, start, startLocation)
        {
            Label = label;
        }

        [CanBeNull]
        public IdentifierNode Label { get; }
    }
}
