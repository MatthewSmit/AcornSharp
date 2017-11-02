using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ConditionalExpressionNode : ExpressionNode
    {
        public readonly ExpressionNode test;
        public readonly ExpressionNode consequent;
        public readonly ExpressionNode alternate;

        internal ConditionalExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode test, ExpressionNode consequent, ExpressionNode alternate) :
            base(parser, start, end)
        {
            this.test = test;
            this.consequent = consequent;
            this.alternate = alternate;
        }
    }
}