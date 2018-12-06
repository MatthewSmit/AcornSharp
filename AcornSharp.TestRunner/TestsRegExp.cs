namespace AcornSharp.TestRunner
{
    internal static class TestsRegExp
    {
        public static void Run()
        {
            Program.test("/foo/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/foo/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/foo/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/foo/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/foo|bar/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/foo|bar/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/foo|bar/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/foo|bar/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/||||/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/||||/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/||||/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/||||/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^|$|\\b|\\B/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^|$|\\b|\\B/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^|$|\\b|\\B/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^|$|\\b|\\B/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(/", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(/", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(/u", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?/", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?/", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?/u", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=/", "Invalid regular expression: /(?=/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=/", "Invalid regular expression: /(?=/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=/u", "Invalid regular expression: /(?=/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=foo/", "Invalid regular expression: /(?=foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=foo/", "Invalid regular expression: /(?=foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=foo/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=foo/u", "Invalid regular expression: /(?=foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=foo)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=foo)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=foo)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=foo)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!/", "Invalid regular expression: /(?!/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?!/", "Invalid regular expression: /(?!/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?!/u", "Invalid regular expression: /(?!/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?!)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?!)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?!)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!foo/", "Invalid regular expression: /(?!foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?!foo/", "Invalid regular expression: /(?!foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!foo/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?!foo/u", "Invalid regular expression: /(?!foo/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?!foo)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?!foo)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?!foo)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?!foo)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a)*/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a)*/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a)*/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a)*/u", "Invalid regular expression: /(?=a)*/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a)+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a)+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a)+/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a)+/u", "Invalid regular expression: /(?=a)+/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a)?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a)?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a)?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a)?/u", "Invalid regular expression: /(?=a)?/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){/u", "Invalid regular expression: /(?=a){/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){}/u", "Invalid regular expression: /(?=a){}/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){a}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){a}/u", "Invalid regular expression: /(?=a){a}/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){1}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){1}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){1}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){1}/u", "Invalid regular expression: /(?=a){1}/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){1,}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){1,}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){1,}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){1,}/u", "Invalid regular expression: /(?=a){1,}/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?=a){1,2}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?=a){1,2}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?=a){1,2}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?=a){1,2}/u", "Invalid regular expression: /(?=a){1,2}/: Invalid quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a*/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a*/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a*/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a*/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a+/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a+/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{/u", "Invalid regular expression: /a{/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{}/u", "Invalid regular expression: /a{}/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{a}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{a}/u", "Invalid regular expression: /a{a}/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1/u", "Invalid regular expression: /a{1/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1,/u", "Invalid regular expression: /a{1,/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,2}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,2}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,2/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1,2/u", "Invalid regular expression: /a{1,2/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1}/", "Invalid regular expression: /a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1}/", "Invalid regular expression: /a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1}/u", "Invalid regular expression: /a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{2,1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{2,1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1/u", "Invalid regular expression: /a{2,1/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a{2,1}/", "Invalid regular expression: /(a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(a{2,1}/", "Invalid regular expression: /(a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a{2,1}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(a{2,1}/u", "Invalid regular expression: /(a{2,1}/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a*?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a*?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a*?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a*?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a+?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a+?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a+?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a+?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a??/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a??/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a??/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a??/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{?/u", "Invalid regular expression: /a{?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{}?/u", "Invalid regular expression: /a{}?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{a}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{a}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{a}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{a}?/u", "Invalid regular expression: /a{a}?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1}?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1?/u", "Invalid regular expression: /a{1?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,}?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1,?/u", "Invalid regular expression: /a{1,?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,2}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2}?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,2}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2}?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{1,2?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{1,2?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{1,2?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{1,2?/u", "Invalid regular expression: /a{1,2?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1}?/", "Invalid regular expression: /a{2,1}?/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1}?/", "Invalid regular expression: /a{2,1}?/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1}?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1}?/u", "Invalid regular expression: /a{2,1}?/: numbers out of order in {} quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/a{2,1?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/a{2,1?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/a{2,1?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/a{2,1?/u", "Invalid regular expression: /a{2,1?/: Incomplete quantifier (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/👍🚀❇️/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/👍🚀❇️/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/👍🚀❇️/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/👍🚀❇️/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/./", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/./", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/./u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/./u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(*)/", "Invalid regular expression: /(*)/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(*)/", "Invalid regular expression: /(*)/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(*)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(*)/u", "Invalid regular expression: /(*)/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/+/", "Invalid regular expression: /+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/+/", "Invalid regular expression: /+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/+/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/+/u", "Invalid regular expression: /+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/?/", "Invalid regular expression: /?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/?/", "Invalid regular expression: /?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/?/u", "Invalid regular expression: /?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(/", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(/", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(/u", "Invalid regular expression: /(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/)/", "Invalid regular expression: /)/: Unmatched ')' (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/)/", "Invalid regular expression: /)/: Unmatched ')' (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/)/u", "Invalid regular expression: /)/: Unmatched ')' (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[/", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[/", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[/u", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[/u", "Unterminated regular expression (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/]/u", "Invalid regular expression: /]/: Lone quantifier brackets (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/{/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/{/u", "Invalid regular expression: /{/: Lone quantifier brackets (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/}/u", "Invalid regular expression: /}/: Lone quantifier brackets (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/|/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/|/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/|/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/|/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^*/", "Invalid regular expression: /^*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/^*/", "Invalid regular expression: /^*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^*/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/^*/u", "Invalid regular expression: /^*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/$*/", "Invalid regular expression: /$*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/$*/", "Invalid regular expression: /$*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/$*/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/$*/u", "Invalid regular expression: /$*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/${1,2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/${1,2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/${1,2/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/${1,2/u", "Invalid regular expression: /${1,2/: Lone quantifier brackets (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/${1,2}/", "Invalid regular expression: /${1,2}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/${1,2}/", "Invalid regular expression: /${1,2}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/${1,2}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/${1,2}/u", "Invalid regular expression: /${1,2}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/${2,1}/", "Invalid regular expression: /${2,1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/${2,1}/", "Invalid regular expression: /${2,1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/${2,1}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/${2,1}/u", "Invalid regular expression: /${2,1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\1/u", "Invalid regular expression: /\\1/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(a)\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a)\\1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)\\1/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\1(a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\1(a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\1(a)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\1(a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\2(a)(/", "Invalid regular expression: /\\2(a)(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\2(a)(/", "Invalid regular expression: /\\2(a)(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\2(a)(/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\2(a)(/u", "Invalid regular expression: /\\2(a)(/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?:a)\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?:a)\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:a)\\1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:a)\\1/u", "Invalid regular expression: /(?:a)\\1/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(a)\\2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)\\2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a)\\2/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(a)\\2/u", "Invalid regular expression: /(a)\\2/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?:a)\\2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?:a)\\2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:a)\\2/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:a)\\2/u", "Invalid regular expression: /(?:a)\\2/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\10/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\10/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\10/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\10/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/u", "Invalid regular expression: /(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)(a)\\11/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?/", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?/", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?/u", "Invalid regular expression: /(?/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?a/", "Invalid regular expression: /(?a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?a/", "Invalid regular expression: /(?a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?a/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?a/u", "Invalid regular expression: /(?a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?a)/", "Invalid regular expression: /(?a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?a)/", "Invalid regular expression: /(?a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?a)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?a)/u", "Invalid regular expression: /(?a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:/", "Invalid regular expression: /(?:/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:/", "Invalid regular expression: /(?:/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:/u", "Invalid regular expression: /(?:/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:a/", "Invalid regular expression: /(?:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:a/", "Invalid regular expression: /(?:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:a/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(?:a/u", "Invalid regular expression: /(?:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/(?:a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?:a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(?:a)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/(?:a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(:a/", "Invalid regular expression: /(:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(:a/", "Invalid regular expression: /(:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/(:a/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/(:a/u", "Invalid regular expression: /(:a/: Unterminated group (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\d/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\d/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\d/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\d/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\D/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\D/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\D/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\D/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\s/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\s/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\s/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\s/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\S/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\S/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\S/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\S/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\w/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\w/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\w/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\w/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\W/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\W/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\W/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\W/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\f/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\n/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\n/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\n/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\n/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\r/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\r/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\r/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\r/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\t/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\t/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\t/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\t/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\v/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\v/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\v/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\v/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\cA/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\cA/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\cA/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\cA/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\cz/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\cz/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\cz/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\cz/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\c1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\c1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\c1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\c1/u", "Invalid regular expression: /\\c1/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\c/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\c/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\c/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\c/u", "Invalid regular expression: /\\c/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\0/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\0/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\0/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\0/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u/u", "Invalid regular expression: /\\u/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u1/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u1/u", "Invalid regular expression: /\\u1/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u12/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u12/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u12/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u12/u", "Invalid regular expression: /\\u12/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u123/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u123/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u123/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u123/u", "Invalid regular expression: /\\u123/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u1234/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u1234/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u1234/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u1234/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u12345/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u12345/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u12345/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u12345/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u{/u", "Invalid regular expression: /\\u{/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{z/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{z/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{z/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u{z/u", "Invalid regular expression: /\\u{z/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{a}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{a}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{a}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{20/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{20/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{20/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u{20/u", "Invalid regular expression: /\\u{20/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{20}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{20}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{20}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{20}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{10FFFF}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{10FFFF}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{10FFFF}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{10FFFF}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{110000}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{110000}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{110000}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\u{110000}/u", "Invalid regular expression: /\\u{110000}/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\u{00000001}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{00000001}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\u{00000001}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\u{00000001}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\377/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\377/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\377/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\377/u", "Invalid regular expression: /\\377/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\400/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\400/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\400/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\400/u", "Invalid regular expression: /\\400/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\^/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\^/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\^/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\^/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\./", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\./", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\./u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\./u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\+/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\+/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\+/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\?/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\?/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\?/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\(/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\(/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\(/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\(/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\)/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\[/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\[/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\[/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\[/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\{/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\{/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\}/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\|/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\|/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\|/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\|/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\//", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\//", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\//u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\//u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\a/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\a/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\a/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/\\a/u", "Invalid regular expression: /\\a/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/\\s/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\s/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/\\s/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/\\s/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[^-a-b-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[^-a-b-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[^-a-b-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[^-a-b-]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[--]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[--]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[--]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[--]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[-a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[-a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[-a-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[-a-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a-]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[a-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[a-b]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-b]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a-b]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-b]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[-a-b-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a-b-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[-a-b-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[-a-b-]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[---]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[---]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[---]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[---]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[b-a]/", "Invalid regular expression: /[b-a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[b-a]/", "Invalid regular expression: /[b-a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[b-a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[b-a]/u", "Invalid regular expression: /[b-a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[a-b--/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-b--/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a-b--/]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[a-b--/]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a-b--+]/", "Invalid regular expression: /[a-b--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[a-b--+]/", "Invalid regular expression: /[a-b--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[a-b--+]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[a-b--+]/u", "Invalid regular expression: /[a-b--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\b-\\n]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\b-\\n]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\b-\\n]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\b-\\n]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[b\\-a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[b\\-a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[b\\-a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[b\\-a]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\d]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\D]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\D]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\s]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\S]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\S]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\w]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\w]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\W]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\W]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\d]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\D]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\D]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\s]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\S]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\S]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\w]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\w]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\W]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\W]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\f]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\f]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\f]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\f]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\n]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\n]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\n]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\n]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\r]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\r]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\r]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\r]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\t]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\t]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\t]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\t]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\v]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\v]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\v]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\v]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\cA]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\cA]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\cA]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\cA]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\cz]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\cz]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\cz]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\cz]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\c1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\c1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c1]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c1]/u", "Invalid regular expression: /[\\c1]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\c]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\c]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c]/u", "Invalid regular expression: /[\\c]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\0]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\0]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\0]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\0]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\x]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\x]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\x]/u", "Invalid regular expression: /[\\x]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\xz]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\xz]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\xz]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\xz]/u", "Invalid regular expression: /[\\xz]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\x1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\x1]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\x1]/u", "Invalid regular expression: /[\\x1]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\x12]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x12]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\x12]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x12]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\x123]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x123]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\x123]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\x123]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u]/u", "Invalid regular expression: /[\\u]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u1]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u1]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u1]/u", "Invalid regular expression: /[\\u1]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u12]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u12]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u12]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u12]/u", "Invalid regular expression: /[\\u12]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u123]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u123]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u123]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u123]/u", "Invalid regular expression: /[\\u123]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u1234]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u1234]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u1234]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u1234]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u12345]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u12345]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u12345]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u12345]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{]/u", "Invalid regular expression: /[\\u{]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{z]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{z]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{z]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{z]/u", "Invalid regular expression: /[\\u{z]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{a}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{a}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{a}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{a}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{20]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{20]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{20]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{20]/u", "Invalid regular expression: /[\\u{20]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{20}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{20}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{20}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{20}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{10FFFF}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{10FFFF}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{10FFFF}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{10FFFF}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{110000}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{110000}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{110000}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{110000}]/u", "Invalid regular expression: /[\\u{110000}]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{00000001}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{00000001}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{00000001}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{00000001}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\77]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\77]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\77]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\77]/u", "Invalid regular expression: /[\\77]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\377]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\377]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\377]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\377]/u", "Invalid regular expression: /[\\377]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\400]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\400]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\400]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\400]/u", "Invalid regular expression: /[\\400]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\^]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\^]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\^]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\^]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\$]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\$]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\$]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\$]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\.]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\.]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\.]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\.]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\+]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\+]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\+]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\+]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\?]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\?]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\?]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\?]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\(]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\(]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\(]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\(]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\)]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\)]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\)]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\)]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\[]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\[]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\[]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\[]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\]]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\]]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\]]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\]]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\{]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\{]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\{]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\{]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\|]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\|]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\|]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\|]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\/]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\/]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\a]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\a]/u", "Invalid regular expression: /[\\a]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\s]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\d-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\d-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\d-\\uFFFF]/u", "Invalid regular expression: /[\\d-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\D-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\D-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\D-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\D-\\uFFFF]/u", "Invalid regular expression: /[\\D-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\s-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\s-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\s-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\s-\\uFFFF]/u", "Invalid regular expression: /[\\s-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\S-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\S-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\S-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\S-\\uFFFF]/u", "Invalid regular expression: /[\\S-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\w-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\w-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\w-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\w-\\uFFFF]/u", "Invalid regular expression: /[\\w-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\W-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\W-\\uFFFF]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\W-\\uFFFF]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\W-\\uFFFF]/u", "Invalid regular expression: /[\\W-\\uFFFF]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\d]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\d]/u", "Invalid regular expression: /[\\u0000-\\d]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\D]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\D]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\D]/u", "Invalid regular expression: /[\\u0000-\\D]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\s]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\s]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\s]/u", "Invalid regular expression: /[\\u0000-\\s]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\S]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\S]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\S]/u", "Invalid regular expression: /[\\u0000-\\S]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\w]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\w]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\w]/u", "Invalid regular expression: /[\\u0000-\\w]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\W]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\W]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0000-\\W]/u", "Invalid regular expression: /[\\u0000-\\W]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\u0001]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\u0001]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\u0001]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u0000-\\u0001]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0001-\\u0000]/", "Invalid regular expression: /[\\u0001-\\u0000]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0001-\\u0000]/", "Invalid regular expression: /[\\u0001-\\u0000]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0001-\\u0000]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u0001-\\u0000]/u", "Invalid regular expression: /[\\u0001-\\u0000]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{1}-\\u{2}]/", "Invalid regular expression: /[\\u{1}-\\u{2}]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{1}-\\u{2}]/", "Invalid regular expression: /[\\u{1}-\\u{2}]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{1}-\\u{2}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{1}-\\u{2}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{2}-\\u{1}]/", "Invalid regular expression: /[\\u{2}-\\u{1}]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{2}-\\u{1}]/", "Invalid regular expression: /[\\u{2}-\\u{1}]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{2}-\\u{1}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{2}-\\u{1}]/u", "Invalid regular expression: /[\\u{2}-\\u{1}]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u{2-\\u{1}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\u{2-\\u{1}]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u{2-\\u{1}]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\u{2-\\u{1}]/u", "Invalid regular expression: /[\\u{2-\\u{1}]/: Invalid unicode escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\a-\\z]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\a-\\z]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\a-\\z]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\a-\\z]/u", "Invalid regular expression: /[\\a-\\z]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\z-\\a]/", "Invalid regular expression: /[\\z-\\a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\z-\\a]/", "Invalid regular expression: /[\\z-\\a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\z-\\a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\z-\\a]/u", "Invalid regular expression: /[\\z-\\a]/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[0-9--/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[0-9--/]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[0-9--/]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[0-9--/]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[0-9--+]/", "Invalid regular expression: /[0-9--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[0-9--+]/", "Invalid regular expression: /[0-9--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[0-9--+]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[0-9--+]/u", "Invalid regular expression: /[0-9--+]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c-a]/", "Invalid regular expression: /[\\c-a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c-a]/", "Invalid regular expression: /[\\c-a]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c-a]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c-a]/u", "Invalid regular expression: /[\\c-a]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\c0-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\c0-]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c0-]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c0-]/u", "Invalid regular expression: /[\\c0-]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\c_]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\c_]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\c_]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\c_]/u", "Invalid regular expression: /[\\c_]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[🌷-🌸]/", "Invalid regular expression: /[🌷-🌸]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[🌷-🌸]/", "Invalid regular expression: /[🌷-🌸]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[🌷-🌸]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[🌷-🌸]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-🌸-\\u0000]/", "Invalid regular expression: /[\\u0000-🌸-\\u0000]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\u0000-\\ud83c\\udf38-\\u0000]/", "Invalid regular expression: /[\\u0000-\\ud83c\\udf38-\\u0000]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-🌸-\\u0000]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\u{1f338}-\\u0000]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\u0000-\\ud83c\\udf38-\\u0000]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[🌸-🌷]/", "Invalid regular expression: /[🌸-🌷]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[🌸-🌷]/", "Invalid regular expression: /[🌸-🌷]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[🌸-🌷]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[🌸-🌷]/u", "Invalid regular expression: /[🌸-🌷]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/", "Invalid regular expression: /[\\uD834\\uDF06-\\uD834\\uDF08a-z]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/", "Invalid regular expression: /[\\uD834\\uDF06-\\uD834\\uDF08a-z]/: Range out of order in character class (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[0-9]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[0-9]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[0-9]+$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]+$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[0-9]+$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]+$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[a-zA-Z]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[a-zA-Z]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[a-zA-Z]+$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z]+$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[a-zA-Z]+$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z]+$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[0-9a-zA-Z]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9a-zA-Z]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[0-9a-zA-Z]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9a-zA-Z]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[a-zA-Z0-9!-/:-@\\[-`{-~]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z0-9!-/:-@\\[-`{-~]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[a-zA-Z0-9!-/:-@\\[-`{-~]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z0-9!-/:-@\\[-`{-~]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^([a-zA-Z0-9]{8,})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([a-zA-Z0-9]{8,})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^([a-zA-Z0-9]{8,})$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([a-zA-Z0-9]{8,})$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^([a-zA-Z0-9]{6,8})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([a-zA-Z0-9]{6,8})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^([a-zA-Z0-9]{6,8})$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([a-zA-Z0-9]{6,8})$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^([0-9]{0,8})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([0-9]{0,8})$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^([0-9]{0,8})$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([0-9]{0,8})$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[0-9]{8}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]{8}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[0-9]{8}$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[0-9]{8}$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^https?:\\/\\//", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^https?:\\/\\//", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^https?:\\/\\//u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^https?:\\/\\//u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^\\d{3}-\\d{4}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\d{3}-\\d{4}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^\\d{3}-\\d{4}$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\d{3}-\\d{4}$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^\\d{1,3}(.\\d{1,3}){3}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\d{1,3}(.\\d{1,3}){3}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^\\d{1,3}(.\\d{1,3}){3}$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\d{1,3}(.\\d{1,3}){3}$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^([1-9][0-9]*|0)(\\.[0-9]+)?$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([1-9][0-9]*|0)(\\.[0-9]+)?$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^([1-9][0-9]*|0)(\\.[0-9]+)?$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^([1-9][0-9]*|0)(\\.[0-9]+)?$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^-?([1-9][0-9]*|0)(\\.[0-9]+)?$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^-?([1-9][0-9]*|0)(\\.[0-9]+)?$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^-?([1-9][0-9]*|0)(\\.[0-9]+)?$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^-?([1-9][0-9]*|0)(\\.[0-9]+)?$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[ぁ-んー]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ぁ-んー]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[ぁ-んー]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ぁ-んー]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[ァ-ンヴー]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ァ-ンヴー]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[ァ-ンヴー]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ァ-ンヴー]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[ｧ-ﾝﾞﾟ\\-]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ｧ-ﾝﾞﾟ\\-]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[ｧ-ﾝﾞﾟ\\-]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[ｧ-ﾝﾞﾟ\\-]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[^\\x20-\\x7e]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[^\\x20-\\x7e]*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[^\\x20-\\x7e]*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[^\\x20-\\x7e]*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^((4\\d{3})|(5[1-5]\\d{2})|(6011))([- ])?\\d{4}([- ])?\\d{4}([- ])?\\d{4}|3[4,7]\\d{13}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^((4\\d{3})|(5[1-5]\\d{2})|(6011))([- ])?\\d{4}([- ])?\\d{4}([- ])?\\d{4}|3[4,7]\\d{13}$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^((4\\d{3})|(5[1-5]\\d{2})|(6011))([- ])?\\d{4}([- ])?\\d{4}([- ])?\\d{4}|3[4,7]\\d{13}$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^((4\\d{3})|(5[1-5]\\d{2})|(6011))([- ])?\\d{4}([- ])?\\d{4}([- ])?\\d{4}|3[4,7]\\d{13}$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/^\\s*|\\s*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\s*|\\s*$/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/^\\s*|\\s*$/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/^\\s*|\\s*$/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/[\\d][\\12-\\14]{1,}[^\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
            Program.test("/[\\d][\\12-\\14]{1,}[^\\d]/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.testFail("/[\\d][\\12-\\14]{1,}[^\\d]/u", "Invalid regular expression flag (1:1)", new TestOptions
            {
                ecmaVersion = 5
            });
            Program.testFail("/[\\d][\\12-\\14]{1,}[^\\d]/u", "Invalid regular expression: /[\\d][\\12-\\14]{1,}[^\\d]/: Invalid class escape (1:1)", new TestOptions
            {
                ecmaVersion = 2015
            });
            Program.test("/([a ]\\b)*\\b/", new TestNode(), new TestOptions
            {
                ecmaVersion = 5
            });
        }
    }
}
