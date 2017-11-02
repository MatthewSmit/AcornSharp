using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MemberExpressionNode : ExpressionNode
    {
        public readonly BaseNode @object;
        public readonly BaseNode property;
        public readonly bool computed;

        internal MemberExpressionNode([NotNull] Parser parser, Position start, Position end, BaseNode @object, BaseNode property, bool computed) :
            base(parser, start, end)
        {
            this.@object = @object;
            this.property = property;
            this.computed = computed;
        }
    }
}