using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MethodDefinitionNode : BaseNode
    {
        public PropertyKind kind;
        public bool computed;
        public bool @static;
        public BaseNode key;
        public BaseNode value;

        public MethodDefinitionNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}