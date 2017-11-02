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

            Test("function foo(a,) { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                        parameters = new List<TestNode>
                        {
                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a" }
                        },
                        generator = false,
                        expression = false,
                        async = false,
                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(function(a,) { })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        expression = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17)),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "a" }
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(a,) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a" }
                            },
                            generator = false,
                            expression = true,
                            async = false,
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("async (a,) => a", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new TestNode { type = typeof(ArrowFunctionExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a" }
                            },
                            generator = false,
                            expression = true,
                            async = true,
                            body = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a" }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("({foo(a,) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        expression = new TestNode { type = typeof(ObjectExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 13, 13)),
                            properties = new List<TestNode>
                            {
                                new TestNode { type = typeof(PropertyNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 12, 12)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5)), name = "foo" },
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12)),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "a" }
                                        },
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {foo(a,) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 19, 19)),
                                    @static = false,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo" },
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 19, 19)),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a" }
                                        },
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("class A {static foo(a,) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ClassDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A" },
                        superClass = null,
                        body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27)),
                            body = new List<TestNode>
                            {
                                new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 26, 26)),
                                    @static = true,
                                    computed = false,
                                    key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo" },
                                    kind = PropertyKind.Method,
                                    value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26)),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "a" }
                                        },
                                        generator = false,
                                        expression = false,
                                        async = false,
                                        body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(class {foo(a,) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        expression = new TestNode { type = typeof(ClassExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 19, 19)),
                            id = null,
                            superClass = null,
                            body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18)),
                                        @static = false,
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11)), name = "foo" },
                                        kind = PropertyKind.Method,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 18, 18)),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "a" }
                                            },
                                            generator = false,
                                            expression = false,
                                            async = false,
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("(class {static foo(a,) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        expression = new TestNode { type = typeof(ClassExpressionNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26)),
                            id = null,
                            superClass = null,
                            body = new TestNode { type = typeof(ClassBodyNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 26, 26)),
                                body = new List<TestNode>
                                {
                                    new TestNode { type = typeof(MethodDefinitionNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25)),
                                        @static = true,
                                        computed = false,
                                        key = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "foo" },
                                        kind = PropertyKind.Method,
                                        value = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "a" }
                                            },
                                            generator = false,
                                            expression = false,
                                            async = false,
                                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("export default function foo(a,) { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportDefaultDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declaration = new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 35, 35)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)), name = "foo" },
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "a" }
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            Test("export default (function foo(a,) { })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportDefaultDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                        declaration = new TestNode { type = typeof(FunctionExpressionNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), name = "foo" },
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 30, 30)), name = "a" }
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 36, 36)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            Test("export function foo(a,) { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExportNamedDeclarationNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        declaration = new TestNode { type = typeof(FunctionDeclarationNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27)),
                            id = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo" },
                            parameters = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "a" }
                            },
                            generator = false,
                            expression = false,
                            async = false,
                            body = new TestNode { type = typeof(BlockStatementNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27)),
                                body = new List<TestNode>()
                            }
                        },
                        specifiers = new List<TestNode>(),
                        source = null
                    }
                }
            }, new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            Test("foo(a,)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                        expression = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "a" }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("new foo(a,)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                        expression = new TestNode { type = typeof(NewExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "foo" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a" }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("foo(...a,)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                        expression = new TestNode { type = typeof(CallExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(SpreadElementNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)),
                                    argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a" }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            Test("new foo(...a,)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                {
                    new TestNode { type = typeof(ExpressionStatementNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        expression = new TestNode { type = typeof(NewExpressionNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            callee = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "foo" },
                            arguments = new List<TestNode>
                            {
                                new TestNode { type = typeof(SpreadElementNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12)),
                                    argument = new TestNode { type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "a" }
                                }
                            }
                        }
                    }
                }
            }, new Options { ecmaVersion = 8 });

            //------------------------------------------------------------------------------
            // disallow in new Options {ecmaVersion = 7}

            testFail("function foo(a,) { }", "Unexpected token (1:15)", new Options { ecmaVersion = 7 });
            testFail("(function(a,) { })", "Unexpected token (1:12)", new Options { ecmaVersion = 7 });
            testFail("(a,) => a", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });
            testFail("async (a,) => a", "Unexpected token (1:9)", new Options { ecmaVersion = 7 });
            testFail("({foo(a,) {}})", "Unexpected token (1:8)", new Options { ecmaVersion = 7 });
            testFail("class A {foo(a,) {}}", "Unexpected token (1:15)", new Options { ecmaVersion = 7 });
            testFail("class A {static foo(a,) {}}", "Unexpected token (1:22)", new Options { ecmaVersion = 7 });
            testFail("(class {foo(a,) {}})", "Unexpected token (1:14)", new Options { ecmaVersion = 7 });
            testFail("(class {static foo(a,) {}})", "Unexpected token (1:21)", new Options { ecmaVersion = 7 });
            testFail("export default function foo(a,) { }", "Unexpected token (1:30)", new Options { ecmaVersion = 7, sourceType = SourceType.Module });
            testFail("export default (function foo(a,) { })", "Unexpected token (1:31)", new Options { ecmaVersion = 7, sourceType = SourceType.Module });
            testFail("export function foo(a,) { }", "Unexpected token (1:22)", new Options { ecmaVersion = 7, sourceType = SourceType.Module });
            testFail("foo(a,)", "Unexpected token (1:6)", new Options { ecmaVersion = 7 });
            testFail("new foo(a,)", "Unexpected token (1:10)", new Options { ecmaVersion = 7 });

            //------------------------------------------------------------------------------
            // disallow after rest parameters

            testFail("function foo(...a,) { }", "Comma is not permitted after the rest element (1:17)", new Options { ecmaVersion = 8 });
            testFail("(function(...a,) { })", "Comma is not permitted after the rest element (1:14)", new Options { ecmaVersion = 8 });
            testFail("(...a,) => a", "Comma is not permitted after the rest element (1:5)", new Options { ecmaVersion = 8 });
            testFail("async (...a,) => a", "Comma is not permitted after the rest element (1:11)", new Options { ecmaVersion = 8 });
            testFail("({foo(...a,) {}})", "Comma is not permitted after the rest element (1:10)", new Options { ecmaVersion = 8 });
            testFail("class A {foo(...a,) {}}", "Comma is not permitted after the rest element (1:17)", new Options { ecmaVersion = 8 });
            testFail("class A {static foo(...a,) {}}", "Comma is not permitted after the rest element (1:24)", new Options { ecmaVersion = 8 });
            testFail("(class {foo(...a,) {}})", "Comma is not permitted after the rest element (1:16)", new Options { ecmaVersion = 8 });
            testFail("(class {static foo(...a,) {}})", "Comma is not permitted after the rest element (1:23)", new Options { ecmaVersion = 8 });
            testFail("export default function foo(...a,) { }", "Comma is not permitted after the rest element (1:32)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });
            testFail("export default (function foo(...a,) { })", "Comma is not permitted after the rest element (1:33)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });
            testFail("export function foo(...a,) { }", "Comma is not permitted after the rest element (1:24)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            //------------------------------------------------------------------------------
            // disallow empty

            testFail("function foo(,) { }", "Unexpected token (1:13)", new Options { ecmaVersion = 8 });
            testFail("(function(,) { })", "Unexpected token (1:10)", new Options { ecmaVersion = 8 });
            testFail("(,) => a", "Unexpected token (1:1)", new Options { ecmaVersion = 8 });
            testFail("async (,) => a", "Unexpected token (1:7)", new Options { ecmaVersion = 8 });
            testFail("({foo(,) {}})", "Unexpected token (1:6)", new Options { ecmaVersion = 8 });
            testFail("class A {foo(,) {}}", "Unexpected token (1:13)", new Options { ecmaVersion = 8 });
            testFail("class A {static foo(,) {}}", "Unexpected token (1:20)", new Options { ecmaVersion = 8 });
            testFail("(class {foo(,) {}})", "Unexpected token (1:12)", new Options { ecmaVersion = 8 });
            testFail("(class {static foo(,) {}})", "Unexpected token (1:19)", new Options { ecmaVersion = 8 });
            testFail("export default function foo(,) { }", "Unexpected token (1:28)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });
            testFail("export default (function foo(,) { })", "Unexpected token (1:29)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });
            testFail("export function foo(,) { }", "Unexpected token (1:20)", new Options { ecmaVersion = 8, sourceType = SourceType.Module });

            //------------------------------------------------------------------------------
            // disallow in parens without arrow

            testFail("(a,)", "Unexpected token (1:3)", new Options { ecmaVersion = 7 });
            testFail("(a,)", "Unexpected token (1:3)", new Options { ecmaVersion = 8 });
        }
    }
}
