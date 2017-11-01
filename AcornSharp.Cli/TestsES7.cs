using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsES7()
        {
            Test("x **= 42", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                    {
                        expression = new AssignmentExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                        {
                            @operator = "**=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new LiteralNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8)))
                            {
                                value = 42
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            testFail("x **= 42", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Test("x ** y", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                    {
                        expression = new BinaryExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)))
                        {
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "**",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y")
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            testFail("x ** y", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            // ** has highest precedence
            Test("3 ** 5 * 1", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            @operator = "*",
                            left = new BinaryExpressionNode(default)
                            {
                                @operator = "**",
                                left = new LiteralNode(default)
                                {
                                    value = 3
                                },
                                right = new LiteralNode(default)
                                {
                                    value = 5
                                }
                            },
                            right = new LiteralNode(default)
                            {
                                value = 1
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("3 % 5 ** 1", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            @operator = "%",
                            left = new LiteralNode(default)
                            {
                                value = 3
                            },
                            right = new BinaryExpressionNode(default)
                            {
                                @operator = "**",
                                left = new LiteralNode(default)
                                {
                                    value = 5
                                },
                                right = new LiteralNode(default)
                                {
                                    value = 1
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            // Disallowed unary ops
            testFail("delete o.p ** 2;", "Unexpected token (1:11)", new Options {ecmaVersion = 7});
            testFail("void 2 ** 2;", "Unexpected token (1:7)", new Options {ecmaVersion = 7});
            testFail("typeof 2 ** 2;", "Unexpected token (1:9)", new Options {ecmaVersion = 7});
            testFail("~3 ** 2;", "Unexpected token (1:3)", new Options {ecmaVersion = 7});
            testFail("!1 ** 2;", "Unexpected token (1:3)", new Options {ecmaVersion = 7});
            testFail("-2** 2;", "Unexpected token (1:2)", new Options {ecmaVersion = 7});
            testFail("+2** 2;", "Unexpected token (1:2)", new Options {ecmaVersion = 7});

            // make sure base operand check doesn't affect other operators
            Test("-a * 5", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            left = new UnaryExpressionNode(default)
                            {
                                @operator = "-",
                                prefix = true,
                                argument = new IdentifierNode(default, "a")
                            },
                            @operator = "*",
                            right = new LiteralNode(default)
                            {
                                value = 5
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});


            Test("(-5) ** y", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            left = new UnaryExpressionNode(default)
                            {
                                @operator = "-",
                                prefix = true,
                                argument = new LiteralNode(default)
                                {
                                    value = 5
                                }
                            },
                            @operator = "**",
                            right = new IdentifierNode(default, "y")
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("++a ** 2", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            left = new UpdateExpressionNode(default)
                            {
                                @operator = "++",
                                prefix = true,
                                argument = new IdentifierNode(default, "a")
                            },
                            @operator = "**",
                            right = new LiteralNode(default)
                            {
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 7});

            Test("a-- ** 2", new ProgramNode(default)
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(default)
                    {
                        expression = new BinaryExpressionNode(default)
                        {
                            left = new UpdateExpressionNode(default)
                            {
                                @operator = "--",
                                prefix = false,
                                argument = new IdentifierNode(default, "a")
                            },
                            @operator = "**",
                            right = new LiteralNode(default)
                            {
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 7});

            testFail("x %* y", "Unexpected token (1:3)", new Options {ecmaVersion = 7});

            testFail("x %*= y", "Unexpected token (1:3)", new Options {ecmaVersion = 7});

            testFail("function foo(a=2) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options {ecmaVersion = 7});
            testFail("(a=2) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options {ecmaVersion = 7});
            testFail("function foo({a}) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options {ecmaVersion = 7});
            testFail("({a}) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options {ecmaVersion = 7});
            Test("function foo(a) { 'use strict'; }", new ProgramNode(default), new Options {ecmaVersion = 7});

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Test("if (x) function f() {}", new ProgramNode(default)
                {
                    body = new List<BaseNode>
                    {
                        new IfStatementNode(default,
                            null,
                            new FunctionDeclarationNode(default),
                            null)
                    }
                },
                new Options {ecmaVersion = 7}
            );

            Test("if (x) function f() { return 23; } else function f() { return 42; }", new ProgramNode(default)
                {
                    body = new List<BaseNode>
                    {
                        new IfStatementNode(default,
                            null,
                            new FunctionDeclarationNode(default), 
                            new FunctionDeclarationNode(default))
                    }
                },
                new Options {ecmaVersion = 7}
            );

            testFail(
                "'use strict'; if(x) function f() {}",
                "Unexpected token (1:20)",
                new Options {ecmaVersion = 7}
            );

            testFail("'use strict'; function y(x = 1) { 'use strict' }",
                "Illegal 'use strict' directive in function with non-simple parameter list (1:14)",
                new Options {ecmaVersion = 7});
        }
    }
}
