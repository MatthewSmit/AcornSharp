using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class SuperNode : ExpressionNode
    {
        /// <inheritdoc />
        internal SuperNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}
