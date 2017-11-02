using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BreakStatementNode : BaseNode
    {
        internal BreakStatementNode([NotNull] Parser parser, Position start, Position end, IdentifierNode label) :
            base(parser, start, end)
        {
            Label = label;
        }

        public IdentifierNode Label { get; }
    }
}