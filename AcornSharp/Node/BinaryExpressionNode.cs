using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BinaryExpressionNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public Operator @operator;

        public BinaryExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
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