using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IfStatementNode : BaseNode
    {
        public readonly BaseNode test;
        public readonly BaseNode consequent;
        public readonly BaseNode alternate;

        internal IfStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode test, BaseNode consequent, BaseNode alternate) :
            base(parser, start, end)
        {
            this.test = test;
            this.consequent = consequent;
            this.alternate = alternate;
        }
    }
}