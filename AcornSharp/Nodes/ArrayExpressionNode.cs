using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ArrayExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ArrayExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemCanBeNull] IList<ExpressionNode> elements)
            : base(parser, start, startLocation)
        {
            Elements = elements;
        }

        [NotNull]
        [ItemCanBeNull]
        public IList<ExpressionNode> Elements { get; }
    }
}