using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TaggedTemplateExpressionNode : ExpressionNode
    {
        public BaseNode tag;
        public BaseNode quasi;

        internal TaggedTemplateExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}