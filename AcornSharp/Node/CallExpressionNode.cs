using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CallExpressionNode : ExpressionNode
    {
        public BaseNode callee;
        public IReadOnlyList<ExpressionNode> arguments;

        internal CallExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}