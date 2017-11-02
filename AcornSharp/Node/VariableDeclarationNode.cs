using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class VariableDeclarationNode : BaseNode
    {
        public VariableKind kind;
        public IList<BaseNode> declarations;

        public VariableDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public VariableDeclarationNode(SourceLocation location) :
            base(location)
        {
        }
    }
}