using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class VariableDeclaratorNode : BaseNode, IDeclarationNode
    {
        public VariableKind kind;
        public BaseNode id;
        public BaseNode init;

        internal VariableDeclaratorNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        [CanBeNull]
        public IdentifierNode Id => id as IdentifierNode;
    }
}