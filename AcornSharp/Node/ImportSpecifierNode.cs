using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportSpecifierNode : BaseNode
    {
        internal ImportSpecifierNode([NotNull] Parser parser, Position start, Position end, IdentifierNode local, IdentifierNode imported) :
            base(parser, start, end)
        {
            Local = local;
            Imported = imported;
        }

        public IdentifierNode Local { get; }
        public IdentifierNode Imported { get; }
    }
}