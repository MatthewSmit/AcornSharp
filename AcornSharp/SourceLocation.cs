using System;

namespace AcornSharp
{
    public sealed class SourceLocation : IEquatable<SourceLocation>
    {
        public SourceLocation(Position start, Position end, string sourceFile = null)
        {
            Start = start;
            End = end;
            Source = sourceFile;
        }

        public SourceLocation(Parser p, Position start, Position end = null)
        {
            Start = start;
            End = end;
            if (p.sourceFile != null) Source = p.sourceFile;
        }

        public Position Start { get; }
        public Position End { get; }
        public string Source { get; }

        public bool Equals(SourceLocation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SourceLocation && Equals((SourceLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Start != null ? Start.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (End != null ? End.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(SourceLocation left, SourceLocation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SourceLocation left, SourceLocation right)
        {
            return !Equals(left, right);
        }
    }
}