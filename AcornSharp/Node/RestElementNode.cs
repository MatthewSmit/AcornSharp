using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class RestElementNode : ExpressionNode
    {
        public BaseNode argument;

        internal RestElementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}