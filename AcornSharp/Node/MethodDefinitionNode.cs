using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MethodDefinitionNode : BaseNode
    {
        public MethodDefinitionNode(SourceLocation sourceLocation, PropertyKind kind, bool isStatic, ExpressionNode key, FunctionExpressionNode value) :
            base(sourceLocation)
        {
            Kind = kind;
            Computed = !(key is IdentifierNode);
            Static = isStatic;
            Key = key;
            Value = value;
        }

        internal MethodDefinitionNode([NotNull] Parser parser, Position start, Position end, PropertyKind kind, bool computed, bool @static, ExpressionNode key, FunctionExpressionNode value) :
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
        public FunctionExpressionNode Value { get; }
    }
}