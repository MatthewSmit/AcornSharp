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

            Test("foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)))
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "!",
                            prefix = true,
                            argument = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 24, 24)))
                            {
                                type = NodeType.FunctionExpression,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                bexpression = false,
                                @params = new List<BaseNode>(),
                                fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)))
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)))
                                        {
                                            type = NodeType.ExpressionStatement,
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

            Test("() => { foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 13, 13)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)))
                                    {
                                        type = NodeType.ExpressionStatement,
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

            Test("100", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)))
                        {
                            type = NodeType.Literal,
                            value = 100,
                            raw = "100"
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("\"use strict\" + 1", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16)))
                        {
                            type = NodeType.BinaryExpression,
                            left = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                            {
                                type = NodeType.Literal,
                                value = "use strict",
                                raw = "\"use strict\""
                            },
                            @operator = "+",
                            right = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)))
                            {
                                type = NodeType.Literal,
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

            Test("\"use strict\"\n foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new BaseNode(new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 14), new Position(2, 4, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("'use strict'; foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "'use strict'"
                        },
                        directive = "use strict"
                    },
                    new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use strict\"\n foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 37)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(2, 6, 37)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new BaseNode(new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(2, 1, 32), new Position(2, 4, 35)), "foo"),
                                    directive = null // check this property does not exist.
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("!function wrap() { \"use strict\"\n foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 38)))
                        {
                            type = NodeType.UnaryExpression,
                            @operator = "!",
                            prefix = true,
                            argument = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(2, 6, 38)))
                            {
                                type = NodeType.FunctionExpression,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                                generator = false,
                                bexpression = false,
                                @params = new List<BaseNode>(),
                                fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(2, 6, 38)))
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                        {
                                            type = NodeType.ExpressionStatement,
                                            expression = new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                            {
                                                type = NodeType.Literal,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = "use strict"
                                        },
                                        new BaseNode(new SourceLocation(new Position(2, 1, 33), new Position(2, 4, 36)))
                                        {
                                            type = NodeType.ExpressionStatement,
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

            Test("() => { \"use strict\"\n foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(2, 6, 27)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 6, 6), new Position(2, 6, 27)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)))
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)))
                                        {
                                            type = NodeType.Literal,
                                            value = "use strict",
                                            raw = "\"use strict\""
                                        },
                                        directive = "use strict"
                                    },
                                    new BaseNode(new SourceLocation(new Position(2, 1, 22), new Position(2, 4, 25)))
                                    {
                                        type = NodeType.ExpressionStatement,
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

            Test("() => \"use strict\"", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = true,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 18, 18)))
                            {
                                type = NodeType.Literal,
                                value = "use strict",
                                raw = "\"use strict\""
                            }
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("({ wrap() { \"use strict\"; foo } })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 33, 33)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 31, 31)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)), "wrap"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 31, 31)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 25, 25)))
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new BaseNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 24, 24)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)))
                                                {
                                                    type = NodeType.ExpressionStatement,
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

            Test("(class { wrap() { \"use strict\"; foo } })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 39, 39)))
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 39, 39)))
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 37, 37)))
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                                        @static = false,
                                        kind = "method",
                                        value = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 37, 37)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            generator = false,
                                            bexpression = false,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 31, 31)))
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30)))
                                                        {
                                                            type = NodeType.Literal,
                                                            value = "use strict",
                                                            raw = "\"use strict\""
                                                        },
                                                        directive = "use strict"
                                                    },
                                                    new BaseNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)))
                                                    {
                                                        type = NodeType.ExpressionStatement,
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
            Test("\"\\u0075se strict\"", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                        {
                            type = NodeType.Literal,
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

            Test("\"use asm\"; \"use strict\"; foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                        {
                            type = NodeType.Literal,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = "use asm"
                    },
                    new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 23, 23)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new BaseNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; \"use strict\"; foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new BaseNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 42, 42)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 41, 41)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                },
                                new BaseNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 46, 46)))
                                {
                                    type = NodeType.ExpressionStatement,
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

            Test("\"use strict\"; foo; \"use asm\"", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = "use strict"
                    },
                    new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                        directive = null // check this property does not exist.
                    },
                    new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 28, 28)))
                        {
                            type = NodeType.Literal,
                            value = "use asm",
                            raw = "\"use asm\""
                        },
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { \"use asm\"; foo; \"use strict\" }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 48, 48)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 28, 28)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 27, 27)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use asm",
                                        raw = "\"use asm\""
                                    },
                                    directive = "use asm"
                                },
                                new BaseNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 33, 33)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), "foo"),
                                    directive = null // check this property does not exist.
                                },
                                new BaseNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 46, 46)))
                                    {
                                        type = NodeType.Literal,
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

            Test("{ \"use strict\"; }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)))
                    {
                        type = NodeType.BlockStatement,
                        body = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15)))
                            {
                                type = NodeType.ExpressionStatement,
                                expression = new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 14, 14)))
                                {
                                    type = NodeType.Literal,
                                    value = "use strict",
                                    raw = "\"use strict\""
                                },
                                directive = null // check this property does not exist.
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { { \"use strict\" } foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 40, 40)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 34, 34)))
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>
                                    {
                                        new BaseNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)))
                                        {
                                            type = NodeType.ExpressionStatement,
                                            expression = new BaseNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 32, 32)))
                                            {
                                                type = NodeType.Literal,
                                                value = "use strict",
                                                raw = "\"use strict\""
                                            },
                                            directive = null // check this property does not exist.
                                        }
                                    }
                                },
                                new BaseNode(new SourceLocation(new Position(1, 35, 35), new Position(1, 38, 38)))
                                {
                                    type = NodeType.ExpressionStatement,
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

            Test("(\"use strict\"); foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function wrap() { (\"use strict\"); foo }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "wrap"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 39, 39)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 33, 33)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = null // check this property does not exist.
                                },
                                new BaseNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)))
                                {
                                    type = NodeType.ExpressionStatement,
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

            Test("function a() { \"use strict\" } \"use strict\"; foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 29, 29)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 27, 27)))
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "\"use strict\""
                                    },
                                    directive = "use strict"
                                }
                            }
                        }
                    },
                    new BaseNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 43, 43)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 42, 42)))
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\""
                        },
                        directive = null // check this property does not exist.
                    },
                    new BaseNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new IdentifierNode(new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)), "foo"),
                        directive = null // check this property does not exist.
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("function a(a = function() { \"use strict\"; foo }) { \"use strict\" }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 65, 65)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "a"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 47, 47)))
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a"),
                                right = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 47, 47)))
                                {
                                    type = NodeType.FunctionExpression,
                                    id = null,
                                    generator = false,
                                    bexpression = false,
                                    @params = new List<BaseNode>(),
                                    fbody = new BaseNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 47, 47)))
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 41, 41)))
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new BaseNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 40, 40)))
                                                {
                                                    type = NodeType.Literal,
                                                    value = "use strict",
                                                    raw = "\"use strict\""
                                                },
                                                directive = "use strict"
                                            },
                                            new BaseNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)))
                                            {
                                                type = NodeType.ExpressionStatement,
                                                expression = new IdentifierNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), "foo"),
                                                directive = null // check this property does not exist.
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BaseNode(new SourceLocation(new Position(1, 49, 49), new Position(1, 65, 65)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)))
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63)))
                                    {
                                        type = NodeType.Literal,
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

            Test("(a = () => { \"use strict\"; foo }) => { \"use strict\" }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 53, 53)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32)))
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    right = new BaseNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 32, 32)))
                                    {
                                        type = NodeType.ArrowFunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 32, 32)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26)))
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new BaseNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 25, 25)))
                                                    {
                                                        type = NodeType.Literal,
                                                        value = "use strict",
                                                        raw = "\"use strict\""
                                                    },
                                                    directive = "use strict"
                                                },
                                                new BaseNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)))
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), "foo"),
                                                    directive = null // check this property does not exist.
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new BaseNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 53, 53)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)))
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 51, 51)))
                                        {
                                            type = NodeType.Literal,
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
