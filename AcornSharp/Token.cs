namespace AcornSharp
{
    public struct Token
    {
        private TokenType type;
        private object value;
        private SourceLocation location;

        public Token(TokenType type, object value, SourceLocation location)
        {
            this.type = type;
            this.value = value;
            this.location = location;
        }
    }
}