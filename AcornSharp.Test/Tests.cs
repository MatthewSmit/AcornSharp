using System.Collections.Generic;
using Xunit;

namespace AcornSharp.Test
{
    public class Tests
    {
        private static void Test(string code, Node expected, Options options = null)
        {
            options = options ?? new Options();
            if (options.ecmaVersion == 0) options.ecmaVersion = 5;

            var ast = Acorn.Parse(code, options);
            Assert.True(expected.TestEquals(ast));
        }

        private static void TestFail(string code, string message, Options options = null)
        {
            //TODO
        }

            [Fact] public void Test1() { Test("this\n", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "ThisExpression",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 0))
            }); }

            [Fact] public void Test1() { Test("null\n", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = null,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 0))
            }); }

            [Fact] public void Test1() { Test("\n    42\n\n", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(4, 0))
            }); }

            [Fact] public void Test1() { Test("/foobar/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            regex = new RegexNode
                            {
                                pattern = "foobar",
                                flags = ""
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("/[a-z]/g", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "g"
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("(1 + 2 ) * 3", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Literal",
                                    value = 1,
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = "Literal",
                                    value = 2,
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 6))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Literal",
                                value = 3,
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }); }

            [Fact] public void Test1() { Test("(1 + 2 ) * 3", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "ParenthesizedExpression",
                                expression = new Node
                                {
                                    type = "BinaryExpression",
                                    left = new Node
                                    {
                                        type = "Literal",
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                    },
                                    @operator = "+",
                                    right = new Node
                                    {
                                        type = "Literal",
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Literal",
                                value = 3,
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                preserveParens = true
            }); }

            [Fact] public void Test1() { Test("(x = 23)", new Node
            {
                body = new List<Node>
                {
                    new Node
                    {
                        expression = new Node
                        {
                            type = "ParenthesizedExpression",
                            expression = new Node
                            {
                                type = "AssignmentExpression",
                                @operator = "=",
                                left = new Node
                                {
                                    name = "x",
                                    type = "Identifier"
                                },
                                right = new Node
                                {
                                    value = 23,
                                    raw = "23",
                                    type = "Literal"
                                }
                            }
                        },
                        type = "ExpressionStatement"
                    }
                },
                type = "Program"
            }, new Options { preserveParens = true }); }

            [Fact] public void Test1() { Test("x = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x = [ ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x = [ 42 ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }); }

            [Fact] public void Test1() { Test("x = [ 42, ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("x = [ ,, 42 ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new[]
                                {
                                    null,
                                    null,
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("x = [ 1, 2, 3, ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                    },
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("x = [ 1, 2,, 3, ]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                    },
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 2,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    null,
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }); }

            [Fact] public void Test1() { Test("日本語 = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "日本語",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("T‿ = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "T‿",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("T‌ = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "T‌",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("T‍ = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "T‍",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("ⅣⅡ = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "ⅣⅡ",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("ⅣⅡ = []", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "ⅣⅡ",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                            },
                            right = new Node
                            {
                                type = "ArrayExpression",
                                elements = new Node[0],
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x = {}", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x = { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x = { answer: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "answer",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }); }

            [Fact] public void Test1() { Test("x = { if: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("x = { true: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 10))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("x = { false: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 11))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }); }

            [Fact] public void Test1() { Test("x = { null: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 10))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("x = { \"answer\": 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Literal",
                                            value = "answer",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 18))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 20))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }); }

            [Fact] public void Test1() { Test("x = { x: 1, x: 2 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 1,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                        },
                                        kind = "init"
                                    },
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 2,
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }); }

            [Fact] public void Test1() { Test("x = { get width() { return m_width } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "width",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ReturnStatement",
                                                        argument = new Node
                                                        {
                                                            type = "Identifier",
                                                            name = "m_width",
                                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 34))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 36))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 36))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 38))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }); }

            [Fact] public void Test1() { Test("x = { get undef() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "undef",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("x = { get if() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 19))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }); }

            [Fact] public void Test1() { Test("x = { get true() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 19))
                                            },
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 19))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 21))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("x = { get false() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 15))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("x = { get null() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 19))
                                            },
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 19))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 21))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("x = { get \"undef\"() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Literal",
                                            value = "undef",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 17))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                                            },
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 22))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 24))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }); }

            [Fact] public void Test1() { Test("x = { get 10() {} }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Literal",
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        kind = "get",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new Node[0],
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 19))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }); }

            [Fact] public void Test1() { Test("x = { set width(w) { m_width = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "width",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 15))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_width",
                                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31), new Position(1, 32))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 19), new Position(1, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            }); }

            [Fact] public void Test1() { Test("x = { set if(w) { m_if = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "if",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_if",
                                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 22))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 26))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 26))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 28))
                                            },
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 28))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }); }

            [Fact] public void Test1() { Test("x = { set true(w) { m_true = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "true",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_true",
                                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 26))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 29), new Position(1, 30))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                                            },
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 32))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 34))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

            [Fact] public void Test1() { Test("x = { set false(w) { m_false = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "false",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 15))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_false",
                                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31), new Position(1, 32))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 19), new Position(1, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            }); }

            [Fact] public void Test1() { Test("x = { set null(w) { m_null = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "null",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 26))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 29), new Position(1, 30))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                                            },
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 32))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 34))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

            [Fact] public void Test1() { Test("x = { set \"null\"(w) { m_null = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Literal",
                                            value = "null",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 16))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 28))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 31), new Position(1, 32))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 22), new Position(1, 32))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 32))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 34))
                                            },
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 34))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 36))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            }); }

            [Fact] public void Test1() { Test("x = { set 10(w) { m_null = w } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Literal",
                                            value = 10,
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        kind = "set",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            id = null,
                                            @params = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "w",
                                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        expression = new Node
                                                        {
                                                            type = "AssignmentExpression",
                                                            @operator = "=",
                                                            left = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "m_null",
                                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 24))
                                                            },
                                                            right = new Node
                                                            {
                                                                type = "Identifier",
                                                                name = "w",
                                                                loc = new SourceLocation(new Position(1, 27), new Position(1, 28))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 28))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 28))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 30))
                                            },
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 30))
                                        }
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 32))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
            }); }

            [Fact] public void Test1() { Test("x = { get: 42 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "get",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 42,
                                            loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }); }

            [Fact] public void Test1() { Test("x = { set: 43 }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "ObjectExpression",
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Property",
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            name = "set",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                        },
                                        value = new Node
                                        {
                                            type = "Literal",
                                            value = 43,
                                            loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                                        },
                                        kind = "init"
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }); }

            [Fact] public void Test1() { Test("/* block comment */ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("42 /*The*/ /*Answer*/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("42 /*the*/ /*answer*/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("/* multiline\ncomment\nshould\nbe\nignored */ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(5, 11), new Position(5, 13))
                        },
                        loc = new SourceLocation(new Position(5, 11), new Position(5, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(5, 13))
            }); }

            [Fact] public void Test1() { Test("/*a\r\nb*/ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("/*a\rb*/ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("/*a\nb*/ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("/*a\nc*/ 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(2, 4), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("// line comment\n42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                        },
                        loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 2))
            }); }

            [Fact] public void Test1() { Test("42 // line comment", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }); }

            [Fact] public void Test1() { Test("// Hello, world!\n42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                        },
                        loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 2))
            }); }

            [Fact] public void Test1() { Test("// Hello, world!\n", new Node
            {
                type = "Program",
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0), new Position(2, 0))
            }); }

            [Fact] public void Test1() { Test("// Hallo, world!\n", new Node
            {
                type = "Program",
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0), new Position(2, 0))
            }); }

            [Fact] public void Test1() { Test("//\n42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                        },
                        loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 2))
            }); }

            [Fact] public void Test1() { Test("//", new Node
            {
                type = "Program",
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("// ", new Node
            {
                type = "Program",
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("/**/42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("// Hello, world!\n\n//   Another hello\n42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(4, 0), new Position(4, 2))
                        },
                        loc = new SourceLocation(new Position(4, 0), new Position(4, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(4, 2))
            }); }

        [Fact]
        public void Test1()
        {
            Test("if (x) { // Some comment\ndoThat(); } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                        },
                        consequent = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 8))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 9))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 7), new Position(2, 11))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 11))
            });
        }

        [Fact] public void Test1() { Test("switch (answer) { case 42: /* perfect */ bingo() }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "SwitchStatement",
                        discriminant = new Node
                        {
                            type = "Identifier",
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = "SwitchCase",
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "bingo",
                                                loc = new SourceLocation(new Position(1, 41), new Position(1, 46))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 41), new Position(1, 48))
                                        },
                                        loc = new SourceLocation(new Position(1, 41), new Position(1, 48))
                                    }
                                },
                                test = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 48))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 50))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 50))
            }); }

            [Fact] public void Test1() { Test("0", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
            }); }

            [Fact] public void Test1() { Test("3", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 3,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
            }); }

            [Fact] public void Test1() { Test("5", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 5,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
            }); }

            [Fact] public void Test1() { Test("42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 42,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test(".14", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 0.14,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("3.14159", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 3.14159,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("6.02214179e+23", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 6.02214179e+23,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("1.492417830e-10", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 1.49241783e-10,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }); }

            [Fact] public void Test1() { Test("0x0", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("0e+100", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 0,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("0xabc", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 2748,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("0xdef", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 3567,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("0X1A", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 26,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }); }

            [Fact] public void Test1() { Test("0x10", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 16,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }); }

            [Fact] public void Test1() { Test("0x100", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 256,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("0X04", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 4,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }); }

            [Fact] public void Test1() { Test("02", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 2,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("012", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 10,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("0012", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 10,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }); }

            [Fact] public void Test1() { Test("\"Hello\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("\"\\n\\r\\t\\v\\b\\f\\\\\\'\\\"\\0\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "\n\r\t\u000b\b\f\\'\"\u0000",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("\"\\u0061\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "a",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("\"\\x61\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "a",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\nworld\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\nworld",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\\nworld\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Helloworld",
                            loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\02World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\u0002World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\012World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\nWorld",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\122World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "HelloRWorld",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\0122World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\n2World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\312World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "HelloÊWorld",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\412World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello!2World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\812World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello812World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\712World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello92World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\0World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\u0000World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\\r\nworld\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Helloworld",
                            loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 6))
            }); }

            [Fact] public void Test1() { Test("\"Hello\\1World\"", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "Hello\u0001World",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("var x = /[a-z]/i", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("var x = /[x-z]/i", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("var x = /[a-c]/i", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("var x = /[P QR]/i", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 17))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }); }

            [Fact] public void Test1() { Test("var x = /foo\\/bar/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 18))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 18))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }); }

            [Fact] public void Test1() { Test("var x = /=([^=\\s])+/g", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 21))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 21))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("var x = /[P QR]/\\u0067", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 22))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("new Button", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "Identifier",
                                name = "Button",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }); }

            [Fact] public void Test1() { Test("new Button()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "Identifier",
                                name = "Button",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }); }

            [Fact] public void Test1() { Test("new new foo", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "NewExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 11))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("new new foo()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "NewExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 11))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("new foo().bar()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "NewExpression",
                                    callee = new Node
                                    {
                                        type = "Identifier",
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 13))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }); }

            [Fact] public void Test1() { Test("new foo[bar]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 11))
                                },
                                computed = true,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }); }

            [Fact] public void Test1() { Test("new foo.bar()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "NewExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 11))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("( new foo).bar()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "NewExpression",
                                    callee = new Node
                                    {
                                        type = "Identifier",
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 9))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 14))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }); }

            [Fact] public void Test1() { Test("foo(bar, baz)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "Identifier",
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            arguments = new[]
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                new Node
                                {
                                    type = "Identifier",
                                    name = "baz",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("(    foo  )()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "Identifier",
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 8))
                            },
                            arguments = new Node[0],
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("universe.milkyway", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "milkyway",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 17))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }); }

            [Fact] public void Test1() { Test("universe.milkyway.solarsystem", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "milkyway",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 17))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "solarsystem",
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 29))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }); }

            [Fact] public void Test1() { Test("universe.milkyway.solarsystem.Earth", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "MemberExpression",
                                    @object = new Node
                                    {
                                        type = "Identifier",
                                        name = "universe",
                                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                                    },
                                    property = new Node
                                    {
                                        type = "Identifier",
                                        name = "milkyway",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 17))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "solarsystem",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 29))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "Earth",
                                loc = new SourceLocation(new Position(1, 30), new Position(1, 35))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 35))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 35))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 35))
            }); }

            [Fact] public void Test1() { Test("universe[galaxyName, otherUselessName]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "SequenceExpression",
                                expressions = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "galaxyName",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 19))
                                    },
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "otherUselessName",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 37))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 37))
                            },
                            computed = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }); }

            [Fact] public void Test1() { Test("universe[galaxyName]", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "galaxyName",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 19))
                            },
                            computed = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }); }

            [Fact] public void Test1() { Test("universe[42].galaxies", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                                },
                                property = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
                                },
                                computed = true,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "galaxies",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 21))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("universe(42).galaxies", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "universe",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "galaxies",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 21))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("universe(42).galaxies(14, 3, 77).milkyway", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "MemberExpression",
                                    @object = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "universe",
                                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = "Literal",
                                                value = 42,
                                                loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                                    },
                                    property = new Node
                                    {
                                        type = "Identifier",
                                        name = "galaxies",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 21))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 14,
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 24))
                                    },
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 3,
                                        loc = new SourceLocation(new Position(1, 26), new Position(1, 27))
                                    },
                                    new Node
                                    {
                                        type = "Literal",
                                        value = 77,
                                        loc = new SourceLocation(new Position(1, 29), new Position(1, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "milkyway",
                                loc = new SourceLocation(new Position(1, 33), new Position(1, 41))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
            }); }

            [Fact] public void Test1() { Test("earth.asia.Indonesia.prepareForElection(2014)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "MemberExpression",
                                    @object = new Node
                                    {
                                        type = "MemberExpression",
                                        @object = new Node
                                        {
                                            type = "Identifier",
                                            name = "earth",
                                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                                        },
                                        property = new Node
                                        {
                                            type = "Identifier",
                                            name = "asia",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 10))
                                        },
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                                    },
                                    property = new Node
                                    {
                                        type = "Identifier",
                                        name = "Indonesia",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 20))
                                    },
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "prepareForElection",
                                    loc = new SourceLocation(new Position(1, 21), new Position(1, 39))
                                },
                                computed = false,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 39))
                            },
                            arguments = new[]
                            {
                                new Node
                                {
                                    type = "Literal",
                                    value = 2014,
                                    loc = new SourceLocation(new Position(1, 40), new Position(1, 44))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 45))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 45))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 45))
            }); }

            [Fact] public void Test1() { Test("universe.if", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "if",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("universe.true", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "true",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("universe.false", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "false",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("universe.null", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "MemberExpression",
                            @object = new Node
                            {
                                type = "Identifier",
                                name = "universe",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                            },
                            property = new Node
                            {
                                type = "Identifier",
                                name = "null",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                            },
                            computed = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

            [Fact] public void Test1() { Test("x++", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("x--", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("eval++", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("eval--", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("arguments++", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("arguments--", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("++x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("--x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }); }

            [Fact] public void Test1() { Test("++eval", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("--eval", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("++arguments", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("--arguments", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "--",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("+x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "+",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("-x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "-",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("~x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "~",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("!x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("void x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "void",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("delete x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "delete",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("typeof x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            @operator = "typeof",
                            prefix = true,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("x * y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x / y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x % y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "%",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x + y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x - y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x << y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "<<",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x >> y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = ">>",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x >>> y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = ">>>",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x < y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x > y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = ">",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x <= y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "<=",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x >= y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = ">=",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x in y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "in",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x instanceof y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "instanceof",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("x < y < z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "<",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x == y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "==",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x != y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "!=",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x === y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "===",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x !== y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "!==",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x & y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "&",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x ^ y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "^",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x | y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("x + y + z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x - y + z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "-",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x + y - z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "+",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x - y - z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "-",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x + y * z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x + y / z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                @operator = "/",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x - y % z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "-",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                @operator = "%",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x * y * z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x * y / z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x * y % z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "*",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "%",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x % y * z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "%",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x << y << z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "<<",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            @operator = "<<",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("x | y | z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "|",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x & y & z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "&",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x ^ y ^ z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "^",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x & y | z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x | y ^ z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x | y & z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "|",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                @operator = "&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x || y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x && y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "&&",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("x || y || z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "LogicalExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "||",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("x && y && z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "LogicalExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            @operator = "&&",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "z",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("x || y && z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = "LogicalExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                },
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("x || y ^ z", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "LogicalExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            @operator = "||",
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                @operator = "^",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }); }

            [Fact] public void Test1() { Test("y ? 1 : 2", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "ConditionalExpression",
                            test = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            consequent = new Node
                            {
                                type = "Literal",
                                value = 1,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            alternate = new Node
                            {
                                type = "Literal",
                                value = 2,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x && y ? 1 : 2", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "ConditionalExpression",
                            test = new Node
                            {
                                type = "LogicalExpression",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                @operator = "&&",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            consequent = new Node
                            {
                                type = "Literal",
                                value = 1,
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                            },
                            alternate = new Node
                            {
                                type = "Literal",
                                value = 2,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("x = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("eval = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("arguments = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("x *= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "*=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x /= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "/=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x %= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "%=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x += 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "+=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x -= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "-=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x <<= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "<<=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("x >>= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = ">>=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 8))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("x >>>= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = ">>>=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("x &= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "&=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x ^= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "^=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("x |= 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "|=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("{ foo }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 5))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 5))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

        [Fact]
        public void Test1()
        {
            Test("{ doThis(); } doThat(); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "CallExpression",
                                    callee = new Node
                                    {
                                        type = "Identifier",
                                        name = "doThis",
                                        loc = new SourceLocation(new Position(1, 2), new Position(1, 8))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 11))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "CallExpression",
                                    callee = new Node
                                    {
                                        type = "Identifier",
                                        name = "doThat",
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 18))
                                    },
                                    arguments = new Node[0],
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 20))
                                },
                                loc = new SourceLocation(new Position(1, 12), new Position(1, 21))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            });
        }

        [Fact] public void Test1() { Test("{}", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>(),
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }); }

            [Fact] public void Test1() { Test("var x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }); }

            [Fact] public void Test1() { Test("var await", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "await",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("var x, y;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

            [Fact] public void Test1() { Test("var x = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }); }

            [Fact] public void Test1() { Test("var eval = 42, arguments = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 8))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                },
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 29))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }); }

            [Fact] public void Test1() { Test("var x = 14, y = 3, z = 1977", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 27))
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 27))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }); }

            [Fact] public void Test1() { Test("var implements, interface, package", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "implements",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "interface",
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 25))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 16), new Position(1, 25))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "package",
                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

            [Fact] public void Test1() { Test("var private, protected, public, static", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "private",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "protected",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 22))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 22))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "public",
                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 30))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 24), new Position(1, 30))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "static",
                                    loc = new SourceLocation(new Position(1, 32), new Position(1, 38))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 32), new Position(1, 38))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }); }

            [Fact] public void Test1() { Test(";", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "EmptyStatement",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
            }); }

            [Fact] public void Test1() { Test("x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
            }); }

            [Fact] public void Test1() { Test("x, y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "SequenceExpression",
                            expressions = new[]
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                },
                                new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }); }

            [Fact] public void Test1() { Test("\\u0061", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "a",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
            }); }

            [Fact] public void Test1() { Test("a\\u0061", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "aa",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }); }

            [Fact] public void Test1() { Test("if (morning) goodMorning()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                        },
                        consequent = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "goodMorning",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }); }

            [Fact] public void Test1() { Test("if (morning) (function(){})", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                        },
                        consequent = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "FunctionExpression",
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 26))
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 27))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }); }

            [Fact] public void Test1() { Test("if (morning) var x = 0;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                        },
                        consequent = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 22))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 23))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }); }

            [Fact] public void Test1() { Test("if (morning) function a(){}", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                        },
                        consequent = new Node
                        {
                            type = "FunctionDeclaration",
                            id = new Node
                            {
                                type = "Identifier",
                                name = "a",
                                loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                            },
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 27))
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 27))
                        },
                        alternate = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }); }

        [Fact]
        public void Test1()
        {
            Test("if (morning) goodMorning(); } else goodDay()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Identifier",
                            name = "morning",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                        },
                        consequent = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "goodMorning",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 27))
                        },
                        alternate = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "goodDay",
                                    loc = new SourceLocation(new Position(1, 33), new Position(1, 40))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 33), new Position(1, 42))
                            },
                            loc = new SourceLocation(new Position(1, 33), new Position(1, 42))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 42))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 42))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("do keep(); } while (true)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "DoWhileStatement",
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "keep",
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 7))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 3), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3), new Position(1, 10))
                        },
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("do keep(); } while (true);", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "DoWhileStatement",
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "keep",
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 7))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 3), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 3), new Position(1, 10))
                        },
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            });
        }

        [Fact] public void Test1() { Test("do { x++; y--; } while (x < 10)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "DoWhileStatement",
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "UpdateExpression",
                                        @operator = "++",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        },
                                        loc = new SourceLocation(new Position(1, 5), new Position(1, 8))
                                    },
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 9))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "UpdateExpression",
                                        @operator = "--",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = "Identifier",
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                        },
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 3), new Position(1, 16))
                        },
                        test = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 24), new Position(1, 25))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Literal",
                                value = 10,
                                loc = new SourceLocation(new Position(1, 28), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 24), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }); }

        [Fact]
        public void Test1()
        {
            Test("{ do { } while (false); }false }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "DoWhileStatement",
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 8))
                                },
                                test = new Node
                                {
                                    type = "Literal",
                                    value = false,
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 21))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 23))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Literal",
                                    value = false,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 28))
                                },
                                loc = new SourceLocation(new Position(1, 23), new Position(1, 28))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            });
        }

        [Fact] public void Test1() { Test("while (true) doSomething()", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "doSomething",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 24))
                                },
                                arguments = new Node[0],
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }); }

            [Fact] public void Test1() { Test("while (x < 10) { x++; y--; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Literal",
                                value = 10,
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                            },
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 13))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "UpdateExpression",
                                        @operator = "++",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                        },
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 21))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "UpdateExpression",
                                        @operator = "--",
                                        prefix = false,
                                        argument = new Node
                                        {
                                            type = "Identifier",
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                        },
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 22), new Position(1, 26))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }); }

        [Fact]
        public void Test1()
        {
            Test("for(;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = null,
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            });
        }

        [Fact] public void Test1() { Test("for(;;){}", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = null,
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

        [Fact]
        public void Test1()
        {
            Test("for(x = 0;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(var x = 0;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(var x = 0, y = 1;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                },
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 20))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(x = 0; x < 42;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                        },
                        test = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11), new Position(1, 17))
                        },
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(x = 0; x < 42; x++); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                        },
                        test = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11), new Position(1, 17))
                        },
                        update = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                            },
                            loc = new SourceLocation(new Position(1, 19), new Position(1, 22))
                        },
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(x = 0; x < 42; x++) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            right = new Node
                            {
                                type = "Literal",
                                value = 0,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                        },
                        test = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            @operator = "<",
                            right = new Node
                            {
                                type = "Literal",
                                value = 42,
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 11), new Position(1, 17))
                        },
                        update = new Node
                        {
                            type = "UpdateExpression",
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                            },
                            loc = new SourceLocation(new Position(1, 19), new Position(1, 22))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 31))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 32), new Position(1, 33))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 24), new Position(1, 34))
                            },
                            loc = new SourceLocation(new Position(1, 24), new Position(1, 35))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 35))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 35))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for(x in list) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForInStatement",
                        left = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                        },
                        right = new Node
                        {
                            type = "Identifier",
                            name = "list",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 22))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 25))
                            },
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for (var x in list) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForInStatement",
                        left = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                        },
                        right = new Node
                        {
                            type = "Identifier",
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 18))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for (var x = 42 in list) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForInStatement",
                        left = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 42,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 15))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 15))
                        },
                        right = new Node
                        {
                            type = "Identifier",
                            name = "list",
                            loc = new SourceLocation(new Position(1, 19), new Position(1, 23))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 25), new Position(1, 32))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 33), new Position(1, 34))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 35))
                            },
                            loc = new SourceLocation(new Position(1, 25), new Position(1, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("for (var i = function() { return 10 in [] } in list) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForInStatement",
                        left = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "i",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = new Node
                                    {
                                        type = "FunctionExpression",
                                        id = null,
                                        @params = new Node[0],
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = "ReturnStatement",
                                                    argument = new Node
                                                    {
                                                        type = "BinaryExpression",
                                                        left = new Node
                                                        {
                                                            type = "Literal",
                                                            value = 10,
                                                            loc = new SourceLocation(new Position(1, 33), new Position(1, 35))
                                                        },
                                                        @operator = "in",
                                                        right = new Node
                                                        {
                                                            type = "ArrayExpression",
                                                            elements = new Node[0],
                                                            loc = new SourceLocation(new Position(1, 39), new Position(1, 41))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 33), new Position(1, 41))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 26), new Position(1, 41))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 24), new Position(1, 43))
                                        },
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 43))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 43))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 43))
                        },
                        right = new Node
                        {
                            type = "Identifier",
                            name = "list",
                            loc = new SourceLocation(new Position(1, 47), new Position(1, 51))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 53), new Position(1, 60))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 61), new Position(1, 62))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 53), new Position(1, 63))
                            },
                            loc = new SourceLocation(new Position(1, 53), new Position(1, 64))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 64))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 64))
            });
        }

        [Fact] public void Test1() { Test("while (true) { continue; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ContinueStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }); }

            [Fact] public void Test1() { Test("while (true) { continue }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ContinueStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 23))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }); }

            [Fact] public void Test1() { Test("done: while (true) { continue done }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "WhileStatement",
                            test = new Node
                            {
                                type = "Literal",
                                value = true,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 17))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ContinueStatement",
                                        label = new Node
                                        {
                                            type = "Identifier",
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 30), new Position(1, 34))
                                        },
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 34))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 36))
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 36))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            }); }

            [Fact] public void Test1() { Test("done: while (true) { continue done; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "WhileStatement",
                            test = new Node
                            {
                                type = "Literal",
                                value = true,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 17))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ContinueStatement",
                                        label = new Node
                                        {
                                            type = "Identifier",
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 30), new Position(1, 34))
                                        },
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 35))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 37))
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 37))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
            }); }

            [Fact] public void Test1() { Test("while (true) { break }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "BreakStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("done: while (true) { break done }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "WhileStatement",
                            test = new Node
                            {
                                type = "Literal",
                                value = true,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 17))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "BreakStatement",
                                        label = new Node
                                        {
                                            type = "Identifier",
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 31))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 33))
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 33))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }); }

            [Fact] public void Test1() { Test("done: while (true) { break done; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "WhileStatement",
                            test = new Node
                            {
                                type = "Literal",
                                value = true,
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 17))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "BreakStatement",
                                        label = new Node
                                        {
                                            type = "Identifier",
                                            name = "done",
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 32))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 34))
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 34))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "done",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

            [Fact] public void Test1() { Test("target1: target2: while (true) { continue target1; }", new Node { }); }
            [Fact] public void Test1() { Test("target1: target2: target3: while (true) { continue target1; }", new Node { }); }

            [Fact] public void Test1() { Test("(function(){ return })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 19))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 21))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("(function(){ return; })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 22))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }); }

            [Fact] public void Test1() { Test("(function(){ return x; })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 21))
                                        },
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 22))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }); }

            [Fact] public void Test1() { Test("(function(){ return x * y })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = new Node
                                        {
                                            type = "BinaryExpression",
                                            left = new Node
                                            {
                                                type = "Identifier",
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 21))
                                            },
                                            @operator = "*",
                                            right = new Node
                                            {
                                                type = "Identifier",
                                                name = "y",
                                                loc = new SourceLocation(new Position(1, 24), new Position(1, 25))
                                            },
                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 25))
                                        },
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 25))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 27))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }); }

            [Fact] public void Test1() { Test("with (x) foo = bar", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WithStatement",
                        @object = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "AssignmentExpression",
                                @operator = "=",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                },
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 18))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }); }

            [Fact] public void Test1() { Test("with (x) foo = bar;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WithStatement",
                        @object = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "AssignmentExpression",
                                @operator = "=",
                                left = new Node
                                {
                                    type = "Identifier",
                                    name = "foo",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                },
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "bar",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 18))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }); }

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            [Fact] public void Test1() { Test("'use\\x20strict'; with (x) foo = bar;", new Node { }); }

            // Test that innocuous string that evaluates to `use strict` is not promoted to
            // Use Strict directive.
            [Fact] public void Test1() { Test(@"""use\\x20strict""; with (x) foo = bar;", new Node { }); }

            [Fact] public void Test1() { Test("with (x) { foo = bar }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WithStatement",
                        @object = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "AssignmentExpression",
                                        @operator = "=",
                                        left = new Node
                                        {
                                            type = "Identifier",
                                            name = "foo",
                                            loc = new SourceLocation(new Position(1, 11), new Position(1, 14))
                                        },
                                        right = new Node
                                        {
                                            type = "Identifier",
                                            name = "bar",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                        },
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }); }

            [Fact] public void Test1() { Test("switch (x) {}", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "SwitchStatement",
                        discriminant = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                        },
                        cases = new Node[0],
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }); }

        [Fact]
        public void Test1()
        {
            Test("switch (answer) { case 42: hi(); } break; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "SwitchStatement",
                        discriminant = new Node
                        {
                            type = "Identifier",
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = "SwitchCase",
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "hi",
                                                loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 32))
                                    },
                                    new Node
                                    {
                                        type = "BreakStatement",
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33), new Position(1, 39))
                                    }
                                },
                                test = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 39))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("switch (answer) { case 42: hi(); } break; default: break }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "SwitchStatement",
                        discriminant = new Node
                        {
                            type = "Identifier",
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = "SwitchCase",
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "hi",
                                                loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                            },
                                            arguments = new Node[0],
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 31))
                                        },
                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 32))
                                    },
                                    new Node
                                    {
                                        type = "BreakStatement",
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 33), new Position(1, 39))
                                    }
                                },
                                test = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 39))
                            },
                            new Node
                            {
                                type = "SwitchCase",
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "BreakStatement",
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 49), new Position(1, 54))
                                    }
                                },
                                test = null,
                                loc = new SourceLocation(new Position(1, 40), new Position(1, 54))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 56))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 56))
            });
        }

        [Fact] public void Test1() { Test("start: for (;;) break start", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "ForStatement",
                            init = null,
                            test = null,
                            update = null,
                            fbody = new Node
                            {
                                type = "BreakStatement",
                                label = new Node
                                {
                                    type = "Identifier",
                                    name = "start",
                                    loc = new SourceLocation(new Position(1, 22), new Position(1, 27))
                                },
                                loc = new SourceLocation(new Position(1, 16), new Position(1, 27))
                            },
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 27))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "start",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }); }

            [Fact] public void Test1() { Test("start: while (true) break start", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "WhileStatement",
                            test = new Node
                            {
                                type = "Literal",
                                value = true,
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 18))
                            },
                            fbody = new Node
                            {
                                type = "BreakStatement",
                                label = new Node
                                {
                                    type = "Identifier",
                                    name = "start",
                                    loc = new SourceLocation(new Position(1, 26), new Position(1, 31))
                                },
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                            },
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 31))
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "start",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }); }

            [Fact] public void Test1() { Test("throw x;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ThrowStatement",
                        argument = new Node
                        {
                            type = "Identifier",
                            name = "x",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }); }

            [Fact] public void Test1() { Test("throw x * y", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ThrowStatement",
                        argument = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                            },
                            @operator = "*",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "y",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }); }

            [Fact] public void Test1() { Test("throw { message: \"Error\" }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ThrowStatement",
                        argument = new Node
                        {
                            type = "ObjectExpression",
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = "Property",
                                    key = new Node
                                    {
                                        type = "Identifier",
                                        name = "message",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 15))
                                    },
                                    value = new Node
                                    {
                                        type = "Literal",
                                        value = "Error",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 24))
                                    },
                                    kind = "init"
                                }
                            },
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }); }

            [Fact] public void Test1() { Test("try { } catch (e) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "e",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 21))
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 21))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }); }

            [Fact] public void Test1() { Test("try { } catch (eval) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "eval",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 19))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 24))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }); }

            [Fact] public void Test1() { Test("try { } catch (arguments) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "arguments",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 26), new Position(1, 29))
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 29))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }); }

            [Fact] public void Test1() { Test("try { } catch (e) { say(e) }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "e",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 20), new Position(1, 23))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 25))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 26))
                                        },
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 26))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 28))
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 28))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }); }

            [Fact] public void Test1() { Test("try { } finally { cleanup(stuff) }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                        },
                        handler = null,
                        finalizer = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "cleanup",
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = "Identifier",
                                                name = "stuff",
                                                loc = new SourceLocation(new Position(1, 26), new Position(1, 31))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                                    },
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

        [Fact]
        public void Test1()
        {
            Test("try { doThat(); } } catch (e) { say(e) }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "e",
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 30), new Position(1, 33))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 34), new Position(1, 35))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 30), new Position(1, 36))
                                        },
                                        loc = new SourceLocation(new Position(1, 30), new Position(1, 36))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 28), new Position(1, 38))
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 38))
                        },
                        finalizer = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("try { doThat(); } } catch (e) { say(e) } finally { cleanup(stuff) }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "TryStatement",
                        block = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "doThat",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                                    },
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                        },
                        handler = new Node
                        {
                            type = "CatchClause",
                            param = new Node
                            {
                                type = "Identifier",
                                name = "e",
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "CallExpression",
                                            callee = new Node
                                            {
                                                type = "Identifier",
                                                name = "say",
                                                loc = new SourceLocation(new Position(1, 30), new Position(1, 33))
                                            },
                                            arguments = new[]
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    name = "e",
                                                    loc = new SourceLocation(new Position(1, 34), new Position(1, 35))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 30), new Position(1, 36))
                                        },
                                        loc = new SourceLocation(new Position(1, 30), new Position(1, 36))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 28), new Position(1, 38))
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 38))
                        },
                        finalizer = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "cleanup",
                                            loc = new SourceLocation(new Position(1, 49), new Position(1, 56))
                                        },
                                        arguments = new[]
                                        {
                                            new Node
                                            {
                                                type = "Identifier",
                                                name = "stuff",
                                                loc = new SourceLocation(new Position(1, 57), new Position(1, 62))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 49), new Position(1, 63))
                                    },
                                    loc = new SourceLocation(new Position(1, 49), new Position(1, 63))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 47), new Position(1, 65))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 65))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 65))
            });
        }

        [Fact] public void Test1() { Test("debugger;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "DebuggerStatement",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }); }

        [Fact]
        public void Test1()
        {
            Test("function hello() { sayHi(); } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 19), new Position(1, 24))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 26))
                                    },
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 27))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 17), new Position(1, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            });
        }

        [Fact] public void Test1() { Test("function eval() { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "eval",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }); }

            [Fact] public void Test1() { Test("function arguments() { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "arguments",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 18))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }); }

            [Fact] public void Test1() { Test("function test(t, t) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "test",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = "Identifier",
                                name = "t",
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                            },
                            new Node
                            {
                                type = "Identifier",
                                name = "t",
                                loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                            }
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }); }

            [Fact] public void Test1() { Test("(function test(t, t) { })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = new Node
                            {
                                type = "Identifier",
                                name = "test",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                            },
                            @params = new[]
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    name = "t",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                },
                                new Node
                                {
                                    type = "Identifier",
                                    name = "t",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                }
                            },
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }); }

            [Fact] public void Test1() { Test("function eval() { function inner() { \"use strict\" } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "eval",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "FunctionDeclaration",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "inner",
                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 32))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                expression = new Node
                                                {
                                                    type = "Literal",
                                                    value = "use strict",
                                                    loc = new SourceLocation(new Position(1, 37), new Position(1, 49))
                                                },
                                                loc = new SourceLocation(new Position(1, 37), new Position(1, 49))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 35), new Position(1, 51))
                                    },
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 51))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 53))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 53))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 53))
            }); }

        [Fact]
        public void Test1()
        {
            Test("function hello(a) { sayHi(); } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = "Identifier",
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                            }
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 25))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                    },
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            });
        }

        [Fact]
        public void Test1()
        {
            Test("function hello(a, b) { sayHi(); } }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = "Identifier",
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                            },
                            new Node
                            {
                                type = "Identifier",
                                name = "b",
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                            }
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "CallExpression",
                                        callee = new Node
                                        {
                                            type = "Identifier",
                                            name = "sayHi",
                                            loc = new SourceLocation(new Position(1, 23), new Position(1, 28))
                                        },
                                        arguments = new Node[0],
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 30))
                                    },
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 21), new Position(1, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            });
        }

        [Fact] public void Test1() { Test("function hello(...rest) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = "RestElement",
                                argument = new Node
                                {
                                    type = "Identifier",
                                    name = "rest",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 22))
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 24), new Position(1, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6
            }); }

            [Fact] public void Test1() { Test("function hello(a, ...rest) { }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node
                        {
                            type = "Identifier",
                            name = "hello",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                        },
                        @params = new[]
                        {
                            new Node
                            {
                                type = "Identifier",
                                name = "a",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                            },
                            new Node
                            {
                                type = "RestElement",
                                argument = new Node
                                {
                                    type = "Identifier",
                                    name = "rest",
                                    loc = new SourceLocation(new Position(1, 21), new Position(1, 25))
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 27), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }, new Options
            {
                ecmaVersion = 6
            }); }

            [Fact] public void Test1() { Test("var hi = function() { sayHi() };", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                                },
                                init = new Node
                                {
                                    type = "FunctionExpression",
                                    id = null,
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                expression = new Node
                                                {
                                                    type = "CallExpression",
                                                    callee = new Node
                                                    {
                                                        type = "Identifier",
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 27))
                                                    },
                                                    arguments = new Node[0],
                                                    loc = new SourceLocation(new Position(1, 22), new Position(1, 29))
                                                },
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 29))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 31))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 31))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
            }); }

            [Fact] public void Test1() { Test("var hi = function (...r) { sayHi() };", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                                },
                                init = new Node
                                {
                                    type = "FunctionExpression",
                                    id = null,
                                    @params = new[]
                                    {
                                        new Node
                                        {
                                            type = "RestElement",
                                            argument = new Node
                                            {
                                                type = "Identifier",
                                                name = "r",
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                            }
                                        }
                                    },
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                expression = new Node
                                                {
                                                    type = "CallExpression",
                                                    callee = new Node
                                                    {
                                                        type = "Identifier",
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 32))
                                                    },
                                                    arguments = new Node[0],
                                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                                                },
                                                loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 25), new Position(1, 36))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 36))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 36))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
            }, new Options
            {
                ecmaVersion = 6
            }); }

            [Fact] public void Test1() { Test("var hi = function eval() { };", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                                },
                                init = new Node
                                {
                                    type = "FunctionExpression",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "eval",
                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 22))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 25), new Position(1, 28))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 28))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 28))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }); }

            [Fact] public void Test1() { Test("var hi = function arguments() { };", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "hi",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                                },
                                init = new Node
                                {
                                    type = "FunctionExpression",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "arguments",
                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 27))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 30), new Position(1, 33))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }); }

            [Fact] public void Test1() { Test("var hello = function hi() { sayHi() };", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "hello",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                                },
                                init = new Node
                                {
                                    type = "FunctionExpression",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "hi",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                                    },
                                    @params = new Node[0],
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                expression = new Node
                                                {
                                                    type = "CallExpression",
                                                    callee = new Node
                                                    {
                                                        type = "Identifier",
                                                        name = "sayHi",
                                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 33))
                                                    },
                                                    arguments = new Node[0],
                                                    loc = new SourceLocation(new Position(1, 28), new Position(1, 35))
                                                },
                                                loc = new SourceLocation(new Position(1, 28), new Position(1, 35))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 26), new Position(1, 37))
                                    },
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 37))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 37))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }); }

            [Fact] public void Test1() { Test("(function(){})", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }); }

            [Fact] public void Test1() { Test("{ x\n++y }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "UpdateExpression",
                                    @operator = "++",
                                    prefix = true,
                                    argument = new Node
                                    {
                                        type = "Identifier",
                                        name = "y",
                                        loc = new SourceLocation(new Position(2, 2), new Position(2, 3))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 3))
                                },
                                loc = new SourceLocation(new Position(2, 0), new Position(2, 3))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
            }); }

            [Fact] public void Test1() { Test("{ x\n--y }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "UpdateExpression",
                                    @operator = "--",
                                    prefix = true,
                                    argument = new Node
                                    {
                                        type = "Identifier",
                                        name = "y",
                                        loc = new SourceLocation(new Position(2, 2), new Position(2, 3))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 3))
                                },
                                loc = new SourceLocation(new Position(2, 0), new Position(2, 3))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
            }); }

            [Fact] public void Test1() { Test("var x /* comment */;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }); }

            [Fact] public void Test1() { Test("{ var x = 14, y = 3\nz; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclaration",
                                declarations = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "VariableDeclarator",
                                        id = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        init = new Node
                                        {
                                            type = "Literal",
                                            value = 14,
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                        },
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                    },
                                    new Node
                                    {
                                        type = "VariableDeclarator",
                                        id = new Node
                                        {
                                            type = "Identifier",
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                        },
                                        init = new Node
                                        {
                                            type = "Literal",
                                            value = 3,
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                        },
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 19))
                                    }
                                },
                                kind = "var",
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 19))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 1))
                                },
                                loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 4))
            }); }

            [Fact] public void Test1() { Test("while (true) { continue\nthere; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ContinueStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 23))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("while (true) { continue // Comment\nthere; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ContinueStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 23))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("while (true) { continue /* Multiline\nComment */there; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ContinueStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 23))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 10), new Position(2, 15))
                                    },
                                    loc = new SourceLocation(new Position(2, 10), new Position(2, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
            }); }

            [Fact] public void Test1() { Test("while (true) { break\nthere; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "BreakStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("while (true) { break // Comment\nthere; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "BreakStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                    },
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 8))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("while (true) { break /* Multiline\nComment */there; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "WhileStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = true,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                        },
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "BreakStatement",
                                    label = null,
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        name = "there",
                                        loc = new SourceLocation(new Position(2, 10), new Position(2, 15))
                                    },
                                    loc = new SourceLocation(new Position(2, 10), new Position(2, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 13), new Position(2, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
            }); }

            [Fact] public void Test1() { Test("(function(){ return\nx; })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 19))
                                    },
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 0), new Position(2, 1))
                                        },
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(2, 4))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(2, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
            }); }

            [Fact] public void Test1() { Test("(function(){ return // Comment\nx; })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 19))
                                    },
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 0), new Position(2, 1))
                                        },
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 2))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(2, 4))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(2, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 5))
            }); }

            [Fact] public void Test1() { Test("(function(){ return/* Multiline\nComment */x; })", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "FunctionExpression",
                            id = null,
                            @params = new Node[0],
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ReturnStatement",
                                        argument = null,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 19))
                                    },
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        expression = new Node
                                        {
                                            type = "Identifier",
                                            name = "x",
                                            loc = new SourceLocation(new Position(2, 10), new Position(2, 11))
                                        },
                                        loc = new SourceLocation(new Position(2, 10), new Position(2, 12))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(2, 14))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(2, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 15))
            }); }

            [Fact] public void Test1() { Test("{ throw error\nerror; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ThrowStatement",
                                argument = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 13))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                },
                                loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("{ throw error// Comment\nerror; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ThrowStatement",
                                argument = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 13))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 0), new Position(2, 5))
                                },
                                loc = new SourceLocation(new Position(2, 0), new Position(2, 6))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 8))
            }); }

            [Fact] public void Test1() { Test("{ throw error/* Multiline\nComment */error; }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ThrowStatement",
                                argument = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                },
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 13))
                            },
                            new Node
                            {
                                type = "ExpressionStatement",
                                expression = new Node
                                {
                                    type = "Identifier",
                                    name = "error",
                                    loc = new SourceLocation(new Position(2, 10), new Position(2, 15))
                                },
                                loc = new SourceLocation(new Position(2, 10), new Position(2, 16))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 18))
            }); }

            [Fact] public void Test1() { Test("", new Node
            {
                type = "Program",
                body = new List<Node>(),
                loc = new SourceLocation(new Position(1, 0), new Position(1, 0))
            }); }

            [Fact] public void Test1() { Test("foo: if (true) break foo;", new Node
            {
                type = "Program",
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25)),
                        fbody = new Node
                        {
                            type = "IfStatement",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 25)),
                            test = new Node
                            {
                                type = "Literal",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 13)),
                                value = true
                            },
                            consequent = new Node
                            {
                                type = "BreakStatement",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 25)),
                                label = new Node
                                {
                                    type = "Identifier",
                                    loc = new SourceLocation(new Position(1, 21), new Position(1, 24)),
                                    name = "foo"
                                }
                            },
                            alternate = null
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3)),
                            name = "foo"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("(function () {\n 'use strict';\n '\0';\n}())", new Node
            {
                type = "Program",
                loc = new SourceLocation(new Position(1, 0), new Position(4, 4)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        loc = new SourceLocation(new Position(1, 0), new Position(4, 4)),
                        expression = new Node
                        {
                            type = "CallExpression",
                            loc = new SourceLocation(new Position(1, 1), new Position(4, 3)),
                            callee = new Node
                            {
                                type = "FunctionExpression",
                                loc = new SourceLocation(new Position(1, 1), new Position(4, 1)),
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    loc = new SourceLocation(new Position(1, 13), new Position(4, 1)),
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            loc = new SourceLocation(new Position(2, 1), new Position(2, 14)),
                                            expression = new Node
                                            {
                                                type = "Literal",
                                                loc = new SourceLocation(new Position(2, 1), new Position(2, 13)),
                                                value = "use strict"
                                            }
                                        },
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            loc = new SourceLocation(new Position(3, 1), new Position(3, 5)),
                                            expression = new Node
                                            {
                                                type = "Literal",
                                                loc = new SourceLocation(new Position(3, 1), new Position(3, 4)),
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
            }); }

            [Fact] public void Test1() { Test("123..toString(10)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Literal",
                                    value = 123
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "toString"
                                },
                                computed = false
                            },
                            arguments = new[]
                            {
                                new Node
                                {
                                    type = "Literal",
                                    value = 10
                                }
                            }
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("123.+2", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Literal",
                                value = 123
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Literal",
                                value = 2
                            }
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("a\u2028b", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "a"
                        }
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "b"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("'a\\u0026b'", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "a\u0026b"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("foo: 10; foo: 20;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "Literal",
                                value = 10,
                                raw = "10"
                            }
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "foo"
                        }
                    },
                    new Node
                    {
                        type = "LabeledStatement",
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "Literal",
                                value = 20,
                                raw = "20"
                            }
                        },
                        label = new Node
                        {
                            type = "Identifier",
                            name = "foo"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("if(1)/  foo/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "IfStatement",
                        test = new Node
                        {
                            type = "Literal",
                            value = 1,
                            raw = "1"
                        },
                        consequent = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "Literal",
                                raw = "/  foo/"
                            }
                        },
                        alternate = null
                    }
                }
            }); }

            [Fact] public void Test1() { Test("price_9̶9̶_89", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Identifier",
                            name = "price_9̶9̶_89"
                        }
                    }
                }
            }); }

            // `\0` is valid even in strict mode
            [Fact] public void Test1() { Test("function hello() { 'use strict'; \"\\0\"; }", new Node { }); }

            // option tests
            [Fact] public void Test1() { Test("var a = 1;", new Node
            {
                type = "Program",
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10)),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 9)),
                                id = new Node
                                {
                                    type = "Identifier",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5)),
                                    name = "a"
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9)),
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
            }); }

            [Fact] public void Test1() { Test("a.in / b", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "a"
                                },
                                property = new Node
                                {
                                    type = "Identifier",
                                    name = "in"
                                },
                                computed = false
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "b"
                            }
                        }
                    }
                }
            }); }

            // A number of slash-disambiguation corner cases
            [Fact] public void Test1() { Test("return {} / 2", new Node { }, new Options { allowReturnOutsideFunction = true }); }
            [Fact] public void Test1() { Test("return\n{}\n/foo/", new Node { }, new Options { allowReturnOutsideFunction = true }); }
            [Fact] public void Test1() { Test("+{} / 2", new Node { }); }
            [Fact] public void Test1() { Test("{}\n/foo/", new Node { }); }
            [Fact] public void Test1() { Test("x++\n{}\n/foo/", new Node { }); }
            [Fact] public void Test1() { Test("{{}\n/foo/}", new Node { }); }
            [Fact] public void Test1() { Test("while (1) /foo/", new Node { }); }
            [Fact] public void Test1() { Test("while (1) {} /foo/", new Node { }); }
            [Fact] public void Test1() { Test("(1) / 2", new Node { }); }
            [Fact] public void Test1() { Test("({a: [1]}+[]) / 2", new Node { }); }
            [Fact] public void Test1() { Test("{[1]}\n/foo/", new Node { }); }
            [Fact] public void Test1() { Test("switch(a) { case 1: {}\n/foo/ }", new Node { }); }
            [Fact] public void Test1() { Test("({1: {} / 2})", new Node { }); }
            [Fact] public void Test1() { Test("+x++ / 2", new Node { }); }
            [Fact] public void Test1() { Test("foo.in\n{}\n/foo/", new Node { }); }
            [Fact] public void Test1() { Test("var x = function f() {} / 3;", new Node { }); }
            [Fact] public void Test1() { Test("+function f() {} / 3;", new Node { }); }
            [Fact] public void Test1() { Test("foo: function x() {} /regexp/", new Node { }); }
            [Fact] public void Test1() { Test("x = {foo: function x() {} / divide}", new Node { }); }
            [Fact] public void Test1() { Test("foo; function f() {} /regexp/", new Node { }); }
            [Fact] public void Test1() { Test("{function f() {} /regexp/}", new Node { }); }

            [Fact] public void Test1() { Test("{}/=/", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        body = new List<Node>()
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            raw = "/=/"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("foo <!--bar\n+baz", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "foo"
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "baz"
                            }
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("x = y-->10;\n --> nothing", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "AssignmentExpression",
                            @operator = "=",
                            left = new Node
                            {
                                type = "Identifier",
                                name = "x"
                            },
                            right = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "UpdateExpression",
                                    @operator = "--",
                                    prefix = false,
                                    argument = new Node
                                    {
                                        type = "Identifier",
                                        name = "y"
                                    }
                                },
                                @operator = ">",
                                right = new Node
                                {
                                    type = "Literal",
                                    value = 10
                                }
                            }
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("'use strict';\nobject.static(); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = "use strict",
                            raw = "'use strict'"
                        }
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "CallExpression",
                            callee = new Node
                            {
                                type = "MemberExpression",
                                @object = new Node
                                {
                                    type = "Identifier",
                                    name = "object"
                                },
                                property = new Node
                                {
                                    type = "Identifier",
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
            TestFail("{",
                "Unexpected token (1:1)");

            TestFail("}",
                "Unexpected token (1:0)");

            TestFail("3ea",
                "Invalid number (1:0)");

            TestFail("3in []",
                "Identifier directly after number (1:1)");

            TestFail("3e",
                "Invalid number (1:0)");

            TestFail("3e+",
                "Invalid number (1:0)");

            TestFail("3e-",
                "Invalid number (1:0)");

            TestFail("3x",
                "Identifier directly after number (1:1)");

            TestFail("3x0",
                "Identifier directly after number (1:1)");

            TestFail("0x",
                "Expected number in radix 16 (1:2)");

            TestFail("'use strict'; 09",
                "Invalid number (1:14)");

            TestFail("'use strict'; 018",
                "Invalid number (1:14)");

            TestFail("01a",
                "Identifier directly after number (1:2)");

            TestFail("3in[]",
                "Identifier directly after number (1:1)");

            TestFail("0x3in[]",
                "Identifier directly after number (1:3)");

            TestFail("\"Hello\nWorld\"",
                "Unterminated string constant (1:0)");

            TestFail("x\\",
                "Expecting Unicode escape sequence \\uXXXX (1:2)");

            TestFail("x\\u005c",
                "Invalid Unicode escape (1:1)");

            TestFail("x\\u002a",
                "Invalid Unicode escape (1:1)");

            TestFail("/",
                "Unterminated regular expression (1:1)");

            TestFail("/test",
                "Unterminated regular expression (1:1)");

            TestFail("var x = /[a-z]/\\ux",
                "Bad character escape sequence (1:17)");

            TestFail("3 = 4",
                "Assigning to rvalue (1:0)");

            TestFail("func() = 4",
                "Assigning to rvalue (1:0)");

            TestFail("(1 + 1) = 10",
                "Parenthesized pattern (1:0)");

            TestFail("1++",
                "Assigning to rvalue (1:0)");

            TestFail("1--",
                "Assigning to rvalue (1:0)");

            TestFail("++1",
                "Assigning to rvalue (1:2)");

            TestFail("--1",
                "Assigning to rvalue (1:2)");

            TestFail("for((1 + 1) in list) process(x);",
                "Assigning to rvalue (1:5)");

            TestFail("[",
                "Unexpected token (1:1)");

            TestFail("[,",
                "Unexpected token (1:2)");

            TestFail("1 + {",
                "Unexpected token (1:5)");

            TestFail("1 + { t:t ",
                "Unexpected token (1:10)");

            TestFail("1 + { t:t,",
                "Unexpected token (1:10)");

            TestFail("var x = /\n/",
                "Unterminated regular expression (1:9)");

            TestFail("var x = \"\n",
                "Unterminated string constant (1:8)");

            TestFail("var if = 42",
                "Unexpected keyword 'if' (1:4)");

            TestFail("i + 2 = 42",
                "Assigning to rvalue (1:0)");

            TestFail("+i = 42",
                "Assigning to rvalue (1:0)");

            TestFail("1 + (",
                "Unexpected token (1:5)");

            TestFail("\n\n\n{",
                "Unexpected token (4:1)");

            TestFail("\n/* Some multiline\ncomment */\n)",
                "Unexpected token (4:0)");

            TestFail("{ set 1 }",
                "Unexpected token (1:6)");

            TestFail("{ get 2 }",
                "Unexpected token (1:6)");

            TestFail("({ set: s(if) { } })",
                "Unexpected token (1:10)");

            TestFail("({ set s(.) { } })",
                "Unexpected token (1:9)");

            TestFail("({ set: s() { } })",
                "Unexpected token (1:12)");

            TestFail("({ set: s(a, b) { } })",
                "Unexpected token (1:16)");

            TestFail("({ get: g(d) { } })",
                "Unexpected token (1:13)");

            TestFail("({ get i() { }, i: 42 })",
                "Redefinition of property (1:16)");

            TestFail("({ i: 42, get i() { } })",
                "Redefinition of property (1:14)");

            TestFail("({ set i(x) { }, i: 42 })",
                "Redefinition of property (1:17)");

            TestFail("({ i: 42, set i(x) { } })",
                "Redefinition of property (1:14)");

            TestFail("({ get i() { }, get i() { } })",
                "Redefinition of property (1:20)");

            TestFail("({ set i(x) { }, set i(x) { } })",
                "Redefinition of property (1:21)");

            TestFail("'use strict'; ({ __proto__: 1, __proto__: 2 })",
                "Redefinition of property (1:31)");

            TestFail("function t(...) { }",
                "Unexpected token (1:11)");

            TestFail("function t(...) { }",
                "Unexpected token (1:14)",
                new Options { ecmaVersion = 6 });

            TestFail("function t(...rest, b) { }",
                "Comma is not permitted after the rest element (1:18)",
                new Options { ecmaVersion = 6 });

            TestFail("function t(if) { }",
                "Unexpected keyword 'if' (1:11)");

            TestFail("function t(true) { }",
                "Unexpected keyword 'true' (1:11)");

            TestFail("function t(false) { }",
                "Unexpected keyword 'false' (1:11)");

            TestFail("function t(null) { }",
                "Unexpected keyword 'null' (1:11)");

            TestFail("function null() { }",
                "Unexpected keyword 'null' (1:9)");

            TestFail("function true() { }",
                "Unexpected keyword 'true' (1:9)");

            TestFail("function false() { }",
                "Unexpected keyword 'false' (1:9)");

            TestFail("function if() { }",
                "Unexpected keyword 'if' (1:9)");

            TestFail("a b;",
                "Unexpected token (1:2)");

            TestFail("if.a;",
                "Unexpected token (1:2)");

            TestFail("a if;",
                "Unexpected token (1:2)");

            TestFail("a class;",
                "Unexpected token (1:2)");

            TestFail("break\n",
                "Unsyntactic break (1:0)");

            TestFail("break 1;",
                "Unexpected token (1:6)");

            TestFail("continue\n",
                "Unsyntactic continue (1:0)");

            TestFail("continue 2;",
                "Unexpected token (1:9)");

            TestFail("throw",
                "Unexpected token (1:5)");

            TestFail("throw;",
                "Unexpected token (1:5)");

            TestFail("for (var i, i2 in {});",
                "Unexpected token (1:15)");

            TestFail("for ((i in {}));",
                "Unexpected token (1:14)");

            TestFail("for (i + 1 in {});",
                "Assigning to rvalue (1:5)");

            TestFail("for (+i in {});",
                "Assigning to rvalue (1:5)");

            TestFail("if(false)",
                "Unexpected token (1:9)");

            TestFail("if(false) doThis(); else",
                "Unexpected token (1:24)");

            TestFail("do",
                "Unexpected token (1:2)");

            TestFail("while(false)",
                "Unexpected token (1:12)");

            TestFail("for(;;)",
                "Unexpected token (1:7)");

            TestFail("with(x)",
                "Unexpected token (1:7)");

            TestFail("try { }",
                "Missing catch or finally clause (1:0)");

            TestFail("‿ = 10",
                "Unexpected character '‿' (1:0)");

            TestFail("if(true) let a = 1;",
                "Unexpected token (1:13)");

            TestFail("switch (c) { default: default: }",
                "Multiple default clauses (1:22)");

            TestFail("new X().\"s\"",
                "Unexpected token (1:8)");

            TestFail("/*",
                "Unterminated comment (1:0)");

            TestFail("/*\n\n\n",
                "Unterminated comment (1:0)");

            TestFail("/**",
                "Unterminated comment (1:0)");

            TestFail("/*\n\n*",
                "Unterminated comment (1:0)");

            TestFail("/*hello",
                "Unterminated comment (1:0)");

            TestFail("/*hello  *",
                "Unterminated comment (1:0)");

            TestFail("\n]",
                "Unexpected token (2:0)");

            TestFail("\r]",
                "Unexpected token (2:0)");

            TestFail("\r\n]",
                "Unexpected token (2:0)");

            TestFail("\n\r]",
                "Unexpected token (3:0)");

            TestFail("//\r\n]",
                "Unexpected token (2:0)");

            TestFail("//\n\r]",
                "Unexpected token (3:0)");

            TestFail("/a\\\n/",
                "Unterminated regular expression (1:1)");

            TestFail("//\r \n]",
                "Unexpected token (3:0)");

            TestFail("/*\r\n*/]",
                "Unexpected token (2:2)");

            TestFail("/*\n\r*/]",
                "Unexpected token (3:2)");

            TestFail("/*\r \n*/]",
                "Unexpected token (3:2)");

            TestFail("\\\\",
                "Expecting Unicode escape sequence \\uXXXX (1:1)");

            TestFail("\\u005c",
                "Invalid Unicode escape (1:0)");

            TestFail("\\x",
                "Expecting Unicode escape sequence \\uXXXX (1:1)");

            TestFail("\\u0000",
                "Invalid Unicode escape (1:0)");

            TestFail("‌ = []",
                "Unexpected character '‌' (1:0)");

            TestFail("‍ = []",
                "Unexpected character '‍' (1:0)");

            TestFail("\"\\",
                "Unterminated string constant (1:0)");

            TestFail("\"\\u",
                "Bad character escape sequence (1:3)");

            TestFail("return",
                "'return' outside of function (1:0)");

            TestFail("break",
                "Unsyntactic break (1:0)");

            TestFail("continue",
                "Unsyntactic continue (1:0)");

            TestFail("switch (x) { default: continue; }",
                "Unsyntactic continue (1:22)");

            TestFail("do { x } *",
                "Unexpected token (1:9)");

            TestFail("while (true) { break x; }",
                "Unsyntactic break (1:15)");

            TestFail("while (true) { continue x; }",
                "Unsyntactic continue (1:15)");

            TestFail("x: while (true) { (function () { break x; }); }",
                "Unsyntactic break (1:33)");

            TestFail("x: while (true) { (function () { continue x; }); }",
                "Unsyntactic continue (1:33)");

            TestFail("x: while (true) { (function () { break; }); }",
                "Unsyntactic break (1:33)");

            TestFail("x: while (true) { (function () { continue; }); }",
                "Unsyntactic continue (1:33)");

            TestFail("x: while (true) { x: while (true) { } }",
                "Label 'x' is already declared (1:18)");

            TestFail("(function () { 'use strict'; delete i; }())",
                "Deleting local variable in strict mode (1:29)");

            TestFail("function x() { '\\12'; 'use strict'; }", "Octal literal in strict mode (1:16)");

            TestFail("(function () { 'use strict'; with (i); }())",
                "'with' in strict mode (1:29)");

            TestFail("function hello() {'use strict'; ({ i: 42, i: 42 }) }",
                "Redefinition of property (1:42)");

            TestFail("function hello() {'use strict'; ({ hasOwnProperty: 42, hasOwnProperty: 42 }) }",
                "Redefinition of property (1:55)");

            TestFail("function hello() {'use strict'; var eval = 10; }",
                "Binding eval in strict mode (1:36)");

            TestFail("function hello() {'use strict'; var arguments = 10; }",
                "Binding arguments in strict mode (1:36)");

            TestFail("function hello() {'use strict'; try { } catch (eval) { } }",
                "Binding eval in strict mode (1:47)");

            TestFail("function hello() {'use strict'; try { } catch (arguments) { } }",
                "Binding arguments in strict mode (1:47)");

            TestFail("function hello() {'use strict'; eval = 10; }",
                "Assigning to eval in strict mode (1:32)");

            TestFail("function hello() {'use strict'; arguments = 10; }",
                "Assigning to arguments in strict mode (1:32)");

            TestFail("function hello() {'use strict'; ++eval; }",
                "Assigning to eval in strict mode (1:34)");

            TestFail("function hello() {'use strict'; --eval; }",
                "Assigning to eval in strict mode (1:34)");

            TestFail("function hello() {'use strict'; ++arguments; }",
                "Assigning to arguments in strict mode (1:34)");

            TestFail("function hello() {'use strict'; --arguments; }",
                "Assigning to arguments in strict mode (1:34)");

            TestFail("function hello() {'use strict'; eval++; }",
                "Assigning to eval in strict mode (1:32)");

            TestFail("function hello() {'use strict'; eval--; }",
                "Assigning to eval in strict mode (1:32)");

            TestFail("function hello() {'use strict'; arguments++; }",
                "Assigning to arguments in strict mode (1:32)");

            TestFail("function hello() {'use strict'; arguments--; }",
                "Assigning to arguments in strict mode (1:32)");

            TestFail("function hello() {'use strict'; function eval() { } }",
                "Binding eval in strict mode (1:41)");

            TestFail("function hello() {'use strict'; function arguments() { } }",
                "Binding arguments in strict mode (1:41)");

            TestFail("function eval() {'use strict'; }",
                "Binding eval in strict mode (1:9)");

            TestFail("function arguments() {'use strict'; }",
                "Binding arguments in strict mode (1:9)");

            TestFail("function hello() {'use strict'; (function eval() { }()) }",
                "Binding eval in strict mode (1:42)");

            TestFail("function hello() {'use strict'; (function arguments() { }()) }",
                "Binding arguments in strict mode (1:42)");

            TestFail("(function eval() {'use strict'; })()",
                "Binding eval in strict mode (1:10)");

            TestFail("(function arguments() {'use strict'; })()",
                "Binding arguments in strict mode (1:10)");

            TestFail("function hello() {'use strict'; ({ s: function eval() { } }); }",
                "Binding eval in strict mode (1:47)");

            TestFail("(function package() {'use strict'; })()",
                "Binding package in strict mode (1:10)");

            TestFail("function hello() {'use strict'; ({ i: 10, set s(eval) { } }); }",
                "Binding eval in strict mode (1:48)");

            TestFail("function hello() {'use strict'; ({ set s(eval) { } }); }",
                "Binding eval in strict mode (1:41)");

            TestFail("function hello() {'use strict'; ({ s: function s(eval) { } }); }",
                "Binding eval in strict mode (1:49)");

            TestFail("function hello(eval) {'use strict';}",
                "Binding eval in strict mode (1:15)");

            TestFail("function hello(arguments) {'use strict';}",
                "Binding arguments in strict mode (1:15)");

            TestFail("function hello() { 'use strict'; function inner(eval) {} }",
                "Binding eval in strict mode (1:48)");

            TestFail("function hello() { 'use strict'; function inner(arguments) {} }",
                "Binding arguments in strict mode (1:48)");

            TestFail("function hello() { 'use strict'; \"\\1\"; }",
                "Octal literal in strict mode (1:34)");

            TestFail("function hello() { 'use strict'; \"\\00\"; }",
                "Octal literal in strict mode (1:34)");

            TestFail("function hello() { 'use strict'; \"\\000\"; }",
                "Octal literal in strict mode (1:34)");

            TestFail("function hello() { 'use strict'; 021; }",
                "Invalid number (1:33)");

            TestFail("function hello() { 'use strict'; ({ \"\\1\": 42 }); }",
                "Octal literal in strict mode (1:37)");

            TestFail("function hello() { 'use strict'; ({ 021: 42 }); }",
                "Invalid number (1:36)");

            TestFail("function hello() { \"use strict\"; function inner() { \"octal directive\\1\"; } }",
                "Octal literal in strict mode (1:68)");

            TestFail("function hello() { \"use strict\"; var implements; }",
                "The keyword 'implements' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var interface; }",
                "The keyword 'interface' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var package; }",
                "The keyword 'package' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var private; }",
                "The keyword 'private' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var protected; }",
                "The keyword 'protected' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var public; }",
                "The keyword 'public' is reserved (1:37)");

            TestFail("function hello() { \"use strict\"; var static; }",
                "The keyword 'static' is reserved (1:37)");

            TestFail("function hello(static) { \"use strict\"; }",
                "Binding static in strict mode (1:15)");

            TestFail("function static() { \"use strict\"; }",
                "Binding static in strict mode (1:9)");

            TestFail("\"use strict\"; function static() { }",
                "The keyword 'static' is reserved (1:23)");

            TestFail("function a(t, t) { \"use strict\"; }",
                "Argument name clash (1:14)");

            TestFail("function a(eval) { \"use strict\"; }",
                "Binding eval in strict mode (1:11)");

            TestFail("function a(package) { \"use strict\"; }",
                "Binding package in strict mode (1:11)");

            TestFail("function a() { \"use strict\"; function b(t, t) { }; }",
                "Argument name clash (1:43)");

            TestFail("(function a(t, t) { \"use strict\"; })",
                "Argument name clash (1:15)");

            TestFail("function a() { \"use strict\"; (function b(t, t) { }); }",
                "Argument name clash (1:44)");

            TestFail("(function a(eval) { \"use strict\"; })",
                "Binding eval in strict mode (1:12)");

            TestFail("(function a(package) { \"use strict\"; })",
                "Binding package in strict mode (1:12)");

            TestFail("\"use strict\";function foo(){\"use strict\";}function bar(){var v = 015}",
                "Invalid number (1:65)");

            TestFail("var this = 10;", "Unexpected keyword 'this' (1:4)");

            TestFail("throw\n10;", "Illegal newline after throw (1:5)");

            // ECMA < 6 mode should work as before

            TestFail("const a;", "The keyword 'const' is reserved (1:0)");

            TestFail("let x;", "Unexpected token (1:4)");

            TestFail("const a = 1;", "The keyword 'const' is reserved (1:0)");

            TestFail("let a = 1;", "Unexpected token (1:4)");

            TestFail("for(const x = 0;;);", "The keyword 'const' is reserved (1:4)");

            TestFail("for(let x = 0;;);", "Unexpected token (1:8)");

            TestFail("function a(b = c) {}", "Unexpected token (1:13)");

            [Fact] public void Test1() { Test("let++", new Node
            {
                type = "Program",
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5)),
                        expression = new Node
                        {
                            type = "UpdateExpression",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 5)),
                            @operator = "++",
                            prefix = false,
                            argument = new Node
                            {
                                type = "Identifier",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3)),
                                name = "let"
                            }
                        }
                    }
                }
            }); }

            // ECMA 6 support
            [Fact] public void Test1() { Test("let x", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("let x, y;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                },
                                init = null,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("let x = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("let eval = 42, arguments = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 8))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                },
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 29))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("let x = 14, y = 3, z = 1977", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                },
                                loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 27))
                                },
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 27))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("for(let x = 0;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options { ecmaVersion = 6 });

            [Fact] public void Test1() { Test("for(let x = 0, y = 1;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 0,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 13))
                                },
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
                                        value = 1,
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                    },
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 20))
                        },
                        test = null,
                        update = null,
                        fbody = new Node
                        {
                            type = "EmptyStatement",
                            loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options { ecmaVersion = 6 });

            [Fact] public void Test1() { Test("for (let x in list) process(x); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForInStatement",
                        left = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                        },
                        right = new Node
                        {
                            type = "Identifier",
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 18))
                        },
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "CallExpression",
                                callee = new Node
                                {
                                    type = "Identifier",
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                },
                                arguments = new[]
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options { ecmaVersion = 6 });

            [Fact] public void Test1() { Test("const x = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("const eval = 42, arguments = 42", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 10))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 26))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 42,
                                    loc = new SourceLocation(new Position(1, 29), new Position(1, 31))
                                },
                                loc = new SourceLocation(new Position(1, 17), new Position(1, 31))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("const x = 14, y = 3, z = 1977", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "VariableDeclaration",
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 14,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "y",
                                    loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 3,
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 19))
                            },
                            new Node
                            {
                                type = "VariableDeclarator",
                                id = new Node
                                {
                                    type = "Identifier",
                                    name = "z",
                                    loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                },
                                init = new Node
                                {
                                    type = "Literal",
                                    value = 1977,
                                    loc = new SourceLocation(new Position(1, 25), new Position(1, 29))
                                },
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 29))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }, new Options { ecmaVersion = 6 }); }

            TestFail("const a;", "Unexpected token (1:7)", new Options { ecmaVersion = 6 });

            [Fact] public void Test1() { Test("for(const x = 0;;); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ForStatement",
                        init = new Node
                        {
                            type = "VariableDeclaration",
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = "VariableDeclarator",
                                    id = new Node
                                    {
                                        type = "Identifier",
                                        name = "x",
                                        range = (10, 11)
                                    },
                                    init = new Node
                                    {
                                        type = "Literal",
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
                            type = "EmptyStatement",
                            range = (18, 19)
                        },
                        range = (0, 19)
                    }
                },
                range = (0, 19)
            }, new Options { ecmaVersion = 6 });

            TestFail("for(x of a);", "Unexpected token (1:6)");

            TestFail("for(var x of a);", "Unexpected token (1:10)");

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
            //    {type = "Line", value = " Bear class"},
            //    {type = "Line", value = " Whatever"},
            //    {type = "Block",  value = [
            //            " 1",
            //      "       2",
            //      "       3"
            //    ].join('\n')},
            //    {type = "Block", value = "stuff"}
            //  ]
            //});

            [Fact] public void Test1() { Test("<!--\n;", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "EmptyStatement"
                    }
                }
            }); }

            //test("\nfunction plop() {\n'use strict';\n/* Comment */\n}", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = "Block",
            //    value = " Comment ",
            //    loc = new SourceLocation(new Position(4, 0), new Position(4, 13))
            //  }}
            //});
            //
            //test("// line comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = "Line",
            //    value = " line comment",
            //    loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            //  }}
            //});

            //test("<!-- HTML comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = "Line",
            //    value = " HTML comment",
            //    loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            //  }}
            //});
            //
            //test(";\n--> HTML comment", new Node {}, new Options{
            //  locations = true,
            //  onComment: [{
            //    type = "Line",
            //    value = " HTML comment",
            //    loc = new SourceLocation(new Position(2, 0), new Position(2, 16))
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
            //      loc = new SourceLocation(new Position(1, 3), new Position(1, 6))
            //    },
            //    {
            //      type = tokTypes.parenL,
            //      value = undefined,
            //      loc = new SourceLocation(new Position(1, 9), new Position(1, 11))
            //    },
            //    {
            //      type = tokTypes.num,
            //      value = 2,
            //      loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
            //    }
            //  ]
            //});

            [Fact] public void Test1() { Test("function f(f) { 'use strict'; }", new Node { }); }

            // https://github.com/ternjs/acorn/issues/180
            [Fact] public void Test1() { Test("#!/usr/bin/node\n;", new Node { }, new Options
            {
                allowHashBang = true,
            }); }

            // https://github.com/ternjs/acorn/issues/204
            [Fact] public void Test1() { Test("(function () {} / 1)", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "FunctionExpression",
                                id = null,
                                @params = new Node[0],
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    body = new List<Node>()
                                }
                            },
                            @operator = "/",
                            right = new Node {type = "Literal", value = 1}
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("function f() {} / 1 /", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        id = new Node {type = "Identifier", name = "f"},
                        @params = new Node[0],
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            body = new List<Node>()
                        }
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            regex = new RegexNode{pattern = " 1 ", flags = ""}
                        }
                    }
                }
            }); }

            // https://github.com/ternjs/acorn/issues/320
            [Fact] public void Test1() { Test(@"do /x/; while (false); }", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "DoWhileStatement",
                        fbody = new Node
                        {
                            type = "ExpressionStatement",
                            expression = new Node
                            {
                                type = "Literal",
                                raw = "/x/",
                                regex = new RegexNode{pattern = "x", flags = ""}
                            }
                        },
                        test = new Node
                        {
                            type = "Literal",
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

            TestFail("({ get prop(x) {} })", "getter should have no params (1:11)");
            TestFail("({ set prop() {} })", "setter should have exactly one param (1:11)");
            TestFail("({ set prop(x, y) {} })", "setter should have exactly one param (1:11)");

            // https://github.com/ternjs/acorn/issues/363

            [Fact] public void Test1() { Test("/[a-z]/gim", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "gim"
                            }
                        }
                    }
                }
            }); }
            TestFail("/[a-z]/u", "Invalid regular expression flag (1:1)");
            TestFail("/[a-z]/y", "Invalid regular expression flag (1:1)");
            TestFail("/[a-z]/s", "Invalid regular expression flag (1:1)");

            TestFail("function(){}", "Unexpected token (1:8)");

            [Fact] public void Test1() { Test("0123. in/foo/i", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            left = new Node
                            {
                                type = "BinaryExpression",
                                left = new Node
                                {
                                    type = "MemberExpression",
                                    @object = new Node
                                    {
                                        type = "Literal",
                                        value = 83,
                                        raw = "0123"
                                    },
                                    property = new Node
                                    {
                                        type = "Identifier",
                                        name = "in"
                                    },
                                    computed = false
                                },
                                @operator = "/",
                                right = new Node
                                {
                                    type = "Identifier",
                                    name = "foo"
                                }
                            },
                            @operator = "/",
                            right = new Node
                            {
                                type = "Identifier",
                                name = "i"
                            }
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("0128", new Node
            {
                type = "Program",
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        expression = new Node
                        {
                            type = "Literal",
                            value = 128,
                            raw = "0128"
                        }
                    }
                }
            }); }

            [Fact] public void Test1() { Test("undefined", new Node { }, new Options { ecmaVersion = 8 }); }

            TestFail("\\u{74}rue", "Escape sequence in keyword true (1:0)", new Options { ecmaVersion = 6 });

            TestFail("(x=1)=2", "Parenthesized pattern (1:0)");

            [Fact] public void Test1() { Test("(foo = [])[0] = 4;", new Node { }); }

            [Fact] public void Test1() { Test("for ((foo = []).bar in {}) {}", new Node { }); }

            [Fact] public void Test1() { Test("((b), a=1)", new Node { }); }

            [Fact] public void Test1() { Test("(x) = 1", new Node { }); }

            TestFail("try {} catch (foo) { var foo; }", "Identifier 'foo' has already been declared (1:25)");
            TestFail("try {} catch (foo) { let foo; }", "Identifier 'foo' has already been declared (1:25)", new Options { ecmaVersion = 6 });
            TestFail("try {} catch (foo) { try {} catch (_) { var foo; } }", "Identifier 'foo' has already been declared (1:44)");
            TestFail("try {} catch ([foo]) { var foo; }", "Identifier 'foo' has already been declared (1:27)", new Options { ecmaVersion = 6 });
            TestFail("try {} catch ({ foo }) { var foo; }", "Identifier 'foo' has already been declared (1:29)", new Options { ecmaVersion = 6 });
            TestFail("try {} catch ([foo, foo]) {}", "Identifier 'foo' has already been declared (1:20)", new Options { ecmaVersion = 6 });
            TestFail("try {} catch ({ a: foo, b: { c: [foo] } }) {}", "Identifier 'foo' has already been declared (1:33)", new Options { ecmaVersion = 6 });
            TestFail("let foo; try {} catch (foo) {} let foo;", "Identifier 'foo' has already been declared (1:35)", new Options { ecmaVersion = 6 });
            TestFail("try {} catch (foo) { function foo() {} }", "Identifier 'foo' has already been declared (1:30)");

            [Fact] public void Test1() { Test("try {} catch (foo) {} var foo;", new Node { }); }
            [Fact] public void Test1() { Test("try {} catch (foo) {} let foo;", new Node { }, new Options { ecmaVersion = 6 }); }
            [Fact] public void Test1() { Test("try {} catch (foo) { { let foo; } }", new Node { }, new Options { ecmaVersion = 6 }); }
            [Fact] public void Test1() { Test("try {} catch (foo) { function x() { var foo; } }", new Node { }, new Options { ecmaVersion = 6 }); }
            [Fact] public void Test1() { Test("try {} catch (foo) { function x(foo) {} }", new Node { }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("'use strict'; let foo = function foo() {}", new Node { }, new Options { ecmaVersion = 6 }); }

            [Fact] public void Test1() { Test("/**/ --> comment\n", new Node { }); }
        }
}
