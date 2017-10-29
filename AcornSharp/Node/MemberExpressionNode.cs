using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MemberExpressionNode : BaseNode
    {
        private readonly BaseNode @object;
        private readonly BaseNode property;
        private readonly bool computed;

        public MemberExpressionNode([NotNull] Parser parser, Position start, Position end, BaseNode @object, BaseNode property, bool computed) :
            base(parser, start, end)
        {
            this.@object = @object;
            this.property = property;
            this.computed = computed;
        }

        public MemberExpressionNode(SourceLocation location, BaseNode @object, BaseNode property, bool computed) :
            base(location)
        {
            this.@object = @object;
            this.property = property;
            this.computed = computed;
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is MemberExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (@object != null && !TestEquals(@object, realOther.@object)) return false;
                if (property != null && !TestEquals(property, realOther.property)) return false;
                if (computed && computed != realOther.computed) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is MemberExpressionNode realOther)
            {
                return base.Equals(other) &&
                       Equals(@object, realOther.@object) &&
                       Equals(property, realOther.property) &&
                       computed == realOther.computed;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (@object != null ? @object.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (property != null ? property.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ computed.GetHashCode();
            return hashCode;
        }
    }
}