using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ArrayPatternNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ArrayPatternNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemCanBeNull] IList<ExpressionNode> elements)
            : base(parser, start, startLocation)
        {
            Elements = elements;
        }

        internal ArrayPatternNode([NotNull] Parser parser, int start, int end, Position startLocation, Position endLocation, [NotNull] [ItemCanBeNull] IList<ExpressionNode> elements)
            : base(parser, start, startLocation)
        {
            Elements = elements;
            Finish(parser, end, endLocation);
        }

        [NotNull]
        [ItemCanBeNull]
        public IList<ExpressionNode> Elements { get; }
    }
}
