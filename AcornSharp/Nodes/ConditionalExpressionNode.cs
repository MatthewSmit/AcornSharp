using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ConditionalExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ConditionalExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode test, [NotNull] ExpressionNode consequent, [NotNull] ExpressionNode alternate)
            : base(parser, start, startLocation)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        [NotNull]
        public ExpressionNode Test { get; }

        [NotNull]
        public ExpressionNode Consequent { get; }

        [NotNull]
        public ExpressionNode Alternate { get; }
    }
}
