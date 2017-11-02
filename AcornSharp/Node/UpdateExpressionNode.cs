using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class UpdateExpressionNode : BaseNode
    {
        public Operator @operator;
        public BaseNode argument;
        public bool prefix;

        public UpdateExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}