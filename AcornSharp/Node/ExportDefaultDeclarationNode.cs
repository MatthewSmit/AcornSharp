using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportDefaultDeclarationNode : BaseNode
    {
        internal ExportDefaultDeclarationNode([NotNull] Parser parser, Position start, Position end, BaseNode declaration) :
            base(parser, start, end)
        {
            Declaration = declaration;
        }

        public BaseNode Declaration { get; }
    }
}