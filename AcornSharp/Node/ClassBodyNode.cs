using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassBodyNode : BaseNode
    {
        internal ClassBodyNode([NotNull] Parser parser, Position start, Position end, [NotNull] IReadOnlyList<MethodDefinitionNode> body) :
            base(parser, start, end)
        {
            Body = body;
        }

        [NotNull]
        public IReadOnlyList<MethodDefinitionNode> Body { get; }
    }
}