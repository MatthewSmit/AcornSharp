using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTemplateLiteralRevision()
        {
            Test("`foo`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)))
                        {
                            type = NodeType.TemplateLiteral,
                            expressions = new List<BaseNode>(),
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4)))
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("foo", "foo"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("`foo\\u25a0`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                        {
                            type = NodeType.TemplateLiteral,
                            expressions = new List<BaseNode>(),
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10)))
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("foo\\u25a0", "foo■"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("`foo${bar}\\u25a0`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            type = NodeType.TemplateLiteral,
                            expressions = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "bar")
                            },
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4)))
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("foo", "foo"),
                                    tail = false
                                },
                                new BaseNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16)))
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\\u25a0", "■"),
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 9});

            Test("foo`\\u25a0`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 11, 11)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`foo${bar}\\u25a0`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 20, 20)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "bar")
                                },
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)))
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 19, 19)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\unicode`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 13, 13)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`foo${bar}\\unicode`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 22, 22)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "bar")
                                },
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)))
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\u`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\u{`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 8, 8)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\u{abcdx`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 13, 13)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\u{abcdx}`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 14, 14)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("foo`\\unicode\\\\`", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            quasi = new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 15, 15)))
                            {
                                type = NodeType.TemplateLiteral,
                                expressions = new List<BaseNode>(),
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)))
                                    {
                                        type = NodeType.TemplateElement,
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

            Test("`${ {class: 1} }`", new BaseNode(default), new Options {ecmaVersion = 9});
            Test("`${ {delete: 1} }`", new BaseNode(default), new Options {ecmaVersion = 9});
            Test("`${ {enum: 1} }`", new BaseNode(default), new Options {ecmaVersion = 9});
            Test("`${ {function: 1} }`", new BaseNode(default), new Options {ecmaVersion = 9});
        }
    }
}
