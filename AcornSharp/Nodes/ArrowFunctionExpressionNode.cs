using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class ArrowFunctionExpressionNode : ExpressionNode
    {
        /// <inheritdoc />
        internal ArrowFunctionExpressionNode([NotNull] Parser parser, int start, Position startLocation, bool async, [NotNull] [ItemCanBeNull] IList<ExpressionNode> parameters, bool expression, [NotNull] BaseNode body)
            : base(parser, start, startLocation)
        {
            Async = async;
            Parameters = parameters;
            Expression = expression;
            Body = body;
        }

        [CanBeNull]
        public ExpressionNode Id => null;

        public bool Generator => false;

        public bool Async { get; }

        [NotNull]
        [ItemCanBeNull]
        public IList<ExpressionNode> Parameters { get; }

        public bool Expression { get; }

        [NotNull]
        public BaseNode Body { get; }
    }
}
