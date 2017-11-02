using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ImportDeclarationNode : BaseNode
    {
        public BaseNode source;
        public IList<BaseNode> specifiers;

        public ImportDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}