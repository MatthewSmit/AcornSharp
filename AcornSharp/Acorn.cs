namespace AcornSharp
{
    public static class Acorn
    {
        public static Node Parse(string input, Options options)
        {
            return new Parser(options, input).Parse();
        }
    }
}
