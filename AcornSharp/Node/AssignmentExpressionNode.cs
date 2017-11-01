using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AssignmentExpressionNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public Operator @operator;

        public AssignmentExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public AssignmentExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is AssignmentExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (left != null && !TestEquals(left, realOther.left)) return false;
                if (right != null && !TestEquals(right, realOther.right)) return false;
                if (!Equals(@operator, realOther.@operator)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is AssignmentExpressionNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(left, realOther.left)) return false;
                if (!Equals(right, realOther.right)) return false;
                if (!Equals(@operator, realOther.@operator)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (left != null ? left.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (right != null ? right.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ @operator.GetHashCode();
            return hashCode;
        }
    }
}