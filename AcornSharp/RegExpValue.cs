namespace AcornSharp
{
    public sealed class RegExpValue
    {
        public RegExpValue(string pattern, string flags)
        {
            Pattern = pattern;
            Flags = flags;
        }

        public string Pattern { get; }
        public string Flags { get; }
    }
}