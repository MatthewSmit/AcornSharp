using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForOfStatementNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public BaseNode body;

        public ForOfStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}