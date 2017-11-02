using System;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void Main()
        {
            var t0 = DateTime.Now;
            Tests();
            TestsHarmony();
            TestsES7();
            TestsAsyncAwait();
            TestsTrailingCommasInFunc();
            TestsTemplateLiteralRevision();
            TestsDirective();
            var duration = DateTime.Now - t0;
            Console.WriteLine("Tests run in " + duration + "ms");
        }

        public static void Test(string code, [NotNull] TestNode expectedAst, Options options = null)
        {
            if (options == null)
                options = new Options();

            if (options.ecmaVersion == 0) options.ecmaVersion = 5;

            var ast = Acorn.Parse(code, options);
            var mis = !expectedAst.TestEquals(ast);
            if (mis)
                throw new NotImplementedException();
        }

        private static void testFail(string code, string error, Options options = null)
        {
            if (options == null)
                options = new Options();

            if (options.ecmaVersion == 0) options.ecmaVersion = 5;

            try
            {
                Acorn.Parse(code, options);
            }
            catch (SyntaxError e)
            {
                if (error[0] == '~' ? e.Message.IndexOf(error.Substring(1), StringComparison.Ordinal) <= -1 : e.Message != error)
                {
                    throw;
                }
            }
        }
    }
}
