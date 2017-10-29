using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTrailingCommasInFunc()
        {
            //------------------------------------------------------------------------------
            // allow

            Test("function foo(a,) { }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 20,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        @params = new List<Node>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a")
                        },
                        generator = false,
                        bexpression = false,
                        async = false,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 17,
                            end = 20,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(function(a,) { })", new Node
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
                            type = NodeType.FunctionExpression,
                            start = 1,
                            end = 17,
                            id = null,
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 14,
                                end = 17,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(a,) => a", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 9,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 9,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 9,
                            id = null,
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a")
                            },
                            generator = false,
                            bexpression = true,
                            async = false,
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("async (a,) => a", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 15,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 15,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            start = 0,
                            end = 15,
                            id = null,
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                            },
                            generator = false,
                            bexpression = true,
                            async = true,
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("({foo(a,) {}})", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 14,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 14,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            start = 1,
                            end = 13,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    start = 2,
                                    end = 12,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), "foo"),
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 5,
                                        end = 12,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 10,
                                            end = 12,
                                            body = new List<Node>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {foo(a,) {}}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        start = 0,
                        end = 20,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            start = 8,
                            end = 20,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    start = 9,
                                    end = 19,
                                    @static = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 12,
                                        end = 19,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 17,
                                            end = 19,
                                            body = new List<Node>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static foo(a,) {}}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        start = 0,
                        end = 27,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            start = 8,
                            end = 27,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    start = 9,
                                    end = 26,
                                    @static = true,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 19,
                                        end = 26,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 24,
                                            end = 26,
                                            body = new List<Node>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(class {foo(a,) {}})", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 20,
                        expression = new Node
                        {
                            type = NodeType.ClassExpression,
                            start = 1,
                            end = 19,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                start = 7,
                                end = 19,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        start = 8,
                                        end = 18,
                                        @static = false,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            start = 11,
                                            end = 18,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                start = 16,
                                                end = 18,
                                                body = new List<Node>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(class {static foo(a,) {}})", new Node
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
                            type = NodeType.ClassExpression,
                            start = 1,
                            end = 26,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                start = 7,
                                end = 26,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        start = 8,
                                        end = 25,
                                        @static = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            start = 18,
                                            end = 25,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a")
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                start = 23,
                                                end = 25,
                                                body = new List<Node>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("export default function foo(a,) { }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 35,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        start = 0,
                        end = 35,
                        declaration = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            start = 15,
                            end = 35,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)), "foo"),
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 32,
                                end = 35,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("export default (function foo(a,) { })", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 37,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        start = 0,
                        end = 37,
                        declaration = new Node
                        {
                            type = NodeType.FunctionExpression,
                            start = 16,
                            end = 36,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "foo"),
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 33,
                                end = 36,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("export function foo(a,) { }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        start = 0,
                        end = 27,
                        declaration = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            start = 7,
                            end = 27,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            @params = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                start = 24,
                                end = 27,
                                body = new List<Node>()
                            }
                        },
                        specifiers = new List<Node>(),
                        source = null
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("foo(a,)", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 7,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 7,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            start = 0,
                            end = 7,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "a")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("new foo(a,)", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 11,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 11,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            start = 0,
                            end = 11,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                            arguments = new List<Node>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("foo(...a,)", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 10,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 10,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            start = 0,
                            end = 10,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.SpreadElement,
                                    start = 4,
                                    end = 8,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("new foo(...a,)", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 14,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 14,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            start = 0,
                            end = 14,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.SpreadElement,
                                    start = 8,
                                    end = 12,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a")
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            //------------------------------------------------------------------------------
            // disallow in new Options {ecmaVersion = 7}

            testFail("function foo(a,) { }", "Unexpected token (1:15)", new Options {ecmaVersion = 7});
            testFail("(function(a,) { })", "Unexpected token (1:12)", new Options {ecmaVersion = 7});
            testFail("(a,) => a", "Unexpected token (1:3)", new Options {ecmaVersion = 7});
            testFail("async (a,) => a", "Unexpected token (1:9)", new Options {ecmaVersion = 7});
            testFail("({foo(a,) {}})", "Unexpected token (1:8)", new Options {ecmaVersion = 7});
            testFail("class A {foo(a,) {}}", "Unexpected token (1:15)", new Options {ecmaVersion = 7});
            testFail("class A {static foo(a,) {}}", "Unexpected token (1:22)", new Options {ecmaVersion = 7});
            testFail("(class {foo(a,) {}})", "Unexpected token (1:14)", new Options {ecmaVersion = 7});
            testFail("(class {static foo(a,) {}})", "Unexpected token (1:21)", new Options {ecmaVersion = 7});
            testFail("export default function foo(a,) { }", "Unexpected token (1:30)", new Options {ecmaVersion = 7, sourceType = "module"});
            testFail("export default (function foo(a,) { })", "Unexpected token (1:31)", new Options {ecmaVersion = 7, sourceType = "module"});
            testFail("export function foo(a,) { }", "Unexpected token (1:22)", new Options {ecmaVersion = 7, sourceType = "module"});
            testFail("foo(a,)", "Unexpected token (1:6)", new Options {ecmaVersion = 7});
            testFail("new foo(a,)", "Unexpected token (1:10)", new Options {ecmaVersion = 7});

            //------------------------------------------------------------------------------
            // disallow after rest parameters

            testFail("function foo(...a,) { }", "Comma is not permitted after the rest element (1:17)", new Options {ecmaVersion = 8});
            testFail("(function(...a,) { })", "Comma is not permitted after the rest element (1:14)", new Options {ecmaVersion = 8});
            testFail("(...a,) => a", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 8});
            testFail("async (...a,) => a", "Comma is not permitted after the rest element (1:11)", new Options {ecmaVersion = 8});
            testFail("({foo(...a,) {}})", "Comma is not permitted after the rest element (1:10)", new Options {ecmaVersion = 8});
            testFail("class A {foo(...a,) {}}", "Comma is not permitted after the rest element (1:17)", new Options {ecmaVersion = 8});
            testFail("class A {static foo(...a,) {}}", "Comma is not permitted after the rest element (1:24)", new Options {ecmaVersion = 8});
            testFail("(class {foo(...a,) {}})", "Comma is not permitted after the rest element (1:16)", new Options {ecmaVersion = 8});
            testFail("(class {static foo(...a,) {}})", "Comma is not permitted after the rest element (1:23)", new Options {ecmaVersion = 8});
            testFail("export default function foo(...a,) { }", "Comma is not permitted after the rest element (1:32)", new Options {ecmaVersion = 8, sourceType = "module"});
            testFail("export default (function foo(...a,) { })", "Comma is not permitted after the rest element (1:33)", new Options {ecmaVersion = 8, sourceType = "module"});
            testFail("export function foo(...a,) { }", "Comma is not permitted after the rest element (1:24)", new Options {ecmaVersion = 8, sourceType = "module"});

            //------------------------------------------------------------------------------
            // disallow empty

            testFail("function foo(,) { }", "Unexpected token (1:13)", new Options {ecmaVersion = 8});
            testFail("(function(,) { })", "Unexpected token (1:10)", new Options {ecmaVersion = 8});
            testFail("(,) => a", "Unexpected token (1:1)", new Options {ecmaVersion = 8});
            testFail("async (,) => a", "Unexpected token (1:7)", new Options {ecmaVersion = 8});
            testFail("({foo(,) {}})", "Unexpected token (1:6)", new Options {ecmaVersion = 8});
            testFail("class A {foo(,) {}}", "Unexpected token (1:13)", new Options {ecmaVersion = 8});
            testFail("class A {static foo(,) {}}", "Unexpected token (1:20)", new Options {ecmaVersion = 8});
            testFail("(class {foo(,) {}})", "Unexpected token (1:12)", new Options {ecmaVersion = 8});
            testFail("(class {static foo(,) {}})", "Unexpected token (1:19)", new Options {ecmaVersion = 8});
            testFail("export default function foo(,) { }", "Unexpected token (1:28)", new Options {ecmaVersion = 8, sourceType = "module"});
            testFail("export default (function foo(,) { })", "Unexpected token (1:29)", new Options {ecmaVersion = 8, sourceType = "module"});
            testFail("export function foo(,) { }", "Unexpected token (1:20)", new Options {ecmaVersion = 8, sourceType = "module"});

            //------------------------------------------------------------------------------
            // disallow in parens without arrow

            testFail("(a,)", "Unexpected token (1:3)", new Options {ecmaVersion = 7});
            testFail("(a,)", "Unexpected token (1:3)", new Options {ecmaVersion = 8});
        }
    }
}
