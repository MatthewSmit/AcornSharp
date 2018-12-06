using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ImportNamespaceSpecifierNode : BaseImportSpecifierNode
    {
        /// <inheritdoc />
        internal ImportNamespaceSpecifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode local)
            : base(parser, start, startLocation, local)
        {
        }
    }
}
