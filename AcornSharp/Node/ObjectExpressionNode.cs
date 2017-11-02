using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ObjectExpressionNode : ExpressionNode
    {
        public IReadOnlyList<PropertyNode> properties;

        internal ObjectExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}