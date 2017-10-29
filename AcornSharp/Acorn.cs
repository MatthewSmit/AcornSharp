using AcornSharp.Node;

namespace AcornSharp
{
    public static class Acorn
    {
        public static BaseNode Parse(string input, Options options)
        {
            return new Parser(options, input).Parse();
        }
    }
}
