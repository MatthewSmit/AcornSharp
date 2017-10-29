using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TryStatementNode : BaseNode
    {
        private readonly BaseNode block;
        private readonly BaseNode handler;
        private readonly BaseNode finaliser;

        public TryStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode block, BaseNode handler, BaseNode finaliser) :
            base(parser, start, end)
        {
            this.block = block;
            this.handler = handler;
            this.finaliser = finaliser;
        }

        public TryStatementNode(SourceLocation location, BaseNode block, BaseNode handler, BaseNode finaliser) :
            base(location)
        {
            this.block = block;
            this.handler = handler;
            this.finaliser = finaliser;
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is TryStatementNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (block != null && !TestEquals(block, realOther.block)) return false;
                if (handler != null && !TestEquals(handler, realOther.handler)) return false;
                if (finaliser != null && !TestEquals(finaliser, realOther.finaliser)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is TryStatementNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(block, realOther.block)) return false;
                if (!Equals(handler, realOther.handler)) return false;
                if (!Equals(finaliser, realOther.finaliser)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (block != null ? block.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (handler != null ? handler.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (finaliser != null ? finaliser.GetHashCode() : 0);
            return hashCode;
        }
    }
}