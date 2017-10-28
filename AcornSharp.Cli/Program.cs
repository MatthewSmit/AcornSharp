using System;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static Driver driver;

        private static void Main()
        {
            driver = new Driver();

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

        private static void Test(string code, Node ast, Options options = null)
        {
            driver.Test(code, ast, options);
        }

        private static void testFail(string code, string message, Options options = null)
        {
            driver.TestFail(code, message, options);
        }
    }

    internal sealed class Driver
    {
        public void Test(string code, Node expectedAst, Options options)
        {
            if (options == null)
                options = new Options();

            if (options.ecmaVersion == 0) options.ecmaVersion = 5;

            var ast = Acorn.Parse(code, options);
            var mis = !expectedAst.TestEquals(ast);
            if (mis)
                throw new NotImplementedException();
        }

        public void TestFail(string code, string error, Options options)
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
                if (error[0] == '~' ? e.Message.IndexOf(error.Substring(1), StringComparison.Ordinal) > -1 : e.Message == error)
                {
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
