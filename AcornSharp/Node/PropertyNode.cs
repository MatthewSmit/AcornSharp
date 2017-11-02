using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class PropertyNode : BaseNode
    {
        internal PropertyNode([NotNull] Parser parser, Position start, Position end, PropertyKind kind, bool computed, bool shorthand, bool method, ExpressionNode key, ExpressionNode value) :
            base(parser, start, end)
        {
            Kind = kind;
            Computed = computed;
            Shorthand = shorthand;
            Method = method;
            Key = key;
            Value = value;
        }

        public PropertyKind Kind { get; }
        public bool Computed { get; }
        public bool Shorthand { get; }
        public bool Method { get; }
        public ExpressionNode Key { get; }
        public ExpressionNode Value { get; }
    }
}