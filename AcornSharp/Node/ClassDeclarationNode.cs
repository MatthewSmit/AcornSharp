using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassDeclarationNode : BaseNode, IDeclarationNode
    {
        internal ClassDeclarationNode([NotNull] Parser parser, Position start, Position end, IdentifierNode id, ExpressionNode superClass, ClassBodyNode body) :
            base(parser, start, end)
        {
            Id = id;
            SuperClass = superClass;
            Body = body;
        }

        public IdentifierNode Id { get; }
        public ExpressionNode SuperClass { get; }
        public ClassBodyNode Body { get; }
    }
}