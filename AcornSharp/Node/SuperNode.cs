using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SuperNode : ExpressionNode
    {
        public SuperNode(SourceLocation sourceLocation) :
            base(sourceLocation)
        {
        }

        internal SuperNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}