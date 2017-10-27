using System;

namespace AcornSharp
{
    public sealed class SyntaxError : Exception
    {
        private int pos;
        private Position loc;
        private int raisedAt;

        public SyntaxError(string message, int pos, Position loc, int raisedAt) :
            base(message)
        {
            this.pos = pos;
            this.loc = loc;
            this.raisedAt = raisedAt;
        }
    }
}