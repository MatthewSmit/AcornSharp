using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ClassBodyNode : BaseNode
    {
        /// <inheritdoc />
        internal ClassBodyNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<MethodDefinitionNode> body)
            : base(parser, start, startLocation)
        {
            Body = body;
        }

        [NotNull]
        [ItemNotNull]
        public IList<MethodDefinitionNode> Body { get; }
    }
}