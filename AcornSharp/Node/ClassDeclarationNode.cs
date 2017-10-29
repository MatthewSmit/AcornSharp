using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ClassDeclarationNode : BaseNode
    {
        public ClassDeclarationNode([NotNull] Parser parser, Position start, Position end) :
            base(parser, start, end)
        {
        }

        public ClassDeclarationNode(SourceLocation location) :
            base(location)
        {
        }

        public override bool TestEquals(BaseNode other)
        {
            if (other is ClassDeclarationNode realOther)
            {
                return base.TestEquals(other);
            }
            return false;
        }

        public override bool Equals(BaseNode other)
        {
            if (other is ClassDeclarationNode realOther)
            {
                return base.Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}