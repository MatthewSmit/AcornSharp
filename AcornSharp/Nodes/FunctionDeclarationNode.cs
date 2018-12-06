using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class FunctionDeclarationNode : StatementNode
    {
        /// <inheritdoc />
        internal FunctionDeclarationNode([NotNull] Parser parser, int start, Position startLocation, [CanBeNull] IdentifierNode id, bool generator, bool async, [NotNull] [ItemCanBeNull] IList<ExpressionNode> parameters, bool expression, [NotNull] BaseNode body)
            : base(parser, start, startLocation)
        {
            Id = id;
            Generator = generator;
            Async = async;
            Parameters = parameters;
            Expression = expression;
            Body = body;
        }

        [CanBeNull]
        public IdentifierNode Id { get; }

        public bool Generator { get; }

        public bool Async { get; }

        [NotNull]
        [ItemCanBeNull]
        public IList<ExpressionNode> Parameters { get; }

        public bool Expression { get; }

        [NotNull]
        public BaseNode Body { get; }
    }
}
