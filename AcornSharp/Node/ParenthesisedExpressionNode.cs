using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ParenthesisedExpressionNode : ExpressionNode
    {
        internal ParenthesisedExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode expression) :
            base(parser, start, end)
        {
            Expression = expression;
        }

        public ExpressionNode Expression { get; }
    }
}