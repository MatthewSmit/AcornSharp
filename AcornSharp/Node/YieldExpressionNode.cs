using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class YieldExpressionNode : ExpressionNode
    {
        internal YieldExpressionNode([NotNull] Parser parser, Position start, Position end, bool @delegate, ExpressionNode argument) :
            base(parser, start, end)
        {
            Delegate = @delegate;
            Argument = argument;
        }

        public bool Delegate { get; }
        public ExpressionNode Argument { get; }
    }
}