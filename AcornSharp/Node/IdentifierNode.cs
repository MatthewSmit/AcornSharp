using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IdentifierNode : ExpressionNode
    {
        internal IdentifierNode([NotNull] Parser parser, Position start, Position end, string name) :
            base(parser, start, end)
        {
            Name = name;
        }

        public string Name { get; }
    }
}