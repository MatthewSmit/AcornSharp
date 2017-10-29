using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsDirective()
        {
            //------------------------------------------------------------------------
            // No directives
            //------------------------------------------------------------------------

            Test("foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 3,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 3,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 23,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 23,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 23,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 18,
                                    end = 21,
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 24,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 24,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            start = 0,
                            end = 24,
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.FunctionExpression,
                                start = 1,
                                end = 24,
                                id =  new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                bexpression = false,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    start = 17,
                                    end = 24,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            start = 19,
                                            end = 22,
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

            Test("() => { foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 13,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 13,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 6,
                                end = 13,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        start = 8,
                                        end = 11,
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

            Test("100", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 3,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 3,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 3,
                            value = 100,
                            raw = "100"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("\"use strict\" + 1", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 16,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 16,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            start = 0,
                            end = 16,
                            left = new Node
                            {
                                type = NodeType.Literal,
                                start = 0,
                                end = 12,
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                start = 15,
                                end = 16,
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

            Test("\"use strict\"\n foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 12,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 14,
                        end = 17,
                        expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("'use strict'; foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 14,
                        end = 17,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use strict\"\n foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 37,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 37,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 37,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 18,
                                    end = 30,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 18,
                                        end = 30,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 32,
                                    end = 35,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { \"use strict\"\n foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 38,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 38,
                        expression = new Node
                        {
                            type = NodeType.UnaryExpression,
                            start = 0,
                            end = 38,
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.FunctionExpression,
                                start = 1,
                                end = 38,
                                id =  new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                bexpression = false,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    start = 17,
                                    end = 38,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            start = 19,
                                            end = 31,
                                            expression = new Node
                                            {
                                                type = NodeType.Literal,
                                                start = 19,
                                                end = 31,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            start = 33,
                                            end = 36,
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

            Test("() => { \"use strict\"\n foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 27,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 27,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 6,
                                end = 27,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        start = 8,
                                        end = 20,
                                        expression = new Node
                                        {
                                            type = NodeType.Literal,
                                            start = 8,
                                            end = 20,
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        start = 22,
                                        end = 25,
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

            Test("() => \"use strict\"", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 18,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 18,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 18,
                            id = null,
                            generator = false,
                            bexpression = true,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                start = 6,
                                end = 18,
                                value = "use strict",
                                raw = "\"use strict\""
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("({ wrap() { \"use strict\"; foo } })", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 34,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 34,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            start = 1,
                            end = 33,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    start = 3,
                                    end = 31,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), "wrap"),
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 7,
                                        end = 31,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 10,
                                            end = 31,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    start = 12,
                                                    end = 25,
                                                    expression = new Node
                                                    {
                                                        type = NodeType.Literal,
                                                        start = 12,
                                                        end = 24,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    start = 26,
                                                    end = 29,
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

            Test("(class { wrap() { \"use strict\"; foo } })", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 40,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 40,
                        expression = new Node
                        {
                            type = NodeType.ClassExpression,
                            start = 1,
                            end = 39,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                start = 7,
                                end = 39,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        start = 9,
                                        end = 37,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                                        @static = false,
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            start = 13,
                                            end = 37,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                start = 16,
                                                end = 37,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        start = 18,
                                                        end = 31,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.Literal,
                                                            start = 18,
                                                            end = 30,
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        start = 32,
                                                        end = 35,
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
            Test("\"\\u0075se strict\"", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 17,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 17,
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

            Test("\"use asm\"; \"use strict\"; foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 28,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 10,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 9,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 11,
                        end = 24,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 11,
                            end = 23,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 25,
                        end = 28,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; \"use strict\"; foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 48,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 48,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 48,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 18,
                                    end = 28,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 29,
                                    end = 42,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 29,
                                        end = 41,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 43,
                                    end = 46,
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

            Test("\"use strict\"; foo; \"use asm\"", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 28,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 14,
                        end = 18,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 19,
                        end = 28,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 19,
                            end = 28,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; foo; \"use strict\" }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 48,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 48,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 48,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 18,
                                    end = 28,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 29,
                                    end = 33,
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), "foo"),
                                    directive = null // check this property does not exist.
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 34,
                                    end = 46,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
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
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string in a block.
            //------------------------------------------------------------------------

            Test("{ \"use strict\"; }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.BlockStatement,
                        start = 0,
                        end = 17,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExpressionStatement,
                                start = 2,
                                end = 15,
                                expression = new Node
                                {
                                    type = NodeType.Literal,
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
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { { \"use strict\" } foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 40,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 40,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 40,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.BlockStatement,
                                    start = 18,
                                    end = 34,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            start = 20,
                                            end = 32,
                                            expression = new Node
                                            {
                                                type = NodeType.Literal,
                                                start = 20,
                                                end = 32,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 35,
                                    end = 38,
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

            Test("(\"use strict\"); foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 19,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 15,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 1,
                            end = 13,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 16,
                        end = 19,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { (\"use strict\"); foo }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 39,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 39,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 16,
                            end = 39,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 18,
                                    end = 33,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 19,
                                        end = 31,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 34,
                                    end = 37,
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

            Test("function a() { \"use strict\" } \"use strict\"; foo", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 47,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 29,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 13,
                            end = 29,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 15,
                                    end = 27,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
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
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 30,
                        end = 43,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            start = 30,
                            end = 42,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 44,
                        end = 47,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 65,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 65,
                        id =  new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                start = 11,
                                end = 47,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a"),
                                right = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    start = 15,
                                    end = 47,
                                    id = null,
                                    generator = false,
                                    bexpression = false,
                                    @params = new List<Node>(),
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        start = 26,
                                        end = 47,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                start = 28,
                                                end = 41,
                                                expression = new Node
                                                {
                                                    type = NodeType.Literal,
                                                    start = 28,
                                                    end = 40,
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                start = 42,
                                                end = 45,
                                                expression = new IdentifierNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), "foo"),
                                                directive = null // check this property does not exist.
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 49,
                            end = 65,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 51,
                                    end = 63,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
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
            }, new Options {ecmaVersion = 6});

            Test("(a = () => { \"use strict\"; foo }) => { \"use strict\" }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 53,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 53,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 53,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.AssignmentPattern,
                                    start = 1,
                                    end = 32,
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    right = new Node
                                    {
                                        type = NodeType.ArrowFunctionExpression,
                                        start = 5,
                                        end = 32,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 11,
                                            end = 32,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    start = 13,
                                                    end = 26,
                                                    expression = new Node
                                                    {
                                                        type = NodeType.Literal,
                                                        start = 13,
                                                        end = 25,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    start = 27,
                                                    end = 30,
                                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), "foo"),
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 37,
                                end = 53,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        start = 39,
                                        end = 51,
                                        expression = new Node
                                        {
                                            type = NodeType.Literal,
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
            }, new Options {ecmaVersion = 6});
        }
    }
}
