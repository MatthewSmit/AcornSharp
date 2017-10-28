using System;

namespace AcornSharp
{
    public class RegexNode : IEquatable<RegexNode>
    {
        public string pattern;
        public string flags;
        public object value;

        public bool Equals(RegexNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(pattern, other.pattern) && string.Equals(flags, other.flags);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RegexNode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((pattern != null ? pattern.GetHashCode() : 0) * 397) ^ (flags != null ? flags.GetHashCode() : 0);
            }
        }

        public static bool operator ==(RegexNode left, RegexNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RegexNode left, RegexNode right)
        {
            return !Equals(left, right);
        }
    }
}