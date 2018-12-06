using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ExportAllDeclarationNode : BaseExportDeclarationNode
    {
        /// <inheritdoc />
        internal ExportAllDeclarationNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] ExpressionNode source)
            : base(parser, start, startLocation)
        {
            Source = source;
        }

        public ExpressionNode Source { get; }
    }
}
