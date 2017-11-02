using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExpressionStatementNode : BaseNode
    {
        public BaseNode expression;
        public string directive;

        internal ExpressionStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}