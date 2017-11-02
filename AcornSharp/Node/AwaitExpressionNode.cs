using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class AwaitExpressionNode : ExpressionNode
    {
        internal AwaitExpressionNode([NotNull] Parser parser, Position start, Position end, ExpressionNode argument) :
            base(parser, start, end)
        {
            Argument = argument;
        }

        public ExpressionNode Argument { get; }
    }
}