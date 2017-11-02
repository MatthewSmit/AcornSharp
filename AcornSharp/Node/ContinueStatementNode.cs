using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ContinueStatementNode : BaseNode
    {
        internal ContinueStatementNode([NotNull] Parser parser, Position start, Position end, IdentifierNode label) :
            base(parser, start, end)
        {
            Label = label;
        }

        public IdentifierNode Label { get; }
    }
}