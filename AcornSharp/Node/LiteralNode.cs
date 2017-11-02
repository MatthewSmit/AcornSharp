using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class LiteralNode : BaseNode
    {
        public LiteralValue value;
        public RegexNode regex;
        public string raw;

        public LiteralNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}