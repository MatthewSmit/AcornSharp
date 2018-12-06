using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public abstract class ExpressionNode : BaseNode
    {
        /// <inheritdoc />
        internal ExpressionNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}
