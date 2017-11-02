using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LabelledStatementNode : BaseNode
    {
        internal LabelledStatementNode([NotNull] Parser parser, Position start, Position end, IdentifierNode label, BaseNode body) :
            base(parser, start, end)
        {
            Label = label;
            Body = body;
        }

        public IdentifierNode Label { get; }
        public BaseNode Body { get; }
    }
}