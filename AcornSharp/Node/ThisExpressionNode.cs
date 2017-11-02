using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ThisExpressionNode : BaseNode
    {
        public ThisExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}