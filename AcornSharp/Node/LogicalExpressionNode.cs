using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LogicalExpressionNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;

        public LogicalExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public LogicalExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is LogicalExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (left != null && !TestEquals(left, realOther.left)) return false;
                if (right != null && !TestEquals(right, realOther.right)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is LogicalExpressionNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(left, realOther.left)) return false;
                if (!Equals(right, realOther.right)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (left != null ? left.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (right != null ? right.GetHashCode() : 0);
            return hashCode;
        }
    }
}