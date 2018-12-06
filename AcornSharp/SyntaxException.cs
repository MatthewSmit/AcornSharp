using System;

namespace AcornSharp
{
    public sealed class SyntaxException : Exception
    {
        public SyntaxException(string message, int position, Position location)
            : base(message)
        {
            Position = position;
            Location = location;
        }

        public int Position { get; }
        public Position Location { get; }
    }
}