using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TemplateElementNode : BaseNode
    {
        internal TemplateElementNode([NotNull] Parser parser, Position start, Position end, TemplateNode value, bool tail) :
            base(parser, start, end)
        {
            Value = value;
            Tail = tail;
        }

        public TemplateNode Value { get; }
        public bool Tail { get; }
    }
}