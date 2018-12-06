using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ParenthesisedExpressionNode : ExpressionNode
    {
        [NotNull] private ExpressionNode expression;

        /// <inheritdoc />
        internal ParenthesisedExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode expression)
            : base(parser, start, startLocation)
        {
            this.expression = expression;
        }

        [NotNull]
        public ExpressionNode Expression => expression;

        [NotNull]
        internal ref ExpressionNode ExpressionRef => ref expression;
    }
}