using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SequenceExpressionNode : ExpressionNode
    {
        internal SequenceExpressionNode([NotNull] Parser parser, Position start, Position end, IReadOnlyList<ExpressionNode> expressions) :
            base(parser, start, end)
        {
            Expressions = expressions;
        }

        public IReadOnlyList<ExpressionNode> Expressions { get; }
    }
}