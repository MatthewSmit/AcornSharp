using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class YieldExpressionNode : ExpressionNode
    {
        public bool @delegate;
        public BaseNode argument;

        internal YieldExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}