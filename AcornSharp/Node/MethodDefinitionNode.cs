using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MethodDefinitionNode : BaseNode
    {
        internal MethodDefinitionNode([NotNull] Parser parser, Position start, Position end, PropertyKind kind, bool computed, bool @static, ExpressionNode key, ExpressionNode value) :
            base(parser, start, end)
        {
            Kind = kind;
            Computed = computed;
            Static = @static;
            Key = key;
            Value = value;
        }

        public PropertyKind Kind { get; }
        public bool Computed { get; }
        public bool Static { get; }
        public ExpressionNode Key { get; }
        public ExpressionNode Value { get; }
    }
}