using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ParenthesisedExpressionNode : ExpressionNode
    {
        public ExpressionNode expression;

        internal ParenthesisedExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}