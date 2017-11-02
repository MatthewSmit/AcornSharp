using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ProgramNode : BaseNode
    {
        internal ProgramNode([NotNull] Parser parser, Position start, Position end, SourceType sourceType, List<BaseNode> body) :
            base(parser, start, end)
        {
            SourceType = sourceType;
            Body = body;
        }

        public SourceType SourceType { get; }
        public List<BaseNode> Body { get; }
    }
}
