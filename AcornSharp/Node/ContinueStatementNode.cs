using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ContinueStatementNode : BaseNode
    {
        public IdentifierNode label;

        internal ContinueStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}