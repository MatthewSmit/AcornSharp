using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TaggedTemplateExpressionNode : BaseNode
    {
        public BaseNode tag;
        public BaseNode quasi;

        public TaggedTemplateExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}