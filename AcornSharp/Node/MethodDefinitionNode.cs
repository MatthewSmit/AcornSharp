using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MethodDefinitionNode : BaseNode
    {
        public BaseNode value;

        public MethodDefinitionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public MethodDefinitionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is MethodDefinitionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (value != null && !TestEquals(value, realOther.value)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is MethodDefinitionNode realOther)
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