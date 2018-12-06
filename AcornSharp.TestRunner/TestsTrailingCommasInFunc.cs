using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsTrailingCommasInFunc
    {
        public static void Run()
        {
            //------------------------------------------------------------------------------
            // allow

            Program.test("function foo(a,) { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 20,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 13,
                                end = 14,
                                name = "a"
                            }
                        },
                        generator = false,
                        expression = false,
                        async = false,
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 17,
                            end = 20,
                            body = new TestNode[0]
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 8
            });

            Program.test("(function(a,) { })", new TestNode
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
                            type = typeof(FunctionExpressionNode),
                            start = 1,
                            end = 17,
                            id = null,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 10,
                                    end = 11,
                                    name = "a"
                                }
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 14,
                                end = 17,
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

            Program.test("(a,) => a", new TestNode
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
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 9,
                            id = null,
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
                            generator = false,
                            expression = true,
                            async = false,
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 8,
                                end = 9,
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

            Program.test("async (a,) => a", new TestNode
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
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 15,
                            id = null,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 7,
                                    end = 8,
                                    name = "a"
                                }
                            },
                            generator = false,
                            expression = true,
                            async = true,
                            body = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 14,
                                end = 15,
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

            Program.test("({foo(a,) {}})", new TestNode
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
                                    end = 12,
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
                                        end = 12,
                                        id = null,
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
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 10,
                                            end = 12,
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

            Program.test("class A {foo(a,) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        start = 0,
                        end = 20,
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
                            end = 20,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    start = 9,
                                    end = 19,
                                    @static = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 12,
                                        name = "foo"
                                    },
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 12,
                                        end = 19,
                                        id = null,
                                        @params = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 13,
                                                end = 14,
                                                name = "a"
                                            }
                                        },
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 17,
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

            Program.test("class A {static foo(a,) {}}", new TestNode
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
                                    @static = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 16,
                                        end = 19,
                                        name = "foo"
                                    },
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 19,
                                        end = 26,
                                        id = null,
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
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 24,
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

            Program.test("(class {foo(a,) {}})", new TestNode
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
                            type = typeof(ClassExpressionNode),
                            start = 1,
                            end = 19,
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                start = 7,
                                end = 19,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        start = 8,
                                        end = 18,
                                        @static = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 8,
                                            end = 11,
                                            name = "foo"
                                        },
                                        kind = PropertyKind.Method,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 11,
                                            end = 18,
                                            id = null,
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
                                            generator = false,
                                            expression = false,
                                            async = false,
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 16,
                                                end = 18,
                                                body = new TestNode[0]
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

            Program.test("(class {static foo(a,) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 27,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 27,
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            start = 1,
                            end = 26,
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                start = 7,
                                end = 26,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        start = 8,
                                        end = 25,
                                        @static = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 15,
                                            end = 18,
                                            name = "foo"
                                        },
                                        kind = PropertyKind.Method,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 18,
                                            end = 25,
                                            id = null,
                                            @params = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 19,
                                                    end = 20,
                                                    name = "a"
                                                }
                                            },
                                            generator = false,
                                            expression = false,
                                            async = false,
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 23,
                                                end = 25,
                                                body = new TestNode[0]
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

            Program.test("export default function foo(a,) { }", new TestNode
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
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 24,
                                end = 27,
                                name = "foo"
                            },
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 28,
                                    end = 29,
                                    name = "a"
                                }
                            },
                            generator = false,
                            expression = false,
                            async = false,
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

            Program.test("export default (function foo(a,) { })", new TestNode
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
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 25,
                                end = 28,
                                name = "foo"
                            },
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 29,
                                    end = 30,
                                    name = "a"
                                }
                            },
                            generator = false,
                            expression = false,
                            async = false,
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

            Program.test("export function foo(a,) { }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 27,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        start = 0,
                        end = 27,
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            start = 7,
                            end = 27,
                            id = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 16,
                                end = 19,
                                name = "foo"
                            },
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
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 24,
                                end = 27,
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

            Program.test("foo(a,)", new TestNode
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
                            type = typeof(CallExpressionNode),
                            start = 0,
                            end = 7,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 4,
                                    end = 5,
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

            Program.test("new foo(a,)", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 11,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 11,
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            start = 0,
                            end = 11,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 4,
                                end = 7,
                                name = "foo"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 8,
                                    end = 9,
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

            Program.test("foo(...a,)", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 10,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 10,
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            start = 0,
                            end = 10,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 4,
                                    end = 8,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 7,
                                        end = 8,
                                        name = "a"
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

            Program.test("new foo(...a,)", new TestNode
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
                            type = typeof(NewExpressionNode),
                            start = 0,
                            end = 14,
                            callee = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 4,
                                end = 7,
                                name = "foo"
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 8,
                                    end = 12,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 11,
                                        end = 12,
                                        name = "a"
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

            //------------------------------------------------------------------------------
            // disallow in {ecmaVersion = 7}

            Program.testFail("function foo(a,) { }", "Unexpected token (1:15)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(function(a,) { })", "Unexpected token (1:12)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(a,) => a", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("async (a,) => a", "Unexpected token (1:9)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("({foo(a,) {}})", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("class A {foo(a,) {}}", "Unexpected token (1:15)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("class A {static foo(a,) {}}", "Unexpected token (1:22)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(class {foo(a,) {}})", "Unexpected token (1:14)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(class {static foo(a,) {}})", "Unexpected token (1:21)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("export default function foo(a,) { }", "Unexpected token (1:30)", new TestOptions
            {
                ecmaVersion = 7,
                sourceType = SourceType.Module
            });
            Program.testFail("export default (function foo(a,) { })", "Unexpected token (1:31)", new TestOptions
            {
                ecmaVersion = 7,
                sourceType = SourceType.Module
            });
            Program.testFail("export function foo(a,) { }", "Unexpected token (1:22)", new TestOptions
            {
                ecmaVersion = 7,
                sourceType = SourceType.Module
            });
            Program.testFail("foo(a,)", "Unexpected token (1:6)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("new foo(a,)", "Unexpected token (1:10)", new TestOptions
            {
                ecmaVersion = 7
            });

            //------------------------------------------------------------------------------
            // disallow after rest parameters

            Program.testFail("function foo(...a,) { }", "Comma is not permitted after the rest element (1:17)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(function(...a,) { })", "Comma is not permitted after the rest element (1:14)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(...a,) => a", "Comma is not permitted after the rest element (1:5)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async (...a,) => a", "Comma is not permitted after the rest element (1:11)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({foo(...a,) {}})", "Comma is not permitted after the rest element (1:10)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {foo(...a,) {}}", "Comma is not permitted after the rest element (1:17)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static foo(...a,) {}}", "Comma is not permitted after the rest element (1:24)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {foo(...a,) {}})", "Comma is not permitted after the rest element (1:16)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {static foo(...a,) {}})", "Comma is not permitted after the rest element (1:23)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("export default function foo(...a,) { }", "Comma is not permitted after the rest element (1:32)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });
            Program.testFail("export default (function foo(...a,) { })", "Comma is not permitted after the rest element (1:33)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });
            Program.testFail("export function foo(...a,) { }", "Comma is not permitted after the rest element (1:24)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            //------------------------------------------------------------------------------
            // disallow empty

            Program.testFail("function foo(,) { }", "Unexpected token (1:13)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(function(,) { })", "Unexpected token (1:10)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(,) => a", "Unexpected token (1:1)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("async (,) => a", "Unexpected token (1:7)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({foo(,) {}})", "Unexpected token (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {foo(,) {}}", "Unexpected token (1:13)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("class A {static foo(,) {}}", "Unexpected token (1:20)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {foo(,) {}})", "Unexpected token (1:12)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("(class {static foo(,) {}})", "Unexpected token (1:19)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("export default function foo(,) { }", "Unexpected token (1:28)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });
            Program.testFail("export default (function foo(,) { })", "Unexpected token (1:29)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });
            Program.testFail("export function foo(,) { }", "Unexpected token (1:20)", new TestOptions
            {
                ecmaVersion = 8,
                sourceType = SourceType.Module
            });

            //------------------------------------------------------------------------------
            // disallow in parens without arrow

            Program.testFail("(a,)", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(a,)", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 8
            });
        }
    }
}
