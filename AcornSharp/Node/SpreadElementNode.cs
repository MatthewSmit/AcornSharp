using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SpreadElementNode : BaseNode
    {
        public BaseNode argument;

        public SpreadElementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}