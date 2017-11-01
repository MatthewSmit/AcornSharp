using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsDirective()
        {
            //------------------------------------------------------------------------
            // No directives
            //------------------------------------------------------------------------

            Test("foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                    {
                        expression = new UnaryExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                        {
                            @operator = "!",
                            prefix = true,
                            argument = new FunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24)))
                            {
                                id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                expression = false,
                                parameters = new List<BaseNode>(),
                                fbody = new BlockStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                {
                                    body = new List<BaseNode>
                                    {
                                        new ExpressionStatementNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)))
                                        {
                                            expression = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)), "foo"),
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("() => { foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 13, 13)))
                            {
                                body = new List<BaseNode>
                                {
                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                    {
                                        expression = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("100", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                        {
                            value = 100,
                            raw = "100"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("\"use strict\" + 1", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                    {
                        expression = new BinaryExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                        {
                            left = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                            {
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = "+",
                            right = new LiteralNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)))
                            {
                                value = 1,
                                raw = "1"
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One directive
            //------------------------------------------------------------------------

            Test("\"use strict\"\n foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 17)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("'use strict'; foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use strict\"\n foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(2, 6, 37)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { \"use strict\"\n foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
                    {
                        expression = new UnaryExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
                        {
                            @operator = "!",
                            prefix = true,
                            argument = new FunctionExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(2, 6, 38)))
                            {
                                id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                expression = false,
                                parameters = new List<BaseNode>(),
                                fbody = new BlockStatementNode(new SourceLocation(new Position(1, 17, 17), new Position(2, 6, 38)))
                                {
                                    body = new List<BaseNode>
                                    {
                                        new ExpressionStatementNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                        {
                                            expression = new LiteralNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                            {
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new ExpressionStatementNode(new SourceLocation(new Position(2, 1, 33), new Position(2, 4, 36)))
                                        {
                                            expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 33), new Position(2, 4, 36)), "foo"),
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("() => { \"use strict\"\n foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
                        {
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<BaseNode>(),
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 6, 6), new Position(2, 6, 27)))
                            {
                                body = new List<BaseNode>
                                {
                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)))
                                    {
                                        expression = new LiteralNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)))
                                        {
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new ExpressionStatementNode(new SourceLocation(new Position(2, 1, 22), new Position(2, 4, 25)))
                                    {
                                        expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 22), new Position(2, 4, 25)), "foo"),
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("() => \"use strict\"", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            id = null,
                            generator = false,
                            expression = true,
                            parameters = new List<BaseNode>(),
                            fbody = new LiteralNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 18, 18)))
                            {
                                value = "use strict",
                                raw = "\"use strict\""
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("({ wrap() { \"use strict\"; foo } })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        expression = new ObjectExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)))
                        {
                            properties = new List<PropertyNode>
                            {
                                new PropertyNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 31, 31)))
                                {
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), "wrap"),
                                    pkind = PropertyKind.Initialise,
                                    value = new FunctionExpressionNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31)))
                                        {
                                            body = new List<BaseNode>
                                            {
                                                new ExpressionStatementNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 25, 25)))
                                                {
                                                    expression = new LiteralNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 24, 24)))
                                                    {
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new ExpressionStatementNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)))
                                                {
                                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), "foo"),
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
            }, new Options {ecmaVersion = 6});

            Test("(class { wrap() { \"use strict\"; foo } })", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
                    {
                        expression = new ClassExpressionNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 39, 39)))
                        {
                            id = null,
                            superClass = null,
                            fbody = new ClassBodyNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 39, 39)))
                            {
                                body = new List<BaseNode>
                                {
                                    new MethodDefinitionNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37)))
                                    {
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                                        @static = false,
                                        pkind = PropertyKind.Method,
                                        value = new FunctionExpressionNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 37, 37)))
                                        {
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            parameters = new List<BaseNode>(),
                                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37)))
                                            {
                                                body = new List<BaseNode>
                                                {
                                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 31, 31)))
                                                    {
                                                        expression = new LiteralNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                                        {
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)))
                                                    {
                                                        expression = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), "foo"),
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
            }, new Options {ecmaVersion = 6});

            // Should not decode escape sequence.
            Test("\"\\u0075se strict\"", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            value = "use strict",
                            raw = "\"\\u0075se strict\""
                        },
                        directive = "\\u0075se strict"
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // Two or more directives.
            //------------------------------------------------------------------------

            Test("\"use asm\"; \"use strict\"; foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                        {
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 23, 23)))
                        {
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; \"use strict\"; foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)))
                                    {
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 42, 42)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 41, 41)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 46, 46)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 46, 46)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string after other expressions.
            //------------------------------------------------------------------------

            Test("\"use strict\"; foo; \"use asm\"", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)))
                        {
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; foo; \"use strict\" }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)))
                                    {
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 33, 33)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), "foo"),
                                    directive = null // check this property does not exist.
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string in a block.
            //------------------------------------------------------------------------

            Test("{ \"use strict\"; }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                body = new List<BaseNode>
                {
                    new BlockStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        body = new List<BaseNode>
                        {
                            new ExpressionStatementNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)))
                            {
                                expression = new LiteralNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 14, 14)))
                                {
                                    value = "use strict",
                                    raw = "\"use strict\""
                                },
                                directive = null // check this property does not exist.
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { { \"use strict\" } foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 40, 40)))
                        {
                            body = new List<BaseNode>
                            {
                                new BlockStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 34, 34)))
                                {
                                    body = new List<BaseNode>
                                    {
                                        new ExpressionStatementNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)))
                                        {
                                            expression = new LiteralNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)))
                                            {
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 35, 35), new Position(1, 38, 38)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 35, 35), new Position(1, 38, 38)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string with parentheses.
            //------------------------------------------------------------------------

            Test("(\"use strict\"); foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)))
                        {
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { (\"use strict\"); foo }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 39, 39)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 33, 33)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)))
                                {
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // Complex cases such as the function in a default parameter.
            //------------------------------------------------------------------------

            Test("function a() { \"use strict\" } \"use strict\"; foo", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>(),
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 29, 29)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 43, 43)))
                    {
                        expression = new LiteralNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 42, 42)))
                        {
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)))
                    {
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)))
            {
                body = new List<BaseNode>
                {
                    new FunctionDeclarationNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)))
                    {
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        expression = false,
                        parameters = new List<BaseNode>
                        {
                            new AssignmentPatternNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 47, 47)))
                            {
                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a"),
                                right = new FunctionExpressionNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 47, 47)))
                                {
                                    id = null,
                                    generator = false,
                                    expression = false,
                                    parameters = new List<BaseNode>(),
                                    fbody = new BlockStatementNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 47, 47)))
                                    {
                                        body = new List<BaseNode>
                                        {
                                            new ExpressionStatementNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 41, 41)))
                                            {
                                                expression = new LiteralNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 40, 40)))
                                                {
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new ExpressionStatementNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)))
                                            {
                                                expression = new IdentifierNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), "foo"),
                                                directive = null // check this property does not exist.
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 49, 49), new Position(1, 65, 65)))
                        {
                            body = new List<BaseNode>
                            {
                                new ExpressionStatementNode(new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)))
                                {
                                    expression = new LiteralNode(new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)))
                                    {
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("(a = () => { \"use strict\"; foo }) => { \"use strict\" }", new ProgramNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
            {
                body = new List<BaseNode>
                {
                    new ExpressionStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
                    {
                        expression = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
                        {
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<BaseNode>
                            {
                                new AssignmentPatternNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32)))
                                {
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    right = new ArrowFunctionExpressionNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 32, 32)))
                                    {
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        parameters = new List<BaseNode>(),
                                        fbody = new BlockStatementNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 32, 32)))
                                        {
                                            body = new List<BaseNode>
                                            {
                                                new ExpressionStatementNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26)))
                                                {
                                                    expression = new LiteralNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25)))
                                                    {
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new ExpressionStatementNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)))
                                                {
                                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), "foo"),
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new BlockStatementNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 53, 53)))
                            {
                                body = new List<BaseNode>
                                {
                                    new ExpressionStatementNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)))
                                    {
                                        expression = new LiteralNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)))
                                        {
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
            }, new Options {ecmaVersion = 6});
        }
    }
}
