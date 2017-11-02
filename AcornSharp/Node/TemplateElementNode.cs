using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TemplateElementNode : BaseNode
    {
        public TemplateNode value;
        public bool tail;

        public TemplateElementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}