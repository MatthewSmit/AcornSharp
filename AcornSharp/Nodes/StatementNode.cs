using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public abstract class StatementNode : BaseNode
    {
        /// <inheritdoc />
        internal StatementNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}