using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UnaryExpressionNode : BaseNode
    {
        public Operator @operator;

        public UnaryExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public UnaryExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is UnaryExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (!Equals(@operator, realOther.@operator)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is UnaryExpressionNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(@operator, realOther.@operator)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ @operator.GetHashCode();
            return hashCode;
        }
    }
}