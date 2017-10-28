using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void Tests()
        {
            Test("this\n", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ThisExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 5))
            });

            Test("null\n", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\n    42\n\n", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/foobar/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/[a-z]/g", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("(1 + 2 ) * 3", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 1,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 2,
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                            },
                            @operator = "*",
                            right = new Node
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

            Test("(1 + 2 ) * 3", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.ParenthesizedExpression,
                                expression = new Node
                                {
                                    type = NodeType.BinaryExpression,
                                    left = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                    },
                                    @operator = "+",
                                    right = new Node
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
                            right = new Node
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

            Test("(x = 23)", new Node
            {
                body = new List<Node>
                {
                    new Node
                    {
                        expression = new Node
                        {
                            type = NodeType.ParenthesizedExpression,
                            expression = new Node
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new Node
                                {
                                    name = "x",
                                    type = NodeType.Identifier
                                },
                                right = new Node
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

            Test("x = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = [ ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = [ 42 ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new Node
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

            Test("x = [ 42, ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new Node
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

            Test("x = [ ,, 42 ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    null,
                                    null,
                                    new Node
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

            Test("x = [ 1, 2, 3, ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    new Node
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

            Test("x = [ 1, 2,, 3, ]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    null,
                                    new Node
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

            Test("日本語 = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "日本語",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("T‿ = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "T‿",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‌ = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "T‌",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("T‍ = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "T‍",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "ⅣⅡ",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("ⅣⅡ = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "ⅣⅡ",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x = { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x = { answer: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "answer",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                        },
                                        value = new Node
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

            Test("x = { if: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 8, 8))
                                        },
                                        value = new Node
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

            Test("x = { true: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10))
                                        },
                                        value = new Node
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

            Test("x = { false: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                                        },
                                        value = new Node
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

            Test("x = { null: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10))
                                        },
                                        value = new Node
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

            Test("x = { \"answer\": 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "answer",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                        },
                                        value = new Node
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

            Test("x = { x: 1, x: 2 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                        },
                                        kind = "init"
                                    },
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                        },
                                        value = new Node
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

            Test("x = { get width() { return m_width } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "width",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ReturnStatement,
                                                        argument = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "m_width",
                                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                                        },
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

            Test("x = { get undef() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "undef",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get if() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get true() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get false() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get null() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get \"undef\"() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "undef",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { get 10() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
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

            Test("x = { set width(w) { m_width = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "width",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_width",
                                                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32))
                                                            },
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

            Test("x = { set if(w) { m_if = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_if",
                                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26))
                                                            },
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

            Test("x = { set true(w) { m_true = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_true",
                                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30))
                                                            },
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

            Test("x = { set false(w) { m_false = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_false",
                                                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 28, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32))
                                                            },
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

            Test("x = { set null(w) { m_null = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30))
                                                            },
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

            Test("x = { set \"null\"(w) { m_null = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "null",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32))
                                                            },
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

            Test("x = { set 10(w) { m_null = w } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.AssignmentExpression,
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 24, 24))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28))
                                                            },
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

            Test("x = { get: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "get",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                        },
                                        value = new Node
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

            Test("x = { set: 43 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "set",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                        },
                                        value = new Node
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

            Test("/* block comment */ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("42 /*The*/ /*Answer*/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("42 /*the*/ /*answer*/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/* multiline\ncomment\nshould\nbe\nignored */ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/*a\r\nb*/ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/*a\rb*/ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/*a\nb*/ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("/*a\nc*/ 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("// line comment\n42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("42 // line comment", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("// Hello, world!\n42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("// Hello, world!\n", new Node
            {
                type = NodeType.Program,
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("// Hallo, world!\n", new Node
            {
                type = NodeType.Program,
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 0, 17))
            });

            Test("//\n42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("//", new Node
            {
                type = NodeType.Program,
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("// ", new Node
            {
                type = NodeType.Program,
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("/**/42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("// Hello, world!\n\n//   Another hello\n42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("if (x) { // Some comment\ndoThat(); }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                        },
                        consequent = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(2, 0, 25), new Position(2, 6, 31))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(2, 0, 25), new Position(2, 8, 33))
                                    },
                                    loc = new SourceLocation(new Position(2, 0, 25), new Position(2, 9, 34))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(2, 11, 36))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 11, 36))
            });

            Test("switch (answer) { case 42: /* perfect */ bingo() }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new Node
                        {
                            type = NodeType.Identifier,
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "bingo",
                                                loc = new SourceLocation(new Position(1, 41, 41), new Position(1, 46, 46))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                        },
                                        loc = new SourceLocation(new Position(1, 41, 41), new Position(1, 48, 48))
                                    }
                                },
                                test = new Node
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

            Test("0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("3", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("5", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test(".14", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("3.14159", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("6.02214179e+23", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("1.492417830e-10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0x0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0e+100", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0xabc", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0xdef", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0X1A", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0x10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0x100", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0X04", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("02", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("012", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0012", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"\\n\\r\\t\\v\\b\\f\\\\\\'\\\"\\0\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"\\u0061\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"\\x61\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\nworld\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\\nworld\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\02World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\012World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\122World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\0122World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\312World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\412World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\812World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\712World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\0World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\\r\nworld\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("\"Hello\\1World\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("var x = /[a-z]/i", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /[x-z]/i", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /[a-c]/i", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /[P QR]/i", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /foo\\/bar/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /=([^=\\s])+/g", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var x = /[P QR]/\\u0067", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("new Button", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "Button",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("new Button()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "Button",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new new foo", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.NewExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("new new foo()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.NewExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("new foo().bar()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.NewExpression,
                                    callee = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            });

            Test("new foo[bar]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                },
                                computed = true,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            });

            Test("new foo.bar()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("( new foo).bar()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.NewExpression,
                                    callee = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            });

            Test("foo(bar, baz)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                            },
                            arguments = new[]
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "baz",
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("(    foo  )()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.milkyway", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "milkyway",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("universe.milkyway.solarsystem", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "milkyway",
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "solarsystem",
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("universe.milkyway.solarsystem.Earth", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.MemberExpression,
                                    @object = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "universe",
                                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                                    },
                                    property = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "milkyway",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "solarsystem",
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 29, 29))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "Earth",
                                loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 35, 35))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35))
            });

            Test("universe[galaxyName, otherUselessName]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.SequenceExpression,
                                expressions = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "galaxyName",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "otherUselessName",
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 37, 37))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37))
                            },
                            computed = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("universe[galaxyName]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "galaxyName",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19))
                            },
                            computed = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("universe[42].galaxies", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                                },
                                property = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                },
                                computed = true,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "galaxies",
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "galaxies",
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("universe(42).galaxies(14, 3, 77).milkyway", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.MemberExpression,
                                    @object = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "universe",
                                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = NodeType.Literal,
                                                value = 42,
                                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                                    },
                                    property = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "galaxies",
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 14,
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 27, 27))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 77,
                                        loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "milkyway",
                                loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            });

            Test("earth.asia.Indonesia.prepareForElection(2014)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.MemberExpression,
                                    @object = new Node
                                    {
                                        type = NodeType.MemberExpression,
                                        @object = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "earth",
                                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                                        },
                                        property = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "asia",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10))
                                        },
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                                    },
                                    property = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "Indonesia",
                                        loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "prepareForElection",
                                    loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 39, 39))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
                            },
                            arguments = new[]
                            {
                                new Node
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

            Test("universe.if", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "if",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 11, 11))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("universe.true", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "true",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("universe.false", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "false",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("universe.null", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.MemberExpression,
                            @object = new Node
                            {
                                type = NodeType.Identifier,
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                            },
                            property = new Node
                            {
                                type = NodeType.Identifier,
                                name = "null",
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("x++", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("x--", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("eval++", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("eval--", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("arguments++", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("arguments--", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("++x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("--x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            });

            Test("++eval", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("--eval", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("++arguments", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("--arguments", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("+x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "+",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("-x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "-",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("~x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "~",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("!x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("void x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "void",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("delete x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "delete",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("typeof x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "typeof",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("x * y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x / y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x % y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "%",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x - y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x << y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "<<",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >> y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = ">>",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >>> y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = ">>>",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x < y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x > y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = ">",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x <= y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "<=",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x >= y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = ">=",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x in y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "in",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x instanceof y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "instanceof",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x < y < z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "<",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x == y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "==",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x != y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "!=",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x === y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "===",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x !== y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "!==",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("x & y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "&",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x ^ y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "^",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x | y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
            });

            Test("x + y + z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y + z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "-",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y - z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y - z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "-",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y * z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x + y / z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                @operator = "/",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x - y % z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                @operator = "%",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y * z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y / z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x * y % z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "%",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x % y * z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "%",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x << y << z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "<<",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "<<",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x | y | z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "|",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y & z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "&",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x ^ y ^ z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "^",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x & y | z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y ^ z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x | y & z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x || y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x && y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "&&",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("x || y || z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.LogicalExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "||",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x && y && z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.LogicalExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            @operator = "&&",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y && z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = NodeType.LogicalExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                },
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("x || y ^ z", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.LogicalExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            });

            Test("y ? 1 : 2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ConditionalExpression,
                            test = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            consequent = new Node
                            {
                                type = NodeType.Literal,
                                value = 1,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            alternate = new Node
                            {
                                type = NodeType.Literal,
                                value = 2,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("x && y ? 1 : 2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ConditionalExpression,
                            test = new Node
                            {
                                type = NodeType.LogicalExpression,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            consequent = new Node
                            {
                                type = NodeType.Literal,
                                value = 1,
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                            },
                            alternate = new Node
                            {
                                type = NodeType.Literal,
                                value = 2,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("x = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("eval = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                            },
                            right = new Node
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

            Test("arguments = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                            },
                            right = new Node
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

            Test("x *= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "*=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x /= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "/=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x %= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "%=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x += 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "+=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x -= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "-=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x <<= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "<<=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x >>= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = ">>=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x >>>= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = ">>>=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x &= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "&=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x ^= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "^=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("x |= 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "|=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                            },
                            right = new Node
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

            Test("{ foo }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("{ doThis(); doThat(); }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.CallExpression,
                                    callee = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "doThis",
                                        loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 8, 8))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 11, 11))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.CallExpression,
                                    callee = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "doThat",
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                    },
                                    arguments = new Node[0],
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

            Test("{}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>(),
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            });

            Test("var x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
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

            Test("var await", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "await",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
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

            Test("var x, y;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                },
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

            Test("var x = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("var eval = 42, arguments = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                },
                                init = new Node
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

            Test("var x = 14, y = 3, z = 1977", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                },
                                init = new Node
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

            Test("var implements, interface, package", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "implements",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "interface",
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 25, 25))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "package",
                                    loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                },
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

            Test("var private, protected, public, static", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "private",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "protected",
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 22, 22))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "public",
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "static",
                                    loc = new SourceLocation(new Position(1, 32, 32), new Position(1, 38, 38))
                                },
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

            Test(";", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.EmptyStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
            });

            Test("x, y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.SequenceExpression,
                            expressions = new[]
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            });

            Test("\\u0061", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "a",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
            });

            Test("a\\u0061", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "aa",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            });

            Test("if (morning) goodMorning()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                        },
                        consequent = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "goodMorning",
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("if (morning) (function(){})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                        },
                        consequent = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) var x = 0;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                        },
                        consequent = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                    },
                                    init = new Node
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
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("if (morning) function a(){}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                        },
                        consequent = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "a",
                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23))
                            },
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("if (morning) goodMorning(); else goodDay()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Identifier,
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                        },
                        consequent = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "goodMorning",
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                        },
                        alternate = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "goodDay",
                                    loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                            },
                            loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 42, 42))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42))
            });

            Test("do keep(); while (true)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "keep",
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new Node
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

            Test("do keep(); while (true);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "keep",
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 10, 10))
                        },
                        test = new Node
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

            Test("do { x++; y--; } while (x < 10)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "++",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                        },
                                        loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                    },
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "--",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                        },
                                        loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 16, 16))
                        },
                        test = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25))
                            },
                            @operator = "<",
                            right = new Node
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

            Test("{ do { } while (false);false }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.DoWhileStatement,
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                },
                                test = new Node
                                {
                                    type = NodeType.Literal,
                                    value = false,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 23, 23))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
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

            Test("while (true) doSomething()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "doSomething",
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 24, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            });

            Test("while (x < 10) { x++; y--; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 10,
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "++",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                        },
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 21, 21))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.UpdateExpression,
                                        @operator = "--",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23))
                                        },
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

            Test("for(;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = null,
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("for(;;){}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = null,
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("for(x = 0;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("for(var x = 0;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                    },
                                    init = new Node
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
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            });

            Test("for(var x = 0, y = 1;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                    },
                                    init = new Node
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
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = null,
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            });

            Test("for(x = 0; x < 42; x++);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                            },
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("for(x = 0; x < 42; x++) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                        },
                        test = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                        },
                        update = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                            },
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 32, 32), new Position(1, 33, 33))
                                    }
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

            Test("for(x in list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        left = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                                    }
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

            Test("for (var x in list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29))
                                    }
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

            Test("for (var x = 42 in list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    init = new Node
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
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 23, 23))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34))
                                    }
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

            Test("for (var i = function() { return 10 in [] } in list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "i",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new Node[0],
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.ReturnStatement,
                                                    argument = new Node
                                                    {
                                                        type = NodeType.BinaryExpression,
                                                        left = new Node
                                                        {
                                                            type = NodeType.Literal,
                                                            value = 10,
                                                            loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 35, 35))
                                                        },
                                                        @operator = "in",
                                                        right = new Node
                                                        {
                                                            type = NodeType.ArrayExpression,
                                                            elements = new Node[0],
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
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 47, 47), new Position(1, 51, 51))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 53, 53), new Position(1, 60, 60))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 61, 61), new Position(1, 62, 62))
                                    }
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

            Test("while (true) { continue; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
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

            Test("while (true) { continue }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
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

            Test("done: while (true) { continue done }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.WhileStatement,
                            test = new Node
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ContinueStatement,
                                        label = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34))
                                        },
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 34, 34))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 36, 36))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            });

            Test("done: while (true) { continue done; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.WhileStatement,
                            test = new Node
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ContinueStatement,
                                        label = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 34, 34))
                                        },
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 35, 35))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 37, 37))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 37, 37))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            });

            Test("while (true) { break }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
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

            Test("done: while (true) { break done }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.WhileStatement,
                            test = new Node
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.BreakStatement,
                                        label = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 31, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 33, 33))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 33, 33))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            });

            Test("done: while (true) { break done; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.WhileStatement,
                            test = new Node
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.BreakStatement,
                                        label = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 32, 32))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 34, 34))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 34, 34))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("target1: target2: while (true) { continue target1; }", new Node { });
            Test("target1: target2: target3: while (true) { continue target1; }", new Node { });

            Test("(function(){ return })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
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

            Test("(function(){ return; })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
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

            Test("(function(){ return x; })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                                        },
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

            Test("(function(){ return x * y })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = new Node
                                        {
                                            type = NodeType.BinaryExpression,
                                            left = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                                            },
                                            @operator = "*",
                                            right = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "y",
                                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25))
                                            },
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

            Test("with (x) foo = bar", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WithStatement,
                        @object = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12))
                                },
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                },
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            });

            Test("with (x) foo = bar;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WithStatement,
                        @object = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.AssignmentExpression,
                                @operator = "=",
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12))
                                },
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                },
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
            Test("'use\\x20strict'; with (x) foo = bar;", new Node { });

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            Test(@"""use\\x20strict""; with (x) foo = bar;", new Node { });

            Test("with (x) { foo = bar }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WithStatement,
                        @object = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.AssignmentExpression,
                                        @operator = "=",
                                        left = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "foo",
                                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "bar",
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                        },
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

            Test("switch (x) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                        },
                        cases = new Node[0],
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            });

            Test("switch (answer) { case 42: hi(); break; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new Node
                        {
                            type = NodeType.Identifier,
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "hi",
                                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new Node
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new Node
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

            Test("switch (answer) { case 42: hi(); break; default: break }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new Node
                        {
                            type = NodeType.Identifier,
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "hi",
                                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 31, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    new Node
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 39, 39))
                                    }
                                },
                                test = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 39, 39))
                            },
                            new Node
                            {
                                type = NodeType.SwitchCase,
                                sconsequent = new List<Node>
                                {
                                    new Node
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

            Test("start: for (;;) break start", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.ForStatement,
                            init = null,
                            test = null,
                            update = null,
                            fbody = new Node
                            {
                                type = NodeType.BreakStatement,
                                label = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "start",
                                    loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27))
                                },
                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "start",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            });

            Test("start: while (true) break start", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.WhileStatement,
                            test = new Node
                            {
                                type = NodeType.Literal,
                                value = true,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BreakStatement,
                                label = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "start",
                                    loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31))
                                },
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31))
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "start",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            });

            Test("throw x;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ThrowStatement,
                        argument = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            });

            Test("throw x * y", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ThrowStatement,
                        argument = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "y",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                            },
                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            });

            Test("throw { message: \"Error\" }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ThrowStatement,
                        argument = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "message",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15))
                                    },
                                    value = new Node
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

            Test("try { } catch (e) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "e",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            });

            Test("try { } catch (eval) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 24, 24))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("try { } catch (arguments) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29))
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            });

            Test("try { } catch (e) { say(e) }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "e",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25))
                                                }
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
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            });

            Test("try { } finally { cleanup(stuff) }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                        },
                        handler = null,
                        finalizer = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "cleanup",
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "stuff",
                                                loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                    },
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 34, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            });

            Test("try { doThat(); } catch (e) { say(e) }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "e",
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35))
                                                }
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
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            });

            Test("try { doThat(); } catch (e) { say(e) } finally { cleanup(stuff) }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            param = new Node
                            {
                                type = NodeType.Identifier,
                                name = "e",
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26))
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.CallExpression,
                                            callee = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 33, 33))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35))
                                                }
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
                        finalizer = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "cleanup",
                                            loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 56, 56))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "stuff",
                                                loc = new SourceLocation(new Position(1, 57, 57), new Position(1, 62, 62))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                    },
                                    loc = new SourceLocation(new Position(1, 49, 49), new Position(1, 63, 63))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 47, 47), new Position(1, 65, 65))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65))
            });

            Test("debugger;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DebuggerStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            });

            Test("function hello() { sayHi(); }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 24, 24))
                                        },
                                        arguments = new Node[0],
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

            Test("function eval() { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "eval",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            });

            Test("function arguments() { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "arguments",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            });

            Test("function test(t, t) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "test",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "t",
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                            },
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "t",
                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            });

            Test("(function test(t, t) { })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "test",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14))
                            },
                            @params = new[]
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "t",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "t",
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            });

            Test("function eval() { function inner() { \"use strict\" } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "eval",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.FunctionDeclaration,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "inner",
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new Node
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

            Test("function hello(a) { sayHi(); }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                        },
                                        arguments = new Node[0],
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

            Test("function hello(a, b) { sayHi(); }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                            },
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "b",
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        callee = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 28, 28))
                                        },
                                        arguments = new Node[0],
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

            Test("function hello(...rest) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = NodeType.RestElement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "rest",
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
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

            Test("function hello(a, ...rest) { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                            },
                            new Node
                            {
                                type = NodeType.RestElement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "rest",
                                    loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 25, 25))
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
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

            Test("var hi = function() { sayHi() };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                },
                                init = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    id = null,
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new Node
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27))
                                                    },
                                                    arguments = new Node[0],
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

            Test("var hi = function (...r) { sayHi() };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                },
                                init = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    id = null,
                                    @params = new[]
                                    {
                                        new Node
                                        {
                                            type = NodeType.RestElement,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "r",
                                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23))
                                            }
                                        }
                                    },
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new Node
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 32, 32))
                                                    },
                                                    arguments = new Node[0],
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

            Test("var hi = function eval() { };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                },
                                init = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "eval",
                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 22, 22))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>(),
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

            Test("var hi = function arguments() { };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                },
                                init = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "arguments",
                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>(),
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

            Test("var hello = function hi() { sayHi() };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "hello",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
                                init = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "hi",
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new Node
                                                {
                                                    type = NodeType.CallExpression,
                                                    callee = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33))
                                                    },
                                                    arguments = new Node[0],
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

            Test("(function(){})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            });

            Test("{ x\n++y }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "++",
                                    prefix = true,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7))
                                    },
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

            Test("{ x\n--y }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "--",
                                    prefix = true,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(2, 2, 6), new Position(2, 3, 7))
                                    },
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

            Test("var x /* comment */;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
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

            Test("{ var x = 14, y = 3\nz; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclaration,
                                declarations = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.VariableDeclarator,
                                        id = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                        },
                                        init = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 14,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                        },
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                    },
                                    new Node
                                    {
                                        type = NodeType.VariableDeclarator,
                                        id = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                        },
                                        init = new Node
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
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21))
                                },
                                loc = new SourceLocation(new Position(2, 0, 20), new Position(2, 2, 22))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 24))
            });

            Test("while (true) { continue\nthere; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29))
                                    },
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

            Test("while (true) { continue // Comment\nthere; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0, 35), new Position(2, 5, 40))
                                    },
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

            Test("while (true) { continue /* Multiline\nComment */there; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ContinueStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 10, 47), new Position(2, 15, 52))
                                    },
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

            Test("while (true) { break\nthere; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0, 21), new Position(2, 5, 26))
                                    },
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

            Test("while (true) { break // Comment\nthere; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0, 32), new Position(2, 5, 37))
                                    },
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

            Test("while (true) { break /* Multiline\nComment */there; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.WhileStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = true,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.BreakStatement,
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 10, 44), new Position(2, 15, 49))
                                    },
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

            Test("(function(){ return\nx; })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 0, 20), new Position(2, 1, 21))
                                        },
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

            Test("(function(){ return // Comment\nx; })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 0, 31), new Position(2, 1, 32))
                                        },
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

            Test("(function(){ return/* Multiline\nComment */x; })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ReturnStatement,
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19))
                                    },
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 10, 42), new Position(2, 11, 43))
                                        },
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

            Test("{ throw error\nerror; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ThrowStatement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 0, 14), new Position(2, 5, 19))
                                },
                                loc = new SourceLocation(new Position(2, 0, 14), new Position(2, 6, 20))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 22))
            });

            Test("{ throw error// Comment\nerror; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ThrowStatement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 0, 24), new Position(2, 5, 29))
                                },
                                loc = new SourceLocation(new Position(2, 0, 24), new Position(2, 6, 30))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 8, 32))
            });

            Test("{ throw error/* Multiline\nComment */error; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ThrowStatement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13))
                            },
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 10, 36), new Position(2, 15, 41))
                                },
                                loc = new SourceLocation(new Position(2, 10, 36), new Position(2, 16, 42))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 18, 44))
            });

            Test("", new Node
            {
                type = NodeType.Program,
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 0, 0))
            });

            Test("foo: if (true) break foo;", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        fbody = new Node
                        {
                            type = NodeType.IfStatement,
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 25, 25)),
                            test = new Node
                            {
                                type = NodeType.Literal,
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)),
                                value = true
                            },
                            consequent = new Node
                            {
                                type = NodeType.BreakStatement,
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25)),
                                label = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)),
                                    name = "foo"
                                }
                            },
                            alternate = null
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                            name = "foo"
                        }
                    }
                }
            });

            Test("(function () {\n 'use strict';\n '\0';\n}())", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 4, 40)),
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(4, 3, 39)),
                            callee = new Node
                            {
                                type = NodeType.FunctionExpression,
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(4, 1, 37)),
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(4, 1, 37)),
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            loc = new SourceLocation(new Position(2, 1, 16), new Position(2, 14, 29)),
                                            expression = new Node
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(2, 1, 16), new Position(2, 13, 28)),
                                                value = "use strict"
                                            }
                                        },
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            loc = new SourceLocation(new Position(3, 1, 31), new Position(3, 5, 35)),
                                            expression = new Node
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(3, 1, 31), new Position(3, 4, 34)),
                                                value = "\u0000"
                                            }
                                        }
                                    }
                                }
                            },
                            arguments = new Node[0]
                        }
                    }
                }
            });

            Test("123..toString(10)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 123
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "toString"
                                },
                                computed = false
                            },
                            arguments = new[]
                            {
                                new Node
                                {
                                    type = NodeType.Literal,
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("123.+2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Literal,
                                value = 123
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 2
                            }
                        }
                    }
                }
            });

            Test("a\u2028b", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "a"
                        }
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "b"
                        }
                    }
                }
            });

            Test("'a\\u0026b'", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "a\u0026b"
                        }
                    }
                }
            });

            Test("foo: 10; foo: 20;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.Literal,
                                value = 10,
                                raw = "10"
                            }
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "foo"
                        }
                    },
                    new Node
                    {
                        type = NodeType.LabeledStatement,
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.Literal,
                                value = 20,
                                raw = "20"
                            }
                        },
                        label = new Node
                        {
                            type = NodeType.Identifier,
                            name = "foo"
                        }
                    }
                }
            });

            Test("if(1)/  foo/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.IfStatement,
                        test = new Node
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "1"
                        },
                        consequent = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.Literal,
                                raw = "/  foo/"
                            }
                        },
                        alternate = null
                    }
                }
            });

            Test("price_9̶9̶_89", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Identifier,
                            name = "price_9̶9̶_89"
                        }
                    }
                }
            });
            
            // `\0` is valid even in strict mode
            Test("function hello() { 'use strict'; \"\\0\"; }", new Node { });
            
            // option tests
            Test("var a = 1;", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)),
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)),
                                    name = "a"
                                },
                                init = new Node
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

            Test("a.in / b", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a"
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "in"
                                },
                                computed = false
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "b"
                            }
                        }
                    }
                }
            });
            
            // A number of slash-disambiguation corner cases
            Test("return {} / 2", new Node { }, new Options {allowReturnOutsideFunction = true});
            Test("return\n{}\n/foo/", new Node { }, new Options {allowReturnOutsideFunction = true});
            Test("+{} / 2", new Node { });
            Test("{}\n/foo/", new Node { });
            Test("x++\n{}\n/foo/", new Node { });
            Test("{{}\n/foo/}", new Node { });
            Test("while (1) /foo/", new Node { });
            Test("while (1) {} /foo/", new Node { });
            Test("(1) / 2", new Node { });
            Test("({a: [1]}+[]) / 2", new Node { });
            Test("{[1]}\n/foo/", new Node { });
            Test("switch(a) { case 1: {}\n/foo/ }", new Node { });
            Test("({1: {} / 2})", new Node { });
            Test("+x++ / 2", new Node { });
            Test("foo.in\n{}\n/foo/", new Node { });
            Test("var x = function f() {} / 3;", new Node { });
            Test("+function f() {} / 3;", new Node { });
            Test("foo: function x() {} /regexp/", new Node { });
            Test("x = {foo: function x() {} / divide}", new Node { });
            Test("foo; function f() {} /regexp/", new Node { });
            Test("{function f() {} /regexp/}", new Node { });

            Test("{}/=/", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        body = new List<Node>()
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            raw = "/=/"
                        }
                    }
                }
            });

            Test("foo <!--bar\n+baz", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "foo"
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "baz"
                            }
                        }
                    }
                }
            });

            Test("x = y-->10;\n --> nothing", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x"
                            },
                            right = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.UpdateExpression,
                                    @operator = "--",
                                    prefix = false,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y"
                                    }
                                },
                                @operator = ">",
                                right = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 10
                                }
                            }
                        }
                    }
                }
            });

            Test("'use strict';\nobject.static();", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "'use strict'"
                        }
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.MemberExpression,
                                @object = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "object"
                                },
                                property = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "static"
                                },
                                computed = false
                            },
                            arguments = new Node[0]
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

            Test("let++", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                                name = "let"
                            }
                        }
                    }
                }
            });
            
            // ECMA 6 support
            Test("let x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
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

            Test("let x, y;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                },
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

            Test("let x = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
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

            Test("let eval = 42, arguments = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                },
                                init = new Node
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

            Test("let x = 14, y = 3, z = 1977", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20))
                                },
                                init = new Node
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

            Test("for(let x = 0;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                    },
                                    init = new Node
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
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options {ecmaVersion = 6});

            Test("for(let x = 0, y = 1;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13))
                                },
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                    },
                                    init = new Node
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
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options {ecmaVersion = 6});

            Test("for (let x in list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29))
                                    }
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

            Test("const x = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                },
                                init = new Node
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

            Test("const eval = 42, arguments = 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 10, 10))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 26, 26))
                                },
                                init = new Node
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

            Test("const x = 14, y = 3, z = 1977", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 19, 19))
                            },
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22))
                                },
                                init = new Node
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

            Test("for(const x = 0;;);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForStatement,
                        init = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        range = (10, 11)
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        range = (14, 15)
                                    },
                                    range = (10, 15)
                                }
                            },
                            kind = "const",
                            range = (4, 15)
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            range = (18, 19)
                        },
                        range = (0, 19)
                    }
                },
                range = (0, 19)
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

            Test("<!--\n;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
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

            Test("function f(f) { 'use strict'; }", new Node { });

            // https://github.com/ternjs/acorn/issues/180
            Test("#!/usr/bin/node\n;", new Node {}, new Options{
              allowHashBang=  true,
            });

            // https://github.com/ternjs/acorn/issues/204
            Test("(function () {} / 1)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>()
                                }
                            },
                            @operator = "/",
                            right = new Node {type = NodeType.Literal, value = 1}
                        }
                    }
                }
            });

            Test("function f() {} / 1 /", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node {type = NodeType.Identifier, name = "f"},
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>()
                        }
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode{pattern = " 1 ", flags = ""}
                        }
                    }
                }
            });

            // https://github.com/ternjs/acorn/issues/320
            Test(@"do /x/; while (false);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DoWhileStatement,
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.Literal,
                                raw = "/x/",
                                regex = new RegexNode{pattern = "x", flags = ""}
                            }
                        },
                        test = new Node
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

            Test("/[a-z]/gim", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("0123. in/foo/i", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.BinaryExpression,
                                left = new Node
                                {
                                    type = NodeType.MemberExpression,
                                    @object = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 83,
                                        raw = "0123"
                                    },
                                    property = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "in"
                                    },
                                    computed = false
                                },
                                @operator = "/",
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo"
                                }
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "i"
                            }
                        }
                    }
                }
            });

            Test("0128", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 128,
                            raw = "0128"
                        }
                    }
                }
            });

            Test("undefined", new Node { }, new Options {ecmaVersion = 8});

            testFail("\\u{74}rue", "Escape sequence in keyword true (1:0)", new Options { ecmaVersion = 6});

            testFail("(x=1)=2", "Parenthesized pattern (1:0)");

            Test("(foo = [])[0] = 4;", new Node { });

            Test("for ((foo = []).bar in {}) {}", new Node { });

            Test("((b), a=1)", new Node { });

            Test("(x) = 1", new Node { });

            testFail("try {} catch (foo) { var foo; }", "Identifier 'foo' has already been declared (1:25)");
            testFail("try {} catch (foo) { let foo; }", "Identifier 'foo' has already been declared (1:25)", new Options { ecmaVersion = 6});
            testFail("try {} catch (foo) { try {} catch (_) { var foo; } }", "Identifier 'foo' has already been declared (1:44)");
            testFail("try {} catch ([foo]) { var foo; }", "Identifier 'foo' has already been declared (1:27)", new Options { ecmaVersion = 6});
            testFail("try {} catch ({ foo }) { var foo; }", "Identifier 'foo' has already been declared (1:29)", new Options { ecmaVersion = 6});
            testFail("try {} catch ([foo, foo]) {}", "Identifier 'foo' has already been declared (1:20)", new Options { ecmaVersion = 6});
            testFail("try {} catch ({ a: foo, b: { c: [foo] } }) {}", "Identifier 'foo' has already been declared (1:33)", new Options { ecmaVersion = 6});
            testFail("let foo; try {} catch (foo) {} let foo;", "Identifier 'foo' has already been declared (1:35)", new Options { ecmaVersion = 6});
            testFail("try {} catch (foo) { function foo() {} }", "Identifier 'foo' has already been declared (1:30)");

            Test("try {} catch (foo) {} var foo;", new Node { });
            Test("try {} catch (foo) {} let foo;", new Node { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { { let foo; } }", new Node { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x() { var foo; } }", new Node { }, new Options {ecmaVersion = 6});
            Test("try {} catch (foo) { function x(foo) {} }", new Node { }, new Options {ecmaVersion = 6});

            Test("'use strict'; let foo = function foo() {}", new Node { }, new Options {ecmaVersion = 6});

            Test("/**/ --> comment\n", new Node { });
        }
    }
}
