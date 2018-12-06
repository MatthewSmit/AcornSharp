using JetBrains.Annotations;

namespace AcornSharp
{
    // Object type used to represent tokens. Note that normally, tokens
    // simply exist as properties on the parser object. This is only
    // used for the onToken callback and the external tokenizer.
    public struct Token
    {
        public TokenType Type;
        public object Value;
        public int Start;
        public int End;
        public SourceLocation Location;
        public (int start, int end) Range;

        public Token([NotNull] Parser p)
        {
            Type = p.Type;
            Value = p.Value;
            Start = p.Start;
            End = p.End;
            if (p.Options.Locations)
            {
                Location = new SourceLocation(p, p.StartLocation, p.EndLocation);
            }
            else
            {
                Location = default;
            }

            if (p.Options.Ranges)
            {
                Range = (p.Start, p.End);
            }
            else
            {
                Range = default;
            }
        }
    }
}