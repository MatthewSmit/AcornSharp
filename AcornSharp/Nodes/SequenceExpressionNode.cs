using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class SequenceExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal SequenceExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<ExpressionNode> expressions)
            : base(parser, start, startLocation)
        {
            Expressions = expressions;
        }

        [NotNull]
        [ItemNotNull]
        public IList<ExpressionNode> Expressions { get; }
    }
}