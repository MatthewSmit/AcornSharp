using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class EmptyStatementNode : BaseNode
    {
        internal EmptyStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}