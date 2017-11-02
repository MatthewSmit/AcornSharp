using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MetaPropertyNode : BaseNode
    {
        public BaseNode meta;
        public BaseNode property;

        public MetaPropertyNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}