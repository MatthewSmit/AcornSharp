using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class MemberExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal MemberExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode @object, [NotNull] ExpressionNode property, bool computed)
            : base(parser, start, startLocation)
        {
            Object = @object;
            Property = property;
            Computed = computed;
        }

        [NotNull]
        public ExpressionNode Object { get; }

        [NotNull]
        public ExpressionNode Property { get; }

        public bool Computed { get; }
    }
}
