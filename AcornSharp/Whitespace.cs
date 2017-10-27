using System.Text.RegularExpressions;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        private static readonly Regex lineBreak = new Regex("\r\n?|\n|\u2028|\u2029");
        private static readonly Regex nonASCIIwhitespace = new Regex(@"[\u1680\u180e\u2000-\u200a\u202f\u205f\u3000\ufeff]");
        private static readonly Regex skipWhiteSpace = new Regex(@"(?:\s|\/\/.*|\/\*.*?\*\/)*");

        private static bool isNewLine(char code)
        {
            return code == 10 || code == 13 || code == 0x2028 || code == 0x2029;
        }
    }
}
