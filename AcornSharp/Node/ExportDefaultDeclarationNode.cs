using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportDefaultDeclarationNode : BaseNode
    {
        public BaseNode declaration;

        public ExportDefaultDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}