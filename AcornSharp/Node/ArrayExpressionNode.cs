using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ArrayExpressionNode : ExpressionNode
    {
        internal ArrayExpressionNode([NotNull] Parser parser, Position start, Position end, IReadOnlyList<ExpressionNode> elements) :
            base(parser, start, end)
        {
            Elements = elements;
        }

        public IReadOnlyList<ExpressionNode> Elements { get; }
    }
}