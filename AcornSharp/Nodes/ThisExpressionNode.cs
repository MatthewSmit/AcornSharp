using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ThisExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ThisExpressionNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}
