using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class VariableDeclaratorNode : BaseNode
    {
        /// <inheritdoc />
        internal VariableDeclaratorNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode id, [CanBeNull] ExpressionNode init)
            : base(parser, start, startLocation)
        {
            Id = id;
            Init = init;
        }

        [NotNull]
        public ExpressionNode Id { get; }

        [CanBeNull]
        public ExpressionNode Init { get; }
    }
}