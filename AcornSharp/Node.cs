using System.Diagnostics.CodeAnalysis;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public sealed partial class Parser
    {
        private void finishNode([NotNull] BaseNode node, NodeType type)
        {
            node.type = type;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
        }
    }
}
