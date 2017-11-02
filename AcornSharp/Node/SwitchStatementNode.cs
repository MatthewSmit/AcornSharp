using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class SwitchStatementNode : BaseNode
    {
        internal SwitchStatementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode discriminant, IReadOnlyList<SwitchCaseNode> cases) :
            base(parser, start, end)
        {
            Discriminant = discriminant;
            Cases = cases;
        }

        public ExpressionNode Discriminant { get; }
        public IReadOnlyList<SwitchCaseNode> Cases { get; }
    }
}