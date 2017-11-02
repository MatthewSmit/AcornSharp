using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public abstract class BaseNode
    {
        public SourceLocation location;

        internal BaseNode([NotNull] Parser parser, Position start, Position end)
        {
            location = new SourceLocation(start, end, parser.sourceFile);
        }
    }
}
