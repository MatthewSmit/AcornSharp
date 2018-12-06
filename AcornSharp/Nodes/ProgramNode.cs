using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ProgramNode : BaseNode
    {
        internal ProgramNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }

        [NotNull]
        [ItemNotNull]
        public IList<StatementNode> Body { get; } = new List<StatementNode>();

        public SourceType SourceType { get; internal set; }
    }
}
