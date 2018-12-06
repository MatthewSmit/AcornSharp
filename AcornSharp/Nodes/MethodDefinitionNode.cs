using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class MethodDefinitionNode : BaseNode
    {
        /// <inheritdoc />
        internal MethodDefinitionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode key, bool computed, PropertyKind kind, bool @static, [NotNull] FunctionExpressionNode value)
            : base(parser, start, startLocation)
        {
            Key = key;
            Computed = computed;
            Kind = kind;
            Static = @static;
            Value = value;
        }

        [NotNull]
        public ExpressionNode Key { get; }

        public bool Computed { get; }

        public PropertyKind Kind { get; }

        public bool Static { get; }

        [NotNull]
        public FunctionExpressionNode Value { get; }
    }
}