using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ProgramNode : BaseNode
    {
        public List<BaseNode> body;

        internal ProgramNode([NotNull] Parser parser, Position start, Position end, SourceType sourceType = SourceType.Script) :
            base(parser, start, end)
        {
            SourceType = sourceType;
        }

        public SourceType SourceType { get; }
    }
}
