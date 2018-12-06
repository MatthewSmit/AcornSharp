using System.Text.RegularExpressions;

namespace AcornSharp
{
    internal static class Whitespace
    {
        // Matches a whole line break (where CRLF is considered a single
        // line break). Used to count lines.

        public static readonly Regex LineBreak = new Regex("\r\n?|\n|\u2028|\u2029");
        //export const lineBreakG = new RegExp(lineBreak.source, "g")

        public static bool IsNewLine(int code, bool ecma2019String = false)
        {
            return code == 10 || code == 13 || !ecma2019String && (code == 0x2028 || code == 0x2029);
        }

        public static readonly Regex NonASCIIwhitespace = new Regex("[\u1680\u180e\u2000-\u200a\u202f\u205f\u3000\ufeff]");
        public static readonly Regex SkipWhiteSpace = new Regex(@"(?:\s|\/\/.*|\/\*(.|\r?\n)*?\*\/)*");
    }
}