using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ArrowFunctionExpressionNode : BaseNode
    {
        public bool expression;
        public bool async;
        public IList<BaseNode> parameters;
        public BaseNode body;

        public ArrowFunctionExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}