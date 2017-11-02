using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class DebuggerStatementNode : BaseNode
    {
        public DebuggerStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}