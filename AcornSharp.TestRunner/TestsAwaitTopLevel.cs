using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal class TestsAwaitTopLevel
    {
        public static void Run()
        {
            //------------------------------------------------------------------------------
            // await-top-level
            //------------------------------------------------------------------------------

            Program.testFail("await 1", "Unexpected token (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("await 1", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 7,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 7,
                        expression = new TestNode
                        {
                            type = typeof(AwaitExpressionNode),
                            start = 0,
                            end = 7,
                            argument = new TestNode
                            {
                                type = typeof(LiteralNode),
                                start = 6,
                                end = 7,
                                value = 1
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                allowAwaitOutsideFunction = true,
                ecmaVersion = 8
            });
            Program.testFail("function foo() {return await 1}", "Unexpected token (1:29)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("function foo() {return await 1}", "Unexpected token (1:29)", new TestOptions
            {
                allowAwaitOutsideFunction = true,
                ecmaVersion = 8
            });
        }
    }
}