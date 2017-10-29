using System.Diagnostics.CodeAnalysis;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public sealed partial class Parser
    {
        [NotNull]
        private BaseNode startNode()
        {
            return new BaseNode(this, start);
        }

        [NotNull]
        public BaseNode startNodeAt(Position pos)
        {
            return new BaseNode(this, pos);
        }

        // Finish an AST node, adding `type` and `end` properties.
        [NotNull]
        private static BaseNode finishNodeAt([NotNull] BaseNode node, NodeType type, Position pos)
        {
            node.type = type;
            node.loc = new SourceLocation(node.loc.Start, pos, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode finishNode([NotNull] BaseNode node, NodeType type)
        {
            return finishNodeAt(node, type, lastTokEnd);
        }
    }
}
