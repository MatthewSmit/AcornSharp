using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class RestElementNode : ExpressionNode
    {
        /// <inheritdoc />
        internal RestElementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
        }

        /// <inheritdoc />
        internal RestElementNode([NotNull] Parser parser, int start, int end, Position startLocation, Position endLocation, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
            Finish(parser, end, endLocation);
        }

        [NotNull]
        public ExpressionNode Argument { get; }
    }
}