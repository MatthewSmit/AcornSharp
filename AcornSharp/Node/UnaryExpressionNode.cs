using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UnaryExpressionNode : ExpressionNode
    {
        internal UnaryExpressionNode([NotNull] Parser parser, Position start, Position end, Operator @operator, ExpressionNode argument) :
            base(parser, start, end)
        {
            Operator = @operator;
            Argument = argument;
        }

        public Operator Operator { get; }
        public ExpressionNode Argument { get; }
    }
}