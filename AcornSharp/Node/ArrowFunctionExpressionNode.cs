using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ArrowFunctionExpressionNode : BaseNode
    {
        public bool expression;

        public ArrowFunctionExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public ArrowFunctionExpressionNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is ArrowFunctionExpressionNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (expression && expression != realOther.expression) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is ArrowFunctionExpressionNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (expression != realOther.expression) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ expression.GetHashCode();
            return hashCode;
        }
    }
}