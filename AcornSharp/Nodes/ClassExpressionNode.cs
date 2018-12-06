using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ClassExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ClassExpressionNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] IdentifierNode id, [CanBeNull] ExpressionNode superClass, [NotNull] ClassBodyNode body)
            : base(parser, start, startLocation)
        {
            Id = id;
            SuperClass = superClass;
            Body = body;
        }

        [CanBeNull]
        public IdentifierNode Id { get; }

        [CanBeNull]
        public ExpressionNode SuperClass { get; }

        [NotNull]
        public ClassBodyNode Body { get; }
    }
}