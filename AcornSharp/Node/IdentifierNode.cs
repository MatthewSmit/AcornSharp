using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IdentifierNode : ExpressionNode
    {
        public readonly string name;

        internal IdentifierNode([NotNull] Parser parser, Position start, Position end, string name) :
            base(parser, start, end)
        {
            this.name = name;
        }
    }
}