using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SequenceExpressionNode : BaseNode
    {
        public IList<BaseNode> expressions;

        public SequenceExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}