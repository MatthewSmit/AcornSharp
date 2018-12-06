using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ExportNamedDeclarationNode : BaseExportDeclarationNode
    {
        /// <inheritdoc />
        internal ExportNamedDeclarationNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] StatementNode declaration, [NotNull] [ItemNotNull] IList<ExportSpecifierNode> specifiers, [CanBeNull] ExpressionNode source)
            : base(parser, start, startLocation)
        {
            Declaration = declaration;
            Specifiers = specifiers;
            Source = source;
        }

        [CanBeNull]
        public StatementNode Declaration { get; }

        [NotNull]
        [ItemNotNull]
        public IList<ExportSpecifierNode> Specifiers { get; }

        [CanBeNull]
        public ExpressionNode Source { get; }
    }
}