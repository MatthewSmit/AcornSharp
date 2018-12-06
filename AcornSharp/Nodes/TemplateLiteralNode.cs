using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class TemplateLiteralNode : ExpressionNode
    {
        /// <inheritdoc />
        internal TemplateLiteralNode([NotNull] Parser parser, int start, Position startLocation, [NotNull] [ItemNotNull] IList<ExpressionNode> expressions, [NotNull] [ItemNotNull] List<TemplateElementNode> quasis)
            : base(parser, start, startLocation)
        {
            Expressions = expressions;
            Quasis = quasis;
        }

        [NotNull]
        [ItemNotNull]
        public IList<ExpressionNode> Expressions { get; }

        [NotNull]
        [ItemNotNull]
        public List<TemplateElementNode> Quasis { get; }
    }
}