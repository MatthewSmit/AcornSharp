using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MemberExpressionNode : ExpressionNode
    {
        internal MemberExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode @object, ExpressionNode property, bool computed) :
            base(parser, start, end)
        {
            Object = @object;
            Property = property;
            Computed = computed;
        }

        public ExpressionNode Object { get; }
        public ExpressionNode Property { get; }
        public bool Computed { get; }
    }
}