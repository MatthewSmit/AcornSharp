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
            Test("function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // async == true
            Test("async function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // a reference and a normal function declaration if there is a linebreak between 'async' and 'function'.
            Test("async\nfunction foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 24)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "async" }
                    },
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(2, 0, 6), new Position(2, 18, 24)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 9, 15), new Position(2, 12, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(2, 15, 21), new Position(2, 18, 24)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // export
            Test("export async function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportNamedDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)),
                        declaration = new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), name = "foo" },
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)),
                                body = new List<TestNode>()
                            }
                        },
                        specifiers = new List<TestNode>(),
                        source = null
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            // export default
            Test("export default async function() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportDefaultDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declaration = new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 35, 35)),
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            // cannot combine with generators
            testFail("async function* foo() { }", "Unexpected token (1:14)", new Options { ecmaVersion = 8 });

            // 'await' is valid as function names.
            Test("async function await() { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), name = "await" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // cannot use 'await' inside async functions.
            testFail("async function wrap() {\nasync function await() { }\n}", "Can not use 'await' as identifier inside an async function (2:15)", new Options { ecmaVersion = 8 });
            testFail("async function foo(await) { }", "Can not use 'await' as identifier inside an async function (1:19)", new Options { ecmaVersion = 8 });
            testFail("async function foo() { return {await} }", "Can not use 'await' as identifier inside an async function (1:31)", new Options { ecmaVersion = 8 });

            //-----------------------------------------------------------------------------
            // Async Function Expressions

            // async == false
            Test("(function foo() { })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        expression = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 19, 19)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo" },
                            generator = false,
                            expression = false,
                            async = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // async == true
            Test("(async function foo() { })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                        expression = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo" },
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // cannot insert a linebreak to between 'async' and 'function'.
            testFail("(async\nfunction foo() { })", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });

            // cannot combine with generators.
            testFail("(async function* foo() { })", "Unexpected token (1:15)", new Options { ecmaVersion = 8 });

            // export default
            Test("export default (async function() { })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportDefaultDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                        declaration = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36)),
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 36, 36)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            // cannot use 'await' inside async functions.
            testFail("(async function await() { })", "Can not use 'await' as identifier inside an async function (1:16)", new Options { ecmaVersion = 8 });
            testFail("(async function foo(await) { })", "Can not use 'await' as identifier inside an async function (1:20)", new Options { ecmaVersion = 8 });
            testFail("(async function foo() { return {await} })", "Can not use 'await' as identifier inside an async function (1:32)", new Options { ecmaVersion = 8 });

            //-----------------------------------------------------------------------------
            // Async Arrow Function Expressions

            // async == false
            Test("a => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "a" }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(a) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a" }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // async == true
            Test("async a => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "a" }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async () => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async (a, b) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a" },
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b" }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // OK even if it's an invalid syntax in the case `=>` didn't exist.
            Test("async ({a = b}) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                source = SourceType.Script,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(ObjectPatternNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 14, 14)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" },
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" },
                                                right = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "b" }
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // syntax error if `=>` didn't exist.
            testFail("async ({a = b})", "Shorthand property assignments are valid only in destructuring patterns (1:10)", new Options { ecmaVersion = 8 });

            // AssignmentPattern/AssignmentExpression
            Test("async ({a: b = c}) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(ObjectPatternNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" },
                                            value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)),
                                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b" },
                                                right = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "c" }
                                            },
                                            kind = PropertyKind.Initialise
                                        }
                                    }
                                }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async ({a: b = c})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        expression = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "async" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" },
                                            value = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)),
                                                @operator = Operator.Assignment,
                                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b" },
                                                right = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "c" }
                                            },
                                            kind = PropertyKind.Initialise
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // a reference and a normal arrow function if there is a linebreak between 'async' and the 1st parameter.
            Test("async\na => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 12)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "async" }
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 6), new Position(2, 1, 7)), name = "a" }
                            },
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 5, 11), new Position(2, 6, 12)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // 'async()' call expression and invalid '=>' token.
            testFail("async\n() => a", "Unexpected token (2:3)", new Options { ecmaVersion = 8 });

            // cannot insert a linebreak before '=>'.
            testFail("async a\n=> a", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });
            testFail("async ()\n=> a", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });

            // a call expression with 'await' reference.
            Test("async (await)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "async" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 12, 12)), name = "await" }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // cannot use 'await' inside async functions.
            testFail("async await => 1", "Can not use 'await' as identifier inside an async function (1:6)", new Options { ecmaVersion = 8 });
            testFail("async (await) => 1", "Can not use 'await' as identifier inside an async function (1:7)", new Options { ecmaVersion = 8 });
            testFail("async ({await}) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options { ecmaVersion = 8 });
            testFail("async ({a: await}) => 1", "Can not use 'await' as identifier inside an async function (1:11)", new Options { ecmaVersion = 8 });
            testFail("async ([await]) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options { ecmaVersion = 8 });

            // can use 'yield' identifier outside generators.
            Test("async yield => 1", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)), name = "yield" }
                            },
                            body = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)),
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            //-----------------------------------------------------------------------------
            // Async Methods (object)

            // async == false
            Test("({foo() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), name = "foo" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // async == true
            Test("({async foo() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // OK with 'async' as a method name
            Test("({async() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // invalid syntax if there is a linebreak after 'async'.
            testFail("({async\nfoo() { }})", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });

            // cannot combine with getters/setters/generators.
            testFail("({async get foo() { }})", "Unexpected token (1:12)", new Options { ecmaVersion = 8 });
            testFail("({async set foo(value) { }})", "Unexpected token (1:12)", new Options { ecmaVersion = 8 });
            testFail("({async* foo() { }})", "Unexpected token (1:7)", new Options { ecmaVersion = 8 });

            // 'await' is valid as function names.
            Test("({async await() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 20, 20)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 19, 19)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), name = "await" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\n({async await() { }})\n}", new TestNode { type = typeof(ProgramNode) }, new Options { ecmaVersion = 8 });
            testFail("({async foo() { var await }})", "Can not use 'await' as identifier inside an async function (1:20)", new Options { ecmaVersion = 8 });
            testFail("({async foo(await) { }})", "Can not use 'await' as identifier inside an async function (1:12)", new Options { ecmaVersion = 8 });
            testFail("({async foo() { return {await} }})", "Can not use 'await' as identifier inside an async function (1:24)", new Options { ecmaVersion = 8 });

            // invalid syntax 'async foo: 1'
            testFail("({async foo: 1})", "Unexpected token (1:11)", new Options { ecmaVersion = 8 });

            //-----------------------------------------------------------------------------
            // Async Methods (class)

            // async == false
            Test("class A {foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 19, 19)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // async == true
            Test("class A {async foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 24, 24)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {static async foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 31, 31)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), name = "foo" },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 31, 31)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // OK 'async' as a method name.
            Test("class A {async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "async" },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {static async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)), name = "async" },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 27, 27)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {*async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 22, 22)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 21, 21)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "async" },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 21, 21)),
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {static* async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 28, 28)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), name = "async" },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28)),
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // invalid syntax if there is a linebreak after 'async'.
            testFail("class A {async\nfoo() { }}", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });
            testFail("class A {static async\nfoo() { }}", "Unexpected token (2:0)", new Options { ecmaVersion = 8 });

            // cannot combine with constructors/getters/setters/generators.
            testFail("class A {async constructor() { }}", "Constructor can't be an async method (1:15)", new Options { ecmaVersion = 8 });
            testFail("class A {async get foo() { }}", "Unexpected token (1:19)", new Options { ecmaVersion = 8 });
            testFail("class A {async set foo(value) { }}", "Unexpected token (1:19)", new Options { ecmaVersion = 8 });
            testFail("class A {async* foo() { }}", "Unexpected token (1:14)", new Options { ecmaVersion = 8 });
            testFail("class A {static async get foo() { }}", "Unexpected token (1:26)", new Options { ecmaVersion = 8 });
            testFail("class A {static async set foo(value) { }}", "Unexpected token (1:26)", new Options { ecmaVersion = 8 });
            testFail("class A {static async* foo() { }}", "Unexpected token (1:21)", new Options { ecmaVersion = 8 });

            // 'await' is valid as function names.
            Test("class A {async await() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 26, 26)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), name = "await" },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {static async await() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 34, 34)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 33, 33)),
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), name = "await" },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 33, 33)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\nclass A {async await() { }}\n}", new TestNode { type = typeof(ProgramNode) }, new Options { ecmaVersion = 8 });
            testFail("class A {async foo() { var await }}", "Can not use 'await' as identifier inside an async function (1:27)", new Options { ecmaVersion = 8 });
            testFail("class A {async foo(await) { }}", "Can not use 'await' as identifier inside an async function (1:19)", new Options { ecmaVersion = 8 });
            testFail("class A {async foo() { return {await} }}", "Can not use 'await' as identifier inside an async function (1:31)", new Options { ecmaVersion = 8 });
            //-----------------------------------------------------------------------------
            // Await Expressions

            // 'await' is an identifier in scripts.
            Test("await", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "await" }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // 'await' is a keyword in modules.
            testFail("await", "The keyword 'await' is reserved (1:0)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            // Await expressions is invalid outside of async functions.
            testFail("await a", "Unexpected token (1:6)", new Options { ecmaVersion = 8 });
            testFail("await a", "The keyword 'await' is reserved (1:0)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            // Await expressions in async functions.
            Test("async function foo(a, b) { await a }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "b" }
                        },
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)),
                                    expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)),
                                        argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), name = "a" }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(async function foo(a) { await a })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        expression = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 34, 34)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo" },
                            generator = false,
                            expression = false,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "a" }
                            },
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 34, 34)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)),
                                        expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)),
                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "a" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(async (a) => await a)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21)),
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" }
                            },
                            body = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 21, 21)),
                                argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "a" }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("({async foo(a) { await a }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 27, 27)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 26, 26)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 26, 26)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "a" }
                                        },
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26)),
                                            body = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)),
                                                    expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)),
                                                        argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "a" }
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
            }, new Options { ecmaVersion = 8 });

            Test("(class {async foo(a) { await a }})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        expression = new TestNode { type = typeof(ClassExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)),
                            id = null,
                            superClass = null,
                            body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 33, 33)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)),
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), name = "foo" },
                                        @static = false,
                                        kind = PropertyKind.Method,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 32, 32)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "a" }
                                            },
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32)),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)),
                                                        expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)),
                                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), name = "a" }
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
            }, new Options { ecmaVersion = 8 });

            // Await expressions are an unary expression.
            Test("async function foo(a, b) { await a + await b }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "b" }
                        },
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 46, 46)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)),
                                    expression = new TestNode { type = typeof(BinaryExpressionNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)),
                                        left = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)),
                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), name = "a" }
                                        },
                                        @operator = Operator.Addition,
                                        right = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 44, 44)),
                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 43, 43), new Position(1, 44, 44)), name = "b" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // 'await + 1' is a binary expression outside of async functions.
            Test("function foo() { await + 1 }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 28, 28)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)),
                                    expression = new TestNode { type = typeof(BinaryExpressionNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)),
                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), name = "await" },
                                        @operator = Operator.Addition,
                                        right = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)),
                                            value = 1,
                                            raw = "1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // 'await + 1' is an await expression in async functions.
            Test("async function foo() { await + 1 }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)),
                                    expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)),
                                        argument = new TestNode { type = typeof(UnaryExpressionNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)),
                                            @operator = Operator.Addition,
                                            prefix = true,
                                            argument = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)),
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
            }, new Options { ecmaVersion = 8 });

            // Await expressions need one argument.
            testFail("async function foo() { await }", "Unexpected token (1:29)", new Options { ecmaVersion = 8 });
            testFail("(async function foo() { await })", "Unexpected token (1:30)", new Options { ecmaVersion = 8 });
            testFail("async () => await", "Unexpected token (1:17)", new Options { ecmaVersion = 8 });
            testFail("({async foo() { await }})", "Unexpected token (1:22)", new Options { ecmaVersion = 8 });
            testFail("(class {async foo() { await }})", "Unexpected token (1:28)", new Options { ecmaVersion = 8 });

            // Forbid await expressions in default parameters:
            testFail("async function foo(a = await b) {}", "Await expression cannot be a default value (1:23)", new Options { ecmaVersion = 8 });
            testFail("(async function foo(a = await b) {})", "Await expression cannot be a default value (1:24)", new Options { ecmaVersion = 8 });
            testFail("async (a = await b) => {}", "Unexpected token (1:17)", new Options { ecmaVersion = 8 });
            testFail("async function wrapper() {\nasync (a = await b) => {}\n}", "Await expression cannot be a default value (2:11)", new Options { ecmaVersion = 8 });
            testFail("({async foo(a = await b) {}})", "Await expression cannot be a default value (1:16)", new Options { ecmaVersion = 8 });
            testFail("(class {async foo(a = await b) {}})", "Await expression cannot be a default value (1:22)", new Options { ecmaVersion = 8 });
            testFail("async function foo(a = class extends (await b) {}) {}", "Await expression cannot be a default value (1:38)", new Options { ecmaVersion = 8 });

            // Allow await expressions inside functions in default parameters:
            Test("async function foo(a = async function foo() { await b }) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 55, 55)),
                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                                right = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 55, 55)),
                                    id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 38, 38), new Position(1, 41, 41)), name = "foo" },
                                    generator = false,
                                    expression = false,
                                    async = true,
                                    parameters = new List<TestNode>(),
                                    body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 55, 55)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)),
                                                expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)),
                                                    argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), name = "b" }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 57, 57), new Position(1, 59, 59)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async function foo(a = async () => await b) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 42, 42)),
                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                                right = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 42, 42)),
                                    id = null,
                                    generator = false,
                                    expression = true,
                                    async = true,
                                    parameters = new List<TestNode>(),
                                    body = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 35, 35), new Position(1, 42, 42)),
                                        argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 42, 42)), name = "b" }
                                    }
                                }
                            }
                        },
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 46, 46)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async function foo(a = {async bar() { await b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 48, 48)),
                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                                right = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 48, 48)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 47, 47)),
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), name = "bar" },
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 47, 47)),
                                                id = null,
                                                generator = false,
                                                expression = false,
                                                async = true,
                                                parameters = new List<TestNode>(),
                                                body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 36, 36), new Position(1, 47, 47)),
                                                    body = new List<TestNode>
                                                    {
                                                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)),
                                                            expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)),
                                                                argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 45, 45)), name = "b" }
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
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 50, 50), new Position(1, 52, 52)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async function foo(a = class {async bar() { await b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 54, 54)),
                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" },
                                right = new TestNode { type = typeof(ClassExpressionNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 54, 54)),
                                    id = null,
                                    superClass = null,
                                    body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 54, 54)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 53, 53)),
                                                computed = false,
                                                key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 36, 36), new Position(1, 39, 39)), name = "bar" },
                                                @static = false,
                                                kind = PropertyKind.Method,
                                                value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 53, 53)),
                                                    id = null,
                                                    generator = false,
                                                    expression = false,
                                                    async = true,
                                                    parameters = new List<TestNode>(),
                                                    body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 42, 42), new Position(1, 53, 53)),
                                                        body = new List<TestNode>
                                                        {
                                                            new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)),
                                                                expression = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)),
                                                                    argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 50, 50), new Position(1, 51, 51)), name = "b" }
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
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 56, 56), new Position(1, 58, 58)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Test("async function wrap() {\n(a = await b)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), name = "wrap" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 39)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 0, 24), new Position(2, 13, 37)),
                                    expression = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(2, 1, 25), new Position(2, 12, 36)),
                                        @operator = Operator.Assignment,
                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 25), new Position(2, 2, 26)), name = "a" },
                                        right = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(2, 5, 29), new Position(2, 12, 36)),
                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 11, 35), new Position(2, 12, 36)), name = "b" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });
            testFail("async function wrap() {\n(a = await b) => a\n}", "Await expression cannot be a default value (2:5)", new Options { ecmaVersion = 8 });

            Test("async function wrap() {\n({a = await b} = obj)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), name = "wrap" },
                        generator = false,
                        expression = false,
                        async = true,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 47)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 0, 24), new Position(2, 21, 45)),
                                    expression = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(2, 1, 25), new Position(2, 20, 44)),
                                        @operator = Operator.Assignment,
                                        left = new TestNode { type = typeof(ObjectPatternNode), location = new SourceLocation(new Position(2, 1, 25), new Position(2, 14, 38)),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), name = "a" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)),
                                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), name = "a" },
                                                        right = new TestNode { type = typeof(AwaitExpressionNode), location = new SourceLocation(new Position(2, 6, 30), new Position(2, 13, 37)),
                                                            argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 12, 36), new Position(2, 13, 37)), name = "b" }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 17, 41), new Position(2, 20, 44)), name = "obj" }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });
            testFail("async function wrap() {\n({a = await b} = obj) => a\n}", "Await expression cannot be a default value (2:6)", new Options { ecmaVersion = 8 });

            Test("function* wrap() {\nasync(a = yield b)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "wrap" },
                        parameters = new List<TestNode>(),
                        generator = true,
                        expression = false,
                        async = false,
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 39)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)),
                                    expression = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)),
                                        callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 19), new Position(2, 5, 24)), name = "async" },
                                        arguments = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(2, 6, 25), new Position(2, 17, 36)),
                                                @operator = Operator.Assignment,
                                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 6, 25), new Position(2, 7, 26)), name = "a" },
                                                right = new TestNode { type = typeof(YieldExpressionNode), location = new SourceLocation(new Position(2, 10, 29), new Position(2, 17, 36)),
                                                    @delegate = false,
                                                    argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 16, 35), new Position(2, 17, 36)), name = "b" }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });
            testFail("function* wrap() {\nasync(a = yield b) => a\n}", "Yield expression cannot be a default value (2:10)", new Options { ecmaVersion = 8 });

            // https://github.com/ternjs/acorn/issues/464
            Test("f = ({ w = counter(), x = counter(), y = counter(), z = counter() } = { w: null, x: 0, y: false, z: '' }) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)),
                        expression = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)),
                            @operator = Operator.Assignment,
                            left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "f" },
                            right = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 111, 111)),
                                id = null,
                                parameters = new List<TestNode>
                                {
                                    new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 104, 104)),
                                        left = new TestNode { type = typeof(ObjectPatternNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 67, 67)),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "w" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)),
                                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "w" },
                                                        right = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20)),
                                                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 18, 18)), name = "counter" },
                                                            arguments = new List<TestNode>()
                                                        }
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "x" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)),
                                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "x" },
                                                        right = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 35, 35)),
                                                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 33, 33)), name = "counter" },
                                                            arguments = new List<TestNode>()
                                                        }
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), name = "y" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)),
                                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), name = "y" },
                                                        right = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 50, 50)),
                                                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48)), name = "counter" },
                                                            arguments = new List<TestNode>()
                                                        }
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), name = "z" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)),
                                                        left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), name = "z" },
                                                        right = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 56, 56), new Position(1, 65, 65)),
                                                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 56, 56), new Position(1, 63, 63)), name = "counter" },
                                                            arguments = new List<TestNode>()
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 70, 70), new Position(1, 104, 104)),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 72, 72), new Position(1, 79, 79)),
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 72, 72), new Position(1, 73, 73)), name = "w" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 75, 75), new Position(1, 79, 79)),
                                                        value = null,
                                                        raw = "null"
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 81, 81), new Position(1, 85, 85)),
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 81, 81), new Position(1, 82, 82)), name = "x" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 84, 84), new Position(1, 85, 85)),
                                                        value = 0,
                                                        raw = "0"
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 87, 87), new Position(1, 95, 95)),
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 87, 87), new Position(1, 88, 88)), name = "y" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 90, 90), new Position(1, 95, 95)),
                                                        value = false,
                                                        raw = "false"
                                                    }
                                                },
                                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 97, 97), new Position(1, 102, 102)),
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 97, 97), new Position(1, 98, 98)), name = "z" },
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 100, 100), new Position(1, 102, 102)),
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
                                body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 109, 109), new Position(1, 111, 111)),
                                    body = new List<TestNode>()
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("({ async: true })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode),
                                    key = new TestNode { type = typeof(IdentifierNode),  name = "async" },
                                    value = new TestNode { type = typeof(LiteralNode),
                                        value = true
                                    },
                                    kind = PropertyKind.Initialise
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Test("if (x) async function f() {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                    {
                        new TestNode
                        {
                            type = typeof(IfStatementNode),
                            location = default,
                            test = null,
                            consequent = new TestNode { type = typeof(FunctionDeclarationNode),
                                async = true
                            },
                            alternate = null
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            testFail("(async)(a) => 12", "Unexpected token (1:11)", new Options { ecmaVersion = 8 });

            testFail("f = async ((x)) => x", "Parenthesized pattern (1:11)", new Options { ecmaVersion = 8 });

            // allow 'async' as a shorthand property in script.
            Test("({async})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({async, foo})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" }
                                    },
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({async = 0} = {})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                            expression = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17)),
                                @operator = Operator.Assignment,
                                left = new TestNode { type = typeof(ObjectPatternNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" },
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)),
                                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), name = "async" },
                                                right = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)),
                                                    value = 0,
                                                    raw = "0"
                                                }
                                            }
                                        }
                                    }
                                },
                                right = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                    properties = new List<TestNode>()
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            // async functions with vary names.
            Test("({async \"foo\"(){}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)),
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                            value = "foo",
                                            raw = "\"foo\""
                                        },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({async 'foo'(){}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)),
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                            value = "foo",
                                            raw = "'foo'"
                                        },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({async 100(){}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 16, 16)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)),
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)),
                                            value = 100,
                                            raw = "100"
                                        },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({async [foo](){}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                    {
                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                            expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)),
                                properties = new List<TestNode>
                                {
                                    new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)),
                                        method = true,
                                        shorthand = false,
                                        computed = true,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            },
                new Options { ecmaVersion = 8 }
            );

            Test("({ async delete() {} })", new TestNode { type = typeof(ProgramNode) }, new Options { ecmaVersion = 8 });
        }
    }
}
