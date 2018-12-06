using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class SwitchCaseNode : BaseNode
    {
        /// <inheritdoc />
        internal SwitchCaseNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] ExpressionNode test, [NotNull] [ItemNotNull] IList<StatementNode> consequent)
            : base(parser, start, startLocation)
        {
            Test = test;
            Consequent = consequent;
        }

        [CanBeNull]
        public ExpressionNode Test { get; }

        [NotNull]
        [ItemNotNull]
        public IList<StatementNode> Consequent { get; }
    }
}