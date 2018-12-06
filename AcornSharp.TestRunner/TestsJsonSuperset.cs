namespace AcornSharp.TestRunner
{
    internal class TestsJsonSuperset
    {
        public static void Run()
        {
            Program.test("'\u2029'", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.test("'\u2028'", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.test("\"\u2029\"", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.test("\"\u2028\"", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.test("`\u2029`", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.test("`\u2028`", new TestNode(), new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.testFail("/\u2029/", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 2019
            });
            Program.testFail("/\u2028/", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 2019
            });
        }
    }
}
