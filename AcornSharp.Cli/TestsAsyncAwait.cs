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
            Test("function foo() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("async function foo() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // a reference and a normal function declaration if there is a linebreak between 'async' and 'function'.
            Test("async\nfunction foo() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 24)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async")
                    },
                    new BaseNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 18, 24)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(2, 9, 15), new Position(2, 12, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(2, 15, 21), new Position(2, 18, 24)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // export
            Test("export async function foo() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31)))
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)))
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), "foo"),
                            generator = false,
                            bexpression = false,
                            async = true,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        },
                        specifiers = new List<BaseNode>(),
                        source = null
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            // export default
            Test("export default async function() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 35, 35)))
                        {
                            type = NodeType.FunctionDeclaration,
                            id = null,
                            generator = false,
                            bexpression = false,
                            async = true,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            // cannot combine with generators
            testFail("async function* foo() { }", "Unexpected token (1:14)", new Options {ecmaVersion = 8});

            // 'await' is valid as function names.
            Test("async function await() { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), "await"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            testFail("async function wrap() {\nasync function await() { }\n}", "Can not use 'await' as identifier inside an async function (2:15)", new Options {ecmaVersion = 8});
            testFail("async function foo(await) { }", "Can not use 'await' as identifier inside an async function (1:19)", new Options {ecmaVersion = 8});
            testFail("async function foo() { return {await} }", "Can not use 'await' as identifier inside an async function (1:31)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Function Expressions

            // async == false
            Test("(function foo() { })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 19, 19)))
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                            generator = false,
                            bexpression = false,
                            async = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("(async function foo() { })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25)))
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            generator = false,
                            bexpression = false,
                            async = true,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // cannot insert a linebreak to between 'async' and 'function'.
            testFail("(async\nfunction foo() { })", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // cannot combine with generators.
            testFail("(async function* foo() { })", "Unexpected token (1:15)", new Options {ecmaVersion = 8});

            // export default
            Test("export default (async function() { })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)))
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36)))
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = false,
                            async = true,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 36, 36)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            // cannot use 'await' inside async functions.
            testFail("(async function await() { })", "Can not use 'await' as identifier inside an async function (1:16)", new Options {ecmaVersion = 8});
            testFail("(async function foo(await) { })", "Can not use 'await' as identifier inside an async function (1:20)", new Options {ecmaVersion = 8});
            testFail("(async function foo() { return {await} })", "Can not use 'await' as identifier inside an async function (1:32)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Arrow Function Expressions

            // async == false
            Test("a => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = false,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(a) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = false,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("async a => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async () => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>(),
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async (a, b) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // OK even if it's an invalid syntax in the case `=>` didn't exist.
            Test("async ({a = b}) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 14, 14)))
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            kind = "init",
                                            value = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                            {
                                                type = NodeType.AssignmentPattern,
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
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // syntax error if `=>` didn't exist.
            testFail("async ({a = b})", "Shorthand property assignments are valid only in destructuring patterns (1:10)", new Options {ecmaVersion = 8});

            // AssignmentPattern/AssignmentExpression
            Test("async ({a: b = c}) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)))
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)))
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)))
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "c")
                                            },
                                            kind = "init"
                                        }
                                    }
                                }
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async ({a: b = c})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 17, 17)))
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)))
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16)))
                                            {
                                                type = NodeType.AssignmentExpression,
                                                @operator = "=",
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "c")
                                            },
                                            kind = "init"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // a reference and a normal arrow function if there is a linebreak between 'async' and the 1st parameter.
            Test("async\na => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 12)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async")
                    },
                    new BaseNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 6, 12)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = false,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(2, 0, 6), new Position(2, 1, 7)), "a")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(2, 5, 11), new Position(2, 6, 12)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // 'async()' call expression and invalid '=>' token.
            testFail("async\n() => a", "Unexpected token (2:3)", new Options {ecmaVersion = 8});

            // cannot insert a linebreak before '=>'.
            testFail("async a\n=> a", "Unexpected token (2:0)", new Options {ecmaVersion = 8});
            testFail("async ()\n=> a", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // a call expression with 'await' reference.
            Test("async (await)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "async"),
                            arguments = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 12, 12)), "await")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            testFail("async await => 1", "Can not use 'await' as identifier inside an async function (1:6)", new Options {ecmaVersion = 8});
            testFail("async (await) => 1", "Can not use 'await' as identifier inside an async function (1:7)", new Options {ecmaVersion = 8});
            testFail("async ({await}) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options {ecmaVersion = 8});
            testFail("async ({a: await}) => 1", "Can not use 'await' as identifier inside an async function (1:11)", new Options {ecmaVersion = 8});
            testFail("async ([await]) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new Options {ecmaVersion = 8});

            // can use 'yield' identifier outside generators.
            Test("async yield => 1", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)), "yield")
                            },
                            fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)))
                            {
                                type = NodeType.Literal,
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Methods (object)

            // async == false
            Test("({foo() { }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), "foo"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("({async foo() { }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // OK with 'async' as a method name
            Test("({async() { }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // invalid syntax if there is a linebreak after 'async'.
            testFail("({async\nfoo() { }})", "Unexpected token (2:0)", new Options {ecmaVersion = 8});

            // cannot combine with getters/setters/generators.
            testFail("({async get foo() { }})", "Unexpected token (1:12)", new Options {ecmaVersion = 8});
            testFail("({async set foo(value) { }})", "Unexpected token (1:12)", new Options {ecmaVersion = 8});
            testFail("({async* foo() { }})", "Unexpected token (1:7)", new Options {ecmaVersion = 8});

            // 'await' is valid as function names.
            Test("({async await() { }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 20, 20)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 19, 19)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), "await"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\n({async await() { }})\n}", new BaseNode(default), new Options {ecmaVersion = 8});
            testFail("({async foo() { var await }})", "Can not use 'await' as identifier inside an async function (1:20)", new Options {ecmaVersion = 8});
            testFail("({async foo(await) { }})", "Can not use 'await' as identifier inside an async function (1:12)", new Options {ecmaVersion = 8});
            testFail("({async foo() { return {await} }})", "Can not use 'await' as identifier inside an async function (1:24)", new Options {ecmaVersion = 8});

            // invalid syntax 'async foo: 1'
            testFail("({async foo: 1})", "Unexpected token (1:11)", new Options {ecmaVersion = 8});

            //-----------------------------------------------------------------------------
            // Async Methods (class)

            // async == false
            Test("class A {foo() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 19, 19)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // async == true
            Test("class A {async foo() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 24, 24)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static async foo() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 31, 31)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), "foo"),
                                    @static = true,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 31, 31)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // OK 'async' as a method name.
            Test("class A {async() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "async"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static async() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)), "async"),
                                    @static = true,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 27, 27)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {*async() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 22, 22)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 21, 21)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "async"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 21, 21)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static* async() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 28, 28)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), "async"),
                                    @static = true,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        async = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
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
            Test("class A {async await() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 26, 26)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), "await"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static async await() { }}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 34, 34)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 33, 33)))
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), "await"),
                                    @static = true,
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 33, 33)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // cannot use 'await' inside async functions.
            Test("async function wrap() {\nclass A {async await() { }}\n}", new BaseNode(default), new Options {ecmaVersion = 8});
            testFail("class A {async foo() { var await }}", "Can not use 'await' as identifier inside an async function (1:27)", new Options {ecmaVersion = 8});
            testFail("class A {async foo(await) { }}", "Can not use 'await' as identifier inside an async function (1:19)", new Options {ecmaVersion = 8});
            testFail("class A {async foo() { return {await} }}", "Can not use 'await' as identifier inside an async function (1:31)", new Options {ecmaVersion = 8});
            //-----------------------------------------------------------------------------
            // Await Expressions

            // 'await' is an identifier in scripts.
            Test("await", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "await")
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // 'await' is a keyword in modules.
            testFail("await", "The keyword 'await' is reserved (1:0)", new Options {ecmaVersion = 8, sourceType = "module"});

            // Await expressions is invalid outside of async functions.
            testFail("await a", "Unexpected token (1:6)", new Options {ecmaVersion = 8});
            testFail("await a", "The keyword 'await' is reserved (1:0)", new Options {ecmaVersion = 8, sourceType = "module"});

            // Await expressions in async functions.
            Test("async function foo(a, b) { await a }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                            new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "b")
                        },
                        fbody = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                    {
                                        type = NodeType.AwaitExpression,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "a")
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(async function foo(a) { await a })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 34, 34)))
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            generator = false,
                            bexpression = false,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            },
                            fbody = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 34, 34)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)))
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)))
                                        {
                                            type = NodeType.AwaitExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "a")
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(async (a) => await a)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            async = true,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                            },
                            fbody = new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 21, 21)))
                            {
                                type = NodeType.AwaitExpression,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("({async foo(a) { await a }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 27, 27)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 26, 26)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 26, 26)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        async = true,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                                        },
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                                    {
                                                        type = NodeType.AwaitExpression,
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
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(class {async foo(a) { await a }})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)))
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 33, 33)))
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 32, 32)))
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                                        @static = false,
                                        kind = "method",
                                        value = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 32, 32)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            async = true,
                                            @params = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "a")
                                            },
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)))
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30)))
                                                        {
                                                            type = NodeType.AwaitExpression,
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
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // Await expressions are an unary expression.
            Test("async function foo(a, b) { await a + await b }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                            new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "b")
                        },
                        fbody = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 46, 46)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 44, 44)))
                                    {
                                        type = NodeType.BinaryExpression,
                                        left = new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)))
                                        {
                                            type = NodeType.AwaitExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "a")
                                        },
                                        @operator = "+",
                                        right = new BaseNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 44, 44)))
                                        {
                                            type = NodeType.AwaitExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 44, 44)), "b")
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // 'await + 1' is a binary expression outside of async functions.
            Test("function foo() { await + 1 }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 28, 28)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)))
                                    {
                                        type = NodeType.BinaryExpression,
                                        left = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)), "await"),
                                        @operator = "+",
                                        right = new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)))
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // 'await + 1' is an await expression in async functions.
            Test("async function foo() { await + 1 }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 32, 32)))
                                    {
                                        type = NodeType.AwaitExpression,
                                        argument = new BaseNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)))
                                        {
                                            type = NodeType.UnaryExpression,
                                            @operator = "+",
                                            prefix = true,
                                            argument = new BaseNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)))
                                            {
                                                type = NodeType.Literal,
                                                value = 1,
                                                raw = "1"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
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
            Test("async function foo(a = async function foo() { await b }) {}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 55, 55)))
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 55, 55)))
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 41, 41)), "foo"),
                                    generator = false,
                                    bexpression = false,
                                    async = true,
                                    @params = new List<BaseNode>(),
                                    fbody = new BaseNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 55, 55)))
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)))
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(new SourceLocation(new Position(1, 46, 46), new Position(1, 53, 53)))
                                                {
                                                    type = NodeType.AwaitExpression,
                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "b")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BaseNode(new SourceLocation(new Position(1, 57, 57), new Position(1, 59, 59)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = async () => await b) {}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 42, 42)))
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 42, 42)))
                                {
                                    type = NodeType.ArrowFunctionExpression,
                                    id = null,
                                    generator = false,
                                    bexpression = true,
                                    async = true,
                                    @params = new List<BaseNode>(),
                                    fbody = new BaseNode(new SourceLocation(new Position(1, 35, 35), new Position(1, 42, 42)))
                                    {
                                        type = NodeType.AwaitExpression,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 42, 42)), "b")
                                    }
                                }
                            }
                        },
                        fbody = new BaseNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 46, 46)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = {async bar() { await b }}) {}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 52, 52)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 48, 48)))
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 48, 48)))
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 47, 47)))
                                        {
                                            type = NodeType.Property,
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), "bar"),
                                            kind = "init",
                                            value = new BaseNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 47, 47)))
                                            {
                                                type = NodeType.FunctionExpression,
                                                id = null,
                                                generator = false,
                                                bexpression = false,
                                                async = true,
                                                @params = new List<BaseNode>(),
                                                fbody = new BaseNode(new SourceLocation(new Position(1, 36, 36), new Position(1, 47, 47)))
                                                {
                                                    type = NodeType.BlockStatement,
                                                    body = new List<BaseNode>
                                                    {
                                                        new BaseNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)))
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            expression = new BaseNode(new SourceLocation(new Position(1, 38, 38), new Position(1, 45, 45)))
                                                            {
                                                                type = NodeType.AwaitExpression,
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
                        fbody = new BaseNode(new SourceLocation(new Position(1, 50, 50), new Position(1, 52, 52)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async function foo(a = class {async bar() { await b }}) {}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 58, 58)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 54, 54)))
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a"),
                                right = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 54, 54)))
                                {
                                    type = NodeType.ClassExpression,
                                    id = null,
                                    superClass = null,
                                    fbody = new BaseNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 54, 54)))
                                    {
                                        type = NodeType.ClassBody,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 53, 53)))
                                            {
                                                type = NodeType.MethodDefinition,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 36, 36), new Position(1, 39, 39)), "bar"),
                                                @static = false,
                                                kind = "method",
                                                value = new BaseNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 53, 53)))
                                                {
                                                    type = NodeType.FunctionExpression,
                                                    id = null,
                                                    generator = false,
                                                    bexpression = false,
                                                    async = true,
                                                    @params = new List<BaseNode>(),
                                                    fbody = new BaseNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 53, 53)))
                                                    {
                                                        type = NodeType.BlockStatement,
                                                        body = new List<BaseNode>
                                                        {
                                                            new BaseNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)))
                                                            {
                                                                type = NodeType.ExpressionStatement,
                                                                expression = new BaseNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 51, 51)))
                                                                {
                                                                    type = NodeType.AwaitExpression,
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
                        fbody = new BaseNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 58, 58)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Test("async function wrap() {\n(a = await b)\n}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), "wrap"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 39)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 13, 37)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 12, 36)))
                                    {
                                        type = NodeType.AssignmentExpression,
                                        @operator = "=",
                                        left = new IdentifierNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 2, 26)), "a"),
                                        right = new BaseNode(new SourceLocation(new Position(2, 5, 29), new Position(2, 12, 36)))
                                        {
                                            type = NodeType.AwaitExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(2, 11, 35), new Position(2, 12, 36)), "b")
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});
            testFail("async function wrap() {\n(a = await b) => a\n}", "Await expression cannot be a default value (2:5)", new Options {ecmaVersion = 8});

            Test("async function wrap() {\n({a = await b} = obj)\n}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 47)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), "wrap"),
                        generator = false,
                        bexpression = false,
                        async = true,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(3, 1, 47)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 21, 45)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 20, 44)))
                                    {
                                        type = NodeType.AssignmentExpression,
                                        @operator = "=",
                                        left = new BaseNode(new SourceLocation(new Position(2, 1, 25), new Position(2, 14, 38)))
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), "a"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 13, 37)))
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        left = new IdentifierNode(new SourceLocation(new Position(2, 2, 26), new Position(2, 3, 27)), "a"),
                                                        right = new BaseNode(new SourceLocation(new Position(2, 6, 30), new Position(2, 13, 37)))
                                                        {
                                                            type = NodeType.AwaitExpression,
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
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});
            testFail("async function wrap() {\n({a = await b} = obj) => a\n}", "Await expression cannot be a default value (2:6)", new Options {ecmaVersion = 8});

            Test("function* wrap() {\nasync(a = yield b)\n}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 39)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                        @params = new List<BaseNode>(),
                        generator = true,
                        bexpression = false,
                        async = false,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 39)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 18, 37)))
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(2, 0, 19), new Position(2, 5, 24)), "async"),
                                        arguments = new List<BaseNode>
                                        {
                                            new BaseNode(new SourceLocation(new Position(2, 6, 25), new Position(2, 17, 36)))
                                            {
                                                type = NodeType.AssignmentExpression,
                                                @operator = "=",
                                                left = new IdentifierNode(new SourceLocation(new Position(2, 6, 25), new Position(2, 7, 26)), "a"),
                                                right = new BaseNode(new SourceLocation(new Position(2, 10, 29), new Position(2, 17, 36)))
                                                {
                                                    type = NodeType.YieldExpression,
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
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});
            testFail("function* wrap() {\nasync(a = yield b) => a\n}", "Yield expression cannot be a default value (2:10)", new Options {ecmaVersion = 8});

            // https://github.com/ternjs/acorn/issues/464
            Test("f = ({ w = counter(), x = counter(), y = counter(), z = counter() } = { w: null, x: 0, y: false, z: '' }) => {}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 111, 111)))
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "f"),
                            right = new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 111, 111)))
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 104, 104)))
                                    {
                                        type = NodeType.AssignmentPattern,
                                        left = new BaseNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 67, 67)))
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "w"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20)))
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "w"),
                                                        right = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20)))
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 18, 18)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "x"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 35, 35)))
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "x"),
                                                        right = new BaseNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 35, 35)))
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 33, 33)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), "y"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 50, 50)))
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), "y"),
                                                        right = new BaseNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 50, 50)))
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "z"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 65, 65)))
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        left = new IdentifierNode(new SourceLocation(new Position(1, 52, 52), new Position(1, 53, 53)), "z"),
                                                        right = new BaseNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 65, 65)))
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 56, 56), new Position(1, 63, 63)), "counter"),
                                                            arguments = new List<BaseNode>()
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new BaseNode(new SourceLocation(new Position(1, 70, 70), new Position(1, 104, 104)))
                                        {
                                            type = NodeType.ObjectExpression,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(1, 72, 72), new Position(1, 79, 79)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 72, 72), new Position(1, 73, 73)), "w"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 75, 75), new Position(1, 79, 79)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = null,
                                                        raw = "null"
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 81, 81), new Position(1, 85, 85)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 81, 81), new Position(1, 82, 82)), "x"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 84, 84), new Position(1, 85, 85)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 0,
                                                        raw = "0"
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 87, 87), new Position(1, 95, 95)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 87, 87), new Position(1, 88, 88)), "y"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 90, 90), new Position(1, 95, 95)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = false,
                                                        raw = "false"
                                                    }
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 97, 97), new Position(1, 102, 102)))
                                                {
                                                    type = NodeType.Property,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 97, 97), new Position(1, 98, 98)), "z"),
                                                    kind = "init",
                                                    value = new BaseNode(new SourceLocation(new Position(1, 100, 100), new Position(1, 102, 102)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = "",
                                                        raw = "''"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                generator = false,
                                bexpression = false,
                                async = false,
                                fbody = new BaseNode(new SourceLocation(new Position(1, 109, 109), new Position(1, 111, 111)))
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>()
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("({ async: true })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(default, "async"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = true
                                    },
                                    kind = "init"
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 8});

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Test("if (x) async function f() {}", new BaseNode(default)
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(default)
                        {
                            type = NodeType.IfStatement,
                            consequent = new BaseNode(default)
                            {
                                type = NodeType.FunctionDeclaration,
                                async = true
                            },
                            alternate = null
                        }
                    }
                },
                new Options {ecmaVersion = 8}
            );

            testFail("(async)(a) => 12", "Unexpected token (1:11)", new Options {ecmaVersion = 8});

            testFail("f = async ((x)) => x", "Parenthesized pattern (1:11)", new Options {ecmaVersion = 8});

            // allow 'async' as a shorthand property in script.
            Test("({async})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)))
                                    {
                                        type = NodeType.Property,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                        kind = "init",
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async")
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async, foo})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)))
                                    {
                                        type = NodeType.Property,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                        kind = "init",
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async")
                                    },
                                    new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)))
                                    {
                                        type = NodeType.Property,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                        kind = "init",
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo")
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async = 0} = {})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17)))
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12)))
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                            kind = "init",
                                            value = new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)))
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)), "async"),
                                                right = new BaseNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)))
                                                {
                                                    type = NodeType.Literal,
                                                    value = 0,
                                                    raw = "0"
                                                }
                                            }
                                        }
                                    }
                                },
                                right = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>()
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            // async functions with vary names.
            Test("({async \"foo\"(){}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        type = NodeType.Property,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            type = NodeType.Literal,
                                            value = "foo",
                                            raw = "\"foo\""
                                        },
                                        kind = "init",
                                        value = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            async = true,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async 'foo'(){}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        type = NodeType.Property,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)))
                                        {
                                            type = NodeType.Literal,
                                            value = "foo",
                                            raw = "'foo'"
                                        },
                                        kind = "init",
                                        value = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            async = true,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async 100(){}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 16, 16)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)))
                                    {
                                        type = NodeType.Property,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                        {
                                            type = NodeType.Literal,
                                            value = 100,
                                            raw = "100"
                                        },
                                        kind = "init",
                                        value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            async = true,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({async [foo](){}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                {
                    type = NodeType.Program,
                    body = new List<BaseNode>
                    {
                        new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18)))
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17)))
                                    {
                                        type = NodeType.Property,
                                        method = true,
                                        shorthand = false,
                                        computed = true,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                        kind = "init",
                                        value = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            async = true,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = "script"
                },
                new Options {ecmaVersion = 8}
            );

            Test("({ async delete() {} })", new BaseNode(default), new Options {ecmaVersion = 8});
        }
    }
}
