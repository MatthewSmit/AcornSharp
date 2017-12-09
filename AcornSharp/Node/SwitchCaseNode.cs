using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SwitchCaseNode : BaseNode
    {
        internal SwitchCaseNode([NotNull] Parser parser, Position start, Position end, [CanBeNull] BaseNode test, [NotNull] IReadOnlyList<BaseNode> consequent) :
            base(parser, start, end)
        {
            Test = test;
            Consequent = consequent;
        }

        [CanBeNull]
        public BaseNode Test { get; }
        [NotNull]
        public IReadOnlyList<BaseNode> Consequent { get; }
    }
}