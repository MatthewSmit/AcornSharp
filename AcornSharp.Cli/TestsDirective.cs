using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Tests
    {
        public static void TestsDirective()
        {
            //------------------------------------------------------------------------
            // No directives
            //------------------------------------------------------------------------

            Program.Test("foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), name = "foo" },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("!function wrap() { foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        expression = new TestNode { type = typeof(UnaryExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                            @operator = Operator.LogicalNot,
                            argument = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24)),
                                id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "wrap" },
                                generator = false,
                                expression = false,
                                parameters = new List<TestNode>(),
                                body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)),
                                    body = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)),
                                            expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)), name = "foo" },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("() => { foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 13, 13)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)),
                                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo" },
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("100", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)),
                            value = 100,
                            raw = "100"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("\"use strict\" + 1", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                        expression = new TestNode { type = typeof(BinaryExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)),
                            left = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = Operator.Addition,
                            right = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)),
                                value = 1,
                                raw = "1"
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // One directive
            //------------------------------------------------------------------------

            Program.Test("\"use strict\"\n foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 17)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("'use strict'; foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(2, 6, 37)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)), name = "foo" },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("!function wrap() { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)),
                        expression = new TestNode { type = typeof(UnaryExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)),
                            @operator = Operator.LogicalNot,
                            argument = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(2, 6, 38)),
                                id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "wrap" },
                                generator = false,
                                expression = false,
                                parameters = new List<TestNode>(),
                                body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(2, 6, 38)),
                                    body = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)),
                                            expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)),
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 1, 33), new Position(2, 4, 36)),
                                            expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 33), new Position(2, 4, 36)), name = "foo" },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("() => { \"use strict\"\n foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)),
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 6, 6), new Position(2, 6, 27)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)),
                                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)),
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(2, 1, 22), new Position(2, 4, 25)),
                                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 22), new Position(2, 4, 25)), name = "foo" },
                                        directive = null // check this property does not exist.
                                    }
                                }
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("() => \"use strict\"", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                            id = null,
                            generator = false,
                            expression = true,
                            parameters = new List<TestNode>(),
                            body = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 18, 18)),
                                value = "use strict",
                                raw = "\"use strict\""
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("({ wrap() { \"use strict\"; foo } })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 31, 31)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), name = "wrap" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31)),
                                            body = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 25, 25)),
                                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 24, 24)),
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)),
                                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), name = "foo" },
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
            }, new Options { ecmaVersion = 6 });

            Program.Test("(class { wrap() { \"use strict\"; foo } })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)),
                        expression = new TestNode { type = typeof(ClassExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 39, 39)),
                            id = null,
                            superClass = null,
                            body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 39, 39)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37)),
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                                        @static = false,
                                        kind = PropertyKind.Method,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 37, 37)),
                                            id = null,
                                            generator = false,
                                            expression = false,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37)),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 31, 31)),
                                                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)),
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)),
                                                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), name = "foo" },
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
            }, new Options { ecmaVersion = 6 });

            // Should not decode escape sequence.
            Program.Test("\"\\u0075se strict\"", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                            value = "use strict",
                            raw = "\"\\u0075se strict\""
                        },
                        directive = "\\u0075se strict"
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // Two or more directives.
            //------------------------------------------------------------------------

            Program.Test("\"use asm\"; \"use strict\"; foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 23, 23)),
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { \"use asm\"; \"use strict\"; foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)),
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 42, 42)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 41, 41)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 43, 43), new Position(1, 46, 46)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 43, 43), new Position(1, 46, 46)), name = "foo" },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // One string after other expressions.
            //------------------------------------------------------------------------

            Program.Test("\"use strict\"; foo; \"use asm\"", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), name = "foo" },
                        directive = null // check this property does not exist.
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)),
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { \"use asm\"; foo; \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)),
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 33, 33)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), name = "foo" },
                                    directive = null // check this property does not exist.
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // One string in a block.
            //------------------------------------------------------------------------

            Program.Test("{ \"use strict\"; }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                        body = new List<TestNode>
                        {
                            new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)),
                                expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 14, 14)),
                                    value = "use strict",
                                    raw = "\"use strict\""
                                },
                                directive = null // check this property does not exist.
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { { \"use strict\" } foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 40, 40)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 34, 34)),
                                    body = new List<TestNode>
                                    {
                                        new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)),
                                            expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)),
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 35, 35), new Position(1, 38, 38)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 35, 35), new Position(1, 38, 38)), name = "foo" },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // One string with parentheses.
            //------------------------------------------------------------------------

            Program.Test("(\"use strict\"); foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)),
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function wrap() { (\"use strict\"); foo }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "wrap" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 39, 39)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 33, 33)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)),
                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)), name = "foo" },
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            //------------------------------------------------------------------------
            // Complex cases such as the function in a default parameter.
            //------------------------------------------------------------------------

            Program.Test("function a() { \"use strict\" } \"use strict\"; foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "a" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 29, 29)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 43, 43)),
                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 42, 42)),
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)),
                        expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)), name = "foo" },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "a" },
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 47, 47)),
                                left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "a" },
                                right = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 47, 47)),
                                    id = null,
                                    generator = false,
                                    expression = false,
                                    parameters = new List<TestNode>(),
                                    body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 47, 47)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 41, 41)),
                                                expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 40, 40)),
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)),
                                                expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), name = "foo" },
                                                directive = null // check this property does not exist.
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 49, 49), new Position(1, 65, 65)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)),
                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)),
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 6 });

            Program.Test("(a = () => { \"use strict\"; foo }) => { \"use strict\" }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)),
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(AssignmentPatternNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32)),
                                    left = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a" },
                                    right = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 32, 32)),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 32, 32)),
                                            body = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26)),
                                                    expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25)),
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)),
                                                    expression = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), name = "foo" },
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 53, 53)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)),
                                        expression = new TestNode { type = typeof(LiteralNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)),
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
            }, new Options { ecmaVersion = 6 });
        }
    }
}
