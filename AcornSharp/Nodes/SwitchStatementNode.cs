using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class SwitchStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal SwitchStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode discriminant, [NotNull] [ItemNotNull] List<SwitchCaseNode> cases)
            : base(parser, start, startLocation)
        {
            Discriminant = discriminant;
            Cases = cases;
        }

        [NotNull]
        public ExpressionNode Discriminant { get; }

        [NotNull]
        [ItemNotNull]
        public List<SwitchCaseNode> Cases { get; }
    }
}
