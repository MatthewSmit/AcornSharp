namespace AcornSharp.TestRunner
{
    internal sealed class TestOptions
    {
        public OnInsertedSemicolon onInsertedSemicolon;
        public OnTrailingComma onTrailingComma;
        public int ecmaVersion;
        public SourceType sourceType = SourceType.Script;
        public bool allowReturnOutsideFunction;
        public bool allowAwaitOutsideFunction;
        public bool locations;
        public AllowReserved allowReserved;
        public bool ranges;
        public bool allowHashBang;
        public bool loose = true;
        public bool preserveParens;
        public string sourceFile;
        public TestNode[] onComment;
        public TestNode[] onToken;
    }
}