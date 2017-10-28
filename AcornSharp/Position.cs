using System;

namespace AcornSharp
{
    public sealed class Position : IEquatable<Position>
    {
        public Position(int line, int column, int index)
        {
            Line = line;
            Column = column;
            Index = index;
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Line == other.Line && Column == other.Column && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Position position && Equals(position);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (((Line * 397) ^ Column) * 397) ^ Index;
            }
        }

        public int Line { get; }

        public int Column { get; }

        public int Index { get; }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }
    }
}