using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UnaryExpressionNode : BaseNode
    {
        public Operator @operator;
        public BaseNode argument;
        public bool prefix;

        public UnaryExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}