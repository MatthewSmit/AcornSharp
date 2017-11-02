using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CatchClauseNode : BaseNode
    {
        public BaseNode param;
        public BaseNode body;

        public CatchClauseNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}