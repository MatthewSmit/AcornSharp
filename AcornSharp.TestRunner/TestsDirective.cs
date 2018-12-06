using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsDirective
    {
        public static void Run()
        {
            //------------------------------------------------------------------------
            // No directives
            //------------------------------------------------------------------------

            Program.test("foo", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 3,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 3,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 0,
                            end = 3,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 23,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 23,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 23,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 18,
                                    end = 21,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 18,
                                        end = 21,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("!function wrap() { foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 24,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 24,
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            start = 0,
                            end = 24,
                            @operator = "!",
                            prefix = true,
                            argument = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                start = 1,
                                end = 24,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 10,
                                    end = 14,
                                    name = "wrap"
                                },
                                generator = false,
                                expression = false,
                                @params = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 17,
                                    end = 24,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 19,
                                            end = 22,
                                            expression = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 19,
                                                end = 22,
                                                name = "foo"
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("() => { foo }", new TestNode
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
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 13,
                            id = null,
                            generator = false,
                            expression = false,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 6,
                                end = 13,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        start = 8,
                                        end = 11,
                                        expression = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 8,
                                            end = 11,
                                            name = "foo"
                                        },
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("100", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 3,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 3,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 3,
                            value = 100,
                            raw = "100"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("\"use strict\" + 1", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 16,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 16,
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            start = 0,
                            end = 16,
                            left = new TestNode
                            {
                                type = typeof(LiteralNode),
                                start = 0,
                                end = 12,
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = "+",
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                start = 15,
                                end = 16,
                                value = 1,
                                raw = "1"
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// One directive
//------------------------------------------------------------------------

            Program.test("\"use strict\"\n foo", new TestNode
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
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 14,
                        end = 17,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("'use strict'; foo", new TestNode
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
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 14,
                        end = 17,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 37,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 37,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 37,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 18,
                                    end = 30,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 18,
                                        end = 30,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 32,
                                    end = 35,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 32,
                                        end = 35,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("!function wrap() { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 38,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 38,
                        expression = new TestNode
                        {
                            type = typeof(UnaryExpressionNode),
                            start = 0,
                            end = 38,
                            @operator = "!",
                            prefix = true,
                            argument = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                start = 1,
                                end = 38,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 10,
                                    end = 14,
                                    name = "wrap"
                                },
                                generator = false,
                                expression = false,
                                @params = new TestNode[0],
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 17,
                                    end = 38,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 19,
                                            end = 31,
                                            expression = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                start = 19,
                                                end = 31,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 33,
                                            end = 36,
                                            expression = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 33,
                                                end = 36,
                                                name = "foo"
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("() => { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 27,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 27,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 27,
                            id = null,
                            generator = false,
                            expression = false,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 6,
                                end = 27,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        start = 8,
                                        end = 20,
                                        expression = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 8,
                                            end = 20,
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        start = 22,
                                        end = 25,
                                        expression = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 22,
                                            end = 25,
                                            name = "foo"
                                        },
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("() => \"use strict\"", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 18,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 18,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 18,
                            id = null,
                            generator = false,
                            expression = true,
                            @params = new TestNode[0],
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                start = 6,
                                end = 18,
                                value = "use strict",
                                raw = "\"use strict\""
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("({ wrap() { \"use strict\"; foo } })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 34,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 34,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 33,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 3,
                                    end = 31,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 3,
                                        end = 7,
                                        name = "wrap"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        start = 7,
                                        end = 31,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 10,
                                            end = 31,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 12,
                                                    end = 25,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 12,
                                                        end = 24,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 26,
                                                    end = 29,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 26,
                                                        end = 29,
                                                        name = "foo"
                                                    },
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("(class { wrap() { \"use strict\"; foo } })", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 40,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 40,
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            start = 1,
                            end = 39,
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                start = 7,
                                end = 39,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        start = 9,
                                        end = 37,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 9,
                                            end = 13,
                                            name = "wrap"
                                        },
                                        @static = false,
                                        kind = PropertyKind.Method,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            start = 13,
                                            end = 37,
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            @params = new TestNode[0],
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                start = 16,
                                                end = 37,
                                                body = new[]
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        start = 18,
                                                        end = 31,
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(LiteralNode),
                                                            start = 18,
                                                            end = 30,
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        start = 32,
                                                        end = 35,
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 32,
                                                            end = 35,
                                                            name = "foo"
                                                        },
                                                        directive = null // check this property does not exist.
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

// Should not decode escape sequence.
            Program.test("\"\\u0075se strict\"", new TestNode
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
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 17,
                            value = "use strict",
                            raw = "\"\\u0075se strict\""
                        },
                        directive = "\\u0075se strict"
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// Two or more directives.
//------------------------------------------------------------------------

            Program.test("\"use asm\"; \"use strict\"; foo", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 28,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 10,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 9,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 11,
                        end = 24,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 11,
                            end = 23,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 25,
                        end = 28,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 25,
                            end = 28,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { \"use asm\"; \"use strict\"; foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 48,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 48,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 48,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 18,
                                    end = 28,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 29,
                                    end = 42,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 29,
                                        end = 41,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 43,
                                    end = 46,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 43,
                                        end = 46,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// One string after other expressions.
//------------------------------------------------------------------------

            Program.test("\"use strict\"; foo; \"use asm\"", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 28,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 14,
                        end = 18,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 19,
                        end = 28,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 19,
                            end = 28,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { \"use asm\"; foo; \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 48,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 48,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 48,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 18,
                                    end = 28,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 29,
                                    end = 33,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 29,
                                        end = 32,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 34,
                                    end = 46,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 34,
                                        end = 46,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// One string in a block.
//------------------------------------------------------------------------

            Program.test("{ \"use strict\"; }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 17,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(BlockStatementNode),
                        start = 0,
                        end = 17,
                        body = new[]
                        {
                            new TestNode
                            {
                                type = typeof(ExpressionStatementNode),
                                start = 2,
                                end = 15,
                                expression = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    start = 2,
                                    end = 14,
                                    value = "use strict",
                                    raw = "\"use strict\""
                                },
                                directive = null // check this property does not exist.
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { { \"use strict\" } foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 40,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 40,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 40,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    start = 18,
                                    end = 34,
                                    body = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            start = 20,
                                            end = 32,
                                            expression = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                start = 20,
                                                end = 32,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 35,
                                    end = 38,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 35,
                                        end = 38,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// One string with parentheses.
//------------------------------------------------------------------------

            Program.test("(\"use strict\"); foo", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 19,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 15,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 1,
                            end = 13,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 16,
                        end = 19,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 16,
                            end = 19,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function wrap() { (\"use strict\"); foo }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 39,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 39,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 16,
                            end = 39,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 18,
                                    end = 33,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 19,
                                        end = 31,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 34,
                                    end = 37,
                                    expression = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 34,
                                        end = 37,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

//------------------------------------------------------------------------
// Complex cases such as the function in a default parameter.
//------------------------------------------------------------------------

            Program.test("function a() { \"use strict\" } \"use strict\"; foo", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 47,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 29,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 10,
                            name = "a"
                        },
                        generator = false,
                        expression = false,
                        @params = new TestNode[0],
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 13,
                            end = 29,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 15,
                                    end = 27,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 15,
                                        end = 27,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 30,
                        end = 43,
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            start = 30,
                            end = 42,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 44,
                        end = 47,
                        expression = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 44,
                            end = 47,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 65,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        start = 0,
                        end = 65,
                        id = new TestNode
                        {
                            type = typeof(IdentifierNode),
                            start = 9,
                            end = 10,
                            name = "a"
                        },
                        generator = false,
                        expression = false,
                        @params = new[]
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                start = 11,
                                end = 47,
                                left = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 11,
                                    end = 12,
                                    name = "a"
                                },
                                right = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    start = 15,
                                    end = 47,
                                    id = null,
                                    generator = false,
                                    expression = false,
                                    @params = new TestNode[0],
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        start = 26,
                                        end = 47,
                                        body = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                start = 28,
                                                end = 41,
                                                expression = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    start = 28,
                                                    end = 40,
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                start = 42,
                                                end = 45,
                                                expression = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 42,
                                                    end = 45,
                                                    name = "foo"
                                                },
                                                directive = null // check this property does not exist.
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            start = 49,
                            end = 65,
                            body = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    start = 51,
                                    end = 63,
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 51,
                                        end = 63,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });

            Program.test("(a = () => { \"use strict\"; foo }) => { \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 53,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 53,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 53,
                            id = null,
                            generator = false,
                            expression = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    start = 1,
                                    end = 32,
                                    left = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 1,
                                        end = 2,
                                        name = "a"
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(ArrowFunctionExpressionNode),
                                        start = 5,
                                        end = 32,
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        @params = new TestNode[0],
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            start = 11,
                                            end = 32,
                                            body = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 13,
                                                    end = 26,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        start = 13,
                                                        end = 25,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    start = 27,
                                                    end = 30,
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 27,
                                                        end = 30,
                                                        name = "foo"
                                                    },
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 37,
                                end = 53,
                                body = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        start = 39,
                                        end = 51,
                                        expression = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            start = 39,
                                            end = 51,
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new TestOptions
            {
                ecmaVersion = 6
            });
        }
    }
}
