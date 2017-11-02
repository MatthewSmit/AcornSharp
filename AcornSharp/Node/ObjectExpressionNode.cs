using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ObjectExpressionNode : BaseNode
    {
        public IList<PropertyNode> properties;

        public ObjectExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}