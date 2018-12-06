using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal class TestsTemplateLiteralRevision
    {
        public static void Run()
        {
            Program.test("`foo`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 5,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 5,
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            start = 0,
                            end = 5,
                            expressions = new TestNode[0],
                            quasis = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    start = 1,
                                    end = 4,
                                    value = new TestNode
                                    {
                                        raw = "foo",
                                        cooked = "foo"
                                    },
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("`foo\\u25a0`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 11,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 11,
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            start = 0,
                            end = 11,
                            expressions = new TestNode[0],
                            quasis = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    start = 1,
                                    end = 10,
                                    value = new TestNode
                                    {
                                        raw = "foo\\u25a0",
                                        cooked = "foo■"
                                    },
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("`foo${bar}\\u25a0`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 17,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 17,
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            start = 0,
                            end = 17,
                            expressions = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 6,
                                    end = 9,
                                    name = "bar"
                                }
                            },
                            quasis = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    start = 1,
                                    end = 4,
                                    value = new TestNode
                                    {
                                        raw = "foo",
                                        cooked = "foo"
                                    },
                                    tail = false
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    start = 10,
                                    end = 16,
                                    value = new TestNode
                                    {
                                        raw = "\\u25a0",
                                        cooked = "■"
                                    },
                                    tail = true
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\u25a0`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 11,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 11,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 11,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 11,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 10,
                                        value = new TestNode
                                        {
                                            raw = "\\u25a0",
                                            cooked = "■"
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`foo${bar}\\u25a0`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 20,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 20,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 20,
                                expressions = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 12,
                                        name = "bar"
                                    }
                                },
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 7,
                                        value = new TestNode
                                        {
                                            raw = "foo",
                                            cooked = "foo"
                                        },
                                        tail = false
                                    },
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 13,
                                        end = 19,
                                        value = new TestNode
                                        {
                                            raw = "\\u25a0",
                                            cooked = "■"
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("`\\unicode`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("`\\u`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("`\\u{`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("`\\u{abcdx`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("`\\u{abcdx}`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("`\\xylophone`", "Bad escape sequence in untagged template literal (1:1)", new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("foo`\\unicode`", "Bad character escape sequence (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("foo`\\xylophone`", "Bad character escape sequence (1:6)", new TestOptions
            {
                ecmaVersion = 8
            });

            Program.testFail("foo`\\unicode", "Unterminated template (1:4)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("foo`\\unicode\\`", "Unterminated template (1:4)", new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\unicode`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 13,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 13,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 13,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 12,
                                        value = new TestNode
                                        {
                                            raw = "\\unicode",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`foo${bar}\\unicode`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 22,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 22,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 22,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 22,
                                expressions = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 12,
                                        name = "bar"
                                    }
                                },
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 7,
                                        value = new TestNode
                                        {
                                            raw = "foo",
                                            cooked = "foo"
                                        },
                                        tail = false
                                    },
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 13,
                                        end = 21,
                                        value = new TestNode
                                        {
                                            raw = "\\unicode",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\u`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 7,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 7,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 7,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 7,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 6,
                                        value = new TestNode
                                        {
                                            raw = "\\u",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\u{`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 8,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 8,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 8,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 8,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 7,
                                        value = new TestNode
                                        {
                                            raw = "\\u{",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\u{abcdx`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 13,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 13,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 13,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 12,
                                        value = new TestNode
                                        {
                                            raw = "\\u{abcdx",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\u{abcdx}`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 14,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 14,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 14,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 14,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 13,
                                        value = new TestNode
                                        {
                                            raw = "\\u{abcdx}",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("foo`\\unicode\\\\`", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 15,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 15,
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            start = 0,
                            end = 15,
                            tag = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                start = 3,
                                end = 15,
                                expressions = new TestNode[0],
                                quasis = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        start = 4,
                                        end = 14,
                                        value = new TestNode
                                        {
                                            raw = "\\unicode\\\\",
                                            cooked = null
                                        },
                                        tail = true
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.test("`${ {class: 1} }`", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("`${ {delete: 1} }`", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("`${ {enum: 1} }`", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("`${ {function: 1} }`", new TestNode(), new TestOptions
            {
                ecmaVersion = 9
            });
        }
    }
}
