using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class VariableDeclaratorNode : BaseNode, IDeclarationNode
    {
        internal VariableDeclaratorNode([NotNull] Parser parser, Position start, Position end, VariableKind kind, BaseNode id, ExpressionNode init) :
            base(parser, start, end)
        {
            Kind = kind;
            Id = id;
            Init = init;
        }

        public VariableKind Kind { get; }
        public BaseNode Id { get; }
        public ExpressionNode Init { get; }

        [CanBeNull]
        IdentifierNode IDeclarationNode.Id => Id as IdentifierNode;
    }
}