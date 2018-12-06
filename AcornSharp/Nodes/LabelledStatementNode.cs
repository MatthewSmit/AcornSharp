using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class LabelledStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal LabelledStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] StatementNode body, [NotNull] ExpressionNode label)
            : base(parser, start, startLocation)
        {
            Body = body;
            Label = label;
        }

        [NotNull]
        public StatementNode Body { get; }

        [NotNull]
        public ExpressionNode Label { get; }
    }
}
