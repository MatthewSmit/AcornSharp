using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ArrowFunctionExpressionNode : ExpressionNode
    {
        internal ArrowFunctionExpressionNode([NotNull] Parser parser, Position start, Position end, bool expression, bool async, IReadOnlyList<ExpressionNode> parameters, BaseNode body) :
            base(parser, start, end)
        {
            Expression = expression;
            Async = async;
            Parameters = parameters;
            Body = body;
        }

        public bool Expression { get; }
        public bool Async { get; }
        public IReadOnlyList<ExpressionNode> Parameters { get; }
        public BaseNode Body { get; }
    }
}