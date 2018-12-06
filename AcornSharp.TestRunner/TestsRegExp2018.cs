namespace AcornSharp.TestRunner
{
    internal class TestsRegExp2018
    {
        public static void Run()
        {
            //------------------------------------------------------------------------------
            // Named capture groups
            //------------------------------------------------------------------------------

            Program.test("/(a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?:a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?a/", "Invalid regular expression: /(?a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?a)/", "Invalid regular expression: /(?a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?</", "Invalid regular expression: /(?</: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<)/", "Invalid regular expression: /(?<)/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a)/", "Invalid regular expression: /(?<a)/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\k/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\k/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\k/u", "Invalid regular expression: /\\k/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\k/u", "Invalid regular expression: /\\k/: Invalid named reference (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\k<a>/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\k<a>/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\k<a>/u", "Invalid regular expression: /\\k<a>/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\k<a>/u", "Invalid regular expression: /\\k<a>/: Invalid named capture referenced (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k</", "Invalid regular expression: /(?<a>a)\\k</: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a>a)\\k</", "Invalid regular expression: /(?<a>a)\\k</: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k</u", "Invalid regular expression: /(?<a>a)\\k</: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a>a)\\k</u", "Invalid regular expression: /(?<a>a)\\k</: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<a/", "Invalid regular expression: /(?<a>a)\\k<a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a>a)\\k<a/", "Invalid regular expression: /(?<a>a)\\k<a/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<a/u", "Invalid regular expression: /(?<a>a)\\k<a/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a>a)\\k<a/u", "Invalid regular expression: /(?<a>a)\\k<a/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<a>/", "Invalid regular expression: /(?<a>a)\\k<a>/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/(?<a>a)\\k<a>/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<a>/u", "Invalid regular expression: /(?<a>a)\\k<a>/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/(?<a>a)\\k<a>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.test("/(?<a>a)\\1/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>a)\\1/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>a)\\2/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\2/u", "Invalid regular expression: /(?<a>a)\\2/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<b>/", "Invalid regular expression: /(?<a>a)\\k<b>/: Invalid named capture referenced (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)\\k<b>/u", "Invalid regular expression: /(?<a>a)\\k<b>/: Invalid named capture referenced (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)(?<a>a)/", "Invalid regular expression: /(?<a>a)(?<a>a)/: Duplicate capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)(?<a>a)/u", "Invalid regular expression: /(?<a>a)(?<a>a)/: Duplicate capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)(?<\\u{61}>a)/u", "Invalid regular expression: /(?<a>a)(?<\\u{61}>a)/: Duplicate capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a>a)(?<\\u0061>a)/u", "Invalid regular expression: /(?<a>a)(?<\\u0061>a)/: Duplicate capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>a)(?<b>a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>a)(?<b>a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.test("/\\k<a>(?<a>a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\k<a>(?<a>a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\1(?<a>a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\1(?<a>a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.test("/(?<$abc>a)\\k<$abc>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<あ>a)\\k<あ>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<𠮷>a)\\k<\\u{20bb7}>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<\\uD842\\uDFB7>a)\\k<\\u{20bb7}>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<\\u{20bb7}>a)\\k<\\uD842\\uDFB7>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<☀>a)\\k<☀>/u", "Invalid regular expression: /(?<☀>a)\\k<☀>/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<\\u0020>a)\\k<\\u0020>/u", "Invalid regular expression: /(?<\\u0020>a)\\k<\\u0020>/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<abc>a)\\k<\\u0061\\u0062\\u0063>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<\\u0061\\u0062\\u0063>a)\\k<abc>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<\\u0061\\u0062\\u0063>a)\\k<\\u{61}\\u{62}\\u{63}>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<\\u0061\\u0062\\u0063>a)\\k<abd>/u", "Invalid regular expression: /(?<\\u0061\\u0062\\u0063>a)\\k<abd>/: Invalid named capture referenced (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<11>a)\\k<11>/u", "Invalid regular expression: /(?<11>a)\\k<11>/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a1>a)\\k<a1>/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            //------------------------------------------------------------------------------
            // Unicode property escapes
            //------------------------------------------------------------------------------

            Program.test("/\\p/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\p/u", "Invalid regular expression: /\\p/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\p/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p/u", "Invalid regular expression: /\\p/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\p{/u", "Invalid regular expression: /\\p{/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\p{/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{/u", "Invalid regular expression: /\\p{/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{ASCII/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\p{ASCII/u", "Invalid regular expression: /\\p{ASCII/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\p{ASCII/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{ASCII/u", "Invalid regular expression: /\\p{ASCII/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{ASCII}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/\\p{ASCII}/u", "Invalid regular expression: /\\p{ASCII}/: Invalid escape (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/\\p{ASCII}/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{ASCII}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.test("/\\p{Emoji}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category}/u", "Invalid regular expression: /\\p{General_Category}/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category=}/u", "Invalid regular expression: /\\p{General_Category=}/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category/u", "Invalid regular expression: /\\p{General_Category/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category=/u", "Invalid regular expression: /\\p{General_Category=/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category=Letter/u", "Invalid regular expression: /\\p{General_Category=Letter/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{General_Category=Letter}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/\\p{General_Category=Hiragana}/u", "Invalid regular expression: /\\p{General_Category=Hiragana}/: Invalid property name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/\\p{Script=Hiragana}/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/[\\p{Script=Hiragana}-\\p{Script=Katakana}]/u", "Invalid regular expression: /[\\p{Script=Hiragana}-\\p{Script=Katakana}]/: Invalid character class (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/[\\p{Script=Hiragana}\\-\\p{Script=Katakana}]/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            //------------------------------------------------------------------------------
            // Lookbehind assertions
            //------------------------------------------------------------------------------

            Program.testFail("/(?<a)/", "Invalid regular expression: /(?<a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a)/u", "Invalid regular expression: /(?<a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<a)/", "Invalid regular expression: /(?<a)/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<a)/u", "Invalid regular expression: /(?<a)/: Invalid capture group name (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)/", "Invalid regular expression: /(?<=a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<=a)/u", "Invalid regular expression: /(?<=a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/(?<=a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<=a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)/", "Invalid regular expression: /(?<!a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.testFail("/(?<!a)/u", "Invalid regular expression: /(?<!a)/: Invalid group (1:1)", new TestOptions
            {
                ecmaVersion = 2017
            });
            Program.test("/(?<!a)/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<!a)/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.testFail("/(?<=a)?/", "Invalid regular expression: /(?<=a)?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)?/u", "Invalid regular expression: /(?<=a)?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)+/", "Invalid regular expression: /(?<=a)+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)+/u", "Invalid regular expression: /(?<=a)+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)*/", "Invalid regular expression: /(?<=a)*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a)*/u", "Invalid regular expression: /(?<=a)*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a){1}/", "Invalid regular expression: /(?<=a){1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<=a){1}/u", "Invalid regular expression: /(?<=a){1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.testFail("/(?<!a)?/", "Invalid regular expression: /(?<!a)?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)?/u", "Invalid regular expression: /(?<!a)?/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)+/", "Invalid regular expression: /(?<!a)+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)+/u", "Invalid regular expression: /(?<!a)+/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)*/", "Invalid regular expression: /(?<!a)*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a)*/u", "Invalid regular expression: /(?<!a)*/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a){1}/", "Invalid regular expression: /(?<!a){1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.testFail("/(?<!a){1}/u", "Invalid regular expression: /(?<!a){1}/: Nothing to repeat (1:1)", new TestOptions
            {
                ecmaVersion = 2018
            });

            Program.test("/(?<=(?<a>\\w){3})f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/((?<=\\w{3}))f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>(?<=\\w{3}))f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<!(?<a>\\d){3})f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<!(?<a>\\D){3})f|f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>(?<!\\D{3}))f|f/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<=(?<a>\\w){3})f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/((?<=\\w{3}))f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>(?<=\\w{3}))f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<!(?<a>\\d){3})f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<a>(?<!\\D{3}))f|f/", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
            Program.test("/(?<=(?<fst>.)|(?<snd>.))/u", new TestNode(), new TestOptions
            {
                ecmaVersion = 2018
            });
        }
    }
}
