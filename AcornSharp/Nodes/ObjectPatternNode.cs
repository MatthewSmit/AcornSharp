using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ObjectPatternNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ObjectPatternNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<ExpressionNode> properties)
            : base(parser, start, startLocation)
        {
            Properties = properties;
        }

        internal ObjectPatternNode([NotNull] Parser parser, int start, int end, Position startLocation, Position endLocation, [NotNull] [ItemNotNull] IList<ExpressionNode> properties)
            : base(parser, start, startLocation)
        {
            Properties = properties;
            Finish(parser, end, endLocation);
        }

        [NotNull]
        [ItemNotNull]
        public IList<ExpressionNode> Properties { get; }
    }
}