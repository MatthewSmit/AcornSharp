using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TaggedTemplateExpressionNode : ExpressionNode
    {
        internal TaggedTemplateExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode tag, ExpressionNode quasi) :
            base(parser, start, end)
        {
            Tag = tag;
            Quasi = quasi;
        }

        public ExpressionNode Tag { get; }
        public ExpressionNode Quasi { get; }
    }
}