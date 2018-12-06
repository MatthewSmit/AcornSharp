using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class IfStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal IfStatementNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] ExpressionNode test, [NotNull] StatementNode consequent, [CanBeNull] StatementNode alternate)
            : base(parser, start, startLocation)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        [CanBeNull]
        public ExpressionNode Test { get; }

        [NotNull]
        public StatementNode Consequent { get; }

        [CanBeNull]
        public StatementNode Alternate { get; }
    }
}