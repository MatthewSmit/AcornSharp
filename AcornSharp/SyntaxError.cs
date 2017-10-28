using System;

namespace AcornSharp
{
    public sealed class SyntaxError : Exception
    {
        private readonly Position position;

        public SyntaxError(string message, Position position) :
            base(message)
        {
            this.position = position;
        }
    }
}