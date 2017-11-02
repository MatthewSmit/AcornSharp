using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class EmptyStatementNode : BaseNode
    {
        public EmptyStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}