using System;
using JetBrains.Annotations;

namespace AcornSharp
{
    public sealed class RegexNode : IEquatable<RegexNode>
    {
        public string Pattern;
        public string Flags;

        public bool Equals([CanBeNull] RegexNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Pattern, other.Pattern) && string.Equals(Flags, other.Flags);
        }

        public override bool Equals([CanBeNull] object obj)
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
                return ((Pattern != null ? Pattern.GetHashCode() : 0) * 397) ^ (Flags != null ? Flags.GetHashCode() : 0);
            }
        }

        public static bool operator ==([CanBeNull] RegexNode left, [CanBeNull] RegexNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] RegexNode left, [CanBeNull] RegexNode right)
        {
            return !Equals(left, right);
        }
    }
}