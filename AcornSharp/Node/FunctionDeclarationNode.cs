using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class FunctionDeclarationNode : BaseNode, IDeclarationNode
    {
        internal FunctionDeclarationNode([NotNull] Parser parser, Position start, Position end, bool expression, bool async, bool generator, [CanBeNull] IdentifierNode id, [NotNull] IReadOnlyList<ExpressionNode> parameters, [NotNull] BaseNode body) :
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