using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ImportDeclarationNode : StatementNode
    {
        /// <inheritdoc />
        internal ImportDeclarationNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<BaseImportSpecifierNode> specifiers, [NotNull] ExpressionNode source)
            : base(parser, start, startLocation)
        {
            Specifiers = specifiers;
            Source = source;
        }

        [NotNull]
        [ItemNotNull]
        public IList<BaseImportSpecifierNode> Specifiers { get; }

        [NotNull]
        public ExpressionNode Source { get; }
    }
}
