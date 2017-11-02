using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ReturnStatementNode : BaseNode
    {
        public BaseNode argument;

        internal ReturnStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}