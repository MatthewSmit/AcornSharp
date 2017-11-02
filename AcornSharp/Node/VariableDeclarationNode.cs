using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class VariableDeclarationNode : BaseNode
    {
        internal VariableDeclarationNode([NotNull] Parser parser, Position start, Position end, VariableKind kind, IReadOnlyList<VariableDeclaratorNode> declarations) :
            base(parser, start, end)
        {
            Kind = kind;
            Declarations = declarations;
        }

        public VariableKind Kind { get; }
        public IReadOnlyList<VariableDeclaratorNode> Declarations { get; }
    }
}