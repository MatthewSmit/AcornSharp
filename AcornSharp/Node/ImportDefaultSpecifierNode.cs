using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportDefaultSpecifierNode : BaseNode
    {
        public IdentifierNode local;

        public ImportDefaultSpecifierNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}