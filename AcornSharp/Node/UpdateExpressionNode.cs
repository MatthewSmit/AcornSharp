using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UpdateExpressionNode : ExpressionNode
    {
        public Operator @operator;
        public ExpressionNode argument;

        internal UpdateExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}