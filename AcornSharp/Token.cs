namespace AcornSharp
{
    public struct Token
    {
        private object value;
        private SourceLocation location;

        public Token(TokenType type, object value, SourceLocation location)
        {
            Type = type;
            this.value = value;
            this.location = location;
        }

        public TokenType Type { get; }
    }
}