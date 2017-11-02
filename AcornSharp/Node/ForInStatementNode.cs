using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForInStatementNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;
        public BaseNode body;

        public ForInStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}