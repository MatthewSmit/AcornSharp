using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class WhileStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal WhileStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode test, [NotNull] StatementNode body)
            : base(parser, start, startLocation)
        {
            Test = test;
            Body = body;
        }

        [NotNull]
        public ExpressionNode Test { get; }

        [NotNull]
        public StatementNode Body { get; }
    }
}
