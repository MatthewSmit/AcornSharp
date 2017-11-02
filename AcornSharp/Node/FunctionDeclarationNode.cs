using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class FunctionDeclarationNode : BaseNode, IDeclarationNode
    {
        public bool expression;
        public bool async;
        public bool generator;
        public IdentifierNode id;
        public IReadOnlyList<BaseNode> parameters;
        public BaseNode body;

        internal FunctionDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public IdentifierNode Id => id;
    }
}