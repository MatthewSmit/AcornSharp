using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class IdentifierNode : ExpressionNode
    {
        /// <inheritdoc />
        internal IdentifierNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] string name)
            : base(parser, start, startLocation)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; }
    }
}
