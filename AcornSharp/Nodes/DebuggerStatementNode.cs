using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class DebuggerStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal DebuggerStatementNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}
