using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IfStatementNode : BaseNode
    {
        private readonly BaseNode test;
        private readonly BaseNode consequent;
        private readonly BaseNode alternate;

        public IfStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode test, BaseNode consequent, BaseNode alternate) :
            base(parser, start, end)
        {
            this.test = test;
            this.consequent = consequent;
            this.alternate = alternate;
        }

        public IfStatementNode(SourceLocation location, BaseNode test, BaseNode consequent, BaseNode alternate) :
            base(location)
        {
            this.test = test;
            this.consequent = consequent;
            this.alternate = alternate;
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is IfStatementNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (test != null && !TestEquals(test, realOther.test)) return false;
                if (consequent != null && !TestEquals(consequent, realOther.consequent)) return false;
                if (alternate != null && !TestEquals(alternate, realOther.alternate)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is IfStatementNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(test, realOther.test)) return false;
                if (!Equals(consequent, realOther.consequent)) return false;
                if (!Equals(alternate, realOther.alternate)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (test != null ? test.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (consequent != null ? consequent.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (alternate != null ? alternate.GetHashCode() : 0);
            return hashCode;
        }
    }
}