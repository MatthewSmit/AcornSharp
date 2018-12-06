using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class CatchClauseNode : BaseNode
    {
        /// <inheritdoc />
        internal CatchClauseNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] ExpressionNode param, [NotNull] BlockStatementNode body)
            : base(parser, start, startLocation)
        {
            Param = param;
            Body = body;
        }

        [CanBeNull]
        public ExpressionNode Param { get; }

        [NotNull]
        public BlockStatementNode Body { get; }
    }
}