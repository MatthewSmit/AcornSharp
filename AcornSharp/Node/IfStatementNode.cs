using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IfStatementNode : BaseNode
    {
        internal IfStatementNode([NotNull] Parser parser, Position start, Position end, [NotNull] ExpressionNode test, [NotNull] BaseNode consequent, [CanBeNull] BaseNode alternate) :
            base(parser, start, end)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        [NotNull]
        public ExpressionNode Test { get; }
        [NotNull]
        public BaseNode Consequent { get; }
        [CanBeNull]
        public BaseNode Alternate { get; }
    }
}