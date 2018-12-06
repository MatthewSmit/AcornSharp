namespace AcornSharp
{
    // These are used when `options.Locations` is on, for the
    // `StartLocation` and `EndLocation` properties.
    public struct Position
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

        public bool IsNull => Line == 0;

        public int Line { get; }
        public int Column { get; }
    }
}
