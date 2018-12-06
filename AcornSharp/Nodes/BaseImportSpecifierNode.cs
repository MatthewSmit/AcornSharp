using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public abstract class BaseImportSpecifierNode : BaseNode
    {
        /// <inheritdoc />
        internal BaseImportSpecifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode local)
            : base(parser, start, startLocation)
        {
            Local = local;
        }

        [NotNull]
        public IdentifierNode Local { get; }
    }
}