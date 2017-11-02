using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ThisExpressionNode : ExpressionNode
    {
        internal ThisExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}