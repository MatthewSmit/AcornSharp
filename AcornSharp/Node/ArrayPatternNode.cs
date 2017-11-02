using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ArrayPatternNode : BaseNode
    {
        public IList<BaseNode> elements;

        public ArrayPatternNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}