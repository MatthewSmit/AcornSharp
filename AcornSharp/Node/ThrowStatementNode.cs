using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ThrowStatementNode : BaseNode
    {
        internal ThrowStatementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode argument) :
            base(parser, start, end)
        {
            Argument = argument;
        }

        public ExpressionNode Argument { get; }
    }
}