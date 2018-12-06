using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ImportSpecifierNode : BaseImportSpecifierNode
    {
        /// <inheritdoc />
        internal ImportSpecifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode imported, [NotNull] IdentifierNode local)
            : base(parser, start, startLocation, local)
        {
            Imported = imported;
        }

        [NotNull]
        public IdentifierNode Imported { get; }
    }
}
