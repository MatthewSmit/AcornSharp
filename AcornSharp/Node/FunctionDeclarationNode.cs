using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class FunctionDeclarationNode : BaseNode, IDeclarationNode
    {
        internal FunctionDeclarationNode([NotNull] Parser parser, Position start, Position end, bool expression, bool async, bool generator, IdentifierNode id, IReadOnlyList<ExpressionNode> parameters, BaseNode body) :
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
        public IdentifierNode Id { get; }
        public IReadOnlyList<ExpressionNode> Parameters { get; }
        public BaseNode Body { get; }
    }
}