using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ObjectPatternNode : ExpressionNode
    {
        internal ObjectPatternNode([NotNull] Parser parser, Position start, Position end, IReadOnlyList<PropertyNode> properties) :
            base(parser, start, end)
        {
            Properties = properties;
        }

        public IReadOnlyList<PropertyNode> Properties { get; }
    }
}