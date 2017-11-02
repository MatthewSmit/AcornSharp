using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IdentifierNode : BaseNode
    {
        public readonly string name;

        public IdentifierNode([NotNull] Parser parser, Position start, Position end, string name) :
            base(parser, start, end)
        {
            this.name = name;
        }
    }
}