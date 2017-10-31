using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class DebuggerStatementNode : BaseNode
    {
        public DebuggerStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public DebuggerStatementNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is DebuggerStatementNode)
            {
                return base.TestEquals(other);
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is DebuggerStatementNode)
            {
                return base.Equals(other);
            }
            return false;
        }
    }
}