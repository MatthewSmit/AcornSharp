using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassExpressionNode : ExpressionNode, IDeclarationNode
    {
        public IdentifierNode id;
        public ExpressionNode superClass;
        public ClassBodyNode body;

        internal ClassExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public IdentifierNode Id => id;
    }
}