using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UnaryExpressionNode : ExpressionNode
    {
        public Operator @operator;
        public ExpressionNode argument;

        internal UnaryExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}