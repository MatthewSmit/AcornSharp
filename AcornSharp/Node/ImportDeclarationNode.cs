using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportDeclarationNode : BaseNode
    {
        internal ImportDeclarationNode([NotNull] Parser parser, Position start, Position end, ExpressionNode source, IReadOnlyList<BaseNode> specifiers) :
            base(parser, start, end)
        {
            Source = source;
            Specifiers = specifiers;
        }

        public ExpressionNode Source { get; }
        public IReadOnlyList<BaseNode> Specifiers { get; }
    }
}