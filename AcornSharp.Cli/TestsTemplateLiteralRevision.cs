using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTemplateLiteralRevision()
        {
            Test("`foo`", new Node
            {
                type = "Program",
                start = 0,
                end = 5,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 5,
                        expression = new Node
                        {
                            type = "TemplateLiteral",
                            start = 0,
                            end = 5,
                            expressions = new List<Node>(),
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = "TemplateElement",
                                    start = 1,
                                    end = 4,
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
                type = "Program",
                start = 0,
                end = 11,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 11,
                        expression = new Node
                        {
                            type = "TemplateLiteral",
                            start = 0,
                            end = 11,
                            expressions = new List<Node>(),
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = "TemplateElement",
                                    start = 1,
                                    end = 10,
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
                type = "Program",
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 17,
                        expression = new Node
                        {
                            type = "TemplateLiteral",
                            start = 0,
                            end = 17,
                            expressions = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 6,
                                    end = 9,
                                    name = "bar"
                                }
                            },
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = "TemplateElement",
                                    start = 1,
                                    end = 4,
                                    value = new TemplateNode("foo", "foo"),
                                    tail = false
                                },
                                new Node
                                {
                                    type = "TemplateElement",
                                    start = 10,
                                    end = 16,
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
                type = "Program",
                start = 0,
                end = 11,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 11,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 11,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 11,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 10,
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
                type = "Program",
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 20,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 20,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 20,
                                expressions = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        start = 9,
                                        end = 12,
                                        name = "bar"
                                    }
                                },
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 7,
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 13,
                                        end = 19,
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
                type = "Program",
                start = 0,
                end = 13,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 13,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 13,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 12,
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
                type = "Program",
                start = 0,
                end = 22,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 22,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 22,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 22,
                                expressions = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "Identifier",
                                        start = 9,
                                        end = 12,
                                        name = "bar"
                                    }
                                },
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 7,
                                        value = new TemplateNode("foo", "foo"),
                                        tail = false
                                    },
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 13,
                                        end = 21,
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
                type = "Program",
                start = 0,
                end = 7,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 7,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 7,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 7,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 6,
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
                type = "Program",
                start = 0,
                end = 8,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 8,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 8,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 8,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 7,
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
                type = "Program",
                start = 0,
                end = 13,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 13,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 13,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 12,
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
                type = "Program",
                start = 0,
                end = 14,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 14,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 14,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 14,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 13,
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
                type = "Program",
                start = 0,
                end = 15,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 15,
                        expression = new Node
                        {
                            type = "TaggedTemplateExpression",
                            start = 0,
                            end = 15,
                            tag = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new Node
                            {
                                type = "TemplateLiteral",
                                start = 3,
                                end = 15,
                                expressions = new List<Node>(),
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "TemplateElement",
                                        start = 4,
                                        end = 14,
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
