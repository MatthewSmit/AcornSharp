using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
    // Acorn# is a tiny, fast JavaScript parser written in C#.
    //
    // Acorn was written by Marijn Haverbeke, Ingvar Stepanyan, and
    // various contributors and released under an MIT license.
    //
    // Acorn# was ported by Matthew Smit and released under an MIT license.
    //
    // Git repositories for Acorn# are available at
    //
    //     https://github.com/MatthewSmit/AcornSharp.git
    //
    // Please use the [github bug tracker][ghbt] to report issues.
    //
    // [ghbt]: https://github.com/MatthewSmit/AcornSharp/issues
    //
    // [walk]: util/walk.js
    public static class Acorn
    {
        public static readonly string Version = "6.0.4";

        // The main exported interface (under `self.acorn` when in the
        // browser) is a `parse` function that takes a code string and
        // returns an abstract syntax tree as specified by [Mozilla parser
        // API][api].
        //
        // [api]: https://developer.mozilla.org/en-US/docs/SpiderMonkey/Parser_API
        [NotNull]
        public static ProgramNode Parse([NotNull] string input, [CanBeNull] Options options = null)
        {
            return Parser.Parse(input, options);
        }

        // This function tries to parse a single expression at a given
        // offset in a string. Useful for parsing mixed-language formats
        // that embed JavaScript expressions.
        [NotNull]
        public static ExpressionNode ParseExpressionAt(string input, int pos = 0, [CanBeNull] Options options = null)
        {
            return Parser.ParseExpressionAt(input, pos, options);
        }
    }
}
