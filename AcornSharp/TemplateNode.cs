using System;
using JetBrains.Annotations;

namespace AcornSharp
{
    public sealed class TemplateNode : IEquatable<TemplateNode>
    {
        public TemplateNode(string raw, string cooked)
        {
            Raw = raw;
            Cooked = cooked;
        }

        public bool Equals([CanBeNull] TemplateNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Raw, other.Raw) && string.Equals(Cooked, other.Cooked);
        }

        public override bool Equals([CanBeNull] object obj)
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
                return ((Raw != null ? Raw.GetHashCode() : 0) * 397) ^ (Cooked != null ? Cooked.GetHashCode() : 0);
            }
        }

        public static bool operator ==([CanBeNull] TemplateNode left, [CanBeNull] TemplateNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] TemplateNode left, [CanBeNull] TemplateNode right)
        {
            return !Equals(left, right);
        }

        public string Raw { get; }
        public string Cooked { get; }
    }
}