using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class BlockStatementNode : StatementNode
    {
        /// <inheritdoc />
        internal BlockStatementNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<StatementNode> body)
            : base(parser, start, startLocation)
        {
            Body = body;
        }

        [NotNull]
        [ItemNotNull]
        public IList<StatementNode> Body { get; }
    }
}