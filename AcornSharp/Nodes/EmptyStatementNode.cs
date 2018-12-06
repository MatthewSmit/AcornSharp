using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class EmptyStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal EmptyStatementNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}