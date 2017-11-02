using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassExpressionNode : BaseNode, IDeclarationNode
    {
        public IdentifierNode id;
        public BaseNode superClass;
        public BaseNode body;

        public ClassExpressionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public IdentifierNode Id => id;
    }
}