using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AssignmentPatternNode : BaseNode
    {
        public BaseNode left;
        public BaseNode right;

        public AssignmentPatternNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}