using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TemplateLiteralNode : ExpressionNode
    {
        public IReadOnlyList<BaseNode> expressions;
        public IReadOnlyList<BaseNode> quasis;

        internal TemplateLiteralNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}