using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CallExpressionNode : ExpressionNode
    {
        internal CallExpressionNode([NotNull] Parser parser, Position start, Position end, BaseNode callee, IReadOnlyList<ExpressionNode> arguments) :
            base(parser, start, end)
        {
            Callee = callee;
            Arguments = arguments;
        }

        public BaseNode Callee { get; }
        public IReadOnlyList<ExpressionNode> Arguments { get; }
    }
}