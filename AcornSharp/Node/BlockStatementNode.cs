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

        public BlockStatementNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is BlockStatementNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (body != null && !TestEquals(body, realOther.body)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is BlockStatementNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(body, realOther.body)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
            return hashCode;
        }
    }
}