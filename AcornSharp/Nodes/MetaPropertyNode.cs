using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class MetaPropertyNode : ExpressionNode
    {
        /// <inheritdoc />
        internal MetaPropertyNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode meta, [NotNull] IdentifierNode property)
            : base(parser, start, startLocation)
        {
            Meta = meta;
            Property = property;
        }

        [NotNull]
        public IdentifierNode Meta { get; }

        [NotNull]
        public IdentifierNode Property { get; }
    }
}
