using System;
using System.Collections.Generic;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
    public delegate void OnInsertedSemicolon(Parser parser, int lastTokenEnd, Position lastTokenEndLocation);

    public delegate void OnTrailingComma(Parser parser, int lastTokenEnd, Position lastTokenEndLocation);

    public delegate void OnToken(Parser parser, Token token);

    public delegate void OnComment(Parser parser, bool isBlock, string text, int start, int end, Position startLocation, Position endLocation);

    public sealed class Options
    {
        // `EcmaVersion` indicates the ECMAScript version to parse. Must be
        // either 3, 5, 6 (2015), 7 (2016), 8 (2017), 9 (2018), or 10
        // (2019). This influences support for strict mode, the set of
        // reserved words, and support for new syntax features. The default
        // is 9.
        public int EcmaVersion = 9;

        // `SourceType` indicates the mode the code should be parsed in.
        // Can be either `Script` or `Module`. This influences global
        // strict mode and parsing of `import` and `export` declarations.
        public SourceType SourceType = SourceType.Script;

        // `OnInsertedSemicolon` can be a callback that will be called
        // when a semicolon is automatically inserted. It will be passed
        // th position of the comma as an offset, and if `locations` is
        // enabled, it is given the location as a `{line, column}` object
        // as second argument.
        public OnInsertedSemicolon OnInsertedSemicolon;

        // `OnTrailingComma` is similar to `OnInsertedSemicolon`, but for
        // trailing commas.
        public OnTrailingComma OnTrailingComma;

        // By default, reserved words are only enforced if ecmaVersion >= 5.
        // Set `AllowReserved` to a boolean value to explicitly turn this on
        // an off. When this option has the value "never", reserved words
        // and keywords can also not be used as property names.
        public AllowReserved AllowReserved;

        // When enabled, a return at the top level is not considered an
        // error.
        public bool AllowReturnOutsideFunction;

        // When enabled, import/export statements are not constrained to
        // appearing at the top of the program.
        public bool AllowImportExportEverywhere;

        // When enabled, await identifiers are allowed to appear at the top-level scope,
        // but they are still not allowed in non-async functions.
        public bool AllowAwaitOutsideFunction;

        // When enabled, hashbang directive in the beginning of file
        // is allowed and treated as a line comment.
        public bool AllowHashBang;

        // When `Locations` is on, `Location` properties holding objects with
        // `Start` and `End` properties in `{Line, Column}` form (with
        // line being 1-based and column 0-based) will be attached to the
        // nodes.
        public bool Locations;

        // A function can be passed as `OnToken` option, which will
        // cause Acorn to call that function with object in the same
        // format as tokens returned from `tokenizer().getToken()`. Note
        // that you are not allowed to call the parser from the
        // callback—that will corrupt its internal state.
        public OnToken OnToken;

        public IList<Token> OnTokenList;

        // A function can be passed as `OnComment` option, which will
        // cause Acorn to call that function with `(block, text, start,
        // end)` parameters whenever a comment is skipped. `block` is a
        // boolean indicating whether this is a block (`/* */`) comment,
        // `text` is the content of the comment, and `start` and `end` are
        // character offsets that denote the start and end of the comment.
        // When the `locations` option is on, two more parameters are
        // passed, the full `{line, column}` locations of the start and
        // end of the comments. Note that you are not allowed to call the
        // parser from the callback—that will corrupt its internal state.
        public OnComment OnComment;

        public IList<CommentToken> OnCommentList;

        // Nodes have their start and end characters offsets recorded in
        // `Start` and `End` properties (directly on the node, rather than
        // the `Location` object, which holds line/column data. To also add a
        // [semi-standardized][range] `Range` property holding a `[start,
        // end]` array with the same numbers, set the `Ranges` option to
        // `true`.
        //
        // [range]: https://bugzilla.mozilla.org/show_bug.cgi?id=745678
        public bool Ranges;

        // It is possible to parse multiple files into a single AST by
        // passing the tree produced by parsing the first file as
        // `Program` option in subsequent parses. This will add the
        // toplevel forms of the parsed file to the `Program` (top) node
        // of an existing parse tree.
        public ProgramNode Program;

        // When `Locations` is on, you can pass this to record the source
        // file in every node's `Location` object.
        public string SourceFile;

        // This value, if given, is stored in every node, whether
        // `Locations` is on or off.
        public string DirectSourceFile;

        // When enabled, parenthesized expressions are represented by
        // (non-standard) ParenthesizedExpression nodes
        public bool PreserveParens;

        // Interpret and default an options object
        [NotNull]
        public static Options GetOptions([CanBeNull] Options options)
        {
            if (options == null)
            {
                options = new Options();
            }

            if (options.EcmaVersion >= 2015)
            {
                options.EcmaVersion -= 2009;
            }

            if (options.AllowReserved == AllowReserved.Default)
            {
                options.AllowReserved = options.EcmaVersion < 5 ? AllowReserved.Yes : AllowReserved.No;
            }

            if (options.OnTokenList != null)
            {
                if (options.OnToken != null)
                {
                    throw new InvalidOperationException();
                }

                options.OnToken = (parser, token) => options.OnTokenList.Add(token);
            }

            if (options.OnCommentList != null)
            {
                if (options.OnComment != null)
                {
                    throw new InvalidOperationException();
                }

                options.OnComment = PushComment(options);
            }

            return options;
        }

        [NotNull]
        private static OnComment PushComment(Options options)
        {
            return (parser, block, text, start, end, startLocation, endLocation) =>
            {
                var comment = new CommentToken
                {
                    Type = block ? "Block" : "Line",
                    Value = text,
                    Start = start,
                    End = end
                };

                if (options.Locations)
                {
                    comment.Location = new SourceLocation(parser, startLocation, endLocation);
                }

                if (options.Ranges)
                {
                    comment.Range = (start, end);
                }
                options.OnCommentList.Add(comment);
            };
        }
    }
}