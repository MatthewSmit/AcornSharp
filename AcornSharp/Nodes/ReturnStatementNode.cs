using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ReturnStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ReturnStatementNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Argument = argument;
        }

        [CanBeNull]
        public ExpressionNode Argument { get; }
    }
}