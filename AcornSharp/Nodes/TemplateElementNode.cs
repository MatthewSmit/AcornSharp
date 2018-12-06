using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class TemplateElementNode : BaseNode
    {
        /// <inheritdoc />
        internal TemplateElementNode([NotNull] Parser parser, int start, Position startLocation, TemplateValue value, bool tail)
            : base(parser, start, startLocation)
        {
            Value = value;
            Tail = tail;
        }

        public TemplateValue Value { get; }

        public bool Tail { get; }
    }
}