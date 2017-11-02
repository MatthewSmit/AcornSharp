using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SwitchCaseNode : BaseNode
    {
        internal SwitchCaseNode([NotNull] Parser parser, Position start, Position end, BaseNode test, IReadOnlyList<BaseNode> consequent) :
            base(parser, start, end)
        {
            Test = test;
            Consequent = consequent;
        }

        public BaseNode Test { get; }
        public IReadOnlyList<BaseNode> Consequent { get; }
    }
}