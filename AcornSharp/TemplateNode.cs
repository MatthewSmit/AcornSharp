using System;

namespace AcornSharp
{
    public class TemplateNode : IEquatable<TemplateNode>
    {
        public string raw;
        public string cooked;

        public bool Equals(TemplateNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(raw, other.raw) && string.Equals(cooked, other.cooked);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TemplateNode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((raw != null ? raw.GetHashCode() : 0) * 397) ^ (cooked != null ? cooked.GetHashCode() : 0);
            }
        }

        public static bool operator ==(TemplateNode left, TemplateNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TemplateNode left, TemplateNode right)
        {
            return !Equals(left, right);
        }
    }
}