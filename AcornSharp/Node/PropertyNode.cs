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
        public ExpressionNode value;

        internal PropertyNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}