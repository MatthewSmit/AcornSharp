using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportDefaultSpecifierNode : BaseNode
    {
        internal ImportDefaultSpecifierNode([NotNull] Parser parser, Position start, Position end, IdentifierNode local) :
            base(parser, start, end)
        {
            Local = local;
        }

        public IdentifierNode Local { get; }
    }
}