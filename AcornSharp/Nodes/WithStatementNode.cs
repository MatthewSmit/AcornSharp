using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class WithStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal WithStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode @object, [NotNull] StatementNode body)
            : base(parser, start, startLocation)
        {
            Object = @object;
            Body = body;
        }

        [NotNull]
        public ExpressionNode Object { get; }

        [NotNull]
        public StatementNode Body { get; }
    }
}
