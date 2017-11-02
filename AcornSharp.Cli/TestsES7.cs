﻿using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsES7()
        {
            Test("x **= 42", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                        expression = new TestNode { type = typeof(AssignmentExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                            @operator = Operator.PowerAssignment,
                            left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x" },
                            right = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8)),
                                value = 42
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            testFail("x **= 42", "Unexpected token (1:3)", new Options { ecmaVersion = 6 });

            Test("x ** y", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                        expression = new TestNode { type = typeof(BinaryExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)),
                            left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x" },
                            @operator = Operator.Power,
                            right = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y" }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            testFail("x ** y", "Unexpected token (1:3)", new Options { ecmaVersion = 6 });

            // ** has highest precedence
            Test("3 ** 5 * 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            @operator = Operator.Multiplication,
                            left = new TestNode { type = typeof(BinaryExpressionNode),
                                @operator = Operator.Power,
                                left = new TestNode { type = typeof(LiteralNode),
                                    value = 3
                                },
                                right = new TestNode { type = typeof(LiteralNode),
                                    value = 5
                                }
                            },
                            right = new TestNode { type = typeof(LiteralNode),
                                value = 1
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("3 % 5 ** 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            @operator = Operator.Modulus,
                            left = new TestNode { type = typeof(LiteralNode),
                                value = 3
                            },
                            right = new TestNode { type = typeof(BinaryExpressionNode),
                                @operator = Operator.Power,
                                left = new TestNode { type = typeof(LiteralNode),
                                    value = 5
                                },
                                right = new TestNode { type = typeof(LiteralNode),
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
            testFail("delete o.p ** 2;", "Unexpected token (1:11)", new Options { ecmaVersion = 7 });
            testFail("void 2 ** 2;", "Unexpected token (1:7)", new Options { ecmaVersion = 7 });
            testFail("typeof 2 ** 2;", "Unexpected token (1:9)", new Options { ecmaVersion = 7 });
            testFail("~3 ** 2;", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });
            testFail("!1 ** 2;", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });
            testFail("-2** 2;", "Unexpected token (1:2)", new Options { ecmaVersion = 7 });
            testFail("+2** 2;", "Unexpected token (1:2)", new Options { ecmaVersion = 7 });

            // make sure base operand check doesn't affect other operators
            Test("-a * 5", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            left = new TestNode { type = typeof(UnaryExpressionNode),
                                @operator = Operator.Subtraction,
                                prefix = true,
                                argument = new TestNode { type = typeof(IdentifierNode),  name = "a" }
                            },
                            @operator = Operator.Multiplication,
                            right = new TestNode { type = typeof(LiteralNode),
                                value = 5
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });


            Test("(-5) ** y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            left = new TestNode { type = typeof(UnaryExpressionNode),
                                @operator = Operator.Subtraction,
                                prefix = true,
                                argument = new TestNode { type = typeof(LiteralNode),
                                    value = 5
                                }
                            },
                            @operator = Operator.Power,
                            right = new TestNode { type = typeof(IdentifierNode),  name = "y" }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("++a ** 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            left = new TestNode { type = typeof(UpdateExpressionNode),
                                @operator = Operator.Increment,
                                prefix = true,
                                argument = new TestNode { type = typeof(IdentifierNode),  name = "a" }
                            },
                            @operator = Operator.Power,
                            right = new TestNode { type = typeof(LiteralNode),
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 7 });

            Test("a-- ** 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode),
                        expression = new TestNode { type = typeof(BinaryExpressionNode),
                            left = new TestNode { type = typeof(UpdateExpressionNode),
                                @operator = Operator.Decrement,
                                prefix = false,
                                argument = new TestNode { type = typeof(IdentifierNode),  name = "a" }
                            },
                            @operator = Operator.Power,
                            right = new TestNode { type = typeof(LiteralNode),
                                value = 2,
                                raw = "2"
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 7 });

            testFail("x %* y", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });

            testFail("x %*= y", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });

            testFail("function foo(a=2) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options { ecmaVersion = 7 });
            testFail("(a=2) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options { ecmaVersion = 7 });
            testFail("function foo({a}) { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options { ecmaVersion = 7 });
            testFail("({a}) => { 'use strict'; }", "Illegal 'use strict' directive in function with non-simple parameter list (1:0)", new Options { ecmaVersion = 7 });
            Test("function foo(a) { 'use strict'; }", new TestNode { type = typeof(ProgramNode) }, new Options { ecmaVersion = 7 });

            // Tests for B.3.4 FunctionDeclarations in IfStatement Statement Clauses
            Test("if (x) function f() {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                    {
                        new TestNode
                        {
                            type = typeof(IfStatementNode),
                            location = default,
                            test = null,
                            consequent = new TestNode { type = typeof(FunctionDeclarationNode) },
                            alternate = null
                        }
                    }
            },
                new Options { ecmaVersion = 7 }
            );

            Test("if (x) function f() { return 23; } else function f() { return 42; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                    {
                        new TestNode
                        {
                            type = typeof(IfStatementNode),
                            location = default,
                            test = null,
                            consequent = new TestNode { type = typeof(FunctionDeclarationNode) },
                            alternate = new TestNode { type = typeof(FunctionDeclarationNode) }
                        }
                    }
            },
                new Options { ecmaVersion = 7 }
            );

            testFail(
                "'use strict'; if(x) function f() {}",
                "Unexpected token (1:20)",
                new Options { ecmaVersion = 7 }
            );

            testFail("'use strict'; function y(x = 1) { 'use strict' }",
                "Illegal 'use strict' directive in function with non-simple parameter list (1:14)",
                new Options { ecmaVersion = 7 });
        }
    }
}
