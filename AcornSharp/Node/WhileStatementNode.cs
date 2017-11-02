using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class WhileStatementNode : BaseNode
    {
        internal WhileStatementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode test, BaseNode body) :
            base(parser, start, end)
        {
            Test = test;
            Body = body;
        }

        public ExpressionNode Test { get; }
        public BaseNode Body { get; }
    }
}