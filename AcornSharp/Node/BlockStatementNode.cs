using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BlockStatementNode : BaseNode
    {
        public IReadOnlyList<BaseNode> body;

        internal BlockStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}