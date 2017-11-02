using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BlockStatementNode : BaseNode
    {
        public IList<BaseNode> body;

        public BlockStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
            return hashCode;
        }
    }
}