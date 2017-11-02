using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ObjectPatternNode : BaseNode
    {
        public IList<PropertyNode> properties;

        public ObjectPatternNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}