using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public abstract class BaseNode
    {
        protected BaseNode(SourceLocation sourceLocation)
        {
            Location = sourceLocation;
        }

        internal BaseNode([NotNull] Parser parser, Position start, Position end)
        {
            Location = new SourceLocation(start, end, parser.SourceFile);
        }

        public SourceLocation Location { get; internal set; }
    }
}
