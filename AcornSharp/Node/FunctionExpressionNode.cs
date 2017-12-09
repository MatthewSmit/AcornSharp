using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    [PublicAPI]
    public sealed class FunctionExpressionNode : ExpressionNode, IDeclarationNode
    {
        public FunctionExpressionNode(SourceLocation sourceLocation, bool isAsync, bool isGenerator, [CanBeNull] IdentifierNode id, [NotNull] IReadOnlyList<ExpressionNode> parameters, [NotNull] ExpressionNode body) :
            base(sourceLocation)
        {
            if (isAsync && isGenerator)
            {
                throw new ArgumentException();
            }

            Expression = true;
            Async = isAsync;
            Generator = isGenerator;
            Id = id;
            Parameters = parameters;
            Body = body;
        }

        public FunctionExpressionNode(SourceLocation sourceLocation, bool isAsync, bool isGenerator, [CanBeNull] IdentifierNode id, [NotNull] IReadOnlyList<ExpressionNode> parameters, [NotNull] BlockStatementNode body) :
            base(sourceLocation)
        {
            if (isAsync && isGenerator)
            {
                throw new ArgumentException();
            }

            Async = isAsync;
            Generator = isGenerator;
            Id = id;
            Parameters = parameters;
            Body = body;
        }

        internal FunctionExpressionNode([NotNull] Parser parser, Position start, Position end, bool expression, bool async, bool generator, [CanBeNull] IdentifierNode id, [NotNull] IReadOnlyList<ExpressionNode> parameters, [NotNull] BaseNode body) :
            base(parser, start, end)
        {
            Expression = expression;
            Async = async;
            Generator = generator;
            Id = id;
            Parameters = parameters;
            Body = body;
        }

        public bool Expression { get; }
        public bool Async { get; }
        public bool Generator { get; }

        [CanBeNull]
        public IdentifierNode Id { get; }

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<ExpressionNode> Parameters { get; }

        [NotNull]
        public BaseNode Body { get; }
    }
}