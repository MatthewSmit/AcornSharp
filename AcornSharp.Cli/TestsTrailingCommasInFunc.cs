using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static void TestsTrailingCommasInFunc()
        {
            //------------------------------------------------------------------------------
            // allow

            test("function foo(a,) { }", new Node
            {
                type = "Program",
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "FunctionDeclaration",
                        start = 0,
                        end = 20,
                        id = new Node
                        {
                            type = "Identifier",
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = "Identifier",
                                start = 13,
                                end = 14,
                                name = "a"
                            }
                        },
                        generator = false,
                        bexpression = false,
                        async = false,
                        fbody = new Node
                        {
                            type = "BlockStatement",
                            start = 17,
                            end = 20,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("(function(a,) { })", new Node
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
                            type = "FunctionExpression",
                            start = 1,
                            end = 17,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 10,
                                    end = 11,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 14,
                                end = 17,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("(a,) => a", new Node
            {
                type = "Program",
                start = 0,
                end = 9,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 9,
                        expression = new Node
                        {
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 9,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 1,
                                    end = 2,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = true,
                            async = false,
                            fbody = new Node
                            {
                                type = "Identifier",
                                start = 8,
                                end = 9,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("async (a,) => a", new Node
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
                            type = "ArrowFunctionExpression",
                            start = 0,
                            end = 15,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 7,
                                    end = 8,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = true,
                            async = true,
                            fbody = new Node
                            {
                                type = "Identifier",
                                start = 14,
                                end = 15,
                                name = "a"
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("({foo(a,) {}})", new Node
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
                            type = "ObjectExpression",
                            start = 1,
                            end = 13,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = "Property",
                                    start = 2,
                                    end = 12,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = "Identifier",
                                        start = 2,
                                        end = 5,
                                        name = "foo"
                                    },
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = "FunctionExpression",
                                        start = 5,
                                        end = 12,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "Identifier",
                                                start = 6,
                                                end = 7,
                                                name = "a"
                                            }
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
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

            test("class A {foo(a,) {}}", new Node
            {
                type = "Program",
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ClassDeclaration",
                        start = 0,
                        end = 20,
                        id = new Node
                        {
                            type = "Identifier",
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = "ClassBody",
                            start = 8,
                            end = 20,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "MethodDefinition",
                                    start = 9,
                                    end = 19,
                                    @static = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = "Identifier",
                                        start = 9,
                                        end = 12,
                                        name = "foo"
                                    },
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = "FunctionExpression",
                                        start = 12,
                                        end = 19,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "Identifier",
                                                start = 13,
                                                end = 14,
                                                name = "a"
                                            }
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
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

            test("class A {static foo(a,) {}}", new Node
            {
                type = "Program",
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ClassDeclaration",
                        start = 0,
                        end = 27,
                        id = new Node
                        {
                            type = "Identifier",
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = "ClassBody",
                            start = 8,
                            end = 27,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = "MethodDefinition",
                                    start = 9,
                                    end = 26,
                                    @static = true,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = "Identifier",
                                        start = 16,
                                        end = 19,
                                        name = "foo"
                                    },
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = "FunctionExpression",
                                        start = 19,
                                        end = 26,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = "Identifier",
                                                start = 20,
                                                end = 21,
                                                name = "a"
                                            }
                                        },
                                        generator = false,
                                        bexpression = false,
                                        async = false,
                                        fbody = new Node
                                        {
                                            type = "BlockStatement",
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

            test("(class {foo(a,) {}})", new Node
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
                            type = "ClassExpression",
                            start = 1,
                            end = 19,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = "ClassBody",
                                start = 7,
                                end = 19,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "MethodDefinition",
                                        start = 8,
                                        end = 18,
                                        @static = false,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            start = 8,
                                            end = 11,
                                            name = "foo"
                                        },
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            start = 11,
                                            end = 18,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    start = 12,
                                                    end = 13,
                                                    name = "a"
                                                }
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
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

            test("(class {static foo(a,) {}})", new Node
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
                            type = "ClassExpression",
                            start = 1,
                            end = 26,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = "ClassBody",
                                start = 7,
                                end = 26,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = "MethodDefinition",
                                        start = 8,
                                        end = 25,
                                        @static = true,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = "Identifier",
                                            start = 15,
                                            end = 18,
                                            name = "foo"
                                        },
                                        kind = "method",
                                        value = new Node
                                        {
                                            type = "FunctionExpression",
                                            start = 18,
                                            end = 25,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = "Identifier",
                                                    start = 19,
                                                    end = 20,
                                                    name = "a"
                                                }
                                            },
                                            generator = false,
                                            bexpression = false,
                                            async = false,
                                            fbody = new Node
                                            {
                                                type = "BlockStatement",
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

            test("export default function foo(a,) { }", new Node
            {
                type = "Program",
                start = 0,
                end = 35,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExportDefaultDeclaration",
                        start = 0,
                        end = 35,
                        declaration = new Node
                        {
                            type = "FunctionDeclaration",
                            start = 15,
                            end = 35,
                            id = new Node
                            {
                                type = "Identifier",
                                start = 24,
                                end = 27,
                                name = "foo"
                            },
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 28,
                                    end = 29,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 32,
                                end = 35,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            test("export default (function foo(a,) { })", new Node
            {
                type = "Program",
                start = 0,
                end = 37,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExportDefaultDeclaration",
                        start = 0,
                        end = 37,
                        declaration = new Node
                        {
                            type = "FunctionExpression",
                            start = 16,
                            end = 36,
                            id = new Node
                            {
                                type = "Identifier",
                                start = 25,
                                end = 28,
                                name = "foo"
                            },
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 29,
                                    end = 30,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = "BlockStatement",
                                start = 33,
                                end = 36,
                                body = new List<Node>()
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 8, sourceType = "module"});

            test("export function foo(a,) { }", new Node
            {
                type = "Program",
                start = 0,
                end = 27,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExportNamedDeclaration",
                        start = 0,
                        end = 27,
                        declaration = new Node
                        {
                            type = "FunctionDeclaration",
                            start = 7,
                            end = 27,
                            id = new Node
                            {
                                type = "Identifier",
                                start = 16,
                                end = 19,
                                name = "foo"
                            },
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 20,
                                    end = 21,
                                    name = "a"
                                }
                            },
                            generator = false,
                            bexpression = false,
                            async = false,
                            fbody = new Node
                            {
                                type = "BlockStatement",
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

            test("foo(a,)", new Node
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
                            type = "CallExpression",
                            start = 0,
                            end = 7,
                            callee = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 4,
                                    end = 5,
                                    name = "a"
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("new foo(a,)", new Node
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
                            type = "NewExpression",
                            start = 0,
                            end = 11,
                            callee = new Node
                            {
                                type = "Identifier",
                                start = 4,
                                end = 7,
                                name = "foo"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = "Identifier",
                                    start = 8,
                                    end = 9,
                                    name = "a"
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("foo(...a,)", new Node
            {
                type = "Program",
                start = 0,
                end = 10,
                body = new List<Node>
                {
                    new Node
                    {
                        type = "ExpressionStatement",
                        start = 0,
                        end = 10,
                        expression = new Node
                        {
                            type = "CallExpression",
                            start = 0,
                            end = 10,
                            callee = new Node
                            {
                                type = "Identifier",
                                start = 0,
                                end = 3,
                                name = "foo"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = "SpreadElement",
                                    start = 4,
                                    end = 8,
                                    argument = new Node
                                    {
                                        type = "Identifier",
                                        start = 7,
                                        end = 8,
                                        name = "a"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 8});

            test("new foo(...a,)", new Node
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
                            type = "NewExpression",
                            start = 0,
                            end = 14,
                            callee = new Node
                            {
                                type = "Identifier",
                                start = 4,
                                end = 7,
                                name = "foo"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = "SpreadElement",
                                    start = 8,
                                    end = 12,
                                    argument = new Node
                                    {
                                        type = "Identifier",
                                        start = 11,
                                        end = 12,
                                        name = "a"
                                    }
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
