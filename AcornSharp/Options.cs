using System;
using AcornSharp.Node;

namespace AcornSharp
{
    public sealed class Options
    {
        // `ecmaVersion` indicates the ECMAScript version to parse. Must
        // be either 3, 5, 6 (2015), 7 (2016), or 8 (2017). This influences support
        // for strict mode, the set of reserved words, and support for
        // new syntax features. The default is 7.
        public int ecmaVersion;
        // `sourceType` indicates the mode the code should be parsed in.
        // Can be either `"script"` or `"module"`. This influences global
        // strict mode and parsing of `import` and `export` declarations.
        public string sourceType = "script";
        // `onInsertedSemicolon` can be a callback that will be called
        // when a semicolon is automatically inserted. It will be passed
        // th position of the comma as an offset, and if `locations` is
        // enabled, it is given the location as a `{line, column}` object
        // as second argument.
        public object onInsertedSemicolon = null;
        // `onTrailingComma` is similar to `onInsertedSemicolon`, but for
        // trailing commas.
        public object onTrailingComma = null;
        // By default, reserved words are only enforced if ecmaVersion >= 5.
        // Set `allowReserved` to a boolean value to explicitly turn this on
        // an off. When this option has the value "never", reserved words
        // and keywords can also not be used as property names.
        public object allowReserved;
        // When enabled, a return at the top level is not considered an
        // error.
        public bool allowReturnOutsideFunction = false;
        // When enabled, import/export statements are not constrained to
        // appearing at the top of the program.
        public bool allowImportExportEverywhere = false;
        // When enabled, hashbang directive in the beginning of file
        // is allowed and treated as a line comment.
        public bool allowHashBang = false;
        // A function can be passed as `onToken` option, which will
        // cause Acorn to call that function with object in the same
        // format as tokens returned from `tokenizer().getToken()`. Note
        // that you are not allowed to call the parser from the
        // callback—that will corrupt its internal state.
        public Action<Token> onToken;
        // A function can be passed as `onComment` option, which will
        // cause Acorn to call that function with `(block, text, start,
        // end)` parameters whenever a comment is skipped. `block` is a
        // boolean indicating whether this is a block (`/* */`) comment,
        // `text` is the content of the comment, and `start` and `end` are
        // character offsets that denote the start and end of the comment.
        // When the `locations` option is on, two more parameters are
        // passed, the full `{line, column}` locations of the start and
        // end of the comments. Note that you are not allowed to call the
        // parser from the callback—that will corrupt its internal state.
        public Action<bool, string, SourceLocation> onComment;
        // It is possible to parse multiple files into a single AST by
        // passing the tree produced by parsing the first file as
        // `program` option in subsequent parses. This will add the
        // toplevel forms of the parsed file to the `Program` (top) node
        // of an existing parse tree.
        public BaseNode program = null;
        // When `locations` is on, you can pass this to record the source
        // file in every node's `loc` object.
        public string sourceFile = null;
        // When enabled, parenthesized expressions are represented by
        // (non-standard) ParenthesizedExpression nodes
        public bool preserveParens = false;

        public static Options getOptions(Options options)
        {
            if (options.ecmaVersion == 0)
                options.ecmaVersion = 7;

            if (options.ecmaVersion >= 2015)
                options.ecmaVersion -= 2009;

            if (options.allowReserved == null)
                options.allowReserved = options.ecmaVersion < 5;

            return options;
        }
    }
}