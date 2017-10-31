using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void Tests()
        {
            Test("this\n", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ThisExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 5))
            });

            Test("null\n", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = null,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 5))
            });

            Test("\n    42\n\n", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4, 5), new Position(2, 6, 7))
                        },
                        loc = new SourceLocation(new Position(2, 4, 5), new Position(2, 6, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 0, 9))
            });

            Test("/foobar/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "foobar",
                                flags = ""
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            });

            Test("/[a-z]/g", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "g"
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            });

            Test("(1 + 2 ) * 3", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                },
                                @operator = "+",
                                right = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 2,
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                            },
                            @operator = "*",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 3,
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("(1 + 2 ) * 3", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.ParenthesizedExpression,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.BinaryExpression,
                                    left = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                    },
                                    @operator = "+",
                                    right = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            @operator = "*",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 3,
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                preserveParens = true
            });

            Test("(x = 23)", new BaseNode(default)
            {
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ParenthesizedExpression,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new IdentifierNode(default, "x"),
                                right = new BaseNode(default)
                                {
                                    value = 23,
                                    raw = "23",
                                    type = NodeType.Literal
                                }
                            }
                        },
                        type = NodeType.ExpressionStatement
                    }
                },
                type = NodeType.Program
            }, new Options {preserveParens = true});

            Test("x = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = [ ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = [ 42 ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("x = [ 42, ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x = [ ,, 42 ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    null,
                                    null,
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("x = [ 1, 2, 3, ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = [ 1, 2,, 3, ]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    null,
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("日本語 = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "日本語"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("T‿ = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), "T‿"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‌ = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), "T‌"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‍ = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), "T‍"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), "ⅣⅡ"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2)), "ⅣⅡ"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = { answer: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), "answer"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("x = { if: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8)), "if"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x = { true: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), "true"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = { false: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)), "false"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("x = { null: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), "null"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("x = { \"answer\": 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "answer",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                        },
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("x = { x: 1, x: 2 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                        },
                                        kind = "init"
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "x"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 2,
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("x = { get width() { return m_width } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "width"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ReturnStatement,
                                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)), "m_width"),
                                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 34, 34))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 36, 36))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 36, 36))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 38, 38))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("x = { get undef() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "undef"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("x = { get if() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)), "if"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("x = { get true() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "true"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                            },
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("x = { get false() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "false"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("x = { get null() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "null"),
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                            },
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("x = { get \"undef\"() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "undef",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                                        },
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                            },
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("x = { get 10() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "get",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new BaseNode[0],
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("x = { set width(w) { m_width = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "width"),
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28)), "m_width"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "w"),
                                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set if(w) { m_if = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)), "if"),
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), "m_if"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "w"),
                                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 26, 26))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 26, 26))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28))
                                            },
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 28, 28))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("x = { set true(w) { m_true = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "true"),
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)), "m_true"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), "w"),
                                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                            },
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 34, 34))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("x = { set false(w) { m_false = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "false"),
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28)), "m_false"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "w"),
                                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set null(w) { m_null = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "null"),
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)), "m_null"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), "w"),
                                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                            },
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 34, 34))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("x = { set \"null\"(w) { m_null = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "null",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16))
                                        },
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28)), "m_null"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "w"),
                                                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 34, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 34, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("x = { set 10(w) { m_null = w } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "set",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "w")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24)), "m_null"),
                                                            right = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), "w"),
                                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                            },
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 30, 30))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 32, 32))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            });

            Test("x = { get: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "get"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("x = { set: 43 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "set"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 43,
                                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("/* block comment */ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("42 /*The*/ /*Answer*/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("42 /*the*/ /*answer*/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("/* multiline\ncomment\nshould\nbe\nignored */ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(5, 11, 42), new Position(5, 13, 44))
                        },
                        loc = new SourceLocation(new Position(5, 11, 42), new Position(5, 13, 44))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(5, 13, 44))
            });

            Test("/*a\r\nb*/ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4, 9), new Position(2, 6, 11))
                        },
                        loc = new SourceLocation(new Position(2, 4, 9), new Position(2, 6, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 11))
            });

            Test("/*a\rb*/ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("/*a\nb*/ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("/*a\nc*/ 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                        },
                        loc = new SourceLocation(new Position(2, 4, 8), new Position(2, 6, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 10))
            });

            Test("// line comment\n42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0, 16), new Position(2, 2, 18))
                        },
                        loc = new SourceLocation(new Position(2, 0, 16), new Position(2, 2, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 18))
            });

            Test("42 // line comment", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("// Hello, world!\n42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0, 17), new Position(2, 2, 19))
                        },
                        loc = new SourceLocation(new Position(2, 0, 17), new Position(2, 2, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 19))
            });

            Test("// Hello, world!\n", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("// Hallo, world!\n", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("//\n42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0, 3), new Position(2, 2, 5))
                        },
                        loc = new SourceLocation(new Position(2, 0, 3), new Position(2, 2, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 2, 5))
            });

            Test("//", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("// ", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("/**/42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("// Hello, world!\n\n//   Another hello\n42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(4, 0, 37), new Position(4, 2, 39))
                        },
                        loc = new SourceLocation(new Position(4, 0, 37), new Position(4, 2, 39))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 2, 39))
            });

            Test("if (x) { // Some comment\ndoThat(); }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36)), 
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(2, 0, 25), new Position(2, 6, 31)), "doThat"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(2, 0, 25), new Position(2, 8, 33))
                                    },
                                    loc = new SourceLocation(new Position(2, 0, 25), new Position(2, 9, 34))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(2, 11, 36))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36))
            });

            Test("switch (answer) { case 42: /* perfect */ bingo() }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), "answer"),
                        cases = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 46, 46)), "bingo"),
                                            arguments = new BaseNode[0],
                                            loc = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                        },
                                        loc = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                    }
                                },
                                test = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 48, 48))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 50, 50))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 50, 50))
            });

            Test("0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("3", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 3,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("5", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 5,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test(".14", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0.14,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("3.14159", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 3.14159,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("6.02214179e+23", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 6.02214179e+23,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("1.492417830e-10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 1.49241783e-10,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("0x0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("0e+100", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("0xabc", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2748,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0xdef", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 3567,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0X1A", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 26,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("0x10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 16,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("0x100", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 256,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("0X04", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 4,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("02", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("012", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 10,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("0012", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 10,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("\"Hello\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("\"\\n\\r\\t\\v\\b\\f\\\\\\'\\\"\\0\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "\n\r\t\u000b\b\f\\'\"\u0000",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("\"\\u0061\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "a",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("\"\\x61\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "a",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("\"Hello\\nworld\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\nworld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("\"Hello\\\nworld\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Helloworld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 14))
            });

            Test("\"Hello\\02World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\u0002World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("\"Hello\\012World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\nWorld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\122World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "HelloRWorld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\0122World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\n2World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("\"Hello\\312World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "HelloÊWorld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\412World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello!2World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\812World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello812World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\712World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello92World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("\"Hello\\0World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\u0000World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("\"Hello\\\r\nworld\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Helloworld",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 15))
            });

            Test("\"Hello\\1World\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "Hello\u0001World",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("var x = /[a-z]/i", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[x-z]/i", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[a-c]/i", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("var x = /[P QR]/i", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 17, 17))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("var x = /foo\\/bar/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("var x = /=([^=\\s])+/g", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 21, 21))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("var x = /[P QR]/\\u0067", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 22, 22))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("new Button", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)), "Button"),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("new Button()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)), "Button"),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new new foo", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new BaseNode(default)
                            {
                                type = NodeType.NewExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("new new foo()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new BaseNode(default)
                            {
                                type = NodeType.NewExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("new foo().bar()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                                new BaseNode(default)
                                {
                                    type = NodeType.NewExpression,
                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                                    arguments = new BaseNode[0],
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                                },
                                new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "bar"),
                                false),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("new foo[bar]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new MemberExpressionNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "bar"),
                                true),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new foo.bar()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new MemberExpressionNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)),
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "bar"),
                                false),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("( new foo).bar()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                                new BaseNode(default)
                                {
                                    type = NodeType.NewExpression,
                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "foo"),
                                    arguments = new BaseNode[0],
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                },
                                new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), "bar"),
                                false),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("foo(bar, baz)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new[]
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "bar"),
                                new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "baz")
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("(    foo  )()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), "foo"),
                            arguments = new BaseNode[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.milkyway", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), "milkyway"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("universe.milkyway.solarsystem", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                            new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                                new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), "milkyway"),
                                false),
                            new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29)), "solarsystem"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("universe.milkyway.solarsystem.Earth", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                            new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                                new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                    new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)), "milkyway"),
                                    false),
                                new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29)), "solarsystem"),
                                false),
                            new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 35, 35)), "Earth"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
            });

            Test("universe[galaxyName, otherUselessName]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new BaseNode(default)
                            {
                                type = NodeType.SequenceExpression,
                                expressions = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)), "galaxyName"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 37, 37)), "otherUselessName")
                                },
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37))
                            },
                            true),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("universe[galaxyName]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)), "galaxyName"),
                            true),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("universe[42].galaxies", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                            new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                                new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                },
                                true),
                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), "galaxies"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                            new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                                arguments = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                            },
                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), "galaxies"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies(14, 3, 77).milkyway", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41)),
                            new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                                    new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                                        arguments = new[]
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                value = 42,
                                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                                    },
                                    new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), "galaxies"),
                                    false),
                                arguments = new[]
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 14,
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 27, 27))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 77,
                                        loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                            },
                            new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41)), "milkyway"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            });

            Test("earth.asia.Indonesia.prepareForElection(2014)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)),
                                new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                                    new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                                        new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "earth"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), "asia"),
                                        false),
                                    new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20)), "Indonesia"),
                                    false),
                                new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 39, 39)), "prepareForElection"),
                                false),
                            arguments = new[]
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 2014,
                                    loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 44, 44))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45))
            });

            Test("universe.if", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11)), "if"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("universe.true", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "true"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.false", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "false"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("universe.null", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new MemberExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)), "universe"),
                            new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "null"),
                            false),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("x++", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("x--", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("eval++", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "eval"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("eval--", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "eval"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("arguments++", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), "arguments"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("arguments--", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), "arguments"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("++x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("--x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("++eval", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6)), "eval"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("--eval", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6)), "eval"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("++arguments", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)), "arguments"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("--arguments", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11)), "arguments"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("+x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "+",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("-x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "-",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("~x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "~",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("!x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "!",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("void x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "void",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("delete x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "delete",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("typeof x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "typeof",
                            prefix = true,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "x"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x * y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "*",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x / y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "/",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x % y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "%",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "+",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x - y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "-",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x << y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "<<",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >> y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = ">>",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >>> y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = ">>>",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x < y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "<",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x > y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = ">",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x <= y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "<=",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >= y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = ">=",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x in y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "in",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x instanceof y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "instanceof",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x < y < z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "<",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "<",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x == y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "==",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x != y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "!=",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x === y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "===",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x !== y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "!==",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x & y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "&",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x ^ y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "^",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x | y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "|",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y + z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "+",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "+",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y + z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "-",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "+",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y - z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "+",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "-",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y - z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "-",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "-",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y * z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "+",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                @operator = "*",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y / z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "+",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                @operator = "/",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y % z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "-",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                @operator = "%",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y * z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "*",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "*",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y / z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "*",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "/",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y % z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "*",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "%",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x % y * z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "%",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "*",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x << y << z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "<<",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "<<",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x | y | z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "|",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "|",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y & z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "&",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x ^ y ^ z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "^",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "^",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y | z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "|",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y ^ z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "|",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                @operator = "^",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y & z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "|",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "y"),
                                @operator = "&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "z"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x || y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "||",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x && y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "&&",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x || y || z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.LogicalExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "||",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "||",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x && y && z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.LogicalExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "&&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "&&",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "z"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y && z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "||",
                            right = new BaseNode(default)
                            {
                                type = NodeType.LogicalExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                @operator = "&&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "z"),
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y ^ z", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.LogicalExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            @operator = "||",
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                @operator = "^",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "z"),
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("y ? 1 : 2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new ConditionalExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "y"),
                            new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 1,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 2,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            }),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x && y ? 1 : 2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new ConditionalExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            new BaseNode(default)
                            {
                                type = NodeType.LogicalExpression,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                @operator = "&&",
                                right = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y"),
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 1,
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 2,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                            }),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("eval = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "eval"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("arguments = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), "arguments"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x *= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "*=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x /= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "/=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x %= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "%=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x += 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "+=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x -= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "-=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x <<= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "<<=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x >>= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = ">>=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x >>>= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = ">>>=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x &= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "&=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x ^= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "^=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x |= 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "|=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("{ foo }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), "foo"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("{ doThis(); doThat(); }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.CallExpression,
                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 8, 8)), "doThis"),
                                    arguments = new BaseNode[0],
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.CallExpression,
                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)), "doThat"),
                                    arguments = new BaseNode[0],
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                },
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 21, 21))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("{}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>(),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("var x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("var await", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), "await"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("var x, y;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "y"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("var x = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("var eval = 42, arguments = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)), "eval"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), "arguments"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                },
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("var x = 14, y = 3, z = 1977", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "y"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "z"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 27, 27))
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("var implements, interface, package", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)), "implements"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25)), "interface"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34)), "package"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("var private, protected, public, static", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "private"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22)), "protected"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30)), "public"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 38, 38)), "static"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 32, 32), new Position(1, 38, 38))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test(";", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.EmptyStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x, y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.SequenceExpression,
                            expressions = new[]
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                                new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "y")
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("\\u0061", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6)), "a"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("a\\u0061", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)), "aa"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("if (morning) goodMorning()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "morning"),
                        new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), "goodMorning"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("if (morning) (function(){})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "morning"),
                        new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new BaseNode[0],
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) var x = 0;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "morning"),
                        new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22))
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 23, 23))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("if (morning) function a(){}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "morning"),
                        new BaseNode(default)
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "a"),
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) goodMorning(); else goodDay()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                        new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)), "morning"),
                        new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), "goodMorning"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)), "goodDay"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                            },
                            loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                        })
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42))
            });

            Test("do keep(); while (true)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), "keep"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("do keep(); while (true);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), "keep"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("do { x++; y--; } while (x < 10)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "++",
                                        prefix = false,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                        loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                    },
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "--",
                                        prefix = false,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "y"),
                                        loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 16, 16))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "x"),
                            @operator = "<",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 10,
                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("{ do { } while (false);false }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.DoWhileStatement,
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                },
                                test = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = false,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 23, 23))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = false,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28))
                                },
                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("while (true) doSomething()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24)), "doSomething"),
                                arguments = new BaseNode[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("while (x < 10) { x++; y--; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "x"),
                            @operator = "<",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 10,
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "++",
                                        prefix = false,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "x"),
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 21, 21))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "--",
                                        prefix = false,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "y"),
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 26, 26))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 28, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("for(;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = null,
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("for(;;){}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = null,
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("for(x = 0;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("for(var x = 0;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("for(var x = 0, y = 1;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "y"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                            @operator = "<",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("for(x = 0; x < 42; x++);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                            @operator = "<",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "x"),
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42; x++) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                            @operator = "<",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "x"),
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 33, 33)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 34, 34))
                            },
                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
            });

            Test("for(x in list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                        right = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25))
                            },
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("for (var x in list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("for (var x = 42 in list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 23, 23)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 35, 35))
                            },
                            loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("for (var i = function() { return 10 in [] } in list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "i"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new BaseNode[0],
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.ReturnStatement,
                                                    argument = new BaseNode(default)
                                                    {
                                                        type = NodeType.BinaryExpression,
                                                        left = new BaseNode(default)
                                                        {
                                                            type = NodeType.Literal,
                                                            value = 10,
                                                            loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 35, 35))
                                                        },
                                                        @operator = "in",
                                                        right = new BaseNode(default)
                                                        {
                                                            type = NodeType.ArrayExpression,
                                                            elements = new BaseNode[0],
                                                            loc = new SourceLocation(new Position(1, 39, 39), new Position(1, 41, 41))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 41, 41))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 43, 43))
                                        },
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 43, 43))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 43, 43))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 43, 43))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 47, 47), new Position(1, 51, 51)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 53, 53), new Position(1, 60, 60)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 61, 61), new Position(1, 62, 62)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 53, 53), new Position(1, 63, 63))
                            },
                            loc = new SourceLocation(new Position(1, 53, 53), new Position(1, 64, 64))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 64, 64))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 64, 64))
            });

            Test("while (true) { continue; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("while (true) { continue }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("done: while (true) { continue done }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.WhileStatement,
                            test = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ContinueStatement,
                                        label = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34)), "done"),
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 36, 36))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "done"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("done: while (true) { continue done; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.WhileStatement,
                            test = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ContinueStatement,
                                        label = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34)), "done"),
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 35, 35))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 37, 37))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 37, 37))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "done"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            });

            Test("while (true) { break }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("done: while (true) { break done }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.WhileStatement,
                            test = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31)), "done"),
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 31, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 33, 33))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 33, 33))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "done"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            });

            Test("done: while (true) { break done; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.WhileStatement,
                            test = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31)), "done"),
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 34, 34))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "done"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("target1: target2: while (true) { continue target1; }", new BaseNode(default) { });
            Test("target1: target2: target3: while (true) { continue target1; }", new BaseNode(default) { });

            Test("(function(){ return })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("(function(){ return; })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 20, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 22, 22))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("(function(){ return x; })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "x"),
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("(function(){ return x * y })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = new BaseNode(default)
                                        {
                                            type = NodeType.BinaryExpression,
                                            left = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "x"),
                                            @operator = "*",
                                            right = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "y"),
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                        },
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 27, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("with (x) foo = bar", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WithStatement,
                        @object = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "bar"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("with (x) foo = bar;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WithStatement,
                        @object = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "bar"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            Test("'use\\x20strict'; with (x) foo = bar;", new BaseNode(default) { });

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            Test(@"""use\\x20strict""; with (x) foo = bar;", new BaseNode(default) { });

            Test("with (x) { foo = bar }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WithStatement,
                        @object = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentExpression,
                                        @operator = "=",
                                        left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), "foo"),
                                        right = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), "bar"),
                                        loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            });

            Test("switch (x) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "x"),
                        cases = new BaseNode[0],
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("switch (answer) { case 42: hi(); break; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), "answer"),
                        cases = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)), "hi"),
                                            arguments = new BaseNode[0],
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 39, 39))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            });

            Test("switch (answer) { case 42: hi(); break; default: break }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), "answer"),
                        cases = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)), "hi"),
                                            arguments = new BaseNode[0],
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 39, 39))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 54, 54))
                                    }
                                },
                                test = null,
                                loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 54, 54))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56))
            });

            Test("start: for (;;) break start", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ForStatement,
                            init = null,
                            test = null,
                            update = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BreakStatement,
                                label = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), "start"),
                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "start"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("start: while (true) break start", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.WhileStatement,
                            test = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18))
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BreakStatement,
                                label = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)), "start"),
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31))
                        },
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "start"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("throw x;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ThrowStatement,
                        argument = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("throw x * y", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ThrowStatement,
                        argument = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                            @operator = "*",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "y"),
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("throw { message: \"Error\" }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ThrowStatement,
                        argument = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15)), "message"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = "Error",
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24))
                                    },
                                    kind = "init"
                                }
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("try { } catch (e) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "e"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("try { } catch (eval) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19)), "eval"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 24, 24))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("try { } catch (arguments) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), "arguments"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("try { } catch (e) { say(e) }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                    new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "e"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), "say"),
                                            arguments = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "e")
                                            },
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                        },
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("try { } finally { cleanup(stuff) }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        null,
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "cleanup"),
                                        arguments = new[]
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)), "stuff")
                                        },
                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                    },
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 34, 34))
                        }
                    )
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("try { doThat(); } catch (e) { say(e) }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), "doThat"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "e"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), "say"),
                                            arguments = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), "e")
                                            },
                                            loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                        },
                                        loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 38, 38))
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38))
                        },
                        null)
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("try { doThat(); } catch (e) { say(e) } finally { cleanup(stuff) }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), "doThat"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            param = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "e"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33)), "say"),
                                            arguments = new[]
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), "e")
                                            },
                                            loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                        },
                                        loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 38, 38))
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38))
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 49, 49), new Position(1, 56, 56)), "cleanup"),
                                        arguments = new[]
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 57, 57), new Position(1, 62, 62)), "stuff")
                                        },
                                        loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                    },
                                    loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 47, 47), new Position(1, 65, 65))
                        })
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65))
            });

            Test("debugger;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new DebuggerStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("function hello() { sayHi(); }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "hello"),
                        @params = new BaseNode[0],
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 24, 24)), "sayHi"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                    },
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("function eval() { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "eval"),
                        @params = new BaseNode[0],
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("function arguments() { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18)), "arguments"),
                        @params = new BaseNode[0],
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("function test(t, t) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "test"),
                        @params = new[]
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "t"),
                            new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "t")
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("(function test(t, t) { })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "test"),
                            @params = new[]
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "t"),
                                new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "t")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("function eval() { function inner() { \"use strict\" } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "eval"),
                        @params = new BaseNode[0],
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.FunctionDeclaration,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32)), "inner"),
                                    @params = new BaseNode[0],
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.Literal,
                                                    value = "use strict",
                                                    loc = new SourceLocation(new Position(1, 37, 37), new Position(1, 49, 49))
                                                },
                                                loc = new SourceLocation(new Position(1, 37, 37), new Position(1, 49, 49))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 35, 35), new Position(1, 51, 51))
                                    },
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 51, 51))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 53, 53))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53))
            });

            Test("function hello(a) { sayHi(); }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "hello"),
                        @params = new[]
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "a")
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25)), "sayHi"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                    },
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            });

            Test("function hello(a, b) { sayHi(); }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "hello"),
                        @params = new[]
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "a"),
                            new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "b")
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28)), "sayHi"),
                                        arguments = new BaseNode[0],
                                        loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 30, 30))
                                    },
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 31, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 33, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            });

            Test("function hello(...rest) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "hello"),
                        @params = new[]
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.RestElement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), "rest")
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function hello(a, ...rest) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)), "hello"),
                        @params = new[]
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "a"),
                            new BaseNode(default)
                            {
                                type = NodeType.RestElement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 25, 25)), "rest")
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var hi = function() { sayHi() };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), "hi"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    id = null,
                                    @params = new BaseNode[0],
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)), "sayHi"),
                                                    arguments = new BaseNode[0],
                                                    loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29))
                                                },
                                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 31, 31))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 31, 31))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            });

            Test("var hi = function (...r) { sayHi() };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), "hi"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    id = null,
                                    @params = new[]
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.RestElement,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "r")
                                        }
                                    },
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32)), "sayHi"),
                                                    arguments = new BaseNode[0],
                                                    loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                                },
                                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 36, 36))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 36, 36))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var hi = function eval() { };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), "hi"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22)), "eval"),
                                    @params = new BaseNode[0],
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 28, 28))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 28, 28))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("var hi = function arguments() { };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)), "hi"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)), "arguments"),
                                    @params = new BaseNode[0],
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 33, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("var hello = function hi() { sayHi() };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), "hello"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23)), "hi"),
                                    @params = new BaseNode[0],
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33)), "sayHi"),
                                                    arguments = new BaseNode[0],
                                                    loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35))
                                                },
                                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 37, 37))
                                    },
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 37, 37))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 37, 37))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("(function(){})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("{ x\n++y }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "++",
                                    prefix = true,
                                    argument = new IdentifierNode(new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7)), "y"),
                                    loc = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                                },
                                loc = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
            });

            Test("{ x\n--y }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(default)
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "--",
                                    prefix = true,
                                    argument = new IdentifierNode(new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7)), "y"),
                                    loc = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                                },
                                loc = new SourceLocation(new Position(2, 0, 4), new Position(2, 3, 7))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 9))
            });

            Test("var x /* comment */;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("{ var x = 14, y = 3\nz; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclaration,
                                declarations = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.VariableDeclarator,
                                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                        init = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 14,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.VariableDeclarator,
                                        id = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "y"),
                                        init = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 3,
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        },
                                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                                    }
                                },
                                kind = "var",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 19, 19))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21)), "z"),
                                loc = new SourceLocation(new Position(2, 0, 20), new Position(2, 2, 22))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
            });

            Test("while (true) { continue\nthere; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29)), "there"),
                                    loc = new SourceLocation(new Position(2, 0, 24), new Position(2, 6, 30))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
            });

            Test("while (true) { continue // Comment\nthere; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 35), new Position(2, 5, 40)), "there"),
                                    loc = new SourceLocation(new Position(2, 0, 35), new Position(2, 6, 41))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 43))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 43))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 43))
            });

            Test("while (true) { continue /* Multiline\nComment */there; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 10, 47), new Position(2, 15, 52)), "there"),
                                    loc = new SourceLocation(new Position(2, 10, 47), new Position(2, 16, 53))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 18, 55))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 55))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 55))
            });

            Test("while (true) { break\nthere; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 21), new Position(2, 5, 26)), "there"),
                                    loc = new SourceLocation(new Position(2, 0, 21), new Position(2, 6, 27))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 29))
            });

            Test("while (true) { break // Comment\nthere; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 32), new Position(2, 5, 37)), "there"),
                                    loc = new SourceLocation(new Position(2, 0, 32), new Position(2, 6, 38))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 8, 40))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 40))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 40))
            });

            Test("while (true) { break /* Multiline\nComment */there; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.WhileStatement,
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 10, 44), new Position(2, 15, 49)), "there"),
                                    loc = new SourceLocation(new Position(2, 10, 44), new Position(2, 16, 50))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(2, 18, 52))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 52))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 52))
            });

            Test("(function(){ return\nx; })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21)), "x"),
                                        loc = new SourceLocation(new Position(2, 0, 20), new Position(2, 2, 22))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(2, 4, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(2, 4, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 25))
            });

            Test("(function(){ return // Comment\nx; })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 31), new Position(2, 1, 32)), "x"),
                                        loc = new SourceLocation(new Position(2, 0, 31), new Position(2, 2, 33))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(2, 4, 35))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(2, 4, 35))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 5, 36))
            });

            Test("(function(){ return/* Multiline\nComment */x; })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new BaseNode[0],
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new IdentifierNode(new SourceLocation(new Position(2, 10, 42), new Position(2, 11, 43)), "x"),
                                        loc = new SourceLocation(new Position(2, 10, 42), new Position(2, 12, 44))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(2, 14, 46))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(2, 14, 46))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 15, 47))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 15, 47))
            });

            Test("{ throw error\nerror; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ThrowStatement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), "error"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 14), new Position(2, 5, 19)), "error"),
                                loc = new SourceLocation(new Position(2, 0, 14), new Position(2, 6, 20))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
            });

            Test("{ throw error// Comment\nerror; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ThrowStatement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), "error"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29)), "error"),
                                loc = new SourceLocation(new Position(2, 0, 24), new Position(2, 6, 30))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
            });

            Test("{ throw error/* Multiline\nComment */error; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ThrowStatement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)), "error"),
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new IdentifierNode(new SourceLocation(new Position(2, 10, 36), new Position(2, 15, 41)), "error"),
                                loc = new SourceLocation(new Position(2, 10, 36), new Position(2, 16, 42))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
            });

            Test("", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 0, 0))
            });

            Test("foo: if (true) break foo;", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        fbody = new IfStatementNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 25, 25)),
                            new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)),
                                value = true
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.BreakStatement,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25)),
                                label = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), "foo")
                            },
                            null),
                        label = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo")
                    }
                }
            });

            Test("(function () {\n 'use strict';\n '\0';\n}())", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(4, 3, 39)),
                            callee = new BaseNode(default)
                            {
                                type = NodeType.FunctionExpression,
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(4, 1, 37)),
                                id = null,
                                @params = new BaseNode[0],
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(4, 1, 37)),
                                    body = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.ExpressionStatement,
                                            loc = new SourceLocation(new Position(2, 1, 16), new Position(2, 14, 29)),
                                            expression = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(2, 1, 16), new Position(2, 13, 28)),
                                                value = "use strict"
                                            }
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.ExpressionStatement,
                                            loc = new SourceLocation(new Position(3, 1, 31), new Position(3, 5, 35)),
                                            expression = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(3, 1, 31), new Position(3, 4, 34)),
                                                value = "\u0000"
                                            }
                                        }
                                    }
                                }
                            },
                            arguments = new BaseNode[0]
                        }
                    }
                }
            });

            Test("123..toString(10)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new MemberExpressionNode(default,
                                new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 123
                                },
                                new IdentifierNode(default, "toString"),
                                false),
                            arguments = new[]
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("123.+2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 123
                            },
                            @operator = "+",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 2
                            }
                        }
                    }
                }
            });

            Test("a\u2028b", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(default, "a")
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(default, "b")
                    }
                }
            });

            Test("'a\\u0026b'", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "a\u0026b"
                        }
                    }
                }
            });

            Test("foo: 10; foo: 20;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 10,
                                raw = "10"
                            }
                        },
                        label = new IdentifierNode(default, "foo")
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 20,
                                raw = "20"
                            }
                        },
                        label = new IdentifierNode(default, "foo")
                    }
                }
            });

            Test("if(1)/  foo/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new IfStatementNode(default,
                        new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "1"
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                raw = "/  foo/"
                            }
                        },
                        null)
                }
            });

            Test("price_9̶9̶_89", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(default, "price_9̶9̶_89")
                    }
                }
            });
            
            // `\0` is valid even in strict mode
            Test("function hello() { 'use strict'; \"\\0\"; }", new BaseNode(default) { });
            
            // option tests
            Test("var a = 1;", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)),
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "a"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)),
                                    value = 1,
                                    raw = "1"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                sourceFile = "test.js"
            });

            Test("a.in / b", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new MemberExpressionNode(default,
                                new IdentifierNode(default, "a"),
                                new IdentifierNode(default, "in"),
                                false),
                            @operator = "/",
                            right = new IdentifierNode(default, "b")
                        }
                    }
                }
            });
            
            // A number of slash-disambiguation corner cases
            Test("return {} / 2", new BaseNode(default) { }, new Options {allowReturnOutsideFunction = true});
            Test("return\n{}\n/foo/", new BaseNode(default) { }, new Options {allowReturnOutsideFunction = true});
            Test("+{} / 2", new BaseNode(default) { });
            Test("{}\n/foo/", new BaseNode(default) { });
            Test("x++\n{}\n/foo/", new BaseNode(default) { });
            Test("{{}\n/foo/}", new BaseNode(default) { });
            Test("while (1) /foo/", new BaseNode(default) { });
            Test("while (1) {} /foo/", new BaseNode(default) { });
            Test("(1) / 2", new BaseNode(default) { });
            Test("({a: [1]}+[]) / 2", new BaseNode(default) { });
            Test("{[1]}\n/foo/", new BaseNode(default) { });
            Test("switch(a) { case 1: {}\n/foo/ }", new BaseNode(default) { });
            Test("({1: {} / 2})", new BaseNode(default) { });
            Test("+x++ / 2", new BaseNode(default) { });
            Test("foo.in\n{}\n/foo/", new BaseNode(default) { });
            Test("var x = function f() {} / 3;", new BaseNode(default) { });
            Test("+function f() {} / 3;", new BaseNode(default) { });
            Test("foo: function x() {} /regexp/", new BaseNode(default) { });
            Test("x = {foo: function x() {} / divide}", new BaseNode(default) { });
            Test("foo; function f() {} /regexp/", new BaseNode(default) { });
            Test("{function f() {} /regexp/}", new BaseNode(default) { });

            Test("{}/=/", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>()
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            raw = "/=/"
                        }
                    }
                }
            });

            Test("foo <!--bar\n+baz", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(default, "foo"),
                            @operator = "+",
                            right = new IdentifierNode(default, "baz")
                        }
                    }
                }
            });

            Test("x = y-->10;\n --> nothing", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(default, "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new BaseNode(default)
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "--",
                                    prefix = false,
                                    argument = new IdentifierNode(default, "y")
                                },
                                @operator = ">",
                                right = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("'use strict';\nobject.static();", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "'use strict'"
                        }
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new MemberExpressionNode(default,
                                new IdentifierNode(default, "object"),
                                new IdentifierNode(default, "static"),
                                false),
                            arguments = new BaseNode[0]
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

            Test("let++", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                            @operator = "++",
                            prefix = false,
                            argument = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "let")
                        }
                    }
                }
            });
            
            // ECMA 6 support
            Test("let x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            }, new Options {ecmaVersion = 6});

            Test("let x, y;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "y"),
                                init = null,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options {ecmaVersion = 6});

            Test("let x = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options {ecmaVersion = 6});

            Test("let eval = 42, arguments = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)), "eval"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24)), "arguments"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                },
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options {ecmaVersion = 6});

            Test("let x = 14, y = 3, z = 1977", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "y"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "z"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 27, 27))
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options {ecmaVersion = 6});

            Test("for(let x = 0;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options {ecmaVersion = 6});

            Test("for(let x = 0, y = 1;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "y"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options {ecmaVersion = 6});

            Test("for (let x in list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), "process"),
                                arguments = new[]
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options {ecmaVersion = 6});

            Test("const x = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options {ecmaVersion = 6});

            Test("const eval = 42, arguments = 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10)), "eval"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26)), "arguments"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                },
                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 31, 31))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options {ecmaVersion = 6});

            Test("const x = 14, y = 3, z = 1977", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "y"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "z"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29))
                                },
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options {ecmaVersion = 6});

            testFail("const a;", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Test("for(const x = 0;;);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForStatement,
                        init = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                }
                            },
                            kind = "const",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 15, 15))
                        },
                        test = null,
                        update = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
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

            Test("<!--\n;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.EmptyStatement
                    }
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

            Test("function f(f) { 'use strict'; }", new BaseNode(default) { });

            // https://github.com/ternjs/acorn/issues/180
            Test("#!/usr/bin/node\n;", new BaseNode(default) {}, new Options{
              allowHashBang=  true,
            });

            // https://github.com/ternjs/acorn/issues/204
            Test("(function () {} / 1)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new BaseNode[0],
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>()
                                }
                            },
                            @operator = "/",
                            right = new BaseNode(default) {type = NodeType.Literal, value = 1}
                        }
                    }
                }
            });

            Test("function f() {} / 1 /", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(default, "f"),
                        @params = new BaseNode[0],
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode{pattern = " 1 ", flags = ""}
                        }
                    }
                }
            });

            // https://github.com/ternjs/acorn/issues/320
            Test(@"do /x/; while (false);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                raw = "/x/",
                                regex = new RegexNode{pattern = "x", flags = ""}
                            }
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
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

            Test("/[a-z]/gim", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
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

            Test("0123. in/foo/i", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                left = new MemberExpressionNode(default,
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 83,
                                        raw = "0123"
                                    },
                                    new IdentifierNode(default, "in"),
                                    false),
                                @operator = "/",
                                right = new IdentifierNode(default, "foo")
                            },
                            @operator = "/",
                            right = new IdentifierNode(default, "i")
                        }
                    }
                }
            });

            Test("0128", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 128,
                            raw = "0128"
                        }
                    }
                }
            });

            Test("undefined", new BaseNode(default) { }, new Options {ecmaVersion = 8});

            testFail("\\u{74}rue", "Escape sequence in keyword true (1:0)", new Options { ecmaVersion = 6});

            testFail("(x=1)=2", "Parenthesized pattern (1:0)");

            Test("(foo = [])[0] = 4;", new BaseNode(default) { });

            Test("for ((foo = []).bar in {}) {}", new BaseNode(default) { });

            Test("((b), a=1)", new BaseNode(default) { });

            Test("(x) = 1", new BaseNode(default) { });

            testFail("try {} catch (foo) { var foo; }", "Identifier 'foo' has already been declared (1:25)");
            testFail("try {} catch (foo) { let foo; }", "Identifier 'foo' has already been declared (1:25)", new Options { ecmaVersion = 6});
            testFail("try {} catch (foo) { try {} catch (_) { var foo; } }", "Identifier 'foo' has already been declared (1:44)");
            testFail("try {} catch ([foo]) { var foo; }", "Identifier 'foo' has already been declared (1:27)", new Options { ecmaVersion = 6});
            testFail("try {} catch ({ foo }) { var foo; }", "Identifier 'foo' has already been declared (1:29)", new Options { ecmaVersion = 6});
            testFail("try {} catch ([foo, foo]) {}", "Identifier 'foo' has already been declared (1:20)", new Options { ecmaVersion = 6});
            testFail("try {} catch ({ a: foo, b: { c: [foo] } }) {}", "Identifier 'foo' has already been declared (1:33)", new Options { ecmaVersion = 6});
            testFail("let foo; try {} catch (foo) {} let foo;", "Identifier 'foo' has already been declared (1:35)", new Options { ecmaVersion = 6});
            testFail("try {} catch (foo) { function foo() {} }", "Identifier 'foo' has already been declared (1:30)");

            Test("try {} catch (foo) {} var foo;", new BaseNode(default) { });
            Test("try {} catch (foo) {} let foo;", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { { let foo; } }", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x() { var foo; } }", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x(foo) {} }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("'use strict'; let foo = function foo() {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("/**/ --> comment\n", new BaseNode(default) { });
            Test("x.class++", new BaseNode(default) { });
        }
    }
}
