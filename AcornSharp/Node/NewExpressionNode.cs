using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class NewExpressionNode : BaseNode
    {
        public BaseNode callee;
        public IList<BaseNode> arguments;

        public NewExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}