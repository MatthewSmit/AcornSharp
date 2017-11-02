using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class BreakStatementNode : BaseNode
    {
        public IdentifierNode label;

        internal BreakStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}