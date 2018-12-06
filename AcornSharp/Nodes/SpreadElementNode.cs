using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class SpreadElementNode : ExpressionNode
    {
        /// <inheritdoc />
        internal SpreadElementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
        }

        [NotNull]
        public ExpressionNode Argument { get; }
    }
}