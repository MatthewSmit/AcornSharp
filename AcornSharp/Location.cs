namespace AcornSharp
{
    public sealed partial class Parser
    {
        // This function is used to raise exceptions on parse errors. It
        // takes an offset integer (into the current `input`) to indicate
        // the location of the error, attaches the position to the end
        // of the error message, and then raises a `SyntaxError` with that
        // message.
        private static void raise(Position position, string message)
        {
            message += " (" + position.Line + ":" + position.Column + ")";
            var err = new SyntaxError(message, position);
            throw err;
        }

        private static void raiseRecoverable(Position position, string message)
        {
            raise(position, message);
        }

        public Position curPosition()
        {
            return new Position(pos.Line, pos.Column, pos.Index);
        }
    }
}
