using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsAsyncAwait
    {
        public static void Run()
        {
            //-----------------------------------------------------------------------------
            // Async Function Declarations

            // async == false
            Program.test("function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 18,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 18,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 15,
                            end = 18,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // async == true
            Program.test("async function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 24,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 24,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 21,
                            end = 24,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // a reference and a normal function declaration if there is a line-break between 'async' and 'function'.
            Program.test("async\nfunction foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 24,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 5,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 0,
                            end = 5,
                            name = "async"
                        }
                    },
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 6,
                        end = 24,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 21,
                            end = 24,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // export
            Program.test("export async function foo() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 31,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        start = 0,
                        end = 31,
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            start = 7,
                            end = 31,
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 22,
                                end = 25,
                                name = "foo"
                            },
                            generator = false,
                            expression = false,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 28,
                                end = 31,
                                body = new TestNode[0]
                            }
                        },
                        specifiers = new TestNode[0],
                        source = null
                    }
                },
                sourceType = SourceType.Module
            }, new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            // export default
            Program.test("export default async function() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 35,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        start = 0,
                        end = 35,
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            start = 15,
                            end = 35,
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 32,
                                end = 35,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Module
            }, new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            // cannot combine with generators
            Program.testFail("async function* foo() { }", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await' is valid as function names.
            Program.test("async function await() { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 26,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 26,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 20,
                            name = "await"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 23,
                            end = 26,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot use 'await' inside async functions.
            Program.testFail("async function wrap() {\nasync function await() { }\n}", "Can not use 'await' as identifier inside an async function (2:15)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function foo(await) { }", "Can not use 'await' as identifier inside an async function (1:19)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function foo() { return {await} }", "Can not use 'await' as identifier inside an async function (1:31)", new TestOptions
            {
                ecmaVersion = 8
            });

            //-----------------------------------------------------------------------------
            // Async Function Expressions

            // async == false
            Program.test("(function foo() { })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 20,
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            start = 1,
                            end = 19,
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 10,
                                end = 13,
                                name = "foo"
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 16,
                                end = 19,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // async == true
            Program.test("(async function foo() { })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 26,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 26,
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            start = 1,
                            end = 25,
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 16,
                                end = 19,
                                name = "foo"
                            },
                            generator = false,
                            expression = false,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 22,
                                end = 25,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot insert a line-break to between 'async' and 'function'.
            Program.testFail("(async\nfunction foo() { })", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot combine with generators.
            Program.testFail("(async function* foo() { })", "Unexpected token (1:15)", new TestOptions
            {
                ecmaVersion = 8
            });

            // export default
            Program.test("export default (async function() { })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 37,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        start = 0,
                        end = 37,
                        declaration = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            start = 16,
                            end = 36,
                            id = null,
                            generator = false,
                            expression = false,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 33,
                                end = 36,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Module
            }, new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            // cannot use 'await' inside async functions.
            Program.testFail("(async function await() { })", "Can not use 'await' as identifier inside an async function (1:16)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(async function foo(await) { })", "Can not use 'await' as identifier inside an async function (1:20)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(async function foo() { return {await} })", "Can not use 'await' as identifier inside an async function (1:32)", new TestOptions
            {
                ecmaVersion = 8
            });

            //-----------------------------------------------------------------------------
            // Async Arrow Function Expressions

            // async == false
            Program.test("a => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 6,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 6,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 6,
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 0,
                                    end = 1,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 5,
                                end = 6,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("(a) => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 8,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 8,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 8,
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 1,
                                    end = 2,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 7,
                                end = 8,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // async == true
            Program.test("async a => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 12,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 12,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 6,
                                    end = 7,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 11,
                                end = 12,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async () => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 13,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 13,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 12,
                                end = 13,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async (a, b) => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 17,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 17,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 17,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 7,
                                    end = 8,
                                    name = "a"
                                },
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 10,
                                    end = 11,
                                    name = "b"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 16,
                                end = 17,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // OK even if it's an invalid syntax in the case `=>` didn't exist.
            Program.test("async ({a = b}) => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 20,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 20,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 7,
                                    end = 14,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 8,
                                            end = 13,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 8,
                                                end = 9,
                                                name = "a"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                start = 8,
                                                end = 13,
                                                left = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 8,
                                                    end = 9,
                                                    name = "a"
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 12,
                                                    end = 13,
                                                    name = "b"
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 19,
                                end = 20,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // syntax error if `=>` didn't exist.
            Program.testFail("async ({a = b})", "Shorthand property assignments are valid only in destructuring patterns (1:10)", new TestOptions
            {
                ecmaVersion = 8
            });

            // AssignmentPattern/AssignmentExpression
            Program.test("async ({a: b = c}) => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 23,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 23,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 23,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 7,
                                    end = 17,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 8,
                                            end = 16,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 8,
                                                end = 9,
                                                name = "a"
                                            },
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                start = 11,
                                                end = 16,
                                                left = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 11,
                                                    end = 12,
                                                    name = "b"
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 15,
                                                    end = 16,
                                                    name = "c"
                                                }
                                            },
                                            kind = PropertyKind.Init,
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 22,
                                end = 23,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async ({a: b = c})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 18,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 18,
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            start = 0,
                            end = 18,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 5,
                                name = "async"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 7,
                                    end = 17,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 8,
                                            end = 16,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 8,
                                                end = 9,
                                                name = "a"
                                            },
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentExpressionNode),
                                                start = 11,
                                                end = 16,
                                                @operator = "=",
                                                left = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 11,
                                                    end = 12,
                                                    name = "b"
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 15,
                                                    end = 16,
                                                    name = "c"
                                                }
                                            },
                                            kind = PropertyKind.Init,
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // a reference and a normal arrow function if there is a line-break between 'async' and the 1st parameter.
            Program.test("async\na => a", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 12,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 5,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 0,
                            end = 5,
                            name = "async"
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 6,
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 6,
                            end = 12,
                            id = null,
                            generator = false,
                            expression = true,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 6,
                                    end = 7,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 11,
                                end = 12,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // 'async()' call expression and invalid '=>' token.
            Program.testFail("async\n() => a", "Unexpected token (2:3)", new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot insert a line-break before '=>'.
            Program.testFail("async a\n=> a", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async ()\n=> a", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });

            // a call expression with 'await' reference.
            Program.test("async (await)", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 13,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            start = 0,
                            end = 13,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 5,
                                name = "async"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 7,
                                    end = 12,
                                    name = "await"
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot use 'await' inside async functions.
            Program.testFail("async await => 1", "Can not use 'await' as identifier inside an async function (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async (await) => 1", "Can not use 'await' as identifier inside an async function (1:7)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async ({await}) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async ({a: await}) => 1", "Can not use 'await' as identifier inside an async function (1:11)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async ([await]) => 1", "Can not use 'await' as identifier inside an async function (1:8)", new TestOptions
            {
                ecmaVersion = 8
            });

            // can use 'yield' identifier outside generators.
            Program.test("async yield => 1", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 16,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 16,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 16,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 6,
                                    end = 11,
                                    name = "yield"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                start = 15,
                                end = 16,
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            //-----------------------------------------------------------------------------
            // Async Methods (object)

            // async == false
            Program.test("({foo() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 13,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 12,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 11,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 5,
                                        name = "foo"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 5,
                                        end = 11,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 8,
                                            end = 11,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // async == true
            Program.test("({async foo() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 19,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 19,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 18,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 17,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 8,
                                        end = 11,
                                        name = "foo"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 11,
                                        end = 17,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 14,
                                            end = 17,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // OK with 'async' as a method name
            Program.test("({async() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 15,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 15,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 14,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 13,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 7,
                                        name = "async"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 7,
                                        end = 13,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 10,
                                            end = 13,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // invalid syntax if there is a line-break after 'async'.
            Program.testFail("({async\nfoo() { }})", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot combine with getters/setters/generators.
            Program.testFail("({async get foo() { }})", "Unexpected token (1:12)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async set foo(value) { }})", "Unexpected token (1:12)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async* foo() { }})", "Unexpected token (1:7)", new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await' is valid as function names.
            Program.test("({async await() { }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 21,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 21,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 20,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 19,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 8,
                                        end = 13,
                                        name = "await"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 13,
                                        end = 19,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 16,
                                            end = 19,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot use 'await' inside async functions.
            Program.test("async function wrap() {\n({async await() { }})\n}", new TestNode(), new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async foo() { var await }})", "Can not use 'await' as identifier inside an async function (1:20)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async foo(await) { }})", "Can not use 'await' as identifier inside an async function (1:12)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async foo() { return {await} }})", "Can not use 'await' as identifier inside an async function (1:24)", new TestOptions
            {
                ecmaVersion = 8
            });

            // invalid syntax 'async foo: 1'
            Program.testFail("({async foo: 1})", "Unexpected token (1:11)", new TestOptions
            {
                ecmaVersion = 8
            });

            //-----------------------------------------------------------------------------
            // Async Methods (class)

            // async == false
            Program.test("class A {foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 19,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 19,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 19,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 18,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 12,
                                        name = "foo"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 12,
                                        end = 18,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 15,
                                            end = 18,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // async == true
            Program.test("class A {async foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 25,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 25,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 25,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 24,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 15,
                                        end = 18,
                                        name = "foo"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 18,
                                        end = 24,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 21,
                                            end = 24,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("class A {static async foo() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 32,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 32,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 32,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 31,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 22,
                                        end = 25,
                                        name = "foo"
                                    },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 25,
                                        end = 31,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 28,
                                            end = 31,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // OK 'async' as a method name.
            Program.test("class A {async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 21,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 21,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 21,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 20,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 14,
                                        name = "async"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 14,
                                        end = 20,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 17,
                                            end = 20,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("class A {static async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 28,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 28,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 28,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 27,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 16,
                                        end = 21,
                                        name = "async"
                                    },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 21,
                                        end = 27,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 24,
                                            end = 27,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("class A {*async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 22,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 22,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 22,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 21,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 10,
                                        end = 15,
                                        name = "async"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 15,
                                        end = 21,
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 18,
                                            end = 21,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("class A {static* async() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 29,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 29,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 29,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 28,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 17,
                                        end = 22,
                                        name = "async"
                                    },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 22,
                                        end = 28,
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 25,
                                            end = 28,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // invalid syntax if there is a line-break after 'async'.
            Program.testFail("class A {async\nfoo() { }}", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static async\nfoo() { }}", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot combine with constructors/getters/setters/generators.
            Program.testFail("class A {async constructor() { }}", "Constructor can't be an async method (1:15)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async get foo() { }}", "Unexpected token (1:19)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async set foo(value) { }}", "Unexpected token (1:19)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async* foo() { }}", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static async get foo() { }}", "Unexpected token (1:26)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static async set foo(value) { }}", "Unexpected token (1:26)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static async* foo() { }}", "Unexpected token (1:21)", new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await' is valid as function names.
            Program.test("class A {async await() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 27,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 27,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 27,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 26,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 15,
                                        end = 20,
                                        name = "await"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 20,
                                        end = 26,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 23,
                                            end = 26,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("class A {static async await() { }}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 34,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 34,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            start = 8,
                            end = 34,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 33,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 22,
                                        end = 27,
                                        name = "await"
                                    },
                                    @static = true,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 27,
                                        end = 33,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 30,
                                            end = 33,
                                            body = new TestNode[0]
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // cannot use 'await' inside async functions.
            Program.test("async function wrap() {\nclass A {async await() { }}\n}", new TestNode(), new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async foo() { var await }}", "Can not use 'await' as identifier inside an async function (1:27)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async foo(await) { }}", "Can not use 'await' as identifier inside an async function (1:19)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {async foo() { return {await} }}", "Can not use 'await' as identifier inside an async function (1:31)", new TestOptions
            {
                ecmaVersion = 8
            });
            //-----------------------------------------------------------------------------
            // Await Expressions

            // 'await' is an identifier in scripts.
            Program.test("await", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 5,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 5,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 0,
                            end = 5,
                            name = "await"
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await' is a keyword in modules.
            Program.testFail("await", "Can not use keyword 'await' outside an async function (1:0)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            // Await expressions is invalid outside of async functions.
            Program.testFail("await a", "Unexpected token (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("await a", "Can not use keyword 'await' outside an async function (1:0)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            // Await expressions in async functions.
            Program.test("async function foo(a, b) { await a }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 36,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 36,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 19,
                                end = 20,
                                name = "a"
                            },
                            new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 22,
                                end = 23,
                                name = "b"
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 25,
                            end = 36,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 27,
                                    end = 34,
                                    expression = new TestNode
                                    {
                                        type = typeof(AwaitExpressionNode),
                                        start = 27,
                                        end = 34,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 33,
                                            end = 34,
                                            name = "a"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("(async function foo(a) { await a })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 35,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 35,
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            start = 1,
                            end = 34,
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 16,
                                end = 19,
                                name = "foo"
                            },
                            generator = false,
                            expression = false,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 20,
                                    end = 21,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 23,
                                end = 34,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        start = 25,
                                        end = 32,
                                        expression = new TestNode
                                        {
                                            type = typeof(AwaitExpressionNode),
                                            start = 25,
                                            end = 32,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 31,
                                                end = 32,
                                                name = "a"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("(async (a) => await a)", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 22,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 22,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 1,
                            end = 21,
                            id = null,
                            generator = false,
                            expression = true,
                            async = true,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 8,
                                    end = 9,
                                    name = "a"
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(AwaitExpressionNode),
                                start = 14,
                                end = 21,
                                argument = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 20,
                                    end = 21,
                                    name = "a"
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("({async foo(a) { await a }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 28,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 28,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 27,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 26,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 8,
                                        end = 11,
                                        name = "foo"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 11,
                                        end = 26,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 12,
                                                end = 13,
                                                name = "a"
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 15,
                                            end = 26,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 17,
                                                    end = 24,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(AwaitExpressionNode),
                                                        start = 17,
                                                        end = 24,
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 23,
                                                            end = 24,
                                                            name = "a"
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
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("(class {async foo(a) { await a }})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 34,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 34,
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            start = 1,
                            end = 33,
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                start = 7,
                                end = 33,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        start = 8,
                                        end = 32,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 14,
                                            end = 17,
                                            name = "foo"
                                        },
                                        @static = false,
                                        kind = PropertyKind.Method,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 17,
                                            end = 32,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 18,
                                                    end = 19,
                                                    name = "a"
                                                }
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 21,
                                                end = 32,
                                                body = new[]
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        start = 23,
                                                        end = 30,
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AwaitExpressionNode),
                                                            start = 23,
                                                            end = 30,
                                                            argument = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 29,
                                                                end = 30,
                                                                name = "a"
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
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // Await expressions are an unary expression.
            Program.test("async function foo(a, b) { await a + await b }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 46,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 46,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 19,
                                end = 20,
                                name = "a"
                            },
                            new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 22,
                                end = 23,
                                name = "b"
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 25,
                            end = 46,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 27,
                                    end = 44,
                                    expression = new TestNode
                                    {
                                        type = typeof(BinaryExpressionNode),
                                        start = 27,
                                        end = 44,
                                        left = new TestNode
                                        {
                                            type = typeof(AwaitExpressionNode),
                                            start = 27,
                                            end = 34,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 33,
                                                end = 34,
                                                name = "a"
                                            }
                                        },
                                        @operator = "+",
                                        right = new TestNode
                                        {
                                            type = typeof(AwaitExpressionNode),
                                            start = 37,
                                            end = 44,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 43,
                                                end = 44,
                                                name = "b"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await + 1' is a binary expression outside of async functions.
            Program.test("function foo() { await + 1 }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 28,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 28,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 15,
                            end = 28,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 17,
                                    end = 26,
                                    expression = new TestNode
                                    {
                                        type = typeof(BinaryExpressionNode),
                                        start = 17,
                                        end = 26,
                                        left = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 17,
                                            end = 22,
                                            name = "await"
                                        },
                                        @operator = "+",
                                        right = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 25,
                                            end = 26,
                                            value = 1,
                                            raw = "1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // 'await + 1' is an await expression in async functions.
            Program.test("async function foo() { await + 1 }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 34,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 34,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 21,
                            end = 34,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 23,
                                    end = 32,
                                    expression = new TestNode
                                    {
                                        type = typeof(AwaitExpressionNode),
                                        start = 23,
                                        end = 32,
                                        argument = new TestNode
                                        {
                                            type = typeof(UnaryExpressionNode),
                                            start = 29,
                                            end = 32,
                                            @operator = "+",
                                            prefix = true,
                                            argument = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                start = 31,
                                                end = 32,
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
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // Await expressions need one argument.
            Program.testFail("async function foo() { await }", "Unexpected token (1:29)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(async function foo() { await })", "Unexpected token (1:30)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async () => await", "Unexpected token (1:17)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async foo() { await }})", "Unexpected token (1:22)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {async foo() { await }})", "Unexpected token (1:28)", new TestOptions
            {
                ecmaVersion = 8
            });

            // Forbid await expressions in default parameters:
            Program.testFail("async function foo(a = await b) {}", "Await expression cannot be a default value (1:23)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(async function foo(a = await b) {})", "Await expression cannot be a default value (1:24)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async (a = await b) => {}", "Unexpected token (1:17)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function wrapper() {\nasync (a = await b) => {}\n}", "Await expression cannot be a default value (2:11)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({async foo(a = await b) {}})", "Await expression cannot be a default value (1:16)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {async foo(a = await b) {}})", "Await expression cannot be a default value (1:22)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function foo(a = class extends (await b) {}) {}", "Await expression cannot be a default value (1:38)", new TestOptions
            {
                ecmaVersion = 8
            });

            // Allow await expressions inside functions in default parameters:
            Program.test("async function foo(a = async function foo() { await b }) {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 59,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 59,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                start = 19,
                                end = 55,
                                left = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 19,
                                    end = 20,
                                    name = "a"
                                },
                                right = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    start = 23,
                                    end = 55,
                                    id = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 38,
                                        end = 41,
                                        name = "foo"
                                    },
                                    generator = false,
                                    expression = false,
                                    async = true,
                                    @params = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        start = 44,
                                        end = 55,
                                        body = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                start = 46,
                                                end = 53,
                                                expression = new TestNode
                                                {
                                                    type = typeof(AwaitExpressionNode),
                                                    start = 46,
                                                    end = 53,
                                                    argument = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 52,
                                                        end = 53,
                                                        name = "b"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 57,
                            end = 59,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async function foo(a = async () => await b) {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 46,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 46,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                start = 19,
                                end = 42,
                                left = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 19,
                                    end = 20,
                                    name = "a"
                                },
                                right = new TestNode
                                {
                                    type = typeof(ArrowFunctionExpressionNode),
                                    start = 23,
                                    end = 42,
                                    id = null,
                                    generator = false,
                                    expression = true,
                                    async = true,
                                    @params = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(AwaitExpressionNode),
                                        start = 35,
                                        end = 42,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 41,
                                            end = 42,
                                            name = "b"
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 44,
                            end = 46,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async function foo(a = {async bar() { await b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 52,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 52,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                start = 19,
                                end = 48,
                                left = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 19,
                                    end = 20,
                                    name = "a"
                                },
                                right = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 23,
                                    end = 48,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 24,
                                            end = 47,
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 30,
                                                end = 33,
                                                name = "bar"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(FunctionExpressionNode),
                                                start = 33,
                                                end = 47,
                                                id = null,
                                                generator = false,
                                                expression = false,
                                                async = true,
                                                @params = new TestNode[0],
                                                body = new TestNode
                                                {
                                                    type = typeof(BlockStatementNode),
                                                    start = 36,
                                                    end = 47,
                                                    body = new[]
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(ExpressionStatementNode),
                                                            start = 38,
                                                            end = 45,
                                                            expression = new TestNode
                                                            {
                                                                type = typeof(AwaitExpressionNode),
                                                                start = 38,
                                                                end = 45,
                                                                argument = new TestNode
                                                                {
                                                                    type = typeof(IdentifierNode),
                                                                    start = 44,
                                                                    end = 45,
                                                                    name = "b"
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
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 50,
                            end = 52,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.test("async function foo(a = class {async bar() { await b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 58,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 58,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 18,
                            name = "foo"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                start = 19,
                                end = 54,
                                left = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 19,
                                    end = 20,
                                    name = "a"
                                },
                                right = new TestNode
                                {
                                    type = typeof(ClassExpressionNode),
                                    start = 23,
                                    end = 54,
                                    id = null,
                                    superClass = null,
                                    body = new TestNode
                                    {
                                        type = typeof(ClassBodyNode),
                                        start = 29,
                                        end = 54,
                                        body = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(MethodDefinitionNode),
                                                start = 30,
                                                end = 53,
                                                computed = false,
                                                key = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 36,
                                                    end = 39,
                                                    name = "bar"
                                                },
                                                @static = false,
                                                kind = PropertyKind.Method,
                                                value = new TestNode
                                                {
                                                    type = typeof(FunctionExpressionNode),
                                                    start = 39,
                                                    end = 53,
                                                    id = null,
                                                    generator = false,
                                                    expression = false,
                                                    async = true,
                                                    @params = new TestNode[0],
                                                    body = new TestNode
                                                    {
                                                        type = typeof(BlockStatementNode),
                                                        start = 42,
                                                        end = 53,
                                                        body = new[]
                                                        {
                                                            new TestNode
                                                            {
                                                                type = typeof(ExpressionStatementNode),
                                                                start = 44,
                                                                end = 51,
                                                                expression = new TestNode
                                                                {
                                                                    type = typeof(AwaitExpressionNode),
                                                                    start = 44,
                                                                    end = 51,
                                                                    argument = new TestNode
                                                                    {
                                                                        type = typeof(IdentifierNode),
                                                                        start = 50,
                                                                        end = 51,
                                                                        name = "b"
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
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 56,
                            end = 58,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Program.test("async function wrap() {\n(a = await b)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 39,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 39,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 19,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 22,
                            end = 39,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 24,
                                    end = 37,
                                    expression = new TestNode
                                    {
                                        type = typeof(AssignmentExpressionNode),
                                        start = 25,
                                        end = 36,
                                        @operator = "=",
                                        left = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 25,
                                            end = 26,
                                            name = "a"
                                        },
                                        right = new TestNode
                                        {
                                            type = typeof(AwaitExpressionNode),
                                            start = 29,
                                            end = 36,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 35,
                                                end = 36,
                                                name = "b"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function wrap() {\n(a = await b) => a\n}", "Await expression cannot be a default value (2:5)", new TestOptions
            {
                ecmaVersion = 8
            });

            Program.test("async function wrap() {\n({a = await b} = obj)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 47,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 47,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 19,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 22,
                            end = 47,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 24,
                                    end = 45,
                                    expression = new TestNode
                                    {
                                        type = typeof(AssignmentExpressionNode),
                                        start = 25,
                                        end = 44,
                                        @operator = "=",
                                        left = new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            start = 25,
                                            end = 38,
                                            properties = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 26,
                                                    end = 37,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 26,
                                                        end = 27,
                                                        name = "a"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 26,
                                                        end = 37,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 26,
                                                            end = 27,
                                                            name = "a"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(AwaitExpressionNode),
                                                            start = 30,
                                                            end = 37,
                                                            argument = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 36,
                                                                end = 37,
                                                                name = "b"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 41,
                                            end = 44,
                                            name = "obj"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async function wrap() {\n({a = await b} = obj) => a\n}", "Await expression cannot be a default value (2:6)", new TestOptions
            {
                ecmaVersion = 8
            });

            Program.test("function* wrap() {\nasync(a = yield b)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 39,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 39,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 10,
                            end = 14,
                            name = "wrap"
                        },
                        @params = new TestNode[0],
                        generator = true,
                        expression = false,
                        async = false,
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 17,
                            end = 39,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 19,
                                    end = 37,
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        start = 19,
                                        end = 37,
                                        callee = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 19,
                                            end = 24,
                                            name = "async"
                                        },
                                        arguments = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(AssignmentExpressionNode),
                                                start = 25,
                                                end = 36,
                                                @operator = "=",
                                                left = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 25,
                                                    end = 26,
                                                    name = "a"
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(YieldExpressionNode),
                                                    start = 29,
                                                    end = 36,
                                                    @delegate = false,
                                                    argument = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 35,
                                                        end = 36,
                                                        name = "b"
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
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("function* wrap() {\nasync(a = yield b) => a\n}", "Yield expression cannot be a default value (2:10)", new TestOptions
            {
                ecmaVersion = 8
            });

            // https://github.com/acornjs/acorn/issues/464
            Program.test("f = ({ w = counter(), x = counter(), y = counter(), z = counter() } = { w: null, x: 0, y: false, z: '' }) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 111,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 111,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 111,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 1,
                                name = "f"
                            },
                            right = new TestNode
                            {
                                type = typeof(ArrowFunctionExpressionNode),
                                start = 4,
                                end = 111,
                                id = null,
                                @params = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(AssignmentPatternNode),
                                        start = 5,
                                        end = 104,
                                        left = new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            start = 5,
                                            end = 67,
                                            properties = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 7,
                                                    end = 20,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 7,
                                                        end = 8,
                                                        name = "w"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 7,
                                                        end = 20,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 7,
                                                            end = 8,
                                                            name = "w"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            start = 11,
                                                            end = 20,
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 11,
                                                                end = 18,
                                                                name = "counter"
                                                            },
                                                            arguments = new TestNode[0]
                                                        }
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 22,
                                                    end = 35,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 22,
                                                        end = 23,
                                                        name = "x"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 22,
                                                        end = 35,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 22,
                                                            end = 23,
                                                            name = "x"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            start = 26,
                                                            end = 35,
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 26,
                                                                end = 33,
                                                                name = "counter"
                                                            },
                                                            arguments = new TestNode[0]
                                                        }
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 37,
                                                    end = 50,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 37,
                                                        end = 38,
                                                        name = "y"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 37,
                                                        end = 50,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 37,
                                                            end = 38,
                                                            name = "y"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            start = 41,
                                                            end = 50,
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 41,
                                                                end = 48,
                                                                name = "counter"
                                                            },
                                                            arguments = new TestNode[0]
                                                        }
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 52,
                                                    end = 65,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 52,
                                                        end = 53,
                                                        name = "z"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 52,
                                                        end = 65,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 52,
                                                            end = 53,
                                                            name = "z"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            start = 56,
                                                            end = 65,
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 56,
                                                                end = 63,
                                                                name = "counter"
                                                            },
                                                            arguments = new TestNode[0]
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new TestNode
                                        {
                                            type = typeof(ObjectExpressionNode),
                                            start = 70,
                                            end = 104,
                                            properties = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 72,
                                                    end = 79,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 72,
                                                        end = 73,
                                                        name = "w"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 75,
                                                        end = 79,
                                                        value = null,
                                                        raw = "null"
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 81,
                                                    end = 85,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 81,
                                                        end = 82,
                                                        name = "x"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 84,
                                                        end = 85,
                                                        value = 0,
                                                        raw = "0"
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 87,
                                                    end = 95,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 87,
                                                        end = 88,
                                                        name = "y"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 90,
                                                        end = 95,
                                                        value = false,
                                                        raw = "false"
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 97,
                                                    end = 102,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 97,
                                                        end = 98,
                                                        name = "z"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 100,
                                                        end = 102,
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
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 109,
                                    end = 111,
                                    body = new TestNode[0]
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            Program.test("({ async: true })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        name = "async"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = true
                                    },
                                    kind = PropertyKind.Init,
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Program.testFail("if (x) async function f() {}", "Unexpected token (1:7)", new TestOptions
            {
                ecmaVersion = 8
            });

            Program.testFail("(async)(a) => 12", "Unexpected token (1:11)", new TestOptions
            {
                ecmaVersion = 8
            });

            Program.testFail("f = async ((x)) => x", "Parenthesized pattern (1:11)", new TestOptions
            {
                ecmaVersion = 8
            });

            // allow 'async' as a shorthand property in script.
            Program.test(
                "({async})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 9,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 9,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 8,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 7,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 7,
                                            name = "async"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 7,
                                            name = "async"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );
            Program.test(
                "({async, foo})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 14,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 14,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 13,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 7,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 7,
                                            name = "async"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 7,
                                            name = "async"
                                        }
                                    },
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 9,
                                        end = 12,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 9,
                                            end = 12,
                                            name = "foo"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 9,
                                            end = 12,
                                            name = "foo"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );
            Program.test(
                "({async = 0} = {})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 18,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 18,
                            expression = new TestNode
                            {
                                type = typeof(AssignmentExpressionNode),
                                start = 1,
                                end = 17,
                                @operator = "=",
                                left = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 1,
                                    end = 12,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 2,
                                            end = 11,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 2,
                                                end = 7,
                                                name = "async"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                start = 2,
                                                end = 11,
                                                left = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 2,
                                                    end = 7,
                                                    name = "async"
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    start = 10,
                                                    end = 11,
                                                    value = 0,
                                                    raw = "0"
                                                }
                                            }
                                        }
                                    }
                                },
                                right = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 15,
                                    end = 17,
                                    properties = new TestNode[0]
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );

            // async functions with vary names.
            Program.test(
                "({async \"foo\"(){}})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 19,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 19,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 18,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 17,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 8,
                                            end = 13,
                                            value = "foo",
                                            raw = "\"foo\""
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 13,
                                            end = 17,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 15,
                                                end = 17,
                                                body = new TestNode[0]
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );
            Program.test(
                "({async 'foo'(){}})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 19,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 19,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 18,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 17,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 8,
                                            end = 13,
                                            value = "foo",
                                            raw = "'foo'"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 13,
                                            end = 17,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 15,
                                                end = 17,
                                                body = new TestNode[0]
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );
            Program.test(
                "({async 100(){}})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 17,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 17,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 16,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 15,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 8,
                                            end = 11,
                                            value = 100,
                                            raw = "100"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 11,
                                            end = 15,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 13,
                                                end = 15,
                                                body = new TestNode[0]
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );
            Program.test(
                "({async [foo](){}})",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    start = 0,
                    end = 19,
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            start = 0,
                            end = 19,
                            expression = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 1,
                                end = 18,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 17,
                                        method = true,
                                        shorthand = false,
                                        computed = true,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 9,
                                            end = 12,
                                            name = "foo"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 13,
                                            end = 17,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 15,
                                                end = 17,
                                                body = new TestNode[0]
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    sourceType = SourceType.Script
                },
                new TestOptions
                {
                    ecmaVersion = 8
                }
            );

            Program.test("({ async delete() {} })", new TestNode(), new TestOptions
            {
                ecmaVersion = 8
            });

            Program.testFail("abc: async function a() {}", "Invalid labeled declaration (1:5)", new TestOptions
            {
                ecmaVersion = 8
            });
        }
    }
}
