using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassDeclarationNode : BaseNode, IDeclarationNode
    {
        public IdentifierNode id;
        public ExpressionNode superClass;
        public ClassBodyNode body;

        internal ClassDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public IdentifierNode Id => id;
    }
}