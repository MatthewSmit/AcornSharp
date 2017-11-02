using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SuperNode : ExpressionNode
    {
        internal SuperNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}