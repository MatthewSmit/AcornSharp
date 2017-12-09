using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BlockStatementNode : BaseNode
    {
        public BlockStatementNode(SourceLocation sourceLocation, [NotNull] [ItemNotNull] IReadOnlyList<BaseNode> body) :
            base(sourceLocation)
        {
            Body = body;
        }

        internal BlockStatementNode([NotNull] Parser parser, Position start, Position end, [NotNull] [ItemNotNull] IReadOnlyList<BaseNode> body) :
            base(parser, start, end)
        {
            Body = body;
        }

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<BaseNode> Body { get; }
    }
}