using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class CallExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal CallExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode callee, [NotNull] [ItemCanBeNull] IList<ExpressionNode> arguments)
            : base(parser, start, startLocation)
        {
            Callee = callee;
            Arguments = arguments;
        }

        [NotNull]
        public ExpressionNode Callee { get; }

        [NotNull]
        [ItemCanBeNull]
        public IList<ExpressionNode> Arguments { get; }
    }
}