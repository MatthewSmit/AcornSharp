using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForOfStatementNode : BaseNode
    {
        internal ForOfStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode left, ExpressionNode right, BaseNode body) :
            base(parser, start, end)
        {
            Left = left;
            Right = right;
            Body = body;
        }

        public BaseNode Left { get; }
        public ExpressionNode Right { get; }
        public BaseNode Body { get; }
    }
}