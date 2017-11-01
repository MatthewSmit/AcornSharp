using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ParenthesisedExpressionNode : BaseNode
    {
        public BaseNode expression;

        public ParenthesisedExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public ParenthesisedExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is ParenthesisedExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (!TestEquals(expression, realOther.expression)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is ParenthesisedExpressionNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(expression, realOther.expression)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (expression != null ? expression.GetHashCode() : 0);
            return hashCode;
        }
    }
}