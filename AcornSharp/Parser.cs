using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    internal sealed partial class Parser
    {
        private sealed class Label
        {
            public string name;
            public string kind;
            public int statementStart;
        }

        internal readonly string SourceFile;
        private readonly string input;
        private bool containsEsc;
        private readonly Regex keywords;
        private readonly Regex reservedWords;
        private readonly Regex reservedWordsStrict;
        private readonly Regex reservedWordsStrictBind;
        private Position pos;
        private readonly Stack<Scope> scopeStack;
        private List<Label> labels;
        private Position awaitPos;
        private Position yieldPos;
        private Position potentialArrowAt;
        private bool inAsync;
        private bool inGenerator;
        private bool inFunction;
        internal TokenType type;
        internal object value;
        internal Position start;
        internal Position end;
        private Position lastTokStart;
        private Position lastTokEnd;
        private bool exprAllowed;
        private readonly List<TokContext> context;
        private readonly bool inModule;
        private bool strict;
        private bool inTemplateElement;

        public Parser(Options options, string input, int? startPos = null)
        {
            Options = options = Options.getOptions(options);
            SourceFile = options.sourceFile;
            keywords = options.ecmaVersion >= 6 ? ecmascript6KeywordsRegex : ecmascript5KeywordsRegex;

            if (options.allowReserved == null || options.allowReserved is bool && (bool)options.allowReserved == false)
            {
                var isModule = options.sourceType == SourceType.Module;
                if (options.ecmaVersion < 3)
                    (reservedWords, reservedWordsStrict, reservedWordsStrictBind) = isModule ? ecmascriptModuleNoReservedRegex : ecmascriptNoReservedRegex;
                else if (options.ecmaVersion < 5)
                    (reservedWords, reservedWordsStrict, reservedWordsStrictBind) = isModule ? ecmascript3ModuleReservedRegex : ecmascript3ReservedRegex;
                else if (options.ecmaVersion < 6)
                    (reservedWords, reservedWordsStrict, reservedWordsStrictBind) = isModule ? ecmascript5ModuleReservedRegex : ecmascript5ReservedRegex;
                else
                    (reservedWords, reservedWordsStrict, reservedWordsStrictBind) = isModule ? ecmascript6ModuleReservedRegex : ecmascript6ReservedRegex;
            }
            else
            {
                (reservedWords, reservedWordsStrict, reservedWordsStrictBind) = ecmascriptNoReservedRegex;
            }

            this.input = input;

            // Used to signal to callers of `readWord1` whether the word
            // contained any escape sequences. This is needed because words with
            // escape sequences must not be interpreted as keywords.
            containsEsc = false;

            // Set up token state

            // The current position of the tokenizer in the input.
            if (startPos.HasValue && startPos.Value > 0)
            {
                var lineStart = input.LastIndexOf('\n', startPos.Value - 1) + 1;
                var currentLine = lineBreak.Matches(input.Substring(0, lineStart)).Count;
                pos = new Position(currentLine, startPos.Value - lineStart, startPos.Value);
            }
            else
            {
                pos = new Position(1, 0, 0);
            }

            // Properties of the current token:
            // Its type
            type = TokenType.EOF;
            // For tokens that include more information than their type, the value
            value = null;
            // Its start and end
            start = end = curPosition();

            // Position information for the previous token
            lastTokStart = lastTokEnd = curPosition();

            // The context stack is used to superficially track syntactic
            // context to predict whether a regular expression is allowed in a
            // given position.
            context = initialContext();
            exprAllowed = true;

            // Figure out if it's a module code.
            inModule = options.sourceType == SourceType.Module;
            strict = inModule || strictDirective(pos.Index);

            // Used to signify the start of a potential arrow function
            potentialArrowAt = default;

            // Flags to track whether we are in a function, a generator, an async function.
            inFunction = inGenerator = inAsync = false;
            // Positions to delayed-check that yield/await does not exist in default parameters.
            yieldPos = awaitPos = default;
            // Labels in scope.
            labels = new List<Label>();

            // If enabled, skip leading hashbang line.
            if (pos.Index == 0 && options.allowHashBang && this.input.Substring(0, 2) == "#!")
                skipLineComment(2);

            // Scope tracking for duplicate variable names (see scope.js)
            scopeStack = new Stack<Scope>();
            enterFunctionScope();
        }

        [NotNull]
        public ProgramNode Parse()
        {
            var node = Options.program ?? new ProgramNode(this, start, lastTokEnd, Options.sourceType, new List<BaseNode>());
            if (node.SourceType != Options.sourceType)
                throw new InvalidOperationException();
            if (Options.ecmaVersion < 6 && node.SourceType != SourceType.Script)
                throw new InvalidOperationException();

            nextToken();
            parseTopLevel(node);
            return node;
        }

        public Options Options { get; }
    }
}
