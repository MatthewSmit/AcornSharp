using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportSpecifierNode : BaseNode
    {
        public IdentifierNode local;
        public IdentifierNode imported;

        public ImportSpecifierNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}