using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportSpecifierNode : BaseNode
    {
        internal ExportSpecifierNode([NotNull] Parser parser, Position start, Position end, IdentifierNode local, IdentifierNode exported) :
            base(parser, start, end)
        {
            Local = local;
            Exported = exported;
        }

        public IdentifierNode Local { get; }
        public IdentifierNode Exported { get; }
    }
}