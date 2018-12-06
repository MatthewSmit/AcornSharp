using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ForStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ForStatementNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] BaseNode init, [CanBeNull] ExpressionNode test, [CanBeNull] ExpressionNode update, [NotNull] StatementNode body)
            : base(parser, start, startLocation)
        {
            Init = init;
            Test = test;
            Update = update;
            Body = body;
        }

        [CanBeNull]
        public BaseNode Init { get; }

        [CanBeNull]
        public ExpressionNode Test { get; }

        [CanBeNull]
        public ExpressionNode Update { get; }

        [NotNull]
        public StatementNode Body { get; }
    }
}
