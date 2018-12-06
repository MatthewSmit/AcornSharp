using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class DoWhileStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal DoWhileStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] StatementNode body, [NotNull] ExpressionNode test)
            : base(parser, start, startLocation)
        {
            Body = body;
            Test = test;
        }

        [NotNull]
        public StatementNode Body { get; }

        [NotNull]
        public ExpressionNode Test { get; }
    }
}
