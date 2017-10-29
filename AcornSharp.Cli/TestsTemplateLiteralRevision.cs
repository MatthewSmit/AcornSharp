﻿using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTemplateLiteralRevision()
        {
            Test("`foo`", new Node
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
                            type = NodeType.TemplateLiteral,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                            expressions = new List<Node>(),
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4)),
                                    value = new TemplateNode("foo", "foo"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("`foo\\u25a0`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                            expressions = new List<Node>(),
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10)),
                                    value = new TemplateNode("foo\\u25a0", "foo■"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("`foo${bar}\\u25a0`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            expressions = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "bar")
                            },
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4)),
                                    value = new TemplateNode("foo", "foo"),
                                    tail = false
                                },
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16)),
                                    value = new TemplateNode("\\u25a0", "■"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u25a0`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 11, 11)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)),
                                        value = new TemplateNode("\\u25a0", "■"),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`foo${bar}\\u25a0`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 20, 20)),
                                expressions = new List<Node>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "bar")
                                },
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)),
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19)),
                                        value = new TemplateNode("\\u25a0", "■"),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            testFail("`\\unicode`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});
            testFail("`\\u`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});
            testFail("`\\u{`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});
            testFail("`\\u{abcdx`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});
            testFail("`\\u{abcdx}`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});
            testFail("`\\xylophone`", "Bad escape sequence in untagged template literal (1:1)", new Options {ecmaVersion = 9});

            testFail("foo`\\unicode`", "Bad character escape sequence (1:6)", new Options {ecmaVersion = 8});
            testFail("foo`\\xylophone`", "Bad character escape sequence (1:6)", new Options {ecmaVersion = 8});

            testFail("foo`\\unicode", "Unterminated template (1:4)", new Options {ecmaVersion = 9});
            testFail("foo`\\unicode\\`", "Unterminated template (1:4)", new Options {ecmaVersion = 9});

            Test("foo`\\unicode`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 13, 13)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                        value = new TemplateNode("\\unicode", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`foo${bar}\\unicode`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 22, 22)),
                                expressions = new List<Node>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "bar")
                                },
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)),
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)),
                                        value = new TemplateNode("\\unicode", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)),
                                        value = new TemplateNode("\\u", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u{`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 8, 8)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)),
                                        value = new TemplateNode("\\u{", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u{abcdx`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 13, 13)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                        value = new TemplateNode("\\u{abcdx", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u{abcdx}`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 14, 14)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13)),
                                        value = new TemplateNode("\\u{abcdx}", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\unicode\\\\`", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 15, 15)),
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)),
                                        value = new TemplateNode("\\unicode\\\\", null),
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("`${ {class: 1} }`", new Node { }, new Options {ecmaVersion = 9});
            Test("`${ {delete: 1} }`", new Node { }, new Options {ecmaVersion = 9});
            Test("`${ {enum: 1} }`", new Node { }, new Options {ecmaVersion = 9});
            Test("`${ {function: 1} }`", new Node { }, new Options {ecmaVersion = 9});
        }
    }
}
