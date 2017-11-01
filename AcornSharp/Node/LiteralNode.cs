using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LiteralNode : BaseNode
    {
        public LiteralValue value;

        public LiteralNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public LiteralNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is LiteralNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (!value.IsNull && !TestEquals(value, realOther.value)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is LiteralNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(value, realOther.value)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (value != null ? value.GetHashCode() : 0);
            return hashCode;
        }
    }
}