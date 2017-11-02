using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public abstract class BaseNode
    {
        internal BaseNode([NotNull] Parser parser, Position start, Position end)
        {
            Location = new SourceLocation(start, end, parser.sourceFile);
        }

        public SourceLocation Location { get; internal set; }
    }
}
