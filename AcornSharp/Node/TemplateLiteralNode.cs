using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class TemplateLiteralNode : ExpressionNode
    {
        internal TemplateLiteralNode([NotNull] Parser parser, Position start, Position end, IReadOnlyList<ExpressionNode> expressions, IReadOnlyList<TemplateElementNode> quasis) :
            base(parser, start, end)
        {
            Expressions = expressions;
            Quasis = quasis;
        }

        public IReadOnlyList<ExpressionNode> Expressions { get; }
        public IReadOnlyList<TemplateElementNode> Quasis { get; }
    }
}