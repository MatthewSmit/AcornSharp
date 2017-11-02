using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForStatementNode : BaseNode
    {
        public BaseNode init;
        public BaseNode test;
        public BaseNode update;
        public BaseNode body;

        public ForStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}