using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class YieldExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal YieldExpressionNode([NotNull] Parser parser, int start, Position startLocation, bool @delegate, [CanBeNull] ExpressionNode argument)
            : base(parser, start, startLocation)
        {
            Delegate = @delegate;
            Argument = argument;
        }

        public bool Delegate { get; }

        [CanBeNull]
        public ExpressionNode Argument { get; }
    }
}