using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IfStatementNode : BaseNode
    {
        internal IfStatementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode test, BaseNode consequent, BaseNode alternate) :
            base(parser, start, end)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        public ExpressionNode Test { get; }
        public BaseNode Consequent { get; }
        public BaseNode Alternate { get; }
    }
}