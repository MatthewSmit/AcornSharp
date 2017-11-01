using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ProgramNode : BaseNode
    {
        public IList<BaseNode> body;

        public ProgramNode([NotNull] Parser parser, Position start, Position end, SourceType sourceType = SourceType.Script) :
            base(parser, start, end)
        {
            SourceType = sourceType;
        }

        public ProgramNode(SourceLocation location, SourceType sourceType = SourceType.Script) :
            base(location)
        {
            SourceType = sourceType;
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is ProgramNode realOther)
            {
                if (!base.TestEquals(other)) return false;
                if (body != null && !TestEquals(body, realOther.body)) return false;
                if (SourceType == SourceType.Module && !Equals(SourceType, realOther.SourceType)) return false;
                return true;
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is ProgramNode realOther)
            {
                if (!base.Equals(other)) return false;
                if (!Equals(body, realOther.body)) return false;
                if (!Equals(SourceType, realOther.SourceType)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ SourceType.GetHashCode();
            return hashCode;
        }

        public SourceType SourceType { get; }
    }
}
