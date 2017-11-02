using System;

namespace AcornSharp
{
    public sealed class SyntaxError : Exception
    {
        public SyntaxError(string message, Position position) :
            base(message)
        {
            Position = position;
        }

        public Position Position { get; }
    }
}