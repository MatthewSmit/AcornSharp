using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ParenthesisedExpressionNode : BaseNode
    {
        public BaseNode expression;

        public ParenthesisedExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}