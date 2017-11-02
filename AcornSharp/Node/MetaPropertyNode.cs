using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MetaPropertyNode : ExpressionNode
    {
        internal MetaPropertyNode([NotNull] Parser parser, Position start, Position end, IdentifierNode meta, IdentifierNode property) :
            base(parser, start, end)
        {
            Meta = meta;
            Property = property;
        }

        public IdentifierNode Meta { get; }
        public IdentifierNode Property { get; }
    }
}