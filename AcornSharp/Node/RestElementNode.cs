using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class RestElementNode : ExpressionNode
    {
        public RestElementNode(SourceLocation sourceLocation, ExpressionNode argument) :
            base(sourceLocation)
        {
            Argument = argument;
        }

        internal RestElementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode argument) :
            base(parser, start, end)
        {
            Argument = argument;
        }

        public ExpressionNode Argument { get; }
    }
}