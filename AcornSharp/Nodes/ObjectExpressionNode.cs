using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ObjectExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ObjectExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<ExpressionNode> properties)
            : base(parser, start, startLocation)
        {
            Properties = properties;
        }

        [NotNull]
        [ItemNotNull]
        public IList<ExpressionNode> Properties { get; }
    }
}