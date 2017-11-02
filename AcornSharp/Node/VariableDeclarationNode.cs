using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class VariableDeclarationNode : BaseNode
    {
        public VariableKind kind;
        public IReadOnlyList<BaseNode> declarations;

        internal VariableDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}