namespace AcornSharp
{
    public sealed partial class Parser
    {
        // This function is used to raise exceptions on parse errors. It
        // takes an offset integer (into the current `input`) to indicate
        // the location of the error, attaches the position to the end
        // of the error message, and then raises a `SyntaxError` with that
        // message.
        private void raise(int pos, string message)
        {
            var loc = getLineInfo(input, pos);
            message += " (" + loc.Line + ":" + loc.Column + ")";
            var err = new SyntaxError(message, pos, loc, this.pos.Index);
            throw err;
        }

        private void raiseRecoverable(int pos, string message)
        {
            raise(pos, message);
        }

        public Position curPosition()
        {
            return new Position(pos.Line, pos.Column, pos.Index);
        }
    }
}
