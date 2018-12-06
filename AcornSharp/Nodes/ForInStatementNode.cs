using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ForInStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal ForInStatementNode([NotNull] Parser parser, int start, Position startLocation, bool isAwait, [NotNull] BaseNode left, [NotNull] ExpressionNode right, [NotNull] StatementNode body)
            : base(parser, start, startLocation)
        {
            Await = isAwait;
            Left = left;
            Right = right;
            Body = body;
        }

        public bool Await { get; }

        [NotNull]
        public BaseNode Left { get; }

        [NotNull]
        public ExpressionNode Right { get; }

        [NotNull]
        public StatementNode Body { get; }
    }
}