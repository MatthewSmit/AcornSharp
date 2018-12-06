using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class VariableDeclarationNode : StatementNode
    {
        /// <inheritdoc />
        internal VariableDeclarationNode([NotNull] Parser parser, int start, Position startLocation, PropertyKind kind, [NotNull] [ItemNotNull] IList<VariableDeclaratorNode> declarations)
            : base(parser, start, startLocation)
        {
            Kind = kind;
            Declarations = declarations;
        }

        public PropertyKind Kind { get; }

        [NotNull]
        [ItemNotNull]
        public IList<VariableDeclaratorNode> Declarations { get; }
    }
}