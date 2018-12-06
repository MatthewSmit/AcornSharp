using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal class TestsAsyncIteration
    {
        public static void Run()
        {
            Program.test("async function f() { for await (x of xs); }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 43,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 43,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 16,
                            name = "f"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 19,
                            end = 43,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ForOfStatementNode),
                                    start = 21,
                                    end = 41,
                                    await = true,
                                    left = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 32,
                                        end = 33,
                                        name = "x"
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 37,
                                        end = 39,
                                        name = "xs"
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(EmptyStatementNode),
                                        start = 40,
                                        end = 41
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("async function f() { for await (var x of xs); }", new TestNode
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
                            end = 16,
                            name = "f"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 19,
                            end = 47,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ForOfStatementNode),
                                    start = 21,
                                    end = 45,
                                    await = true,
                                    left = new TestNode
                                    {
                                        type = typeof(VariableDeclarationNode),
                                        start = 32,
                                        end = 37,
                                        declarations = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(VariableDeclaratorNode),
                                                start = 36,
                                                end = 37,
                                                id = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 36,
                                                    end = 37,
                                                    name = "x"
                                                },
                                                init = null
                                            }
                                        },
                                        kind = PropertyKind.Var
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 41,
                                        end = 43,
                                        name = "xs"
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(EmptyStatementNode),
                                        start = 44,
                                        end = 45
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("async function f() { for await (let x of xs); }", new TestNode
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
                            end = 16,
                            name = "f"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 19,
                            end = 47,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ForOfStatementNode),
                                    start = 21,
                                    end = 45,
                                    await = true,
                                    left = new TestNode
                                    {
                                        type = typeof(VariableDeclarationNode),
                                        start = 32,
                                        end = 37,
                                        declarations = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(VariableDeclaratorNode),
                                                start = 36,
                                                end = 37,
                                                id = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 36,
                                                    end = 37,
                                                    name = "x"
                                                },
                                                init = null
                                            }
                                        },
                                        kind = PropertyKind.Let
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 41,
                                        end = 43,
                                        name = "xs"
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(EmptyStatementNode),
                                        start = 44,
                                        end = 45
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("async function f() { for\nawait (x of xs); }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 43,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 43,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 15,
                            end = 16,
                            name = "f"
                        },
                        generator = false,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 19,
                            end = 43,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ForOfStatementNode),
                                    start = 21,
                                    end = 41,
                                    await = true,
                                    left = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 32,
                                        end = 33,
                                        name = "x"
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 37,
                                        end = 39,
                                        name = "xs"
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(EmptyStatementNode),
                                        start = 40,
                                        end = 41
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("f = async function() { for await (x of xs); }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 45,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 45,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 45,
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
                                type = typeof(FunctionExpressionNode),
                                start = 4,
                                end = 45,
                                id = null,
                                generator = false,
                                expression = false,
                                async = true,
                                @params = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 21,
                                    end = 45,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ForOfStatementNode),
                                            start = 23,
                                            end = 43,
                                            await = true,
                                            left = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 34,
                                                end = 35,
                                                name = "x"
                                            },
                                            right = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 39,
                                                end = 41,
                                                name = "xs"
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(EmptyStatementNode),
                                                start = 42,
                                                end = 43
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
                ecmaVersion = 9
            });
            Program.test("f = async() => { for await (x of xs); }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 39,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 39,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 39,
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
                                end = 39,
                                id = null,
                                generator = false,
                                expression = false,
                                async = true,
                                @params = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 15,
                                    end = 39,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ForOfStatementNode),
                                            start = 17,
                                            end = 37,
                                            await = true,
                                            left = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 28,
                                                end = 29,
                                                name = "x"
                                            },
                                            right = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 33,
                                                end = 35,
                                                name = "xs"
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(EmptyStatementNode),
                                                start = 36,
                                                end = 37
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
                ecmaVersion = 9
            });
            Program.test("obj = { async f() { for await (x of xs); } }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 44,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 44,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 44,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "obj"
                            },
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 6,
                                end = 44,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 8,
                                        end = 42,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 14,
                                            end = 15,
                                            name = "f"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 15,
                                            end = 42,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 18,
                                                end = 42,
                                                body = new[]
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ForOfStatementNode),
                                                        start = 20,
                                                        end = 40,
                                                        await = true,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 31,
                                                            end = 32,
                                                            name = "x"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 36,
                                                            end = 38,
                                                            name = "xs"
                                                        },
                                                        body = new TestNode
                                                        {
                                                            type = typeof(EmptyStatementNode),
                                                            start = 39,
                                                            end = 40
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
                ecmaVersion = 9
            });
            Program.test("class A { async f() { for await (x of xs); } }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 46,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 46,
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
                            end = 46,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 10,
                                    end = 44,
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 16,
                                        end = 17,
                                        name = "f"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 17,
                                        end = 44,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 20,
                                            end = 44,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ForOfStatementNode),
                                                    start = 22,
                                                    end = 42,
                                                    await = true,
                                                    left = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 33,
                                                        end = 34,
                                                        name = "x"
                                                    },
                                                    right = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 38,
                                                        end = 40,
                                                        name = "xs"
                                                    },
                                                    body = new TestNode
                                                    {
                                                        type = typeof(EmptyStatementNode),
                                                        start = 41,
                                                        end = 42
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
                ecmaVersion = 9
            });

// ForOfStatement has `await:false` in `ecmaVersion = 9`
            Program.test("for (x of xs);", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 14,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        start = 0,
                        end = 14,
                        await = false,
                        left = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 5,
                            end = 6,
                            name = "x"
                        },
                        right = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 10,
                            end = 12,
                            name = "xs"
                        },
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            start = 13,
                            end = 14
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("for await (x of xs);", "Unexpected token (1:4)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("function f() { for await (x of xs); }", "Unexpected token (1:19)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("f = function() { for await (x of xs); }", "Unexpected token (1:21)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("f = () => { for await (x of xs); }", "Unexpected token (1:16)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { () => { for await (x of xs); } }", "Unexpected token (1:33)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { for await (x in xs); }", "Unexpected token (1:25)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { for await (;;); }", "Unexpected token (1:25)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { for await (x;;); }", "Unexpected token (1:25)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { for await (let x = 0;;); }", "Unexpected token (1:25)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function f() { for await (x of xs); }", "Unexpected token (1:25)", new TestOptions
            {
                ecmaVersion = 8
            });

//------------------------------------------------------------------------------
// FunctionDeclaration#await
//------------------------------------------------------------------------------

            Program.test("async function* f() { await a; yield b; }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 41,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 41,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 16,
                            end = 17,
                            name = "f"
                        },
                        generator = true,
                        expression = false,
                        async = true,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 20,
                            end = 41,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 22,
                                    end = 30,
                                    expression = new TestNode
                                    {
                                        type = typeof(AwaitExpressionNode),
                                        start = 22,
                                        end = 29,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 28,
                                            end = 29,
                                            name = "a"
                                        }
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 31,
                                    end = 39,
                                    expression = new TestNode
                                    {
                                        type = typeof(YieldExpressionNode),
                                        start = 31,
                                        end = 38,
                                        @delegate = false,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 37,
                                            end = 38,
                                            name = "b"
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
                ecmaVersion = 9
            });

            Program.testFail("async function* f() { () => await a; }", "Unexpected token (1:34)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("async function* f() { () => yield a; }", "Unexpected token (1:34)", new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("async function* f() { await a; yield b; }", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 8
            });

//------------------------------------------------------------------------------
// FunctionExpression#await
//------------------------------------------------------------------------------

            Program.test("f = async function*() { await a; yield b; }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 43,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 43,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 43,
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
                                type = typeof(FunctionExpressionNode),
                                start = 4,
                                end = 43,
                                id = null,
                                generator = true,
                                expression = false,
                                async = true,
                                @params = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 22,
                                    end = 43,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 24,
                                            end = 32,
                                            expression = new TestNode
                                            {
                                                type = typeof(AwaitExpressionNode),
                                                start = 24,
                                                end = 31,
                                                argument = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 30,
                                                    end = 31,
                                                    name = "a"
                                                }
                                            }
                                        },
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 33,
                                            end = 41,
                                            expression = new TestNode
                                            {
                                                type = typeof(YieldExpressionNode),
                                                start = 33,
                                                end = 40,
                                                @delegate = false,
                                                argument = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 39,
                                                    end = 40,
                                                    name = "b"
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
                ecmaVersion = 9
            });
            Program.test("obj = { async* f() { await a; yield b; } }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 42,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 42,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 42,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "obj"
                            },
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 6,
                                end = 42,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 8,
                                        end = 40,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 15,
                                            end = 16,
                                            name = "f"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 16,
                                            end = 40,
                                            id = null,
                                            generator = true,
                                            expression = false,
                                            async = true,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 19,
                                                end = 40,
                                                body = new[]
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        start = 21,
                                                        end = 29,
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AwaitExpressionNode),
                                                            start = 21,
                                                            end = 28,
                                                            argument = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 27,
                                                                end = 28,
                                                                name = "a"
                                                            }
                                                        }
                                                    },
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        start = 30,
                                                        end = 38,
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(YieldExpressionNode),
                                                            start = 30,
                                                            end = 37,
                                                            @delegate = false,
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
                ecmaVersion = 9
            });
            Program.test("class A { async* f() { await a; yield b; } }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 44,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 44,
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
                            end = 44,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 10,
                                    end = 42,
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 17,
                                        end = 18,
                                        name = "f"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 18,
                                        end = 42,
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 21,
                                            end = 42,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 23,
                                                    end = 31,
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
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 32,
                                                    end = 40,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(YieldExpressionNode),
                                                        start = 32,
                                                        end = 39,
                                                        @delegate = false,
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 38,
                                                            end = 39,
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
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("class A { static async* f() { await a; yield b; } }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 51,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 51,
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
                            end = 51,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 10,
                                    end = 49,
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 24,
                                        end = 25,
                                        name = "f"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 25,
                                        end = 49,
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        async = true,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 28,
                                            end = 49,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 30,
                                                    end = 38,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(AwaitExpressionNode),
                                                        start = 30,
                                                        end = 37,
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 36,
                                                            end = 37,
                                                            name = "a"
                                                        }
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 39,
                                                    end = 47,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(YieldExpressionNode),
                                                        start = 39,
                                                        end = 46,
                                                        @delegate = false,
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 45,
                                                            end = 46,
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
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("f = async function*() { () => await a; }", "Unexpected token (1:36)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("f = async function*() { () => yield a; }", "Unexpected token (1:36)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("obj = { async\n* f() {} }", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("obj = { *async f() {}", "Unexpected token (1:15)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("obj = { *async* f() {}", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("obj = { async* f() { () => await a; } }", "Unexpected token (1:33)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("obj = { async* f() { () => yield a; } }", "Unexpected token (1:33)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("class A { async\n* f() {} }", "Unexpected token (2:0)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("class A { *async f() {} }", "Unexpected token (1:17)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("class A { *async* f() {} }", "Unexpected token (1:16)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("class A { async* f() { () => await a; } }", "Unexpected token (1:35)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("class A { async* f() { () => yield a; } }", "Unexpected token (1:35)", new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("f = async function*() { await a; yield b; }", "Unexpected token (1:18)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("obj = { async* f() { await a; yield b; } }", "Unexpected token (1:13)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A { async* f() { await a; yield b; } }", "Unexpected token (1:15)", new TestOptions
            {
                ecmaVersion = 8
            });

//------------------------------------------------------------------------------
// From https://github.com/acornjs/acorn-async-iteration/blob/fc72be2928ed0ffd46041f8c19052a9a282602ea/test/test.js
//------------------------------------------------------------------------------

// Commented this test out because this should throw syntax error.
// for-await-of statements can be only in async functions.
//
// test("for await (const line of readLines(filePath)) {\n  console.log(line);\n}", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 70,
//   "body": [
//     {
//       "type": "ForOfStatement",
//       "start": 0,
//       "end": 70,
//       "await": true,
//       "left": {
//         "type": "VariableDeclaration",
//         "start": 11,
//         "end": 21,
//         "declarations": [
//           {
//             "type": "VariableDeclarator",
//             "start": 17,
//             "end": 21,
//             "id": {
//               "type": "Identifier",
//               "start": 17,
//               "end": 21,
//               "name": "line"
//             },
//             "init": null
//           }
//         ],
//         "kind": "const"
//       },
//       "right": {
//         "type": "CallExpression",
//         "start": 25,
//         "end": 44,
//         "callee": {
//           "type": "Identifier",
//           "start": 25,
//           "end": 34,
//           "name": "readLines"
//         },
//         "arguments": [
//           {
//             "type": "Identifier",
//             "start": 35,
//             "end": 43,
//             "name": "filePath"
//           }
//         ]
//       },
//       "body": {
//         "type": "BlockStatement",
//         "start": 46,
//         "end": 70,
//         "body": [
//           {
//             "type": "ExpressionStatement",
//             "start": 50,
//             "end": 68,
//             "expression": {
//               "type": "CallExpression",
//               "start": 50,
//               "end": 67,
//               "callee": {
//                 "type": "MemberExpression",
//                 "start": 50,
//                 "end": 61,
//                 "object": {
//                   "type": "Identifier",
//                   "start": 50,
//                   "end": 57,
//                   "name": "console"
//                 },
//                 "property": {
//                   "type": "Identifier",
//                   "start": 58,
//                   "end": 61,
//                   "name": "log"
//                 },
//                 "computed": false
//               },
//               "arguments": [
//                 {
//                   "type": "Identifier",
//                   "start": 62,
//                   "end": 66,
//                   "name": "line"
//                 }
//               ]
//             }
//           }
//         ]
//       }
//     }
//   ],
//   "sourceType": "script"
// }, new TestOptions { ecmaVersion = 9 })
            Program.test("async function* x() {}", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("ref = async function*() {}", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("(async function*() {})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("var gen = { async *method() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 32,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        start = 0,
                        end = 32,
                        declarations = new[]
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                start = 4,
                                end = 32,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 4,
                                    end = 7,
                                    name = "gen"
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 10,
                                    end = 32,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 12,
                                            end = 30,
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 19,
                                                end = 25,
                                                name = "method"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(FunctionExpressionNode),
                                                start = 25,
                                                end = 30,
                                                id = null,
                                                generator = true,
                                                expression = false,
                                                async = true,
                                                @params = new TestNode[0],
                                                body = new TestNode
                                                {
                                                    body = new TestNode[0],
                                                    end = 30,
                                                    start = 28,
                                                    type = typeof(BlockStatementNode)
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        kind = PropertyKind.Var
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("export default async function*() {}", new TestNode
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
                            generator = true,
                            expression = false,
                            async = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                body = new TestNode[0],
                                end = 35,
                                start = 33,
                                type = typeof(BlockStatementNode)
                            }
                        }
                    }
                },
                sourceType = SourceType.Module
            }, new TestOptions
            {
                ecmaVersion = 9,
                sourceType = SourceType.Module
            });
            Program.test("var C = class { async *method() {} }", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });

// Commented the tests of direct `super()` calls out because this is not unique matter in async iteration syntax.
// Class's constructor is the only place which allows `super()`.
// See also: https://github.com/acornjs/acorn/issues/448
//
// test("var C = class { static async *method() {} }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// testFail("async function* x() { super(); }", "", new TestOptions { ecmaVersion = 9 })
// testFail("ref = async function*() { super(); }", "", new TestOptions { ecmaVersion = 9 })
// testFail("(async function*() { super(); })", "", new TestOptions { ecmaVersion = 9 })
// testFail("var gen = { async *method() { super(); } }", "", new TestOptions { ecmaVersion = 9 })
// testFail("export default async function*() { super(); }", "", new TestNode { "ecmaVersion": 9, "sourceType": "module" })
// testFail("var C = class { async *method() { super(); } }", "", new TestOptions { ecmaVersion = 9 })
// testFail("var C = class { static async *method() { super(); } }", "", new TestOptions { ecmaVersion = 9 })
// test("async function* x() { var x = () => { super(); } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("ref = async function*() { var x = () => { super(); } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("(async function*() { var x = () => { super(); } })", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var gen = { async *method() { var x = () => { super(); } } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 60,
//   "body": [
//     {
//       "type": "VariableDeclaration",
//       "start": 0,
//       "end": 60,
//       "declarations": [
//         {
//           "type": "VariableDeclarator",
//           "start": 4,
//           "end": 60,
//           "id": {
//             "type": "Identifier",
//             "start": 4,
//             "end": 7,
//             "name": "gen"
//           },
//           "init": {
//             "type": "ObjectExpression",
//             "start": 10,
//             "end": 60,
//             "properties": [
//               {
//                 "type": "Property",
//                 "start": 12,
//                 "end": 58,
//                 "method": true,
//                 "shorthand": false,
//                 "computed": false,
//                 "key": {
//                   "type": "Identifier",
//                   "start": 19,
//                   "end": 25,
//                   "name": "method"
//                 },
//                 "kind": "init",
//                 "value": {
//                   "type": "FunctionExpression",
//                   "start": 25,
//                   "end": 58,
//                   "id": null,
//                   "generator": true,
//                   "expression": false,
//                   "async": true,
//                   "params": [],
//                   "body": {
//                     "end": 58,
//                     "start": 28,
//                     "type": "BlockStatement",
//                     "body": [
//                       {
//                         "type": "VariableDeclaration",
//                         "start": 30,
//                         "end": 56,
//                         "kind": "var",
//                         "declarations": [
//                           {
//                             "type": "VariableDeclarator",
//                             "start": 34,
//                             "end": 56,
//                             "id": {
//                               "type": "Identifier",
//                               "start": 34,
//                               "end": 35,
//                               "name": "x"
//                             },
//                             "init": {
//                               "type": "ArrowFunctionExpression",
//                               "start": 38,
//                               "end": 56,
//                               "id": null,
//                               "generator": false,
//                               "expression": false,
//                               "async": false,
//                               "params": [],
//                               "body": {
//                                 "type": "BlockStatement",
//                                 "start": 44,
//                                 "end": 56,
//                                 "body": [
//                                   {
//                                     "type": "ExpressionStatement",
//                                     "start": 46,
//                                     "end": 54,
//                                     "expression": {
//                                       "type": "CallExpression",
//                                       "start": 46,
//                                       "end": 53,
//                                       "callee": {
//                                         "type": "Super",
//                                         "start": 46,
//                                         "end": 51
//                                       },
//                                       "arguments": []
//                                     }
//                                   }
//                                 ]
//                               }
//                             }
//                           }
//                         ]
//                       }
//                     ]
//                   }
//                 }
//               }
//             ]
//           }
//         }
//       ],
//       "kind": "var"
//     }
//   ],
//   "sourceType": "script"
// }, new TestOptions { ecmaVersion = 9 })
// test("export default async function*() { var x = () => { super(); } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 63,
//   "body": [
//     {
//       "type": "ExportDefaultDeclaration",
//       "start": 0,
//       "end": 63,
//       "declaration": {
//         "type": "FunctionDeclaration",
//         "start": 15,
//         "end": 63,
//         "id": null,
//         "generator": true,
//         "expression": false,
//         "async": true,
//         "params": [],
//         "body": {
//           "end": 63,
//           "start": 33,
//           "type": "BlockStatement",
//           "body": [
//             {
//               "type": "VariableDeclaration",
//               "start": 35,
//               "end": 61,
//               "kind": "var",
//               "declarations": [
//                 {
//                   "type": "VariableDeclarator",
//                   "start": 39,
//                   "end": 61,
//                   "id": {
//                     "type": "Identifier",
//                     "start": 39,
//                     "end": 40,
//                     "name": "x"
//                   },
//                   "init": {
//                     "type": "ArrowFunctionExpression",
//                     "start": 43,
//                     "end": 61,
//                     "id": null,
//                     "generator": false,
//                     "expression": false,
//                     "async": false,
//                     "params": [],
//                     "body": {
//                       "type": "BlockStatement",
//                       "start": 49,
//                       "end": 61,
//                       "body": [
//                         {
//                           "type": "ExpressionStatement",
//                           "start": 51,
//                           "end": 59,
//                           "expression": {
//                             "type": "CallExpression",
//                             "start": 51,
//                             "end": 58,
//                             "callee": {
//                               "type": "Super",
//                               "start": 51,
//                               "end": 56
//                             },
//                             "arguments": []
//                           }
//                         }
//                       ]
//                     }
//                   }
//                 }
//               ]
//             }
//           ]
//         }
//       }
//     }
//   ],
//   "sourceType": "module"
// }, new TestNode { "ecmaVersion": 9, "sourceType": "module" })
// test("var C = class { async *method() { var x = () => { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var C = class { static async *method() { var x = () => { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("async function* x() { var x = function () { super(); } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("ref = async function*() { var x = function () { super(); } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("(async function*() { var x = function () { super(); } })", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var gen = { async *method() { var x = function () { super(); } } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 66,
//   "body": [
//     {
//       "type": "VariableDeclaration",
//       "start": 0,
//       "end": 66,
//       "declarations": [
//         {
//           "type": "VariableDeclarator",
//           "start": 4,
//           "end": 66,
//           "id": {
//             "type": "Identifier",
//             "start": 4,
//             "end": 7,
//             "name": "gen"
//           },
//           "init": {
//             "type": "ObjectExpression",
//             "start": 10,
//             "end": 66,
//             "properties": [
//               {
//                 "type": "Property",
//                 "start": 12,
//                 "end": 64,
//                 "method": true,
//                 "shorthand": false,
//                 "computed": false,
//                 "key": {
//                   "type": "Identifier",
//                   "start": 19,
//                   "end": 25,
//                   "name": "method"
//                 },
//                 "kind": "init",
//                 "value": {
//                   "type": "FunctionExpression",
//                   "start": 25,
//                   "end": 64,
//                   "id": null,
//                   "generator": true,
//                   "expression": false,
//                   "async": true,
//                   "params": [],
//                   "body": {
//                     "end": 64,
//                     "start": 28,
//                     "type": "BlockStatement",
//                     "body": [
//                       {
//                         "type": "VariableDeclaration",
//                         "start": 30,
//                         "end": 62,
//                         "kind": "var",
//                         "declarations": [
//                           {
//                             "type": "VariableDeclarator",
//                             "start": 34,
//                             "end": 62,
//                             "id": {
//                               "type": "Identifier",
//                               "start": 34,
//                               "end": 35,
//                               "name": "x"
//                             },
//                             "init": {
//                               "type": "FunctionExpression",
//                               "start": 38,
//                               "end": 62,
//                               "id": null,
//                               "generator": false,
//                               "expression": false,
//                               "async": false,
//                               "params": [],
//                               "body": {
//                                 "type": "BlockStatement",
//                                 "start": 50,
//                                 "end": 62,
//                                 "body": [
//                                   {
//                                     "type": "ExpressionStatement",
//                                     "start": 52,
//                                     "end": 60,
//                                     "expression": {
//                                       "type": "CallExpression",
//                                       "start": 52,
//                                       "end": 59,
//                                       "callee": {
//                                         "type": "Super",
//                                         "start": 52,
//                                         "end": 57
//                                       },
//                                       "arguments": []
//                                     }
//                                   }
//                                 ]
//                               }
//                             }
//                           }
//                         ]
//                       }
//                     ]
//                   }
//                 }
//               }
//             ]
//           }
//         }
//       ],
//       "kind": "var"
//     }
//   ],
//   "sourceType": "script"
// }, new TestOptions { ecmaVersion = 9 })
// test("export default async function*() { var x = function () { super(); } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 69,
//   "body": [
//     {
//       "type": "ExportDefaultDeclaration",
//       "start": 0,
//       "end": 69,
//       "declaration": {
//         "type": "FunctionDeclaration",
//         "start": 15,
//         "end": 69,
//         "id": null,
//         "generator": true,
//         "expression": false,
//         "async": true,
//         "params": [],
//         "body": {
//           "end": 69,
//           "start": 33,
//           "type": "BlockStatement",
//           "body": [
//             {
//               "type": "VariableDeclaration",
//               "start": 35,
//               "end": 67,
//               "kind": "var",
//               "declarations": [
//                 {
//                   "type": "VariableDeclarator",
//                   "start": 39,
//                   "end": 67,
//                   "id": {
//                     "type": "Identifier",
//                     "start": 39,
//                     "end": 40,
//                     "name": "x"
//                   },
//                   "init": {
//                     "type": "FunctionExpression",
//                     "start": 43,
//                     "end": 67,
//                     "id": null,
//                     "generator": false,
//                     "expression": false,
//                     "async": false,
//                     "params": [],
//                     "body": {
//                       "type": "BlockStatement",
//                       "start": 55,
//                       "end": 67,
//                       "body": [
//                         {
//                           "type": "ExpressionStatement",
//                           "start": 57,
//                           "end": 65,
//                           "expression": {
//                             "type": "CallExpression",
//                             "start": 57,
//                             "end": 64,
//                             "callee": {
//                               "type": "Super",
//                               "start": 57,
//                               "end": 62
//                             },
//                             "arguments": []
//                           }
//                         }
//                       ]
//                     }
//                   }
//                 }
//               ]
//             }
//           ]
//         }
//       }
//     }
//   ],
//   "sourceType": "module"
// }, new TestNode { "ecmaVersion": 9, "sourceType": "module" })
// test("var C = class { async *method() { var x = function () { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var C = class { static async *method() { var x = function () { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("async function* x() { var x = { y: function () { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("ref = async function*() { var x = { y: function () { super(); } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("(async function*() { var x = { y: function () { super(); } } })", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var gen = { async *method() { var x = { y: function () { super(); } } } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 73,
//   "body": [
//     {
//       "type": "VariableDeclaration",
//       "start": 0,
//       "end": 73,
//       "declarations": [
//         {
//           "type": "VariableDeclarator",
//           "start": 4,
//           "end": 73,
//           "id": {
//             "type": "Identifier",
//             "start": 4,
//             "end": 7,
//             "name": "gen"
//           },
//           "init": {
//             "type": "ObjectExpression",
//             "start": 10,
//             "end": 73,
//             "properties": [
//               {
//                 "type": "Property",
//                 "start": 12,
//                 "end": 71,
//                 "method": true,
//                 "shorthand": false,
//                 "computed": false,
//                 "key": {
//                   "type": "Identifier",
//                   "start": 19,
//                   "end": 25,
//                   "name": "method"
//                 },
//                 "kind": "init",
//                 "value": {
//                   "type": "FunctionExpression",
//                   "start": 25,
//                   "end": 71,
//                   "id": null,
//                   "generator": true,
//                   "expression": false,
//                   "async": true,
//                   "params": [],
//                   "body": {
//                     "type": "BlockStatement",
//                     "start": 28,
//                     "end": 71,
//                     "body": [
//                       {
//                         "type": "VariableDeclaration",
//                         "start": 30,
//                         "end": 69,
//                         "declarations": [
//                           {
//                             "type": "VariableDeclarator",
//                             "start": 34,
//                             "end": 69,
//                             "id": {
//                               "type": "Identifier",
//                               "start": 34,
//                               "end": 35,
//                               "name": "x"
//                             },
//                             "init": {
//                               "type": "ObjectExpression",
//                               "start": 38,
//                               "end": 69,
//                               "properties": [
//                                 {
//                                   "type": "Property",
//                                   "start": 40,
//                                   "end": 67,
//                                   "method": false,
//                                   "shorthand": false,
//                                   "computed": false,
//                                   "key": {
//                                     "type": "Identifier",
//                                     "start": 40,
//                                     "end": 41,
//                                     "name": "y"
//                                   },
//                                   "value": {
//                                     "type": "FunctionExpression",
//                                     "start": 43,
//                                     "end": 67,
//                                     "id": null,
//                                     "generator": false,
//                                     "expression": false,
//                                     "async": false,
//                                     "params": [],
//                                     "body": {
//                                       "type": "BlockStatement",
//                                       "start": 55,
//                                       "end": 67,
//                                       "body": [
//                                         {
//                                           "type": "ExpressionStatement",
//                                           "start": 57,
//                                           "end": 65,
//                                           "expression": {
//                                             "type": "CallExpression",
//                                             "start": 57,
//                                             "end": 64,
//                                             "callee": {
//                                               "type": "Super",
//                                               "start": 57,
//                                               "end": 62
//                                             },
//                                             "arguments": []
//                                           }
//                                         }
//                                       ]
//                                     }
//                                   },
//                                   "kind": "init"
//                                 }
//                               ]
//                             }
//                           }
//                         ],
//                         "kind": "var"
//                       }
//                     ]
//                   }
//                 }
//               }
//             ]
//           }
//         }
//       ],
//       "kind": "var"
//     }
//   ],
//   "sourceType": "script"
// }, new TestOptions { ecmaVersion = 9 })
// test("export default async function*() { var x = { y: function () { super(); } } }", new TestNode {
//   "type": "Program",
//   "start": 0,
//   "end": 76,
//   "body": [
//     {
//       "type": "ExportDefaultDeclaration",
//       "start": 0,
//       "end": 76,
//       "declaration": {
//         "type": "FunctionDeclaration",
//         "start": 15,
//         "end": 76,
//         "id": null,
//         "generator": true,
//         "expression": false,
//         "async": true,
//         "params": [],
//         "body": {
//           "type": "BlockStatement",
//           "start": 33,
//           "end": 76,
//           "body": [
//             {
//               "type": "VariableDeclaration",
//               "start": 35,
//               "end": 74,
//               "declarations": [
//                 {
//                   "type": "VariableDeclarator",
//                   "start": 39,
//                   "end": 74,
//                   "id": {
//                     "type": "Identifier",
//                     "start": 39,
//                     "end": 40,
//                     "name": "x"
//                   },
//                   "init": {
//                     "type": "ObjectExpression",
//                     "start": 43,
//                     "end": 74,
//                     "properties": [
//                       {
//                         "type": "Property",
//                         "start": 45,
//                         "end": 72,
//                         "method": false,
//                         "shorthand": false,
//                         "computed": false,
//                         "key": {
//                           "type": "Identifier",
//                           "start": 45,
//                           "end": 46,
//                           "name": "y"
//                         },
//                         "value": {
//                           "type": "FunctionExpression",
//                           "start": 48,
//                           "end": 72,
//                           "id": null,
//                           "generator": false,
//                           "expression": false,
//                           "async": false,
//                           "params": [],
//                           "body": {
//                             "type": "BlockStatement",
//                             "start": 60,
//                             "end": 72,
//                             "body": [
//                               {
//                                 "type": "ExpressionStatement",
//                                 "start": 62,
//                                 "end": 70,
//                                 "expression": {
//                                   "type": "CallExpression",
//                                   "start": 62,
//                                   "end": 69,
//                                   "callee": {
//                                     "type": "Super",
//                                     "start": 62,
//                                     "end": 67
//                                   },
//                                   "arguments": []
//                                 }
//                               }
//                             ]
//                           }
//                         },
//                         "kind": "init"
//                       }
//                     ]
//                   }
//                 }
//               ],
//               "kind": "var"
//             }
//           ]
//         }
//       }
//     }
//   ],
//   "sourceType": "module"
// }, new TestNode { "ecmaVersion": 9, "sourceType": "module" })
// test("var C = class { async *method() { var x = { y: function () { super(); } } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })
// test("var C = class { static async *method() { var x = { y: function () { super(); } } } }", new TestNode {}, new TestOptions { ecmaVersion = 9 })

            Program.test("({ async *method(){} })", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("({ \\u0061sync *method(){} })", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("void \\u0061sync function* f(){};", "Unexpected token (1:16)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("for ( ; false; ) async function* g() {}", "Unexpected token (1:17)", new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("({async() { }})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({async = 0} = {})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({async, foo})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({async})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({async: true})", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("({async\n    foo() { }})", "Unexpected token (2:4)", new TestOptions
            {
                ecmaVersion = 9
            });
        }
    }
}
