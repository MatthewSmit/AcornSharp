using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class PropertyNode : BaseNode
    {
        public PropertyKind kind;
        public bool computed;
        public bool shorthand;
        public bool method;
        public BaseNode key;
        public BaseNode value;

        public PropertyNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}