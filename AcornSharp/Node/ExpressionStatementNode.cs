using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExpressionStatementNode : BaseNode
    {
        public ExpressionStatementNode(SourceLocation sourceLocation, [NotNull] ExpressionNode expression) :
            base(sourceLocation)
        {
            Expression = expression;
        }

        internal ExpressionStatementNode([NotNull] Parser parser, Position start, Position end, [NotNull] ExpressionNode expression) :
            base(parser, start, end)
        {
            Expression = expression;
        }

        [NotNull]
        public ExpressionNode Expression { get; }
        public string Directive { get; internal set; }
    }
}