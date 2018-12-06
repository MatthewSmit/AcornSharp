using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class PropertyNode : ExpressionNode
    {
        [NotNull] private ExpressionNode value;

        /// <inheritdoc />
        internal PropertyNode([NotNull] Parser parser, int start, Position startLocation, bool computed, [NotNull] ExpressionNode key, PropertyKind kind, [NotNull] ExpressionNode value, bool method, bool shorthand)
            : base(parser, start, startLocation)
        {
            Computed = computed;
            Key = key;
            Kind = kind;
            this.value = value;
            Method = method;
            Shorthand = shorthand;
        }

        public bool Computed { get; }

        [NotNull]
        public ExpressionNode Key { get; }

        public PropertyKind Kind { get; }

        [NotNull]
        public ExpressionNode Value => value;

        [NotNull]
        internal ref ExpressionNode RefValue => ref value;

        public bool Method { get; }

        public bool Shorthand { get; }
    }
}