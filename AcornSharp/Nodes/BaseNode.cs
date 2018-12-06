using System.Diagnostics;
using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public abstract class BaseNode
    {
        internal BaseNode([NotNull] Parser parser, int start, Position startLocation)
        {
            Start = start;
            End = 0;
            if (parser.Options.Locations)
            {
                Location = new SourceLocation(parser, startLocation);
            }

            if (parser.Options.DirectSourceFile != null)
            {
                SourceFile = parser.Options.DirectSourceFile;
            }

            if (parser.Options.Ranges)
            {
                Range = (start, 0);
            }
        }

        internal void Finish([NotNull] Parser parser, int position, Position? loc)
        {
            End = position;
            if (parser.Options.Locations)
            {
                Debug.Assert(loc != null, nameof(loc) + " != null");
                Location = new SourceLocation
                {
                    Start = Location.Start,
                    End = loc.Value,
                    Source = Location.Source
                };
            }

            if (parser.Options.Ranges)
            {
                Range = (Range.Item1, position);
            }
        }

        public SourceLocation Location { get; private set; }
        public int Start { get; }
        public int End { get; private set; }
        public string SourceFile { get; }
        public (int, int) Range { get; private set; }
    }
}
