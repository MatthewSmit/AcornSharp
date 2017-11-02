using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassBodyNode : BaseNode
    {
        internal ClassBodyNode([NotNull] Parser parser, Position start, Position end, IReadOnlyList<BaseNode> body) :
            base(parser, start, end)
        {
            Body = body;
        }

        public IReadOnlyList<BaseNode> Body { get; }
    }
}