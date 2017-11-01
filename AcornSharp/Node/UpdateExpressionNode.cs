using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UpdateExpressionNode : BaseNode
    {
        public Operator @operator;

        public UpdateExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public UpdateExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is UpdateExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (!Equals(@operator, realOther.@operator)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is UpdateExpressionNode realOther)
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