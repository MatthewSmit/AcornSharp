using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LabelledStatementNode : BaseNode
    {
        public IdentifierNode label;
        public BaseNode body;

        public LabelledStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}