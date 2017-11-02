using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AwaitExpressionNode : BaseNode
    {
        public BaseNode argument;

        public AwaitExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}