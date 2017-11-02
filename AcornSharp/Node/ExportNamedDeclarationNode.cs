using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportNamedDeclarationNode : BaseNode
    {
        internal ExportNamedDeclarationNode([NotNull] Parser parser, Position start, Position end, ExpressionNode source, BaseNode declaration, IReadOnlyList<ExportSpecifierNode> specifiers) :
            base(parser, start, end)
        {
            Source = source;
            Declaration = declaration;
            Specifiers = specifiers;
        }

        public ExpressionNode Source { get; }
        public BaseNode Declaration { get; }
        public IReadOnlyList<ExportSpecifierNode> Specifiers { get; }
    }
}