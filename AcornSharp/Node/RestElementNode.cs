using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class RestElementNode : BaseNode
    {
        public BaseNode argument;

        public RestElementNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }
    }
}