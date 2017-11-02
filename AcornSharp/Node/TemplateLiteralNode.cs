using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TemplateLiteralNode : BaseNode
    {
        public IList<BaseNode> expressions;
        public IList<BaseNode> quasis;

        public TemplateLiteralNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}