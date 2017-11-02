using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SwitchCaseNode : BaseNode
    {
        public BaseNode test;
        public IList<BaseNode> consequent;

        public SwitchCaseNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}