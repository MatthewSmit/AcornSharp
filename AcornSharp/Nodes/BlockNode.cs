using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class BlockNode : BaseNode
    {
        /// <inheritdoc />
        internal BlockNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}
