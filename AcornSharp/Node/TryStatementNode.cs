using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TryStatementNode : BaseNode
    {
        public readonly BaseNode block;
        public readonly BaseNode handler;
        public readonly BaseNode finaliser;

        internal TryStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode block, BaseNode handler, BaseNode finaliser) :
            base(parser, start, end)
        {
            this.block = block;
            this.handler = handler;
            this.finaliser = finaliser;
        }
    }
}