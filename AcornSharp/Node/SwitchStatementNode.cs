using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SwitchStatementNode : BaseNode
    {
        public BaseNode discriminant;
        public IReadOnlyList<BaseNode> cases;

        internal SwitchStatementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}