using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsAsyncAwait()
        {
            //-----------------------------------------------------------------------------
            // Async Function Declarations

            // async == false
            Test("function foo() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("async function foo() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // a reference and a normal function declaration if there is a linebreak between 'async' and 'function'.
            Test("async\nfunction foo() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 24)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async")
                    },
                    new FunctionDeclarationNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 18, 24)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(2, 9, 15), new Position(2, 12, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(2, 15, 21), new Position(2, 18, 24)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // export
            Test("export async function foo() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)),
                SourceType.Module)
            {
                body = new List<BaseNode>
                {
                    new ExportNamedDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)))
                    {
                        declaration = new FunctionDeclarationNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)))
                        {
                            id = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), "foo"),
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)))
                            {
                                body = new List<BaseNode>()
                            }
                        },
                        specifiers = new List<BaseNode>(),
                        source = null
                    }
                }
            }, new Options {ecmaVersion = 8, sourceType = SourceType.Module});

            // export default
            Test("export default async function() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                SourceType.Module)
            {
                body = new List<BaseNode>
                {
                    new ExportDefaultDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
                    {
                        declaration = new FunctionDeclarationNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 35, 35)))
                        {
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)))
                            {
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8, sourceType = SourceType.Module});

            // cannot combine with generators
            testFail("async function* foo() { }", "Unexpected token (1:14)", new Options {ecmaVersion = 8});

            // 'await' is valid as function names.
            Test("async function await() { }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), "await"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            testFail("async function wrap() {\nasync function await() { }\n}", "Can not use 'await' as identifier inside an async function (2:15)", new Options {ecmaVersion = 8});
            testFail("async function foo(await) { }", "Can not use 'await' as identifier inside an async function (1:19)", new Options {ecmaVersion = 8});
            testFail("async function foo() { return {await} }", "Can not use 'await' as identifier inside an async function (1:31)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Function Expressions

            // async == false
            Test("(function foo() { })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        expression = new FunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 19, 19)))
                        {
                            id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                            generator = false,
                            expression = false,
                            async = false,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                            {
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("(async function foo() { })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
                    {
                        expression = new FunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25)))
                        {
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)))
                            {
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // cannot insert a linebreak to between 'async' and 'function'.
            testFail("(async\nfunction foo() { })", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // cannot combine with generators.
            testFail("(async function* foo() { })", "Unexpected token (1:15)", new Options {ecmaVersion = 8});

            // export default
            Test("export default (async function() { })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                SourceType.Module)
            {
                body = new List<BaseNode>
                {
                    new ExportDefaultDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)))
                    {
                        declaration = new FunctionExpressionNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36)))
                        {
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 36, 36)))
                            {
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8, sourceType = SourceType.Module});

            // cannot use 'await' inside async functions.
            testFail("(async function await() { })", "Can not use 'await' as identifier inside an async function (1:16)", new Options {ecmaVersion = 8});
            testFail("(async function foo(await) { })", "Can not use 'await' as identifier inside an async function (1:20)", new Options {ecmaVersion = 8});
            testFail("(async function foo() { return {await} })", "Can not use 'await' as identifier inside an async function (1:32)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Arrow Function Expressions

            // async == false
            Test("a => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("(a) => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("async a => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async () => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>(),
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async (a, b) => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // OK even if it's an invalid syntax in the case `=>` didn't exist.
            Test("async ({a = b}) => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                SourceType.Script)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new ObjectPatternNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 14, 14)))
                                {
                                    properties = new List<PropertyNode>
                                    {
                                        new PropertyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            pkind = PropertyKind.Initialise,
                                            value = new AssignmentPatternNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                            {
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "b")
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // syntax error if `=>` didn't exist.
            testFail("async ({a = b})", "Shorthand property assignments are valid only in destructuring patterns (1:10)", new Options {ecmaVersion = 8});

            // AssignmentPattern/AssignmentExpression
            Test("async ({a: b = c}) => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new ObjectPatternNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)))
                                {
                                    properties = new List<PropertyNode>
                                    {
                                        new PropertyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)))
                                        {
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            value = new AssignmentPatternNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)))
                                            {
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "c")
                                            },
                                            pkind = PropertyKind.Initialise
                                        }
                                    }
                                }
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async ({a: b = c})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        expression = new CallExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async"),
                            arguments = new List<BaseNode>
                            {
                                new ObjectExpressionNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)))
                                {
                                    properties = new List<PropertyNode>
                                    {
                                        new PropertyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)))
                                        {
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            value = new AssignmentExpressionNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)))
                                            {
                                                @operator = "=",
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "c")
                                            },
                                            pkind = PropertyKind.Initialise
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // a reference and a normal arrow function if there is a linebreak between 'async' and the 1st parameter.
            Test("async\na => a", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 12)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async")
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 1, 7)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(2, 5, 11), new Position(2, 6, 12)), "a")
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // 'async()' call expression and invalid '=>' token.
            testFail("async\n() => a", "Unexpected token (2:3)", new Options {ecmaVersion = 8});

            // cannot insert a linebreak before '=>'.
            testFail("async a\n=> a", "Unexpected token (2:0)", new Options {ecmaVersion = 8});
            testFail("async ()\n=> a", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // a call expression with 'await' reference.
            Test("async (await)", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new CallExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async"),
                            arguments = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 12, 12)), "await")
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            testFail("async await => 1", "Can not use 'await' as identifier inside an async function (1:6)", new Options {ecmaVersion = 8});
            testFail("async (await) => 1", "Can not use 'await' as identifier inside an async function (1:7)", new Options {ecmaVersion = 8});
            testFail("async ({await}) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options {ecmaVersion = 8});
            testFail("async ({a: await}) => 1", "Can not use 'await' as identifier inside an async function (1:11)", new Options {ecmaVersion = 8});
            testFail("async ([await]) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options {ecmaVersion = 8});

            // can use 'yield' identifier outside generators.
            Test("async yield => 1", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)), "yield")
                            },
                            fbody = new LiteralNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)))
                            {
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Methods (object)

            // async == false
            Test("({foo() { }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), "foo"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("({async foo() { }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // OK with 'async' as a method name
            Test("({async() { }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // invalid syntax if there is a linebreak after 'async'.
            testFail("({async\nfoo() { }})", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // cannot combine with getters/setters/generators.
            testFail("({async get foo() { }})", "Unexpected token (1:12)", new Options {ecmaVersion = 8});
            testFail("({async set foo(value) { }})", "Unexpected token (1:12)", new Options {ecmaVersion = 8});
            testFail("({async* foo() { }})", "Unexpected token (1:7)", new Options {ecmaVersion = 8});

            // 'await' is valid as function names.
            Test("({async await() { }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 20, 20)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 19, 19)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), "await"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\n({async await() { }})\n}", new ProgramNode(default), new Options {ecmaVersion = 8});
            testFail("({async foo() { var await }})", "Can not use 'await' as identifier inside an async function (1:20)", new Options {ecmaVersion = 8});
            testFail("({async foo(await) { }})", "Can not use 'await' as identifier inside an async function (1:12)", new Options {ecmaVersion = 8});
            testFail("({async foo() { return {await} }})", "Can not use 'await' as identifier inside an async function (1:24)", new Options {ecmaVersion = 8});

            // invalid syntax 'async foo: 1'
            testFail("({async foo: 1})", "Unexpected token (1:11)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Methods (class)

            // async == false
            Test("class A {foo() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 19, 19)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                    @static = false,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("class A {async foo() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 24, 24)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                                    @static = false,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("class A {static async foo() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 31, 31)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), "foo"),
                                    @static = true,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 31, 31)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // OK 'async' as a method name.
            Test("class A {async() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "async"),
                                    @static = false,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("class A {static async() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)), "async"),
                                    @static = true,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 27, 27)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("class A {*async() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 22, 22)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 21, 21)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "async"),
                                    @static = false,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 21, 21)))
                                    {
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("class A {static* async() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 28, 28)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), "async"),
                                    @static = true,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28)))
                                    {
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // invalid syntax if there is a linebreak after 'async'.
            testFail("class A {async\nfoo() { }}", "Unexpected token (2:0)", new Options {ecmaVersion = 8});
            testFail("class A {static async\nfoo() { }}", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // cannot combine with constructors/getters/setters/generators.
            testFail("class A {async constructor() { }}", "Constructor can't be an async method (1:15)", new Options {ecmaVersion = 8});
            testFail("class A {async get foo() { }}", "Unexpected token (1:19)", new Options {ecmaVersion = 8});
            testFail("class A {async set foo(value) { }}", "Unexpected token (1:19)", new Options {ecmaVersion = 8});
            testFail("class A {async* foo() { }}", "Unexpected token (1:14)", new Options {ecmaVersion = 8});
            testFail("class A {static async get foo() { }}", "Unexpected token (1:26)", new Options {ecmaVersion = 8});
            testFail("class A {static async set foo(value) { }}", "Unexpected token (1:26)", new Options {ecmaVersion = 8});
            testFail("class A {static async* foo() { }}", "Unexpected token (1:21)", new Options {ecmaVersion = 8});

            // 'await' is valid as function names.
            Test("class A {async await() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 26, 26)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), "await"),
                                    @static = false,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("class A {static async await() { }}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                body = new List<BaseNode>
                {
                    new ClassDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new ClassBodyNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 34, 34)))
                        {
                            body = new List<BaseNode>
                            {
                                new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 33, 33)))
                                {
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), "await"),
                                    @static = true,
                                    pkind = PropertyKind.Method,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 33, 33)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)))
                                        {
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\nclass A {async await() { }}\n}", new ProgramNode(default), new Options {ecmaVersion = 8});
            testFail("class A {async foo() { var await }}", "Can not use 'await' as identifier inside an async function (1:27)", new Options {ecmaVersion = 8});
            testFail("class A {async foo(await) { }}", "Can not use 'await' as identifier inside an async function (1:19)", new Options {ecmaVersion = 8});
            testFail("class A {async foo() { return {await} }}", "Can not use 'await' as identifier inside an async function (1:31)", new Options {ecmaVersion = 8});
            //-----------------------------------------------------------------------------
            // Await Expressions

            // 'await' is an identifier in scripts.
            Test("await", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "await")
                    }
                }
            }, new Options {ecmaVersion = 8});

            // 'await' is a keyword in modules.
            testFail("await", "The keyword 'await' is reserved (1:0)", new Options {ecmaVersion = 8, sourceType = SourceType.Module});

            // Await expressions is invalid outside of async functions.
            testFail("await a", "Unexpected token (1:6)", new Options {ecmaVersion = 8});
            testFail("await a", "The keyword 'await' is reserved (1:0)", new Options {ecmaVersion = 8, sourceType = SourceType.Module});

            // Await expressions in async functions.
            Test("async function foo(a, b) { await a }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                            new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "b")
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                {
                                    expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                    {
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "a")
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("(async function foo(a) { await a })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
                    {
                        expression = new FunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 34, 34)))
                        {
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            },
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 34, 34)))
                            {
                                body = new List<BaseNode>
                                {
                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)))
                                    {
                                        expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)))
                                        {
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "a")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("(async (a) => await a)", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                            },
                            fbody = new AwaitExpressionNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 21, 21)))
                            {
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("({async foo(a) { await a }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 27, 27)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 26, 26)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 26, 26)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                                        },
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26)))
                                        {
                                            body = new List<BaseNode>
                                            {
                                                new ExpressionStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                                {
                                                    expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                                    {
                                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "a")
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("(class {async foo(a) { await a }})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        expression = new ClassExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)))
                        {
                            id = null,
                            superClass = null,
                            fbody = new ClassBodyNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 33, 33)))
                            {
                                body = new List<BaseNode>
                                {
                                    new MethodDefinitionNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)))
                                    {
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                                        @static = false,
                                        pkind = PropertyKind.Method,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 32, 32)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "a")
                                            },
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32)))
                                            {
                                                body = new List<BaseNode>
                                                {
                                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)))
                                                    {
                                                        expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)))
                                                        {
                                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), "a")
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // Await expressions are an unary expression.
            Test("async function foo(a, b) { await a + await b }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                            new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "b")
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 46, 46)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)))
                                {
                                    expression = new BinaryExpressionNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)))
                                    {
                                        left = new AwaitExpressionNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                        {
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "a")
                                        },
                                        @operator = "+",
                                        right = new AwaitExpressionNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 44, 44)))
                                        {
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 44, 44)), "b")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // 'await + 1' is a binary expression outside of async functions.
            Test("function foo() { await + 1 }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 28, 28)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)))
                                {
                                    expression = new BinaryExpressionNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)))
                                    {
                                        left = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), "await"),
                                        @operator = "+",
                                        right = new LiteralNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)))
                                        {
                                            value = 1,
                                            raw = "1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // 'await + 1' is an await expression in async functions.
            Test("async function foo() { await + 1 }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)))
                                {
                                    expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)))
                                    {
                                        argument = new UnaryExpressionNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)))
                                        {
                                            @operator = "+",
                                            prefix = true,
                                            argument = new LiteralNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)))
                                            {
                                                value = 1,
                                                raw = "1"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // Await expressions need one argument.
            testFail("async function foo() { await }", "Unexpected token (1:29)", new Options {ecmaVersion = 8});
            testFail("(async function foo() { await })", "Unexpected token (1:30)", new Options {ecmaVersion = 8});
            testFail("async () => await", "Unexpected token (1:17)", new Options {ecmaVersion = 8});
            testFail("({async foo() { await }})", "Unexpected token (1:22)", new Options {ecmaVersion = 8});
            testFail("(class {async foo() { await }})", "Unexpected token (1:28)", new Options {ecmaVersion = 8});

            // Forbid await expressions in default parameters:
            testFail("async function foo(a = await b) {}", "Await expression cannot be a default value (1:23)", new Options {ecmaVersion = 8});
            testFail("(async function foo(a = await b) {})", "Await expression cannot be a default value (1:24)", new Options {ecmaVersion = 8});
            testFail("async (a = await b) => {}", "Unexpected token (1:17)", new Options {ecmaVersion = 8});
            testFail("async function wrapper() {\nasync (a = await b) => {}\n}", "Await expression cannot be a default value (2:11)", new Options {ecmaVersion = 8});
            testFail("({async foo(a = await b) {}})", "Await expression cannot be a default value (1:16)", new Options {ecmaVersion = 8});
            testFail("(class {async foo(a = await b) {}})", "Await expression cannot be a default value (1:22)", new Options {ecmaVersion = 8});
            testFail("async function foo(a = class extends (await b) {}) {}", "Await expression cannot be a default value (1:38)", new Options {ecmaVersion = 8});

            // Allow await expressions inside functions in default parameters:
            Test("async function foo(a = async function foo() { await b }) {}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new AssignmentPatternNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 55, 55)))
                            {
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new FunctionExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 55, 55)))
                                {
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 41, 41)), "foo"),
                                    generator = false,
                                    expression = false,
                                    async = true,
                                    parameters = new List<BaseNode>(),
                                    fbody = new BlockStatementNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 55, 55)))
                                    {
                                        body = new List<BaseNode>
                                        {
                                            new ExpressionStatementNode(new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)))
                                            {
                                                expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)))
                                                {
                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "b")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 57, 57), new Position(1, 59, 59)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = async () => await b) {}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new AssignmentPatternNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 42, 42)))
                            {
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 42, 42)))
                                {
                                    id = null,
                                    generator = false,
                                    expression = true,
                                    async = true,
                                    parameters = new List<BaseNode>(),
                                    fbody = new AwaitExpressionNode(new SourceLocation(new Position(1, 35, 35), new Position(1, 42, 42)))
                                    {
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 42, 42)), "b")
                                    }
                                }
                            }
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 46, 46)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = {async bar() { await b }}) {}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new AssignmentPatternNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 48, 48)))
                            {
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new ObjectExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 48, 48)))
                                {
                                    properties = new List<PropertyNode>
                                    {
                                        new PropertyNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 47, 47)))
                                        {
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), "bar"),
                                            pkind = PropertyKind.Initialise,
                                            value = new FunctionExpressionNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 47, 47)))
                                            {
                                                id = null,
                                                generator = false,
                                                expression = false,
                                                async = true,
                                                parameters = new List<BaseNode>(),
                                                fbody = new BlockStatementNode(new SourceLocation(new Position(1, 36, 36), new Position(1, 47, 47)))
                                                {
                                                    body = new List<BaseNode>
                                                    {
                                                        new ExpressionStatementNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)))
                                                        {
                                                            expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)))
                                                            {
                                                                argument = new IdentifierNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 45, 45)), "b")
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 50, 50), new Position(1, 52, 52)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = class {async bar() { await b }}) {}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>
                        {
                            new AssignmentPatternNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 54, 54)))
                            {
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new ClassExpressionNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 54, 54)))
                                {
                                    id = null,
                                    superClass = null,
                                    fbody = new ClassBodyNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 54, 54)))
                                    {
                                        body = new List<BaseNode>
                                        {
                                            new MethodDefinitionNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 53, 53)))
                                            {
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 36, 36), new Position(1, 39, 39)), "bar"),
                                                @static = false,
                                                pkind = PropertyKind.Method,
                                                value = new FunctionExpressionNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 53, 53)))
                                                {
                                                    id = null,
                                                    generator = false,
                                                    expression = false,
                                                    async = true,
                                                    parameters = new List<BaseNode>(),
                                                    fbody = new BlockStatementNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 53, 53)))
                                                    {
                                                        body = new List<BaseNode>
                                                        {
                                                            new ExpressionStatementNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)))
                                                            {
                                                                expression = new AwaitExpressionNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)))
                                                                {
                                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 50, 50), new Position(1, 51, 51)), "b")
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 58, 58)))
                        {
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Test("async function wrap() {\n(a = await b)\n}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), "wrap"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 39)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 13, 37)))
                                {
                                    expression = new AssignmentExpressionNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 12, 36)))
                                    {
                                        @operator = "=",
                                        left = new IdentifierNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 2, 26)), "a"),
                                        right = new AwaitExpressionNode(new SourceLocation(new Position(2, 5, 29), new Position(2, 12, 36)))
                                        {
                                            argument = new IdentifierNode(new SourceLocation(new Position(2, 11, 35), new Position(2, 12, 36)), "b")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});
            testFail("async function wrap() {\n(a = await b) => a\n}", "Await expression cannot be a default value (2:5)", new Options {ecmaVersion = 8});

            Test("async function wrap() {\n({a = await b} = obj)\n}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), "wrap"),
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 47)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 21, 45)))
                                {
                                    expression = new AssignmentExpressionNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 20, 44)))
                                    {
                                        @operator = "=",
                                        left = new ObjectPatternNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 14, 38)))
                                        {
                                            properties = new List<PropertyNode>
                                            {
                                                new PropertyNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)))
                                                {
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), "a"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new AssignmentPatternNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)))
                                                    {
                                                        left = new IdentifierNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), "a"),
                                                        right = new AwaitExpressionNode(new SourceLocation(new Position(2, 6, 30), new Position(2, 13, 37)))
                                                        {
                                                            argument = new IdentifierNode(new SourceLocation(new Position(2, 12, 36), new Position(2, 13, 37)), "b")
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new IdentifierNode(new SourceLocation(new Position(2, 17, 41), new Position(2, 20, 44)), "obj")
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});
            testFail("async function wrap() {\n({a = await b} = obj) => a\n}", "Await expression cannot be a default value (2:6)", new Options {ecmaVersion = 8});

            Test("function* wrap() {\nasync(a = yield b)\n}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                        parameters = new List<BaseNode>(),
                        generator = true,
                        expression = false,
                        async = false,
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 39)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)))
                                {
                                    expression = new CallExpressionNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)))
                                    {
                                        callee = new IdentifierNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 5, 24)), "async"),
                                        arguments = new List<BaseNode>
                                        {
                                            new AssignmentExpressionNode(new SourceLocation(new Position(2, 6, 25), new Position(2, 17, 36)))
                                            {
                                                @operator = "=",
                                                left = new IdentifierNode(new SourceLocation(new Position(2, 6, 25), new Position(2, 7, 26)), "a"),
                                                right = new YieldExpressionNode(new SourceLocation(new Position(2, 10, 29), new Position(2, 17, 36)))
                                                {
                                                    @delegate = false,
                                                    argument = new IdentifierNode(new SourceLocation(new Position(2, 16, 35), new Position(2, 17, 36)), "b")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});
            testFail("function* wrap() {\nasync(a = yield b) => a\n}", "Yield expression cannot be a default value (2:10)", new Options {ecmaVersion = 8});

            // https://github.com/ternjs/acorn/issues/464
            Test("f = ({ w = counter(), x = counter(), y = counter(), z = counter() } = { w: null, x: 0, y: false, z: '' }) => {}", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
                    {
                        expression = new AssignmentExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
                        {
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "f"),
                            right = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 111, 111)))
                            {
                                id = null,
                                parameters = new List<BaseNode>
                                {
                                    new AssignmentPatternNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 104, 104)))
                                    {
                                        left = new ObjectPatternNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 67, 67)))
                                        {
                                            properties = new List<PropertyNode>
                                            {
                                                new PropertyNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)))
                                                {
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "w"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new AssignmentPatternNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)))
                                                    {
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "w"),
                                                        right = new CallExpressionNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20)))
                                                        {
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 18, 18)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)))
                                                {
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "x"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new AssignmentPatternNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)))
                                                    {
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "x"),
                                                        right = new CallExpressionNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 35, 35)))
                                                        {
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 33, 33)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)))
                                                {
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), "y"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new AssignmentPatternNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)))
                                                    {
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), "y"),
                                                        right = new CallExpressionNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 50, 50)))
                                                        {
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)))
                                                {
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "z"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new AssignmentPatternNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)))
                                                    {
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "z"),
                                                        right = new CallExpressionNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 65, 65)))
                                                        {
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 63, 63)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new ObjectExpressionNode(new SourceLocation(new Position(1, 70, 70), new Position(1, 104, 104)))
                                        {
                                            properties = new List<PropertyNode>
                                            {
                                                new PropertyNode(new SourceLocation(new Position(1, 72, 72), new Position(1, 79, 79)))
                                                {
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 72, 72), new Position(1, 73, 73)), "w"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new LiteralNode(new SourceLocation(new Position(1, 75, 75), new Position(1, 79, 79)))
                                                    {
                                                        value = null,
                                                        raw = "null"
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 81, 81), new Position(1, 85, 85)))
                                                {
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 81, 81), new Position(1, 82, 82)), "x"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new LiteralNode(new SourceLocation(new Position(1, 84, 84), new Position(1, 85, 85)))
                                                    {
                                                        value = 0,
                                                        raw = "0"
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 87, 87), new Position(1, 95, 95)))
                                                {
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 87, 87), new Position(1, 88, 88)), "y"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new LiteralNode(new SourceLocation(new Position(1, 90, 90), new Position(1, 95, 95)))
                                                    {
                                                        value = false,
                                                        raw = "false"
                                                    }
                                                },
                                                new PropertyNode(new SourceLocation(new Position(1, 97, 97), new Position(1, 102, 102)))
                                                {
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 97, 97), new Position(1, 98, 98)), "z"),
                                                    pkind = PropertyKind.Initialise,
                                                    value = new LiteralNode(new SourceLocation(new Position(1, 100, 100), new Position(1, 102, 102)))
                                                    {
                                                        value = "",
                                                        raw = "''"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                generator = false,
                                expression = false,
                                async = false,
                                fbody = new BlockStatementNode(new SourceLocation(new Position(1, 109, 109), new Position(1, 111, 111)))
                                {
                                    body = new List<BaseNode>()
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            Test("({ async: true })", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new ObjectExpressionNode(default)
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(default)
                                {
                                    key = new IdentifierNode(default, "async"),
                                    value = new LiteralNode(default)
                                    {
                                        value = true
                                    },
                                    pkind = PropertyKind.Initialise
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Test("if (x) async function f() {}", new ProgramNode(default)
                {
                    body = new List<BaseNode>
                    {
                        new IfStatementNode(default,
                            null,
                            new FunctionDeclarationNode(default)
                            {
                                async = true
                            },
                            null)
                    }
                },
                new Options {ecmaVersion = 8}
            );

            testFail("(async)(a) => 12", "Unexpected token (1:11)", new Options {ecmaVersion = 8});

            testFail("f = async ((x)) => x", "Parenthesized pattern (1:11)", new Options {ecmaVersion = 8});

            // allow 'async' as a shorthand property in script.
            Test("({async})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)))
                                    {
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                        pkind = PropertyKind.Initialise,
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async")
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async, foo})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)))
                                    {
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                        pkind = PropertyKind.Initialise,
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async")
                                    },
                                    new PropertyNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)))
                                    {
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                        pkind = PropertyKind.Initialise,
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo")
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async = 0} = {})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            expression = new AssignmentExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17)))
                            {
                                @operator = "=",
                                left = new ObjectPatternNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)))
                                {
                                    properties = new List<PropertyNode>
                                    {
                                        new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                        {
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                            pkind = PropertyKind.Initialise,
                                            value = new AssignmentPatternNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                            {
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                                right = new LiteralNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)))
                                                {
                                                    value = 0,
                                                    raw = "0"
                                                }
                                            }
                                        }
                                    }
                                },
                                right = new ObjectExpressionNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                {
                                    properties = new List<PropertyNode>()
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            // async functions with vary names.
            Test("({async \"foo\"(){}})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new LiteralNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            value = "foo",
                                            raw = "\"foo\""
                                        },
                                        pkind = PropertyKind.Initialise,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<BaseNode>(),
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async 'foo'(){}})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new LiteralNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            value = "foo",
                                            raw = "'foo'"
                                        },
                                        pkind = PropertyKind.Initialise,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<BaseNode>(),
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async 100(){}})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 16, 16)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)))
                                    {
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new LiteralNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                        {
                                            value = 100,
                                            raw = "100"
                                        },
                                        pkind = PropertyKind.Initialise,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<BaseNode>(),
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15)))
                                            {
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async [foo](){}})", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    body = new List<BaseNode>
                    {
                        new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                properties = new List<PropertyNode>
                                {
                                    new PropertyNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        method = true,
                                        shorthand = false,
                                        computed = true,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                        pkind = PropertyKind.Initialise,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<BaseNode>(),
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            Test("({ async delete() {} })", new ProgramNode(default), new Options {ecmaVersion = 8});
        }
    }
}
