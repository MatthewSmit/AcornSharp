using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LiteralNode : BaseNode
    {
        public LiteralValue value;
        public RegexNode regex;

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
                if (regex != null && !Equals(regex, realOther.regex)) return false;
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
                if (!Equals(regex, realOther.regex)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (value != null ? value.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (regex != null ? regex.GetHashCode() : 0);
            return hashCode;
        }
    }
}