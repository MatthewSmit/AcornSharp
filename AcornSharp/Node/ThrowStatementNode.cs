using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ThrowStatementNode : BaseNode
    {
        public BaseNode argument;

        public ThrowStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}