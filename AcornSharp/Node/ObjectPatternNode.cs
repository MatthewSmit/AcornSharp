using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ObjectPatternNode : ExpressionNode
    {
        public IReadOnlyList<PropertyNode> properties;

        internal ObjectPatternNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}