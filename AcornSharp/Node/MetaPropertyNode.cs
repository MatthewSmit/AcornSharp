using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class MetaPropertyNode : ExpressionNode
    {
        public BaseNode meta;
        public BaseNode property;

        internal MetaPropertyNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}