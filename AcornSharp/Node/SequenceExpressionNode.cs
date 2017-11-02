using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SequenceExpressionNode : ExpressionNode
    {
        public IReadOnlyList<ExpressionNode> expressions;

        internal SequenceExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}