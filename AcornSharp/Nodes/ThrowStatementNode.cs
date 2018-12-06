using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ThrowStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ThrowStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
        }

        [NotNull]
        public ExpressionNode Argument { get; }
    }
}
