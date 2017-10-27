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
                type = "Program",
                start = 0,
                end = 3,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 3,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 0,
                            end = 3,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 23,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 23,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 23,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 18,
                                    end = 21,
                                    expression = new Node
                                    {
                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 24,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 24,
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            start = 0,
                            end = 24,
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = "FunctionExpression",
                                start = 1,
                                end = 24,
                                id =  new Node
                                {
                                    type = "Identifier",
                                    start = 10,
                                    end = 14,
                                    name = "wrap"
                                },
                                generator = false,
                                bexpression = false,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    start = 17,
                                    end = 24,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            start = 19,
                                            end = 22,
                                            expression = new Node
                                            {
                                                type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("() => { foo }", new Node
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
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 13,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 6,
                                end = 13,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        start = 8,
                                        end = 11,
                                        expression = new Node
                                        {
                                            type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("100", new Node
            {
                type = "Program",
                start = 0,
                end = 3,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 3,
                        expression = new Node
                        {
                            type = "Literal",
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
                type = "Program",
                start = 0,
                end = 16,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 16,
                        expression = new Node
                        {
                            type = "BinaryExpression",
                            start = 0,
                            end = 16,
                            left = new Node
                            {
                                type = "Literal",
                                start = 0,
                                end = 12,
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = "Literal",
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
                type = "Program",
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 12,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 14,
                        end = 17,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("'use strict'; foo", new Node
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
                        end = 13,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 14,
                        end = 17,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use strict\"\n foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 37,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 37,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 37,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 18,
                                    end = 30,
                                    expression = new Node
                                    {
                                        type = "Literal",
                                        start = 18,
                                        end = 30,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 32,
                                    end = 35,
                                    expression = new Node
                                    {
                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { \"use strict\"\n foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 38,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 38,
                        expression = new Node
                        {
                            type = "UnaryExpression",
                            start = 0,
                            end = 38,
                            @operator = "!",
                            prefix = true,
                            argument = new Node
                            {
                                type = "FunctionExpression",
                                start = 1,
                                end = 38,
                                id =  new Node
                                {
                                    type = "Identifier",
                                    start = 10,
                                    end = 14,
                                    name = "wrap"
                                },
                                generator = false,
                                bexpression = false,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = "BlockStatement",
                                    start = 17,
                                    end = 38,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            start = 19,
                                            end = 31,
                                            expression = new Node
                                            {
                                                type = "Literal",
                                                start = 19,
                                                end = 31,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            start = 33,
                                            end = 36,
                                            expression = new Node
                                            {
                                                type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("() => { \"use strict\"\n foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 27,
                        expression = new Node
                        {
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 27,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 6,
                                end = 27,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        start = 8,
                                        end = 20,
                                        expression = new Node
                                        {
                                            type = "Literal",
                                            start = 8,
                                            end = 20,
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        start = 22,
                                        end = 25,
                                        expression = new Node
                                        {
                                            type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("() => \"use strict\"", new Node
            {
                type = "Program",
                start = 0,
                end = 18,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 18,
                        expression = new Node
                        {
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 18,
                            id = null,
                            generator = false,
                            bexpression = true,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = "Literal",
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
                type = "Program",
                start = 0,
                end = 34,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 34,
                        expression = new Node
                        {
                            type = "ObjectExpression",
                            start = 1,
                            end = 33,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = "Property",
                                    start = 3,
                                    end = 31,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = "Identifier",
                                        start = 3,
                                        end = 7,
                                        name = "wrap"
                                    },
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = "FunctionExpression",
                                        start = 7,
                                        end = 31,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
                                            start = 10,
                                            end = 31,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = "ExpressionStatement",
                                                    start = 12,
                                                    end = 25,
                                                    expression = new Node
                                                    {
                                                        type = "Literal",
                                                        start = 12,
                                                        end = 24,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new Node
                                                {
                                                    type = "ExpressionStatement",
                                                    start = 26,
                                                    end = 29,
                                                    expression = new Node
                                                    {
                                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            Test("(class { wrap() { \"use strict\"; foo } })", new Node
            {
                type = "Program",
                start = 0,
                end = 40,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 40,
                        expression = new Node
                        {
                            type = "ClassExpression",
                            start = 1,
                            end = 39,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = "ClassBody",
                                start = 7,
                                end = 39,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "MethodDefinition",
                                        start = 9,
                                        end = 37,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            start = 9,
                                            end = 13,
                                            name = "wrap"
                                        },
                                        @static = false,
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            start = 13,
                                            end = 37,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
                                                start = 16,
                                                end = 37,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        start = 18,
                                                        end = 31,
                                                        expression = new Node
                                                        {
                                                            type = "Literal",
                                                            start = 18,
                                                            end = 30,
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new Node
                                                    {
                                                        type = "ExpressionStatement",
                                                        start = 32,
                                                        end = 35,
                                                        expression = new Node
                                                        {
                                                            type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            // Should not decode escape sequence.
            Test("\"\\u0075se strict\"", new Node
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
                            type = "Literal",
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
                type = "Program",
                start = 0,
                end = 28,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 10,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 0,
                            end = 9,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 11,
                        end = 24,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 11,
                            end = 23,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 25,
                        end = 28,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 25,
                            end = 28,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; \"use strict\"; foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 48,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 48,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 48,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 18,
                                    end = 28,
                                    expression = new Node
                                    {
                                        type = "Literal",
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 29,
                                    end = 42,
                                    expression = new Node
                                    {
                                        type = "Literal",
                                        start = 29,
                                        end = 41,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 43,
                                    end = 46,
                                    expression = new Node
                                    {
                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string after other expressions.
            //------------------------------------------------------------------------

            Test("\"use strict\"; foo; \"use asm\"", new Node
            {
                type = "Program",
                start = 0,
                end = 28,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 13,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 0,
                            end = 12,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 14,
                        end = 18,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 14,
                            end = 17,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 19,
                        end = 28,
                        expression = new Node
                        {
                            type = "Literal",
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
                type = "Program",
                start = 0,
                end = 48,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 48,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 48,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 18,
                                    end = 28,
                                    expression = new Node
                                    {
                                        type = "Literal",
                                        start = 18,
                                        end = 27,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 29,
                                    end = 33,
                                    expression = new Node
                                    {
                                        type = "Identifier",
                                        start = 29,
                                        end = 32,
                                        name = "foo"
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 34,
                                    end = 46,
                                    expression = new Node
                                    {
                                        type = "Literal",
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
                type = "Program",
                start = 0,
                end = 17,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "BlockStatement",
                        start = 0,
                        end = 17,
                        body = new List<Node>
                        {
                            new Node
                            {
                                type = "ExpressionStatement",
                                start = 2,
                                end = 15,
                                expression = new Node
                                {
                                    type = "Literal",
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
                type = "Program",
                start = 0,
                end = 40,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 40,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 40,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "BlockStatement",
                                    start = 18,
                                    end = 34,
                                    body = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = "ExpressionStatement",
                                            start = 20,
                                            end = 32,
                                            expression = new Node
                                            {
                                                type = "Literal",
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
                                    type = "ExpressionStatement",
                                    start = 35,
                                    end = 38,
                                    expression = new Node
                                    {
                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // One string with parentheses.
            //------------------------------------------------------------------------

            Test("(\"use strict\"); foo", new Node
            {
                type = "Program",
                start = 0,
                end = 19,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 15,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 1,
                            end = 13,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 16,
                        end = 19,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 16,
                            end = 19,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { (\"use strict\"); foo }", new Node
            {
                type = "Program",
                start = 0,
                end = 39,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 39,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 13,
                            name = "wrap"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 16,
                            end = 39,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 18,
                                    end = 33,
                                    expression = new Node
                                    {
                                        type = "Literal",
                                        start = 19,
                                        end = 31,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 34,
                                    end = 37,
                                    expression = new Node
                                    {
                                        type = "Identifier",
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
            }, new Options {ecmaVersion = 6});

            //------------------------------------------------------------------------
            // Complex cases such as the function in a default parameter.
            //------------------------------------------------------------------------

            Test("function a() { \"use strict\" } \"use strict\"; foo", new Node
            {
                type = "Program",
                start = 0,
                end = 47,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 29,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 10,
                            name = "a"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 13,
                            end = 29,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 15,
                                    end = 27,
                                    expression = new Node
                                    {
                                        type = "Literal",
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
                        type = "ExpressionStatement",
                        start = 30,
                        end = 43,
                        expression = new Node
                        {
                            type = "Literal",
                            start = 30,
                            end = 42,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 44,
                        end = 47,
                        expression = new Node
                        {
                            type = "Identifier",
                            start = 44,
                            end = 47,
                            name = "foo"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new Node
            {
                type = "Program",
                start = 0,
                end = 65,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 65,
                        id =  new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 10,
                            name = "a"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = "AssignmentPattern",
                                start = 11,
                                end = 47,
                                left = new Node
                                {
                                    type = "Identifier",
                                    start = 11,
                                    end = 12,
                                    name = "a"
                                },
                                right = new Node
                                {
                                    type = "FunctionExpression",
                                    start = 15,
                                    end = 47,
                                    id = null,
                                    generator = false,
                                    bexpression = false,
                                    @params = new List<Node>(),
                                    fbody = new Node
                                    {
                                        type = "BlockStatement",
                                        start = 26,
                                        end = 47,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                start = 28,
                                                end = 41,
                                                expression = new Node
                                                {
                                                    type = "Literal",
                                                    start = 28,
                                                    end = 40,
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new Node
                                            {
                                                type = "ExpressionStatement",
                                                start = 42,
                                                end = 45,
                                                expression = new Node
                                                {
                                                    type = "Identifier",
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
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 49,
                            end = 65,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "ExpressionStatement",
                                    start = 51,
                                    end = 63,
                                    expression = new Node
                                    {
                                        type = "Literal",
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
                type = "Program",
                start = 0,
                end = 53,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 53,
                        expression = new Node
                        {
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 53,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "AssignmentPattern",
                                    start = 1,
                                    end = 32,
                                    left = new Node
                                    {
                                        type = "Identifier",
                                        start = 1,
                                        end = 2,
                                        name = "a"
                                    },
                                    right = new Node
                                    {
                                        type = "ArrowFunctionExpression",
                                        start = 5,
                                        end = 32,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
                                            start = 11,
                                            end = 32,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = "ExpressionStatement",
                                                    start = 13,
                                                    end = 26,
                                                    expression = new Node
                                                    {
                                                        type = "Literal",
                                                        start = 13,
                                                        end = 25,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new Node
                                                {
                                                    type = "ExpressionStatement",
                                                    start = 27,
                                                    end = 30,
                                                    expression = new Node
                                                    {
                                                        type = "Identifier",
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
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 37,
                                end = 53,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "ExpressionStatement",
                                        start = 39,
                                        end = 51,
                                        expression = new Node
                                        {
                                            type = "Literal",
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
