using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IdentifierNode : ExpressionNode
    {
        public IdentifierNode(SourceLocation sourceLocation, [NotNull] string name) :
            base(sourceLocation)
        {
            Name = name;
        }

        internal IdentifierNode([NotNull] Parser parser, Position start, Position end, [NotNull] string name) :
            base(parser, start, end)
        {
            Name = name;
        }

        [NotNull]        public string Name { get; }
    }
}