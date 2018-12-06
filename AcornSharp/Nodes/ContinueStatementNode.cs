using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ContinueStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ContinueStatementNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] IdentifierNode label)
            : base(parser, start, startLocation)
        {
            Label = label;
        }

        [CanBeNull]
        public IdentifierNode Label { get; }
    }
}
