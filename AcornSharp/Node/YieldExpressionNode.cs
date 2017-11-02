using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class YieldExpressionNode : BaseNode
    {
        public bool @delegate;
        public BaseNode argument;

        public YieldExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}