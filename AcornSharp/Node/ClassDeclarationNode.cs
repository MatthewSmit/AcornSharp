using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassDeclarationNode : BaseNode, IDeclarationNode
    {
        public IdentifierNode id;
        public BaseNode superClass;
        public BaseNode body;

        public ClassDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public IdentifierNode Id => id;
    }
}