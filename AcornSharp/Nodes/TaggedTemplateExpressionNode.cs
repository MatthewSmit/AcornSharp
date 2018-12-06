using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class TaggedTemplateExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal TaggedTemplateExpressionNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode tag, [NotNull] TemplateLiteralNode quasi)
            : base(parser, start, startLocation)
        {
            Tag = tag;
            Quasi = quasi;
        }

        [NotNull]
        public ExpressionNode Tag { get; }

        [NotNull]
        public TemplateLiteralNode Quasi { get; }
    }
}