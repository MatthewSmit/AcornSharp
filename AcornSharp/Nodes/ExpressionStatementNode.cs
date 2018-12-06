using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ExpressionStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ExpressionStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode expression)
            : base(parser, start, startLocation)
        {
            Expression = expression;
        }

        [NotNull]
        public ExpressionNode Expression { get; }

        [CanBeNull]
        public string Directive { get; internal set; }
    }
}
