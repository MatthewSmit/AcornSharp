using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public abstract class BaseExportDeclarationNode : StatementNode
    {
        /// <inheritdoc />
        internal BaseExportDeclarationNode([NotNull] Parser parser, int start, Position startLocation)
            : base(parser, start, startLocation)
        {
        }
    }
}