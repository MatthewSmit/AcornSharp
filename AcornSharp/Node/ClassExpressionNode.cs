using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassExpressionNode : ExpressionNode, IDeclarationNode
    {
        internal ClassExpressionNode([NotNull] Parser parser, Position start, Position end, IdentifierNode id, ExpressionNode superClass, ClassBodyNode body) :
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