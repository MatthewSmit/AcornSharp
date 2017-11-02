using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SuperNode : BaseNode
    {
        public SuperNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}