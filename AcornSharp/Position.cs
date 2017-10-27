using System;

namespace AcornSharp
{
    public sealed class Position : IEquatable<Position>
    {
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public Position Offset(int n)
        {
            return new Position(Line, Column + n);
        }

        public int Line { get; }

        public int Column { get; }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Line == other.Line && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Position && Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Line * 397) ^ Column;
            }
        }

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