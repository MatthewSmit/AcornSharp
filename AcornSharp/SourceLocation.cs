using JetBrains.Annotations;

namespace AcornSharp
{
    public struct SourceLocation
    {
        public SourceLocation([NotNull] Parser parser, Position start, Position end = default)
        {
            Start = start;
            End = end;
            if (parser.SourceFile != null)
            {
                Source = parser.SourceFile;
            }
            else
            {
                Source = null;
            }
        }

        public Position Start { get; set; }
        public Position End { get; set; }
        public string Source { get; set; }
    }
}