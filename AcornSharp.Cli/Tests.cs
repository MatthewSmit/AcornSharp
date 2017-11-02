using System;
using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void Tests()
        {
            Test("this\n", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 5)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ThisExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                }
            });

            Test("null\n", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 5)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = null,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                }
            });

            Test("\n    42\n\n", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 4, 5), new Position(2, 6, 7))
                        },
                        location = new SourceLocation(new Position(2, 4, 5), new Position(2, 6, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(4, 0, 9))
            });

            Test("/foobar/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                pattern = "foobar",
                                flags = ""
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            });

            Test("/[a-z]/g", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "g"
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            });

            Test("(1 + 2 ) * 3", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                },
                                @operator = Operator.Addition,
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 2,
                                    location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                location = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                            },
                            @operator = Operator.Multiplication,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 3,
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("(1 + 2 ) * 3", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(ParenthesisedExpressionNode),
                                expression = new TestNode
                                {
                                    type = typeof(BinaryExpressionNode),
                                    left = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                    },
                                    @operator = Operator.Addition,
                                    right = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 2,
                                        location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            @operator = Operator.Multiplication,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 3,
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                preserveParens = true
            });

            Test("(x = 23)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ParenthesisedExpressionNode),
                            expression = new TestNode
                            {
                                type = typeof(AssignmentExpressionNode),
                                @operator = Operator.Assignment,
                                left = new TestNode {type = typeof(IdentifierNode), name = "x"},
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 23,
                                    raw = "23"
                                }
                            }
                        }
                    }
                }
            }, new Options {preserveParens = true});

            Test("x = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = [ ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = [ 42 ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("x = [ 42, ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x = [ ,, 42 ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new[]
                                {
                                    null,
                                    null,
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("x = [ 1, 2, 3, ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 2,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 3,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = [ 1, 2,, 3, ]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 2,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    null,
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 3,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("日本語 = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "日本語"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("T‿ = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), name = "T‿"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‌ = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), name = "T‌"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‍ = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), name = "T‍"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), name = "ⅣⅡ"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), name = "ⅣⅡ"},
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new TestNode[0],
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = { answer: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), name = "answer"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("x = { if: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8)), name = "if"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x = { true: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), name = "true"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = { false: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)), name = "false"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("x = { null: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), name = "null"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = { \"answer\": 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "answer",
                                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                        },
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("x = { x: 1, x: 2 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 1,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                        },
                                        kind = PropertyKind.Initialise
                                    },
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "x"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 2,
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("x = { get width() { return m_width } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "width"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ReturnStatementNode),
                                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)), name = "m_width"},
                                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 34, 34))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 36, 36))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 36, 36))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 38, 38))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("x = { get undef() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "undef"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("x = { get if() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)), name = "if"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                            },
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("x = { get true() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "true"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                            },
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("x = { get false() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "false"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("x = { get null() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "null"},
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                            },
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("x = { get \"undef\"() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "undef",
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                                        },
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                            },
                                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("x = { get 10() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 10,
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = PropertyKind.Get,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                            },
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("x = { set width(w) { m_width = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "width"},
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28)), name = "m_width"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                        },
                                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set if(w) { m_if = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)), name = "if"},
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), name = "m_if"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 26, 26))
                                                        },
                                                        location = new SourceLocation(new Position(1, 18, 18), new Position(1, 26, 26))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28))
                                            },
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 28, 28))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("x = { set true(w) { m_true = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "true"},
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)), name = "m_true"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                        },
                                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                            },
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 34, 34))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("x = { set false(w) { m_false = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "false"},
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28)), name = "m_false"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                        },
                                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set null(w) { m_null = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "null"},
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)), name = "m_null"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                        },
                                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                            },
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 34, 34))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("x = { set \"null\"(w) { m_null = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "null",
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16))
                                        },
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28)), name = "m_null"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                                                        },
                                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 34, 34))
                                            },
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set 10(w) { m_null = w } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 10,
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = PropertyKind.Set,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "w"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(AssignmentExpressionNode),
                                                            @operator = Operator.Assignment,
                                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24)), name = "m_null"},
                                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), name = "w"},
                                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                                                        },
                                                        location = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                            },
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 30, 30))
                                        }
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 32, 32))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            });

            Test("x = { get: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), name = "get"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("x = { set: 43 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), name = "set"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 43,
                                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                        },
                                        kind = PropertyKind.Initialise
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("/* block comment */ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("42 /*The*/ /*Answer*/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("42 /*the*/ /*answer*/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("/* multiline\ncomment\nshould\nbe\nignored */ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(5, 11, 42), new Position(5, 13, 44))
                        },
                        location = new SourceLocation(new Position(5, 11, 42), new Position(5, 13, 44))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(5, 13, 44))
            });

            Test("/*a\r\nb*/ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 4, 9), new Position(2, 6, 11))
                        },
                        location = new SourceLocation(new Position(2, 4, 9), new Position(2, 6, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 11))
            });

            Test("/*a\rb*/ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("/*a\nb*/ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("/*a\nc*/ 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        location = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("// line comment\n42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 0, 16), new Position(2, 2, 18))
                        },
                        location = new SourceLocation(new Position(2, 0, 16), new Position(2, 2, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 18))
            });

            Test("42 // line comment", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("// Hello, world!\n42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 0, 17), new Position(2, 2, 19))
                        },
                        location = new SourceLocation(new Position(2, 0, 17), new Position(2, 2, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 19))
            });

            Test("// Hello, world!\n", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>(),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("// Hallo, world!\n", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>(),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("//\n42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(2, 0, 3), new Position(2, 2, 5))
                        },
                        location = new SourceLocation(new Position(2, 0, 3), new Position(2, 2, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 5))
            });

            Test("//", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>(),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("// ", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>(),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("/**/42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("// Hello, world!\n\n//   Another hello\n42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(4, 0, 37), new Position(4, 2, 39))
                        },
                        location = new SourceLocation(new Position(4, 0, 37), new Position(4, 2, 39))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(4, 2, 39))
            });

            Test("if (x) { // Some comment\ndoThat(); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                        consequent = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 25), new Position(2, 6, 31)), name = "doThat"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(2, 0, 25), new Position(2, 8, 33))
                                    },
                                    location = new SourceLocation(new Position(2, 0, 25), new Position(2, 9, 34))
                                }
                            },
                            location = new SourceLocation(new Position(1, 7, 7), new Position(2, 11, 36))
                        },
                        alternate = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36))
            });

            Test("switch (answer) { case 42: /* perfect */ bingo() }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(SwitchStatementNode),
                        discriminant = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), name = "answer"},
                        cases = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(SwitchCaseNode),
                                consequent = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 46, 46)), name = "bingo"},
                                            arguments = new TestNode[0],
                                            location = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                        },
                                        location = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                    }
                                },
                                test = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 48, 48))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 50, 50))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 50, 50))
            });

            Test("0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("3", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 3,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("5", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 5,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test(".14", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0.14,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("3.14159", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 3.14159,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("6.02214179e+23", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 6.02214179e+23,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("1.492417830e-10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 1.49241783e-10,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("0x0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("0e+100", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("0xabc", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2748,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0xdef", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 3567,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0X1A", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 26,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("0x10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 16,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("0x100", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 256,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0X04", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 4,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("02", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("012", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 10,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("0012", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 10,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("\"Hello\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("\"\\n\\r\\t\\v\\b\\f\\\\\\'\\\"\\0\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "\n\r\t\u000b\b\f\\'\"\u0000",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("\"\\u0061\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "a",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("\"\\x61\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "a",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("\"Hello\\nworld\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\nworld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("\"Hello\\\nworld\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Helloworld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
            });

            Test("\"Hello\\02World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\u0002World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("\"Hello\\012World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\nWorld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\122World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "HelloRWorld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\0122World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\n2World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("\"Hello\\312World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "HelloÊWorld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\412World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello!2World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\812World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello812World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\712World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello92World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\0World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\u0000World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("\"Hello\\\r\nworld\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Helloworld",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
            });

            Test("\"Hello\\1World\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "Hello\u0001World",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("var x = /[a-z]/i", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[x-z]/i", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[a-c]/i", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[P QR]/i", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 17, 17))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("var x = /foo\\/bar/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("var x = /=([^=\\s])+/g", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("var x = /[P QR]/\\u0067", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 22, 22))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("new Button", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)), name = "Button"},
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("new Button()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)), name = "Button"},
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new new foo", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(NewExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("new new foo()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(NewExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("new foo().bar()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                                @object = new TestNode
                                {
                                    type = typeof(NewExpressionNode),
                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "foo"},
                                    arguments = new TestNode[0],
                                    location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                                },
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "bar"},
                                computed = false
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("new foo[bar]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "foo"},
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "bar"},
                                computed = true
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new foo.bar()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)),
                                @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "foo"},
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "bar"},
                                computed = false
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("( new foo).bar()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                                @object = new TestNode
                                {
                                    type = typeof(NewExpressionNode),
                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), name = "foo"},
                                    arguments = new TestNode[0],
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                },
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), name = "bar"},
                                computed = false
                            },
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("foo(bar, baz)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo"},
                            arguments = new[]
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "bar"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "baz"}
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("(    foo  )()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), name = "foo"},
                            arguments = new TestNode[0],
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.milkyway", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), name = "milkyway"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("universe.milkyway.solarsystem", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                            @object = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), name = "milkyway"},
                                computed = false
                            },
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29)), name = "solarsystem"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("universe.milkyway.solarsystem.Earth", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                            @object = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                                @object = new TestNode
                                {
                                    type = typeof(MemberExpressionNode),
                                    location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                    @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                                    property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), name = "milkyway"},
                                    computed = false
                                },
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29)), name = "solarsystem"},
                                computed = false
                            },
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 35, 35)), name = "Earth"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
            });

            Test("universe[galaxyName, otherUselessName]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode
                            {
                                type = typeof(SequenceExpressionNode),
                                expressions = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)), name = "galaxyName"},
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 37, 37)), name = "otherUselessName"}
                                },
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37))
                            },
                            computed = true
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("universe[galaxyName]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)), name = "galaxyName"},
                            computed = true
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("universe[42].galaxies", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                            @object = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                                @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                                property = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                },
                                computed = true
                            },
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), name = "galaxies"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                            @object = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                                arguments = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                            },
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), name = "galaxies"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies(14, 3, 77).milkyway", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41)),
                            @object = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode
                                {
                                    type = typeof(MemberExpressionNode),
                                    location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                                    @object = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                                        arguments = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                value = 42,
                                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                                    },
                                    property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), name = "galaxies"},
                                    computed = false
                                },
                                arguments = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 14,
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 3,
                                        location = new SourceLocation(new Position(1, 26, 26), new Position(1, 27, 27))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 77,
                                        location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                            },
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41)), name = "milkyway"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            });

            Test("earth.asia.Indonesia.prepareForElection(2014)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)),
                                @object = new TestNode
                                {
                                    type = typeof(MemberExpressionNode),
                                    location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                                    @object = new TestNode
                                    {
                                        type = typeof(MemberExpressionNode),
                                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                                        @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "earth"},
                                        property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), name = "asia"},
                                        computed = false
                                    },
                                    property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20)), name = "Indonesia"},
                                    computed = false
                                },
                                property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 39, 39)), name = "prepareForElection"},
                                computed = false
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 2014,
                                    location = new SourceLocation(new Position(1, 40, 40), new Position(1, 44, 44))
                                }
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
            });

            Test("universe.if", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11)), name = "if"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("universe.true", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "true"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.false", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "false"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("universe.null", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(MemberExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), name = "universe"},
                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "null"},
                            computed = false
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("x++", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("x--", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("eval++", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "eval"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("eval--", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "eval"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("arguments++", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), name = "arguments"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("arguments--", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), name = "arguments"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("++x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("--x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("++eval", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6)), name = "eval"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("--eval", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6)), name = "eval"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("++arguments", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)), name = "arguments"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("--arguments", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Decrement,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)), name = "arguments"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("+x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.Addition,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("-x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.Subtraction,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("~x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.BitwiseNot,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("!x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.LogicalNot,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("void x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.Void,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("delete x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.Delete,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("typeof x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            @operator = Operator.TypeOf,
                            prefix = true,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "x"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x * y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Multiplication,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x / y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Division,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x % y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Modulus,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Addition,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x - y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Subtraction,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x << y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LeftShift,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >> y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.RightShift,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >>> y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.RightShiftUnsigned,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x < y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x > y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.GreaterThan,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x <= y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LessEquals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >= y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.GreaterEquals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x in y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.In,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x instanceof y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.InstanceOf,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x < y < z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.LessThan,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.LessThan,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x == y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Equals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x != y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.NotEquals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x === y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.StrictEquals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x !== y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.StrictNotEquals,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x & y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.BitwiseAnd,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x ^ y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.BitwiseXOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x | y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.BitwiseOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y + z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Addition,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Addition,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y + z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Subtraction,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Addition,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y - z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Addition,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Subtraction,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y - z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Subtraction,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Subtraction,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y * z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Addition,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                @operator = Operator.Multiplication,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y / z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Addition,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                @operator = Operator.Division,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y % z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.Subtraction,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                @operator = Operator.Modulus,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y * z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Multiplication,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Multiplication,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y / z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Multiplication,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Division,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y % z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Multiplication,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Modulus,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x % y * z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.Modulus,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.Multiplication,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x << y << z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.LeftShift,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = Operator.LeftShift,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x | y | z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.BitwiseOr,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.BitwiseOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y & z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.BitwiseAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.BitwiseAnd,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x ^ y ^ z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.BitwiseXOr,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.BitwiseXOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y | z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.BitwiseAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = Operator.BitwiseOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y ^ z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.BitwiseOr,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                @operator = Operator.BitwiseXOr,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y & z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.BitwiseOr,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "y"},
                                @operator = Operator.BitwiseAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "z"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x || y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LogicalOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x && y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LogicalAnd,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x || y || z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(LogicalExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.LogicalOr,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = Operator.LogicalOr,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x && y && z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(LogicalExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.LogicalAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = Operator.LogicalAnd,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "z"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y && z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LogicalOr,
                            right = new TestNode
                            {
                                type = typeof(LogicalExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                @operator = Operator.LogicalAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "z"},
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y ^ z", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LogicalExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            @operator = Operator.LogicalOr,
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                @operator = Operator.BitwiseXOr,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "z"},
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("y ? 1 : 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ConditionalExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "y"},
                            consequent = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 1,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            alternate = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 2,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x && y ? 1 : 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ConditionalExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            test = new TestNode
                            {
                                type = typeof(LogicalExpressionNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                @operator = Operator.LogicalAnd,
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"},
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            consequent = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 1,
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                            },
                            alternate = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 2,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("eval = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "eval"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("arguments = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), name = "arguments"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x *= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.MultiplicationAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x /= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.DivisionAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x %= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.ModulusAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x += 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.AdditionAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x -= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.SubtractionAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x <<= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.LeftShiftAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x >>= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.RightShiftAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x >>>= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.RightShiftUnsignedAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x &= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.BitwiseAndAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x ^= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.BitwiseXOrAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x |= 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.BitwiseOrAssignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("{ foo }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), name = "foo"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("{ doThis(); doThat(); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode
                                {
                                    type = typeof(CallExpressionNode),
                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 8, 8)), name = "doThis"},
                                    arguments = new TestNode[0],
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                                },
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode
                                {
                                    type = typeof(CallExpressionNode),
                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)), name = "doThat"},
                                    arguments = new TestNode[0],
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                },
                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 21, 21))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("{}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>(),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("var x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("var await", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), name = "await"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("var x, y;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "y"},
                                init = null,
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("var x = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("var eval = 42, arguments = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)), name = "eval"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), name = "arguments"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                },
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("var x = 14, y = 3, z = 1977", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 14,
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "y"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 3,
                                    location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "z"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1977,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 27, 27))
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("var implements, interface, package", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)), name = "implements"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25)), name = "interface"},
                                init = null,
                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)), name = "package"},
                                init = null,
                                location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("var private, protected, public, static", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "private"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22)), name = "protected"},
                                init = null,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30)), name = "public"},
                                init = null,
                                location = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 38, 38)), name = "static"},
                                init = null,
                                location = new SourceLocation(new Position(1, 32, 32), new Position(1, 38, 38))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test(";", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(EmptyStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x, y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(SequenceExpressionNode),
                            expressions = new[]
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "y"}
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("\\u0061", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)), name = "a"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("a\\u0061", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)), name = "aa"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("if (morning) goodMorning()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "morning"},
                        consequent = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), name = "goodMorning"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        alternate = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("if (morning) (function(){})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "morning"},
                        consequent = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                id = null,
                                parameters = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 26, 26))
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) var x = 0;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "morning"},
                        consequent = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22))
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 23, 23))
                        },
                        alternate = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("if (morning) function a(){}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "morning"},
                        consequent = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "a"},
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) goodMorning(); else goodDay()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                        test = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), name = "morning"},
                        consequent = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), name = "goodMorning"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)), name = "goodDay"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                            },
                            location = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                        }
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42))
            });

            Test("do keep(); while (true)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DoWhileStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), name = "keep"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("do keep(); while (true);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DoWhileStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), name = "keep"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("do { x++; y--; } while (x < 10)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DoWhileStatementNode),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(UpdateExpressionNode),
                                        @operator = Operator.Increment,
                                        prefix = false,
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                                        location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                    },
                                    location = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(UpdateExpressionNode),
                                        @operator = Operator.Decrement,
                                        prefix = false,
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "y"},
                                        location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                }
                            },
                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 16, 16))
                        },
                        test = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 10,
                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("{ do { } while (false);false }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(DoWhileStatementNode),
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                },
                                test = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = false,
                                    location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                },
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 23, 23))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = false,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28))
                                },
                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("while (true) doSomething()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), name = "doSomething"},
                                arguments = new TestNode[0],
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("while (x < 10) { x++; y--; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 10,
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(UpdateExpressionNode),
                                        @operator = Operator.Increment,
                                        prefix = false,
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "x"},
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 21, 21))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(UpdateExpressionNode),
                                        @operator = Operator.Decrement,
                                        prefix = false,
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "y"},
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    location = new SourceLocation(new Position(1, 22, 22), new Position(1, 26, 26))
                                }
                            },
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 28, 28))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("for(;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = null,
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("for(;;){}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = null,
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("for(x = 0;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("for(var x = 0;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("for(var x = 0, y = 1;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "y"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                    },
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("for(x = 0; x < 42; x++);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "x"},
                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42; x++) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                            @operator = Operator.LessThan,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "x"},
                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 33, 33)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 24, 24), new Position(1, 34, 34))
                            },
                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
            });

            Test("for(x in list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25))
                            },
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("for (var x in list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("for (var x = 42 in list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 23, 23)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 35, 35))
                            },
                            location = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("for (var i = function() { return 10 in [] } in list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "i"},
                                    init = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ReturnStatementNode),
                                                    argument = new TestNode
                                                    {
                                                        type = typeof(BinaryExpressionNode),
                                                        left = new TestNode
                                                        {
                                                            type = typeof(LiteralNode),
                                                            value = 10,
                                                            location = new SourceLocation(new Position(1, 33, 33), new Position(1, 35, 35))
                                                        },
                                                        @operator = Operator.In,
                                                        right = new TestNode
                                                        {
                                                            type = typeof(ArrayExpressionNode),
                                                            elements = new TestNode[0],
                                                            location = new SourceLocation(new Position(1, 39, 39), new Position(1, 41, 41))
                                                        },
                                                        location = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41))
                                                    },
                                                    location = new SourceLocation(new Position(1, 26, 26), new Position(1, 41, 41))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 43, 43))
                                        },
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 43, 43))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 43, 43))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 43, 43))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 47, 47), new Position(1, 51, 51)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 53, 53), new Position(1, 60, 60)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 61, 61), new Position(1, 62, 62)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 53, 53), new Position(1, 63, 63))
                            },
                            location = new SourceLocation(new Position(1, 53, 53), new Position(1, 64, 64))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 64, 64))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 64, 64))
            });

            Test("while (true) { continue; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ContinueStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("while (true) { continue }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ContinueStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("done: while (true) { continue done }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(WhileStatementNode),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = true,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ContinueStatementNode),
                                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34)), name = "done"},
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 36, 36))
                            },
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 36, 36))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "done"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("done: while (true) { continue done; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(WhileStatementNode),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = true,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ContinueStatementNode),
                                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34)), name = "done"},
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 35, 35))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 37, 37))
                            },
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 37, 37))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "done"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            });

            Test("while (true) { break }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(BreakStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("done: while (true) { break done }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(WhileStatementNode),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = true,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31)), name = "done"},
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 31, 31))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 33, 33))
                            },
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 33, 33))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "done"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            });

            Test("done: while (true) { break done; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(WhileStatementNode),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = true,
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31)), name = "done"},
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                            },
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 34, 34))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "done"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("target1: target2: while (true) { continue target1; }", new TestNode {type = typeof(ProgramNode)});
            Test("target1: target2: target3: while (true) { continue target1; }", new TestNode {type = typeof(ProgramNode)});

            Test("(function(){ return })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = null,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 21, 21))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("(function(){ return; })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = null,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 20, 20))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 22, 22))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("(function(){ return x; })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "x"},
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("(function(){ return x * y })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = new TestNode
                                        {
                                            type = typeof(BinaryExpressionNode),
                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "x"},
                                            @operator = Operator.Multiplication,
                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "y"},
                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                        },
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 27, 27))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 27, 27))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("with (x) foo = bar", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WithStatementNode),
                        @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(AssignmentExpressionNode),
                                @operator = Operator.Assignment,
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "bar"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                            },
                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("with (x) foo = bar;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WithStatementNode),
                        @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(AssignmentExpressionNode),
                                @operator = Operator.Assignment,
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "bar"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                            },
                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            Test("'use\\x20strict'; with (x) foo = bar;", new TestNode {type = typeof(ProgramNode)});

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            Test(@"""use\\x20strict""; with (x) foo = bar;", new TestNode {type = typeof(ProgramNode)});

            Test("with (x) { foo = bar }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WithStatementNode),
                        @object = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(AssignmentExpressionNode),
                                        @operator = Operator.Assignment,
                                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), name = "foo"},
                                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), name = "bar"},
                                        location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                                }
                            },
                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("switch (x) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(SwitchStatementNode),
                        discriminant = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "x"},
                        cases = new TestNode[0],
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("switch (answer) { case 42: hi(); break; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(SwitchStatementNode),
                        discriminant = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), name = "answer"},
                        cases = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(SwitchCaseNode),
                                consequent = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)), name = "hi"},
                                            arguments = new TestNode[0],
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        location = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = null,
                                        location = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 39, 39))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            });

            Test("switch (answer) { case 42: hi(); break; default: break }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(SwitchStatementNode),
                        discriminant = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), name = "answer"},
                        cases = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(SwitchCaseNode),
                                consequent = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)), name = "hi"},
                                            arguments = new TestNode[0],
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        location = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = null,
                                        location = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 39, 39))
                            },
                            new TestNode
                            {
                                type = typeof(SwitchCaseNode),
                                consequent = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = null,
                                        location = new SourceLocation(new Position(1, 49, 49), new Position(1, 54, 54))
                                    }
                                },
                                test = null,
                                location = new SourceLocation(new Position(1, 40, 40), new Position(1, 54, 54))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56))
            });

            Test("start: for (;;) break start", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ForStatementNode),
                            init = null,
                            test = null,
                            update = null,
                            body = new TestNode
                            {
                                type = typeof(BreakStatementNode),
                                label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), name = "start"},
                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 27, 27))
                            },
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "start"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("start: while (true) break start", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(WhileStatementNode),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = true,
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18))
                            },
                            body = new TestNode
                            {
                                type = typeof(BreakStatementNode),
                                label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)), name = "start"},
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                            },
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31))
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "start"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("throw x;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ThrowStatementNode),
                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("throw x * y", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ThrowStatementNode),
                        argument = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                            @operator = Operator.Multiplication,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "y"},
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("throw { message: \"Error\" }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ThrowStatementNode),
                        argument = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15)), name = "message"},
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = "Error",
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24))
                                    },
                                    kind = PropertyKind.Initialise
                                }
                            },
                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("try { } catch (e) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "e"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21))
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                        },
                        finaliser = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("try { } catch (eval) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), name = "eval"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 24, 24))
                        },
                        finaliser = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("try { } catch (arguments) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), name = "arguments"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29))
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29))
                        },
                        finaliser = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("try { } catch (e) { say(e) }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "e"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), name = "say"},
                                            arguments = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "e"}
                                            },
                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                        },
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        finaliser = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("try { } finally { cleanup(stuff) }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = null,
                        finaliser = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "cleanup"},
                                        arguments = new[]
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)), name = "stuff"}
                                        },
                                        location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                    },
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                }
                            },
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 34, 34))
                        }
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("try { doThat(); } catch (e) { say(e) }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), name = "doThat"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "e"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), name = "say"},
                                            arguments = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), name = "e"}
                                            },
                                            location = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                        },
                                        location = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 38, 38))
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38))
                        },
                        finaliser = null
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("try { doThat(); } catch (e) { say(e) } finally { cleanup(stuff) }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), name = "doThat"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            param = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "e"},
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(CallExpressionNode),
                                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), name = "say"},
                                            arguments = new[]
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), name = "e"}
                                            },
                                            location = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                        },
                                        location = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 38, 38))
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38))
                        },
                        finaliser = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 49, 49), new Position(1, 56, 56)), name = "cleanup"},
                                        arguments = new[]
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 57, 57), new Position(1, 62, 62)), name = "stuff"}
                                        },
                                        location = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                    },
                                    location = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                }
                            },
                            location = new SourceLocation(new Position(1, 47, 47), new Position(1, 65, 65))
                        }
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65))
            });

            Test("debugger;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DebuggerStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("function hello() { sayHi(); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "hello"},
                        parameters = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 24, 24)), name = "sayHi"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                    },
                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                }
                            },
                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("function eval() { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "eval"},
                        parameters = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("function arguments() { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18)), name = "arguments"},
                        parameters = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("function test(t, t) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "test"},
                        parameters = new[]
                        {
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "t"},
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "t"}
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("(function test(t, t) { })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "test"},
                            parameters = new[]
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "t"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "t"}
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("function eval() { function inner() { \"use strict\" } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "eval"},
                        parameters = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(FunctionDeclarationNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32)), name = "inner"},
                                    parameters = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                expression = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    value = "use strict",
                                                    location = new SourceLocation(new Position(1, 37, 37), new Position(1, 49, 49))
                                                },
                                                location = new SourceLocation(new Position(1, 37, 37), new Position(1, 49, 49))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 35, 35), new Position(1, 51, 51))
                                    },
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 51, 51))
                                }
                            },
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 53, 53))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53))
            });

            Test("function hello(a) { sayHi(); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "hello"},
                        parameters = new[]
                        {
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "a"}
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25)), name = "sayHi"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                    },
                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                }
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("function hello(a, b) { sayHi(); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "hello"},
                        parameters = new[]
                        {
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "a"},
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "b"}
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28)), name = "sayHi"},
                                        arguments = new TestNode[0],
                                        location = new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30))
                                    },
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 31, 31))
                                }
                            },
                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 33, 33))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            });

            Test("function hello(...rest) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "hello"},
                        parameters = new[]
                        {
                            new TestNode
                            {
                                type = typeof(RestElementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), name = "rest"}
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function hello(a, ...rest) { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), name = "hello"},
                        parameters = new TestNode[]
                        {
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "a"},
                            new TestNode
                            {
                                type = typeof(RestElementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 25, 25)), name = "rest"}
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var hi = function() { sayHi() };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), name = "hi"},
                                init = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    id = null,
                                    parameters = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                expression = new TestNode
                                                {
                                                    type = typeof(CallExpressionNode),
                                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), name = "sayHi"},
                                                    arguments = new TestNode[0],
                                                    location = new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29))
                                                },
                                                location = new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 31, 31))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 31, 31))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            });

            Test("var hi = function (...r) { sayHi() };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), name = "hi"},
                                init = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    id = null,
                                    parameters = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "r"}
                                        }
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                expression = new TestNode
                                                {
                                                    type = typeof(CallExpressionNode),
                                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32)), name = "sayHi"},
                                                    arguments = new TestNode[0],
                                                    location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                                },
                                                location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 36, 36))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var hi = function eval() { };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), name = "hi"},
                                init = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), name = "eval"},
                                    parameters = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 28, 28))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 28, 28))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("var hi = function arguments() { };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), name = "hi"},
                                init = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)), name = "arguments"},
                                    parameters = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 33, 33))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("var hello = function hi() { sayHi() };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), name = "hello"},
                                init = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23)), name = "hi"},
                                    parameters = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                expression = new TestNode
                                                {
                                                    type = typeof(CallExpressionNode),
                                                    callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33)), name = "sayHi"},
                                                    arguments = new TestNode[0],
                                                    location = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35))
                                                },
                                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 26, 26), new Position(1, 37, 37))
                                    },
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 37, 37))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 37, 37))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("(function(){})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("{ x\n++y }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode
                                {
                                    type = typeof(UpdateExpressionNode),
                                    @operator = Operator.Increment,
                                    prefix = true,
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7)), name = "y"},
                                    location = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                                },
                                location = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
            });

            Test("{ x\n--y }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode
                                {
                                    type = typeof(UpdateExpressionNode),
                                    @operator = Operator.Decrement,
                                    prefix = true,
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7)), name = "y"},
                                    location = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                                },
                                location = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
            });

            Test("var x /* comment */;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("{ var x = 14, y = 3\nz; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclarationNode),
                                declarations = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(VariableDeclaratorNode),
                                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                        init = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 14,
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(VariableDeclaratorNode),
                                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "y"},
                                        init = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 3,
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        },
                                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                    }
                                },
                                kind = VariableKind.Var,
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 19, 19))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21)), name = "z"},
                                location = new SourceLocation(new Position(2, 0, 20), new Position(2, 2, 22))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
            });

            Test("while (true) { continue\nthere; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ContinueStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29)), name = "there"},
                                    location = new SourceLocation(new Position(2, 0, 24), new Position(2, 6, 30))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 32))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
            });

            Test("while (true) { continue // Comment\nthere; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ContinueStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 35), new Position(2, 5, 40)), name = "there"},
                                    location = new SourceLocation(new Position(2, 0, 35), new Position(2, 6, 41))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 43))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 43))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 43))
            });

            Test("while (true) { continue /* Multiline\nComment */there; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ContinueStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 10, 47), new Position(2, 15, 52)), name = "there"},
                                    location = new SourceLocation(new Position(2, 10, 47), new Position(2, 16, 53))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 18, 55))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 55))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 55))
            });

            Test("while (true) { break\nthere; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(BreakStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 21), new Position(2, 5, 26)), name = "there"},
                                    location = new SourceLocation(new Position(2, 0, 21), new Position(2, 6, 27))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 29))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 29))
            });

            Test("while (true) { break // Comment\nthere; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(BreakStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 32), new Position(2, 5, 37)), name = "there"},
                                    location = new SourceLocation(new Position(2, 0, 32), new Position(2, 6, 38))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 40))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 40))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 40))
            });

            Test("while (true) { break /* Multiline\nComment */there; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(WhileStatementNode),
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = true,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(BreakStatementNode),
                                    label = null,
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 10, 44), new Position(2, 15, 49)), name = "there"},
                                    location = new SourceLocation(new Position(2, 10, 44), new Position(2, 16, 50))
                                }
                            },
                            location = new SourceLocation(new Position(1, 13, 13), new Position(2, 18, 52))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 52))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 52))
            });

            Test("(function(){ return\nx; })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = null,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21)), name = "x"},
                                        location = new SourceLocation(new Position(2, 0, 20), new Position(2, 2, 22))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(2, 4, 24))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(2, 4, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 25))
            });

            Test("(function(){ return // Comment\nx; })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = null,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 31), new Position(2, 1, 32)), name = "x"},
                                        location = new SourceLocation(new Position(2, 0, 31), new Position(2, 2, 33))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(2, 4, 35))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(2, 4, 35))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 36))
            });

            Test("(function(){ return/* Multiline\nComment */x; })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ReturnStatementNode),
                                        argument = null,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 10, 42), new Position(2, 11, 43)), name = "x"},
                                        location = new SourceLocation(new Position(2, 10, 42), new Position(2, 12, 44))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(2, 14, 46))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(2, 14, 46))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 15, 47))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 15, 47))
            });

            Test("{ throw error\nerror; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ThrowStatementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), name = "error"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 14), new Position(2, 5, 19)), name = "error"},
                                location = new SourceLocation(new Position(2, 0, 14), new Position(2, 6, 20))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
            });

            Test("{ throw error// Comment\nerror; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ThrowStatementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), name = "error"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29)), name = "error"},
                                location = new SourceLocation(new Position(2, 0, 24), new Position(2, 6, 30))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
            });

            Test("{ throw error/* Multiline\nComment */error; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ThrowStatementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), name = "error"},
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 10, 36), new Position(2, 15, 41)), name = "error"},
                                location = new SourceLocation(new Position(2, 10, 36), new Position(2, 16, 42))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
            });

            Test("", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>(),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 0, 0))
            });

            Test("foo: if (true) break foo;", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        body = new TestNode
                        {
                            type = typeof(IfStatementNode),
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 25, 25)),
                            test = new TestNode
                            {
                                type = typeof(LiteralNode),
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)),
                                value = true
                            },
                            consequent = new TestNode
                            {
                                type = typeof(BreakStatementNode),
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25)),
                                label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), name = "foo"}
                            },
                            alternate = null
                        },
                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo"}
                    }
                }
            });

            Test("(function () {\n 'use strict';\n '\0';\n}())", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            location = new SourceLocation(new Position(1, 1, 1), new Position(4, 3, 39)),
                            callee = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                location = new SourceLocation(new Position(1, 1, 1), new Position(4, 1, 37)),
                                id = null,
                                parameters = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    location = new SourceLocation(new Position(1, 13, 13), new Position(4, 1, 37)),
                                    body = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            location = new SourceLocation(new Position(2, 1, 16), new Position(2, 14, 29)),
                                            expression = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                location = new SourceLocation(new Position(2, 1, 16), new Position(2, 13, 28)),
                                                value = "use strict"
                                            }
                                        },
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            location = new SourceLocation(new Position(3, 1, 31), new Position(3, 5, 35)),
                                            expression = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                location = new SourceLocation(new Position(3, 1, 31), new Position(3, 4, 34)),
                                                value = "\u0000"
                                            }
                                        }
                                    }
                                }
                            },
                            arguments = new TestNode[0]
                        }
                    }
                }
            });

            Test("123..toString(10)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = default,
                                @object = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 123
                                },
                                property = new TestNode {type = typeof(IdentifierNode), name = "toString"},
                                computed = false
                            },
                            arguments = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("123.+2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 123
                            },
                            @operator = Operator.Addition,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 2
                            }
                        }
                    }
                }
            });

            Test("a\u2028b", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), name = "a"}
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), name = "b"}
                    }
                }
            });

            Test("'a\\u0026b'", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "a\u0026b"
                        }
                    }
                }
            });

            Test("foo: 10; foo: 20;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 10,
                                raw = "10"
                            }
                        },
                        label = new TestNode {type = typeof(IdentifierNode), name = "foo"}
                    },
                    new TestNode
                    {
                        type = typeof(LabelledStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 20,
                                raw = "20"
                            }
                        },
                        label = new TestNode {type = typeof(IdentifierNode), name = "foo"}
                    }
                }
            });

            Test("if(1)/  foo/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(IfStatementNode),
                        location = default,
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 1,
                            raw = "1"
                        },
                        consequent = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(LiteralNode),
                                raw = "/  foo/"
                            }
                        },
                        alternate = null
                    }
                }
            });

            Test("price_9̶9̶_89", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode {type = typeof(IdentifierNode), name = "price_9̶9̶_89"}
                    }
                }
            });

            // `\0` is valid even in strict mode
            Test("function hello() { 'use strict'; \"\\0\"; }", new TestNode {type = typeof(ProgramNode)});

            // option tests
            Test("var a = 1;", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "a"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)),
                                    value = 1,
                                    raw = "1"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                sourceFile = "test.js"
            });

            Test("a.in / b", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = default,
                                @object = new TestNode {type = typeof(IdentifierNode), name = "a"},
                                property = new TestNode {type = typeof(IdentifierNode), name = "in"},
                                computed = false
                            },
                            @operator = Operator.Division,
                            right = new TestNode {type = typeof(IdentifierNode), name = "b"}
                        }
                    }
                }
            });

            // A number of slash-disambiguation corner cases
            Test("return {} / 2", new TestNode {type = typeof(ProgramNode)}, new Options {allowReturnOutsideFunction = true});
            Test("return\n{}\n/foo/", new TestNode {type = typeof(ProgramNode)}, new Options {allowReturnOutsideFunction = true});
            Test("+{} / 2", new TestNode {type = typeof(ProgramNode)});
            Test("{}\n/foo/", new TestNode {type = typeof(ProgramNode)});
            Test("x++\n{}\n/foo/", new TestNode {type = typeof(ProgramNode)});
            Test("{{}\n/foo/}", new TestNode {type = typeof(ProgramNode)});
            Test("while (1) /foo/", new TestNode {type = typeof(ProgramNode)});
            Test("while (1) {} /foo/", new TestNode {type = typeof(ProgramNode)});
            Test("(1) / 2", new TestNode {type = typeof(ProgramNode)});
            Test("({a: [1]}+[]) / 2", new TestNode {type = typeof(ProgramNode)});
            Test("{[1]}\n/foo/", new TestNode {type = typeof(ProgramNode)});
            Test("switch(a) { case 1: {}\n/foo/ }", new TestNode {type = typeof(ProgramNode)});
            Test("({1: {} / 2})", new TestNode {type = typeof(ProgramNode)});
            Test("+x++ / 2", new TestNode {type = typeof(ProgramNode)});
            Test("foo.in\n{}\n/foo/", new TestNode {type = typeof(ProgramNode)});
            Test("var x = function f() {} / 3;", new TestNode {type = typeof(ProgramNode)});
            Test("+function f() {} / 3;", new TestNode {type = typeof(ProgramNode)});
            Test("foo: function x() {} /regexp/", new TestNode {type = typeof(ProgramNode)});
            Test("x = {foo: function x() {} / divide}", new TestNode {type = typeof(ProgramNode)});
            Test("foo; function f() {} /regexp/", new TestNode {type = typeof(ProgramNode)});
            Test("{function f() {} /regexp/}", new TestNode {type = typeof(ProgramNode)});

            Test("{}/=/", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        body = new List<TestNode>()
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            raw = "/=/"
                        }
                    }
                }
            });

            Test("foo <!--bar\n+baz", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), name = "foo"},
                            @operator = Operator.Addition,
                            right = new TestNode {type = typeof(IdentifierNode), name = "baz"}
                        }
                    }
                }
            });

            Test("x = y-->10;\n --> nothing", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode
                                {
                                    type = typeof(UpdateExpressionNode),
                                    @operator = Operator.Decrement,
                                    prefix = false,
                                    argument = new TestNode {type = typeof(IdentifierNode), name = "y"}
                                },
                                @operator = Operator.GreaterThan,
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("'use strict';\nobject.static();", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "use strict",
                            raw = "'use strict'"
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(MemberExpressionNode),
                                location = default,
                                @object = new TestNode {type = typeof(IdentifierNode), name = "object"},
                                property = new TestNode {type = typeof(IdentifierNode), name = "static"},
                                computed = false
                            },
                            arguments = new TestNode[0]
                        }
                    }
                }
            });

            // Failure tests
            testFail("{",
                "Unexpected token (1:1)");

            testFail("}",
                "Unexpected token (1:0)");

            testFail("3ea",
                "Invalid number (1:0)");

            testFail("3in []",
                "Identifier directly after number (1:1)");

            testFail("3e",
                "Invalid number (1:0)");

            testFail("3e+",
                "Invalid number (1:0)");

            testFail("3e-",
                "Invalid number (1:0)");

            testFail("3x",
                "Identifier directly after number (1:1)");

            testFail("3x0",
                "Identifier directly after number (1:1)");

            testFail("0x",
                "Expected number in radix 16 (1:2)");

            testFail("'use strict'; 09",
                "Invalid number (1:14)");

            testFail("'use strict'; 018",
                "Invalid number (1:14)");

            testFail("01a",
                "Identifier directly after number (1:2)");

            testFail("3in[]",
                "Identifier directly after number (1:1)");

            testFail("0x3in[]",
                "Identifier directly after number (1:3)");

            testFail("\"Hello\nWorld\"",
                "Unterminated string constant (1:0)");

            testFail("x\\",
                "Expecting Unicode escape sequence \\uXXXX (1:2)");

            testFail("x\\u005c",
                "Invalid Unicode escape (1:1)");

            testFail("x\\u002a",
                "Invalid Unicode escape (1:1)");

            testFail("/",
                "Unterminated regular expression (1:1)");

            testFail("/test",
                "Unterminated regular expression (1:1)");

            testFail("var x = /[a-z]/\\ux",
                "Bad character escape sequence (1:17)");

            testFail("3 = 4",
                "Assigning to rvalue (1:0)");

            testFail("func() = 4",
                "Assigning to rvalue (1:0)");

            testFail("(1 + 1) = 10",
                "Parenthesized pattern (1:0)");

            testFail("1++",
                "Assigning to rvalue (1:0)");

            testFail("1--",
                "Assigning to rvalue (1:0)");

            testFail("++1",
                "Assigning to rvalue (1:2)");

            testFail("--1",
                "Assigning to rvalue (1:2)");

            testFail("for((1 + 1) in list) process(x);",
                "Assigning to rvalue (1:5)");

            testFail("[",
                "Unexpected token (1:1)");

            testFail("[,",
                "Unexpected token (1:2)");

            testFail("1 + {",
                "Unexpected token (1:5)");

            testFail("1 + { t:t ",
                "Unexpected token (1:10)");

            testFail("1 + { t:t,",
                "Unexpected token (1:10)");

            testFail("var x = /\n/",
                "Unterminated regular expression (1:9)");

            testFail("var x = \"\n",
                "Unterminated string constant (1:8)");

            testFail("var if = 42",
                "Unexpected keyword 'if' (1:4)");

            testFail("i + 2 = 42",
                "Assigning to rvalue (1:0)");

            testFail("+i = 42",
                "Assigning to rvalue (1:0)");

            testFail("1 + (",
                "Unexpected token (1:5)");

            testFail("\n\n\n{",
                "Unexpected token (4:1)");

            testFail("\n/* Some multiline\ncomment */\n)",
                "Unexpected token (4:0)");

            testFail("{ set 1 }",
                "Unexpected token (1:6)");

            testFail("{ get 2 }",
                "Unexpected token (1:6)");

            testFail("({ set: s(if) { } })",
                "Unexpected token (1:10)");

            testFail("({ set s(.) { } })",
                "Unexpected token (1:9)");

            testFail("({ set: s() { } })",
                "Unexpected token (1:12)");

            testFail("({ set: s(a, b) { } })",
                "Unexpected token (1:16)");

            testFail("({ get: g(d) { } })",
                "Unexpected token (1:13)");

            testFail("({ get i() { }, i: 42 })",
                "Redefinition of property (1:16)");

            testFail("({ i: 42, get i() { } })",
                "Redefinition of property (1:14)");

            testFail("({ set i(x) { }, i: 42 })",
                "Redefinition of property (1:17)");

            testFail("({ i: 42, set i(x) { } })",
                "Redefinition of property (1:14)");

            testFail("({ get i() { }, get i() { } })",
                "Redefinition of property (1:20)");

            testFail("({ set i(x) { }, set i(x) { } })",
                "Redefinition of property (1:21)");

            testFail("'use strict'; ({ __proto__: 1, __proto__: 2 })",
                "Redefinition of property (1:31)");

            testFail("function t(...) { }",
                "Unexpected token (1:11)");

            testFail("function t(...) { }",
                "Unexpected token (1:14)",
                new Options {ecmaVersion = 6});

            testFail("function t(...rest, b) { }",
                "Comma is not permitted after the rest element (1:18)",
                new Options {ecmaVersion = 6});

            testFail("function t(if) { }",
                "Unexpected keyword 'if' (1:11)");

            testFail("function t(true) { }",
                "Unexpected keyword 'true' (1:11)");

            testFail("function t(false) { }",
                "Unexpected keyword 'false' (1:11)");

            testFail("function t(null) { }",
                "Unexpected keyword 'null' (1:11)");

            testFail("function null() { }",
                "Unexpected keyword 'null' (1:9)");

            testFail("function true() { }",
                "Unexpected keyword 'true' (1:9)");

            testFail("function false() { }",
                "Unexpected keyword 'false' (1:9)");

            testFail("function if() { }",
                "Unexpected keyword 'if' (1:9)");

            testFail("a b;",
                "Unexpected token (1:2)");

            testFail("if.a;",
                "Unexpected token (1:2)");

            testFail("a if;",
                "Unexpected token (1:2)");

            testFail("a class;",
                "Unexpected token (1:2)");

            testFail("break\n",
                "Unsyntactic break (1:0)");

            testFail("break 1;",
                "Unexpected token (1:6)");

            testFail("continue\n",
                "Unsyntactic continue (1:0)");

            testFail("continue 2;",
                "Unexpected token (1:9)");

            testFail("throw",
                "Unexpected token (1:5)");

            testFail("throw;",
                "Unexpected token (1:5)");

            testFail("for (var i, i2 in {});",
                "Unexpected token (1:15)");

            testFail("for ((i in {}));",
                "Unexpected token (1:14)");

            testFail("for (i + 1 in {});",
                "Assigning to rvalue (1:5)");

            testFail("for (+i in {});",
                "Assigning to rvalue (1:5)");

            testFail("if(false)",
                "Unexpected token (1:9)");

            testFail("if(false) doThis(); else",
                "Unexpected token (1:24)");

            testFail("do",
                "Unexpected token (1:2)");

            testFail("while(false)",
                "Unexpected token (1:12)");

            testFail("for(;;)",
                "Unexpected token (1:7)");

            testFail("with(x)",
                "Unexpected token (1:7)");

            testFail("try { }",
                "Missing catch or finally clause (1:0)");

            testFail("‿ = 10",
                "Unexpected character '‿' (1:0)");

            testFail("if(true) let a = 1;",
                "Unexpected token (1:13)");

            testFail("switch (c) { default: default: }",
                "Multiple default clauses (1:22)");

            testFail("new X().\"s\"",
                "Unexpected token (1:8)");

            testFail("/*",
                "Unterminated comment (1:0)");

            testFail("/*\n\n\n",
                "Unterminated comment (1:0)");

            testFail("/**",
                "Unterminated comment (1:0)");

            testFail("/*\n\n*",
                "Unterminated comment (1:0)");

            testFail("/*hello",
                "Unterminated comment (1:0)");

            testFail("/*hello  *",
                "Unterminated comment (1:0)");

            testFail("\n]",
                "Unexpected token (2:0)");

            testFail("\r]",
                "Unexpected token (2:0)");

            testFail("\r\n]",
                "Unexpected token (2:0)");

            testFail("\n\r]",
                "Unexpected token (3:0)");

            testFail("//\r\n]",
                "Unexpected token (2:0)");

            testFail("//\n\r]",
                "Unexpected token (3:0)");

            testFail("/a\\\n/",
                "Unterminated regular expression (1:1)");

            testFail("//\r \n]",
                "Unexpected token (3:0)");

            testFail("/*\r\n*/]",
                "Unexpected token (2:2)");

            testFail("/*\n\r*/]",
                "Unexpected token (3:2)");

            testFail("/*\r \n*/]",
                "Unexpected token (3:2)");

            testFail("\\\\",
                "Expecting Unicode escape sequence \\uXXXX (1:1)");

            testFail("\\u005c",
                "Invalid Unicode escape (1:0)");

            testFail("\\x",
                "Expecting Unicode escape sequence \\uXXXX (1:1)");

            testFail("\\u0000",
                "Invalid Unicode escape (1:0)");

            testFail("‌ = []",
                "Unexpected character '‌' (1:0)");

            testFail("‍ = []",
                "Unexpected character '‍' (1:0)");

            testFail("\"\\",
                "Unterminated string constant (1:0)");

            testFail("\"\\u",
                "Bad character escape sequence (1:3)");

            testFail("return",
                "'return' outside of function (1:0)");

            testFail("break",
                "Unsyntactic break (1:0)");

            testFail("continue",
                "Unsyntactic continue (1:0)");

            testFail("switch (x) { default: continue; }",
                "Unsyntactic continue (1:22)");

            testFail("do { x } *",
                "Unexpected token (1:9)");

            testFail("while (true) { break x; }",
                "Unsyntactic break (1:15)");

            testFail("while (true) { continue x; }",
                "Unsyntactic continue (1:15)");

            testFail("x: while (true) { (function () { break x; }); }",
                "Unsyntactic break (1:33)");

            testFail("x: while (true) { (function () { continue x; }); }",
                "Unsyntactic continue (1:33)");

            testFail("x: while (true) { (function () { break; }); }",
                "Unsyntactic break (1:33)");

            testFail("x: while (true) { (function () { continue; }); }",
                "Unsyntactic continue (1:33)");

            testFail("x: while (true) { x: while (true) { } }",
                "Label 'x' is already declared (1:18)");

            testFail("(function () { 'use strict'; delete i; }())",
                "Deleting local variable in strict mode (1:29)");

            testFail("function x() { '\\12'; 'use strict'; }", "Octal literal in strict mode (1:16)");

            testFail("(function () { 'use strict'; with (i); }())",
                "'with' in strict mode (1:29)");

            testFail("function hello() {'use strict'; ({ i: 42, i: 42 }) }",
                "Redefinition of property (1:42)");

            testFail("function hello() {'use strict'; ({ hasOwnProperty: 42, hasOwnProperty: 42 }) }",
                "Redefinition of property (1:55)");

            testFail("function hello() {'use strict'; var eval = 10; }",
                "Binding eval in strict mode (1:36)");

            testFail("function hello() {'use strict'; var arguments = 10; }",
                "Binding arguments in strict mode (1:36)");

            testFail("function hello() {'use strict'; try { } catch (eval) { } }",
                "Binding eval in strict mode (1:47)");

            testFail("function hello() {'use strict'; try { } catch (arguments) { } }",
                "Binding arguments in strict mode (1:47)");

            testFail("function hello() {'use strict'; eval = 10; }",
                "Assigning to eval in strict mode (1:32)");

            testFail("function hello() {'use strict'; arguments = 10; }",
                "Assigning to arguments in strict mode (1:32)");

            testFail("function hello() {'use strict'; ++eval; }",
                "Assigning to eval in strict mode (1:34)");

            testFail("function hello() {'use strict'; --eval; }",
                "Assigning to eval in strict mode (1:34)");

            testFail("function hello() {'use strict'; ++arguments; }",
                "Assigning to arguments in strict mode (1:34)");

            testFail("function hello() {'use strict'; --arguments; }",
                "Assigning to arguments in strict mode (1:34)");

            testFail("function hello() {'use strict'; eval++; }",
                "Assigning to eval in strict mode (1:32)");

            testFail("function hello() {'use strict'; eval--; }",
                "Assigning to eval in strict mode (1:32)");

            testFail("function hello() {'use strict'; arguments++; }",
                "Assigning to arguments in strict mode (1:32)");

            testFail("function hello() {'use strict'; arguments--; }",
                "Assigning to arguments in strict mode (1:32)");

            testFail("function hello() {'use strict'; function eval() { } }",
                "Binding eval in strict mode (1:41)");

            testFail("function hello() {'use strict'; function arguments() { } }",
                "Binding arguments in strict mode (1:41)");

            testFail("function eval() {'use strict'; }",
                "Binding eval in strict mode (1:9)");

            testFail("function arguments() {'use strict'; }",
                "Binding arguments in strict mode (1:9)");

            testFail("function hello() {'use strict'; (function eval() { }()) }",
                "Binding eval in strict mode (1:42)");

            testFail("function hello() {'use strict'; (function arguments() { }()) }",
                "Binding arguments in strict mode (1:42)");

            testFail("(function eval() {'use strict'; })()",
                "Binding eval in strict mode (1:10)");

            testFail("(function arguments() {'use strict'; })()",
                "Binding arguments in strict mode (1:10)");

            testFail("function hello() {'use strict'; ({ s: function eval() { } }); }",
                "Binding eval in strict mode (1:47)");

            testFail("(function package() {'use strict'; })()",
                "Binding package in strict mode (1:10)");

            testFail("function hello() {'use strict'; ({ i: 10, set s(eval) { } }); }",
                "Binding eval in strict mode (1:48)");

            testFail("function hello() {'use strict'; ({ set s(eval) { } }); }",
                "Binding eval in strict mode (1:41)");

            testFail("function hello() {'use strict'; ({ s: function s(eval) { } }); }",
                "Binding eval in strict mode (1:49)");

            testFail("function hello(eval) {'use strict';}",
                "Binding eval in strict mode (1:15)");

            testFail("function hello(arguments) {'use strict';}",
                "Binding arguments in strict mode (1:15)");

            testFail("function hello() { 'use strict'; function inner(eval) {} }",
                "Binding eval in strict mode (1:48)");

            testFail("function hello() { 'use strict'; function inner(arguments) {} }",
                "Binding arguments in strict mode (1:48)");

            testFail("function hello() { 'use strict'; \"\\1\"; }",
                "Octal literal in strict mode (1:34)");

            testFail("function hello() { 'use strict'; \"\\00\"; }",
                "Octal literal in strict mode (1:34)");

            testFail("function hello() { 'use strict'; \"\\000\"; }",
                "Octal literal in strict mode (1:34)");

            testFail("function hello() { 'use strict'; 021; }",
                "Invalid number (1:33)");

            testFail("function hello() { 'use strict'; ({ \"\\1\": 42 }); }",
                "Octal literal in strict mode (1:37)");

            testFail("function hello() { 'use strict'; ({ 021: 42 }); }",
                "Invalid number (1:36)");

            testFail("function hello() { \"use strict\"; function inner() { \"octal directive\\1\"; } }",
                "Octal literal in strict mode (1:68)");

            testFail("function hello() { \"use strict\"; var implements; }",
                "The keyword 'implements' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var interface; }",
                "The keyword 'interface' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var package; }",
                "The keyword 'package' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var private; }",
                "The keyword 'private' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var protected; }",
                "The keyword 'protected' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var public; }",
                "The keyword 'public' is reserved (1:37)");

            testFail("function hello() { \"use strict\"; var static; }",
                "The keyword 'static' is reserved (1:37)");

            testFail("function hello(static) { \"use strict\"; }",
                "Binding static in strict mode (1:15)");

            testFail("function static() { \"use strict\"; }",
                "Binding static in strict mode (1:9)");

            testFail("\"use strict\"; function static() { }",
                "The keyword 'static' is reserved (1:23)");

            testFail("function a(t, t) { \"use strict\"; }",
                "Argument name clash (1:14)");

            testFail("function a(eval) { \"use strict\"; }",
                "Binding eval in strict mode (1:11)");

            testFail("function a(package) { \"use strict\"; }",
                "Binding package in strict mode (1:11)");

            testFail("function a() { \"use strict\"; function b(t, t) { }; }",
                "Argument name clash (1:43)");

            testFail("(function a(t, t) { \"use strict\"; })",
                "Argument name clash (1:15)");

            testFail("function a() { \"use strict\"; (function b(t, t) { }); }",
                "Argument name clash (1:44)");

            testFail("(function a(eval) { \"use strict\"; })",
                "Binding eval in strict mode (1:12)");

            testFail("(function a(package) { \"use strict\"; })",
                "Binding package in strict mode (1:12)");

            testFail("\"use strict\";function foo(){\"use strict\";}function bar(){var v = 015}",
                "Invalid number (1:65)");

            testFail("var this = 10;", "Unexpected keyword 'this' (1:4)");

            testFail("throw\n10;", "Illegal newline after throw (1:5)");

            // ECMA < 6 mode should work as before

            testFail("const a;", "The keyword 'const' is reserved (1:0)");

            testFail("let x;", "Unexpected token (1:4)");

            testFail("const a = 1;", "The keyword 'const' is reserved (1:0)");

            testFail("let a = 1;", "Unexpected token (1:4)");

            testFail("for(const x = 0;;);", "The keyword 'const' is reserved (1:4)");

            testFail("for(let x = 0;;);", "Unexpected token (1:8)");

            testFail("function a(b = c) {}", "Unexpected token (1:13)");

            Test("let++", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                            @operator = Operator.Increment,
                            prefix = false,
                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "let"}
                        }
                    }
                }
            });

            // ECMA 6 support
            Test("let x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            }, new Options {ecmaVersion = 6});

            Test("let x, y;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = null,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "y"},
                                init = null,
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options {ecmaVersion = 6});

            Test("let x = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options {ecmaVersion = 6});

            Test("let eval = 42, arguments = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)), name = "eval"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), name = "arguments"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                },
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options {ecmaVersion = 6});

            Test("let x = 14, y = 3, z = 1977", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 14,
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "y"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 3,
                                    location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "z"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1977,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 27, 27))
                                },
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options {ecmaVersion = 6});

            Test("for(let x = 0;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options {ecmaVersion = 6});

            Test("for(let x = 0, y = 1;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "y"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                    },
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options {ecmaVersion = 6});

            Test("for (let x in list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), name = "process"},
                                arguments = new[]
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options {ecmaVersion = 6});

            Test("const x = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options {ecmaVersion = 6});

            Test("const eval = 42, arguments = 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), name = "eval"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)), name = "arguments"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                },
                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 31, 31))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options {ecmaVersion = 6});

            Test("const x = 14, y = 3, z = 1977", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 14,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "y"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 3,
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                            },
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "z"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1977,
                                    location = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29))
                                },
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options {ecmaVersion = 6});

            testFail("const a;", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Test("for(const x = 0;;);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForStatementNode),
                        init = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                    },
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                }
                            },
                            kind = VariableKind.Const,
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                        },
                        test = null,
                        update = null,
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options {ecmaVersion = 6});

            testFail("for(x of a);", "Unexpected token (1:6)");

            testFail("for(var x of a);", "Unexpected token (1:10)");

            // Assertion Tests
            //test(@"function TestComments() {
            //    // Bear class
            //    function Bear(x, y, z)
            //    {
            //        this.position = [x || 0, y || 0, z || 0]
            //    }
            //
            //    Bear.prototype.roar = function(message) {
            //        return 'RAWWW: ' + message; // Whatever
            //    };
            //
            //    function Cat()
            //    {
            //        /* 1
            //           2
            //           3*/
            //    }
            //
            //    Cat.prototype.roar = function(message) {
            //        return 'MEOOWW: ' + /*stuff*/ message;
            //    };
            //}".Replace("\r\n", "\n"), new Node{}, new Options{
            //  onComment= new object[]{
            //    {type = NodeType.Line, value = " Bear class"},
            //    {type = NodeType.Line, value = " Whatever"},
            //    {type = NodeType.Block,  value = [
            //            " 1",
            //      "       2",
            //      "       3"
            //    ].join('\n')},
            //    {type = NodeType.Block, value = "stuff"}
            //  ]
            //});

            Test("<!--\n;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode {type = typeof(EmptyStatementNode)}
                }
            });

            //test("\nfunction plop() {\n'use strict';\n/* Comment */\n}", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = NodeType.Block,
            //    value = " Comment ",
            //    loc = new SourceLocation(new Position(4, 0, -1), new Position(4, 13, -1))
            //  }}
            //});
            //
            //test("// line comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = NodeType.Line,
            //    value = " line comment",
            //    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            //  }}
            //});

            //test("<!-- HTML comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = NodeType.Line,
            //    value = " HTML comment",
            //    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            //  }}
            //});
            //
            //test(";\n--> HTML comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = NodeType.Line,
            //    value = " HTML comment",
            //    loc = new SourceLocation(new Position(2, 0, -1), new Position(2, 16, -1))
            //  }}
            //});

            //var tokTypes = acorn.tokTypes;
            //
            //test('var x = (1 + 2)', new Node{ }, new Options{
            //  locations = true,
            //  loose: false,
            //  onToken: [
            //    {
            //      type = tokTypes._var,
            //      value = "var",
            //      loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 6, 6))
            //    },
            //    {
            //      type = tokTypes.parenL,
            //      value = undefined,
            //      loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
            //    },
            //    {
            //      type = tokTypes.num,
            //      value = 2,
            //      loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
            //    }
            //  ]
            //});

            Test("function f(f) { 'use strict'; }", new TestNode {type = typeof(ProgramNode)});

            // https://github.com/ternjs/acorn/issues/180
            Test("#!/usr/bin/node\n;", new TestNode {type = typeof(ProgramNode)}, new Options
            {
                allowHashBang = true,
            });

            // https://github.com/ternjs/acorn/issues/204
            Test("(function () {} / 1)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                id = null,
                                parameters = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>()
                                }
                            },
                            @operator = Operator.Division,
                            right = new TestNode {type = typeof(LiteralNode), value = 1}
                        }
                    }
                }
            });

            Test("function f() {} / 1 /", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "f"},
                        parameters = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>()
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode {pattern = " 1 ", flags = ""}
                        }
                    }
                }
            });

            // https://github.com/ternjs/acorn/issues/320
            Test(@"do /x/; while (false);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DoWhileStatementNode),
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(LiteralNode),
                                raw = "/x/",
                                regex = new RegexNode {pattern = "x", flags = ""}
                            }
                        },
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = false,
                            raw = "false"
                        }
                    }
                }
            });

            //var semicolons = []
            //testAssert("var x\nreturn\n10", function() {
            //    var result = semicolons.join(" ");
            //    semicolons.length = 0;
            //    if (result != "5 12 15")
            //        return "Unexpected result for onInsertedSemicolon: " + result;
            //}, new Options{onInsertedSemicolon: function(pos) { semicolons.push(pos); },
            //    allowReturnOutsideFunction = true,
            //    loose: false})
            //
            //var trailingCommas = []
            //testAssert("[1,2,] + {foo: 1,}", function() {
            //    var result = trailingCommas.join(" ");
            //    trailingCommas.length = 0;
            //    if (result != "4 16")
            //        return "Unexpected result for onTrailingComma: " + result;
            //}, new Options{onTrailingComma: function(pos) { trailingCommas.push(pos); },
            //    loose: false})

            // https://github.com/ternjs/acorn/issues/275

            testFail("({ get prop(x) {} })", "getter should have no params (1:11)");
            testFail("({ set prop() {} })", "setter should have exactly one param (1:11)");
            testFail("({ set prop(x, y) {} })", "setter should have exactly one param (1:11)");

            // https://github.com/ternjs/acorn/issues/363

            Test("/[a-z]/gim", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "gim"
                            }
                        }
                    }
                }
            });
            testFail("/[a-z]/u", "Invalid regular expression flag (1:1)");
            testFail("/[a-z]/y", "Invalid regular expression flag (1:1)");
            testFail("/[a-z]/s", "Invalid regular expression flag (1:1)");

            testFail("function(){}", "Unexpected token (1:8)");

            Test("0123. in/foo/i", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                left = new TestNode
                                {
                                    type = typeof(MemberExpressionNode),
                                    location = default,
                                    @object = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 83,
                                        raw = "0123"
                                    },
                                    property = new TestNode {type = typeof(IdentifierNode), name = "in"},
                                    computed = false
                                },
                                @operator = Operator.Division,
                                right = new TestNode {type = typeof(IdentifierNode), name = "foo"}
                            },
                            @operator = Operator.Division,
                            right = new TestNode {type = typeof(IdentifierNode), name = "i"}
                        }
                    }
                }
            });

            Test("0128", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 128,
                            raw = "0128"
                        }
                    }
                }
            });

            testFail("07.5", "Unexpected token (1:2)");

            Test("08.5", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 8.5,
                            raw = "08.5"
                        }
                    }
                }
            });

            Test("undefined", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 8});

            testFail("\\u{74}rue", "Escape sequence in keyword true (1:0)", new Options {ecmaVersion = 6});

            testFail("(x=1)=2", "Parenthesized pattern (1:0)");

            Test("(foo = [])[0] = 4;", new TestNode {type = typeof(ProgramNode)});

            Test("for ((foo = []).bar in {}) {}", new TestNode {type = typeof(ProgramNode)});

            Test("((b), a=1)", new TestNode {type = typeof(ProgramNode)});

            Test("(x) = 1", new TestNode {type = typeof(ProgramNode)});

            testFail("try {} catch (foo) { var foo; }", "Identifier 'foo' has already been declared (1:25)");
            testFail("try {} catch (foo) { let foo; }", "Identifier 'foo' has already been declared (1:25)", new Options {ecmaVersion = 6});
            testFail("try {} catch (foo) { try {} catch (_) { var foo; } }", "Identifier 'foo' has already been declared (1:44)");
            testFail("try {} catch ([foo]) { var foo; }", "Identifier 'foo' has already been declared (1:27)", new Options {ecmaVersion = 6});
            testFail("try {} catch ({ foo }) { var foo; }", "Identifier 'foo' has already been declared (1:29)", new Options {ecmaVersion = 6});
            testFail("try {} catch ([foo, foo]) {}", "Identifier 'foo' has already been declared (1:20)", new Options {ecmaVersion = 6});
            testFail("try {} catch ({ a: foo, b: { c: [foo] } }) {}", "Identifier 'foo' has already been declared (1:33)", new Options {ecmaVersion = 6});
            testFail("let foo; try {} catch (foo) {} let foo;", "Identifier 'foo' has already been declared (1:35)", new Options {ecmaVersion = 6});
            testFail("try {} catch (foo) { function foo() {} }", "Identifier 'foo' has already been declared (1:30)");

            Test("try {} catch (foo) {} var foo;", new TestNode {type = typeof(ProgramNode)});
            Test("try {} catch (foo) {} let foo;", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { { let foo; } }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x() { var foo; } }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x(foo) {} }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Test("'use strict'; let foo = function foo() {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Test("/**/ --> comment\n", new TestNode {type = typeof(ProgramNode)});
            Test("x.class++", new TestNode {type = typeof(ProgramNode)});
        }
    }
}
