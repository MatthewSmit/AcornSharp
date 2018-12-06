namespace AcornSharp
{
    public sealed class CommentToken
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public SourceLocation Location { get; set; }
        public (int start, int end) Range { get; set; }
    }
}
