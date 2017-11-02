using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class NewExpressionNode : ExpressionNode
    {
        public BaseNode callee;
        public IReadOnlyList<ExpressionNode> arguments;

        internal NewExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}