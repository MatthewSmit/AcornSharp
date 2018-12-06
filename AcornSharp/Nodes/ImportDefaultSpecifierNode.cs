using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ImportDefaultSpecifierNode : BaseImportSpecifierNode
    {
        /// <inheritdoc />
        internal ImportDefaultSpecifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode local)
            : base(parser, start, startLocation, local)
        {
        }
    }
}
