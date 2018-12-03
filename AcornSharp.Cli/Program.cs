using System;
using JetBrains.Annotations;

namespace AcornSharp.Cli
{
    internal static class Program
    {
        private static void Main()
        {
//            SandboxTest.Test();
            TestSuite();
        }

        private static void TestSuite()
        {
            var t0 = DateTime.Now;
            //            for (var i = 0; i < 100; i++)
            {
                Tests.TestsStandard();
                Tests.TestsHarmony();
                Tests.TestsES7();
                Tests.TestsAsyncAwait();
                Tests.TestsTrailingCommasInFunc();
                Tests.TestsTemplateLiteralRevision();
                Tests.TestsDirective();
            }
            var duration = DateTime.Now - t0;
            Console.WriteLine("Tests run in " + duration + "ms");
        }

        public static void Test([NotNull] string code, [NotNull] TestNode expectedAst, [CanBeNull] Options options = null)
        {
            if (options == null)
            {
                options = new Options();
            }

            if (options.ecmaVersion == 0)
            {
                options.ecmaVersion = 5;
            }

            var ast = Acorn.Parse(code, options);
            var mis = !expectedAst.TestEquals(ast);
            if (mis)
            {
                throw new NotImplementedException();
            }
        }

        public static void TestFail([NotNull] string code, string error, [CanBeNull] Options options = null)
        {
            if (options == null)
            {
                options = new Options();
            }

            if (options.ecmaVersion == 0)
            {
                options.ecmaVersion = 5;
            }

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

    internal static class SandboxTest
    {
        public static void Test()
        {
        }
    }
}
