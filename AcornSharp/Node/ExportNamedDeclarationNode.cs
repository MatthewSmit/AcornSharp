using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportNamedDeclarationNode : BaseNode
    {
        public BaseNode source;
        public BaseNode declaration;
        public IList<ExportSpecifierNode> specifiers;

        public ExportNamedDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}