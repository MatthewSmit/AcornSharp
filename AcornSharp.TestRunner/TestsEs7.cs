using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsEs7
    {
        public static void Run()
        {
            Program.test("x **= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = "**=",
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                name = "x",
                                loc = new TestNode
                                {
                                    start = new TestNode
                                    {
                                        line = 1,
                                        column = 0
                                    },
                                    end = new TestNode
                                    {
                                        line = 1,
                                        column = 1
                                    }
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                loc = new TestNode
                                {
                                    start = new TestNode
                                    {
                                        line = 1,
                                        column = 6
                                    },
                                    end = new TestNode
                                    {
                                        line = 1,
                                        column = 8
                                    }
                                }
                            },
                            loc = new TestNode
                            {
                                start = new TestNode
                                {
                                    line = 1,
                                    column = 0
                                },
                                end = new TestNode
                                {
                                    line = 1,
                                    column = 8
                                }
                            }
                        },
                        loc = new TestNode
                        {
                            start = new TestNode
                            {
                                line = 1,
                                column = 0
                            },
                            end = new TestNode
                            {
                                line = 1,
                                column = 8
                            }
                        }
                    }
                },
                loc = new TestNode
                {
                    start = new TestNode
                    {
                        line = 1,
                        column = 0
                    },
                    end = new TestNode
                    {
                        line = 1,
                        column = 8
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 7,
                locations = true
            });

            Program.testFail("x **= 42", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("x ** y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                name = "x",
                                loc = new TestNode
                                {
                                    start = new TestNode
                                    {
                                        line = 1,
                                        column = 0
                                    },
                                    end = new TestNode
                                    {
                                        line = 1,
                                        column = 1
                                    }
                                }
                            },
                            @operator = "**",
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                name = "y",
                                loc = new TestNode
                                {
                                    start = new TestNode
                                    {
                                        line = 1,
                                        column = 5
                                    },
                                    end = new TestNode
                                    {
                                        line = 1,
                                        column = 6
                                    }
                                }
                            },
                            loc = new TestNode
                            {
                                start = new TestNode
                                {
                                    line = 1,
                                    column = 0
                                },
                                end = new TestNode
                                {
                                    line = 1,
                                    column = 6
                                }
                            }
                        },
                        loc = new TestNode
                        {
                            start = new TestNode
                            {
                                line = 1,
                                column = 0
                            },
                            end = new TestNode
                            {
                                line = 1,
                                column = 6
                            }
                        }
                    }
                },
                loc = new TestNode
                {
                    start = new TestNode
                    {
                        line = 1,
                        column = 0
                    },
                    end = new TestNode
                    {
                        line = 1,
                        column = 6
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 7,
                locations = true
            });

            Program.testFail("x ** y", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 6
            });

// ** has highest precedence
            Program.test("3 ** 5 * 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            @operator = "*",
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                @operator = "**",
                                left = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 3
                                },
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 5
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 1
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 7,
            });

            Program.test("3 % 5 ** 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            @operator = "%",
                            left = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 3
                            },
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                @operator = "**",
                                left = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 5
                                },
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 7,
            });

// Disallowed unary ops
            Program.testFail("delete o.p ** 2;", "Unexpected token (1:11)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("void 2 ** 2;", "Unexpected token (1:7)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("typeof 2 ** 2;", "Unexpected token (1:9)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("~3 ** 2;", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("!1 ** 2;", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("-2** 2;", "Unexpected token (1:2)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("+2** 2;", "Unexpected token (1:2)", new TestOptions
            {
                ecmaVersion = 7
            });

// make sure base operand check doesn't affect other operators
            Program.test("-a * 5", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(UnaryExpressionNode),
                                @operator = "-",
                                prefix = true,
                                argument = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    name = "a"
                                }
                            },
                            @operator = "*",
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 5,
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 6
            });


            Program.test("(-5) ** y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(UnaryExpressionNode),
                                @operator = "-",
                                prefix = true,
                                argument = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 5
                                }
                            },
                            @operator = "**",
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                name = "y"
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 7
            });

            Program.test("++a ** 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(UpdateExpressionNode),
                                @operator = "++",
                                prefix = true,
                                argument = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    name = "a"
                                }
                            },
                            @operator = "**",
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 7
            });

            Program.test("a-- ** 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(UpdateExpressionNode),
                                @operator = "--",
                                prefix = false,
                                argument = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    name = "a"
                                }
                            },
                            @operator = "**",
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 7
            });

            Program.testFail("x %* y", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });

            Program.testFail("x %*= y", "Unexpected token (1:3)", new TestOptions
            {
                ecmaVersion = 7
            });

            Program.testFail("function foo(a=2) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("(a=2) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("function foo({a}) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.testFail("({a}) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new TestOptions
            {
                ecmaVersion = 7
            });
            Program.test("function foo(a) { 'use strict'; }", new TestNode(), new TestOptions
            {
                ecmaVersion = 7
            });

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Program.test(
                "if (x) function f() {}",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(IfStatementNode),
                            consequent = new TestNode
                            {
                                type = typeof(FunctionDeclarationNode)
                            },
                            alternate = null
                        }
                    }
                },
                new TestOptions
                {
                    ecmaVersion = 7
                }
            );

            Program.test(
                "if (x) function f() { return 23; } else function f() { return 42; }",
                new TestNode
                {
                    type = typeof(ProgramNode),
                    body = new[]
                    {
                        new TestNode
                        {
                            type = typeof(IfStatementNode),
                            consequent = new TestNode
                            {
                                type = typeof(FunctionDeclarationNode)
                            },
                            alternate = new TestNode {
                                type = typeof(FunctionDeclarationNode)
                            }
                        }
                    }
                },
                new TestOptions
                {
                    ecmaVersion = 7
                }
            );

            Program.testFail(
                "'use strict'; if(x) function f() {}",
                "Unexpected token (1:20)",
                new TestOptions
                {
                    ecmaVersion = 7
                }
            );

            Program.testFail("'use strict'; function y(x = 1) { 'use strict' }",
                "Illegal 'use strict' directive in function with non-simple parameter list (1:14)",
                new TestOptions
                {
                    ecmaVersion = 7
                });
        }
    }
}
