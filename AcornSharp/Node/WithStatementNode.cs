using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class WithStatementNode : BaseNode
    {
        public BaseNode @object;
        public BaseNode body;

        internal WithStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}