using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportSpecifierNode : BaseNode
    {
        public IdentifierNode local;
        public IdentifierNode exported;

        public ExportSpecifierNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}