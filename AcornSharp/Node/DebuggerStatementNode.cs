using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class DebuggerStatementNode : BaseNode
    {
        internal DebuggerStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}