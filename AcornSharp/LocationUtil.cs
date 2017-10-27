namespace AcornSharp
{
    public sealed partial class Parser
    {
        // The `getLineInfo` function is mostly useful when the
        // `locations` option is off (for performance reasons) and you
        // want to find the line/column position for a given character
        // offset. `input` should be the code string that the offset refers
        // into.
        private static Position getLineInfo(string input, int offset)
        {
            var line = 1;
            var cur = 0;
            while (true)
            {
                var match = lineBreak.Match(input, cur);
                if (match.Success && match.Index < offset)
                {
                    ++line;
                    cur = match.Index + match.Groups[0].Length;
                }
                else
                {
                    return new Position(line, offset - cur);
                }
            }
        }
    }
}
