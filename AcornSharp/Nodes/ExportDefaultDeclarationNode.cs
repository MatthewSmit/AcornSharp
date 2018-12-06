using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ExportDefaultDeclarationNode : BaseExportDeclarationNode
    {
        /// <inheritdoc />
        internal ExportDefaultDeclarationNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] BaseNode declaration)
            : base(parser, start, startLocation)
        {
            Declaration = declaration;
        }

        [NotNull]
        public BaseNode Declaration { get; }
    }
}