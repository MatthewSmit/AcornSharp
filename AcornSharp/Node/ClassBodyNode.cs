using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassBodyNode : BaseNode
    {
        public IList<BaseNode> body;

        public ClassBodyNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
            return hashCode;
        }
    }
}