using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExpressionStatementNode : BaseNode
    {
        public BaseNode expression;
        public string directive;

        public ExpressionStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}