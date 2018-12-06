using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class AwaitExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal AwaitExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
        }

        [NotNull]
        public ExpressionNode Argument { get; }
    }
}