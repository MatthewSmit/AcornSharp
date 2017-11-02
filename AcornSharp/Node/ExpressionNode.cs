using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public abstract class ExpressionNode : BaseNode
    {
        internal ExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}