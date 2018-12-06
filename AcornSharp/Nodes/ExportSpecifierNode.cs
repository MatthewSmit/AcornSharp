using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ExportSpecifierNode : BaseNode
    {
        /// <inheritdoc />
        internal ExportSpecifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] IdentifierNode local, [NotNull] IdentifierNode exported)
            : base(parser, start, startLocation)
        {
            Local = local;
            Exported = exported;
        }

        [NotNull]
        public IdentifierNode Local { get; }

        [NotNull]
        public IdentifierNode Exported { get; }
    }
}
