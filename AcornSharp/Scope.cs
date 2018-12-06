using System.Collections.Generic;

namespace AcornSharp
{
    internal sealed class Scope
    {
        private readonly List<string> var;
        private readonly List<string> lexical;

        public Scope(ScopeFlags flags)
        {
            this.Flags = flags;
            // A list of var-declared names in the current lexical scope
            var = new List<string>();
            // A list of lexically-declared names in the current lexical scope
            lexical = new List<string>();
        }

        public ScopeFlags Flags { get; }
        public IList<string> Var => var;
        public IList<string> Lexical => lexical;
    }
}