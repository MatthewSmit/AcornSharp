using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsOptionalCatchBinding
    {
        public static void Run()
        {
            Program.test("try {} catch {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 15,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        start = 0,
                        end = 15,
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 4,
                            end = 6,
                            body = new TestNode[0]
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            start = 7,
                            end = 15,
                            param = null,
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 13,
                                end = 15,
                                body = new TestNode[0]
                            }
                        },
                        finaliser = null
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 10
            });
        }
    }
}
