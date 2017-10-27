namespace AcornSharp
{
    public class Acorn
    {
        public Node Parse(string input, Options options)
        {
            return new Parser(options, input).Parse();
        }

        public Node ParseDammit(string input, Options options)
        {
            return new Parser(options, input).Parse();
        }
    }
}
