using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class UnaryExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal UnaryExpressionNode([NotNull] Parser parser, int start, Position startLocation, Operator @operator, bool prefix, [NotNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Operator = @operator;
            Prefix = prefix;
            Argument = argument;
        }

        public Operator Operator { get; }

        public bool Prefix { get; }

        [NotNull]
        public ExpressionNode Argument { get; }
    }
}