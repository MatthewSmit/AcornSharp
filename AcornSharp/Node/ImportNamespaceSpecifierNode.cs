using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportNamespaceSpecifierNode : BaseNode
    {
        internal ImportNamespaceSpecifierNode([NotNull] Parser parser, Position start, Position end, IdentifierNode local) :
            base(parser, start, end)
        {
            Local = local;
        }

        public IdentifierNode Local { get; }
    }
}