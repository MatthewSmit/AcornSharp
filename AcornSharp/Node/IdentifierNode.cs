using System;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class IdentifierNode : BaseNode
    {
        public readonly string name;

        public IdentifierNode([NotNull] Parser parser, Position start, Position end, string name) :
            base(parser, start, end)
        {
            this.name = name;
        }

        public IdentifierNode(SourceLocation location, string name) :
            base(location)
        {
            this.name = name;
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is IdentifierNode realOther)
            {
                return base.TestEquals(other) &&
                       (name == null || string.Equals(name, realOther.name, StringComparison.Ordinal));
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is IdentifierNode realOther)
            {
                return base.Equals(other) &&
                       string.Equals(name, realOther.name, StringComparison.Ordinal);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() * 397) ^ name.GetHashCode();
        }
    }
}