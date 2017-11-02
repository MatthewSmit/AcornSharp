using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ConditionalExpressionNode : ExpressionNode
    {
        internal ConditionalExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode test, ExpressionNode consequent, ExpressionNode alternate) :
            base(parser, start, end)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        public ExpressionNode Test { get; }
        public ExpressionNode Consequent { get; }
        public ExpressionNode Alternate { get; }
    }
}