using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class CallExpressionNode : BaseNode
    {
        public BaseNode callee;
        public IList<BaseNode> arguments;

        public CallExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}