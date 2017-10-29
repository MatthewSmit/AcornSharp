using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTrailingCommasInFunc()
        {
            //------------------------------------------------------------------------------
            // allow

            Test("function foo(a,) { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        @params = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a")
                        },
                        generator = false,
                        bexpression = false,
                        async = false,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)))
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(function(a,) { })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17)))
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(a,) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
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

            Test("async (a,) => a", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)))
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
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

            Test("({foo(a,) {}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)))
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 12, 12)))
                                {
                                    type = NodeType.Property,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), "foo"),
                                    kind = "init",
                                    value = new BaseNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {foo(a,) {}}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)))
                                {
                                    type = NodeType.MethodDefinition,
                                    @static = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 19, 19)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("class A {static foo(a,) {}}", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27)))
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 26, 26)))
                                {
                                    type = NodeType.MethodDefinition,
                                    @static = true,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                                    kind = "method",
                                    value = new BaseNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26)))
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new BaseNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26)))
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("(class {foo(a,) {}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 19, 19)))
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19)))
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18)))
                                    {
                                        type = NodeType.MethodDefinition,
                                        @static = false,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), "foo"),
                                        kind = "method",
                                        value = new BaseNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 18, 18)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "a")
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
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

            Test("(class {static foo(a,) {}})", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26)))
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 26, 26)))
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25)))
                                    {
                                        type = NodeType.MethodDefinition,
                                        @static = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "foo"),
                                        kind = "method",
                                        value = new BaseNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)))
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "a")
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new BaseNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25)))
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>()
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

            Test("export default function foo(a,) { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)))
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 35, 35)))
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)), "foo"),
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("export default (function foo(a,) { })", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)))
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36)))
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "foo"),
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 36, 36)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("export function foo(a,) { }", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)))
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27)))
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "a")
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new BaseNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)))
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>()
                            }
                        },
                        specifiers = new List<BaseNode>(),
                        source = null
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            Test("foo(a,)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)))
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "a")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("new foo(a,)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)))
                        {
                            type = NodeType.NewExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("foo(...a,)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)))
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)))
                                {
                                    type = NodeType.SpreadElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            Test("new foo(...a,)", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)))
                        {
                            type = NodeType.NewExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12)))
                                {
                                    type = NodeType.SpreadElement,
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
