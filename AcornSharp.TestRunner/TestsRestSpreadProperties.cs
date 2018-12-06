using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal static class TestsRestSpreadProperties
    {
        public static void Run()
        {
            //------------------------------------------------------------------------------
            // Spread Properties
            //------------------------------------------------------------------------------

            Program.test("({...obj})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 10,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 10,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 9,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 8,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 5,
                                        end = 8,
                                        name = "obj"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...obj1,})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 12,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 11,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 9,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 5,
                                        end = 9,
                                        name = "obj1"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...obj1,...obj2})", new TestNode
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
                        end = 19,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 18,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 9,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 5,
                                        end = 9,
                                        name = "obj1"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 10,
                                    end = 17,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 13,
                                        end = 17,
                                        name = "obj2"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({a,...obj1,b:1,...obj2,c:2})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 29,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 29,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 28,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 3,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 3,
                                        name = "a"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 3,
                                        name = "a"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 4,
                                    end = 11,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 7,
                                        end = 11,
                                        name = "obj1"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 12,
                                    end = 15,
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 12,
                                        end = 13,
                                        name = "b"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 14,
                                        end = 15,
                                        value = 1,
                                        raw = "1"
                                    },
                                    kind = PropertyKind.Init,
                                },
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 16,
                                    end = 23,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 19,
                                        end = 23,
                                        name = "obj2"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 24,
                                    end = 27,
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 24,
                                        end = 25,
                                        name = "c"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        start = 26,
                                        end = 27,
                                        value = 2,
                                        raw = "2"
                                    },
                                    kind = PropertyKind.Init,
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...(obj)})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 12,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 11,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 10,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 6,
                                        end = 9,
                                        name = "obj"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...a,b,c})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 12,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 12,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 11,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 6,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 5,
                                        end = 6,
                                        name = "a"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 7,
                                    end = 8,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 7,
                                        end = 8,
                                        name = "b"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 7,
                                        end = 8,
                                        name = "b"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 9,
                                    end = 10,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 10,
                                        name = "c"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 9,
                                        end = 10,
                                        name = "c"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...(a,b),c})", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 14,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 14,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 13,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 2,
                                    end = 10,
                                    argument = new TestNode
                                    {
                                        type = typeof(SequenceExpressionNode),
                                        start = 6,
                                        end = 9,
                                        expressions = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 6,
                                                end = 7,
                                                name = "a"
                                            },
                                            new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 8,
                                                end = 9,
                                                name = "b"
                                            }
                                        }
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 11,
                                    end = 12,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 11,
                                        end = 12,
                                        name = "c"
                                    },
                                    kind = PropertyKind.Init,
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 11,
                                        end = 12,
                                        name = "c"
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("({...})", "Unexpected token (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj})", "Unexpected token (1:2)", new TestOptions
            {
                ecmaVersion = 8
            });

//------------------------------------------------------------------------------
// Rest Properties
//------------------------------------------------------------------------------

            Program.test("({...obj} = foo)", new TestNode
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
                            type = typeof(AssignmentExpressionNode),
                            start = 1,
                            end = 15,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                start = 1,
                                end = 9,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        start = 2,
                                        end = 8,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 5,
                                            end = 8,
                                            name = "obj"
                                        }
                                    }
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 12,
                                end = 15,
                                name = "foo"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({a,...obj} = foo)", new TestNode
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
                            type = typeof(AssignmentExpressionNode),
                            start = 1,
                            end = 17,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                start = 1,
                                end = 11,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 3,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 3,
                                            name = "a"
                                        },
                                        kind = PropertyKind.Init,
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 3,
                                            name = "a"
                                        }
                                    },
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        start = 4,
                                        end = 10,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 7,
                                            end = 10,
                                            name = "obj"
                                        }
                                    }
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 14,
                                end = 17,
                                name = "foo"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({a:b,...obj} = foo)", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 20,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 1,
                            end = 19,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                start = 1,
                                end = 13,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 2,
                                        end = 5,
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 2,
                                            end = 3,
                                            name = "a"
                                        },
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 4,
                                            end = 5,
                                            name = "b"
                                        },
                                        kind = PropertyKind.Init,
                                    },
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        start = 6,
                                        end = 12,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 9,
                                            end = 12,
                                            name = "obj"
                                        }
                                    }
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 16,
                                end = 19,
                                name = "foo"
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...obj}) => {}", new TestNode
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
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 16,
                            id = null,
                            generator = false,
                            expression = false,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 1,
                                    end = 9,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            start = 2,
                                            end = 8,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 5,
                                                end = 8,
                                                name = "obj"
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 14,
                                end = 16,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({...obj} = {}) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 21,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 21,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 21,
                            id = null,
                            generator = false,
                            expression = false,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    start = 1,
                                    end = 14,
                                    left = new TestNode
                                    {
                                        type = typeof(ObjectPatternNode),
                                        start = 1,
                                        end = 9,
                                        properties = new[]
                                        {
                                            new TestNode
                                            {
                                                type = typeof(RestElementNode),
                                                start = 2,
                                                end = 8,
                                                argument = new TestNode
                                                {
                                                    type = typeof(IdentifierNode),
                                                    start = 5,
                                                    end = 8,
                                                    name = "obj"
                                                }
                                            }
                                        }
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        start = 12,
                                        end = 14,
                                        properties = new TestNode[0]
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 19,
                                end = 21,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({a,...obj}) => {}", new TestNode
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
                            expression = false,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 1,
                                    end = 11,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 2,
                                            end = 3,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 2,
                                                end = 3,
                                                name = "a"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 2,
                                                end = 3,
                                                name = "a"
                                            }
                                        },
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            start = 4,
                                            end = 10,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 7,
                                                end = 10,
                                                name = "obj"
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 16,
                                end = 18,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({a:b,...obj}) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                start = 0,
                end = 20,
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 20,
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            start = 0,
                            end = 20,
                            id = null,
                            generator = false,
                            expression = false,
                            async = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 1,
                                    end = 13,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 2,
                                            end = 5,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 2,
                                                end = 3,
                                                name = "a"
                                            },
                                            value = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 4,
                                                end = 5,
                                                name = "b"
                                            },
                                            kind = PropertyKind.Init,
                                        },
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            start = 6,
                                            end = 12,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 9,
                                                end = 12,
                                                name = "obj"
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 18,
                                end = 20,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("let {...obj1,} = foo", "Comma is not permitted after the rest element (1:12)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...obj1,a} = foo", "Comma is not permitted after the rest element (1:12)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...obj1,...obj2} = foo", "Comma is not permitted after the rest element (1:12)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...(obj)} = foo", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...(a,b)} = foo", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...{a,b}} = foo", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...[a,b]} = foo", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj1,} = foo)", "Comma is not permitted after the rest element (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj1,a} = foo)", "Comma is not permitted after the rest element (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj1,...obj2} = foo)", "Comma is not permitted after the rest element (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...(obj)} = foo)", "Parenthesized pattern (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...(a,b)} = foo)", "Parenthesized pattern (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...{a,b}} = foo)", "Unexpected token (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...[a,b]} = foo)", "Unexpected token (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj} = foo)", "Unexpected token (1:2)", new TestOptions
            {
                ecmaVersion = 8
            });
            Program.testFail("({...(obj)}) => {}", "Parenthesized pattern (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...(a,b)}) => {}", "Parenthesized pattern (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...{a,b}}) => {}", "Unexpected token (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...[a,b]}) => {}", "Unexpected token (1:5)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...obj}) => {}", "Unexpected token (1:2)", new TestOptions
            {
                ecmaVersion = 8
            });

//------------------------------------------------------------------------------
// From https://github.com/adrianheine/acorn5-object-spread/tree/49839ac662fe34e1b4ad56767115f54747db2e7c/test
//------------------------------------------------------------------------------

            Program.test("let z = {...x}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        start = 0,
                        end = 14,
                        declarations = new[]
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                start = 4,
                                end = 14,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 4,
                                    end = 5,
                                    name = "z"
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 8,
                                    end = 14,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(SpreadElementNode),
                                            start = 9,
                                            end = 13,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 12,
                                                end = 13,
                                                name = "x"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        kind = PropertyKind.Let
                    }
                },
                start = 0,
                end = 14,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("z = {x, ...y}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 13,
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            start = 0,
                            end = 13,
                            @operator = "=",
                            left = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                start = 0,
                                end = 1,
                                name = "z"
                            },
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                start = 4,
                                end = 13,
                                properties = new[]
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        start = 5,
                                        end = 6,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 5,
                                            end = 6,
                                            name = "x"
                                        },
                                        value = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 5,
                                            end = 6,
                                            name = "x"
                                        },
                                        kind = PropertyKind.Init,
                                    },
                                    new TestNode
                                    {
                                        type = typeof(SpreadElementNode),
                                        start = 8,
                                        end = 12,
                                        argument = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 11,
                                            end = 12,
                                            name = "y"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                start = 0,
                end = 13,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("({x, ...y, a, ...b, c})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 23,
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            start = 1,
                            end = 22,
                            properties = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 2,
                                    end = 3,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 3,
                                        name = "x"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 2,
                                        end = 3,
                                        name = "x"
                                    },
                                    kind = PropertyKind.Init,
                                },
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 5,
                                    end = 9,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 8,
                                        end = 9,
                                        name = "y"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 11,
                                    end = 12,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 11,
                                        end = 12,
                                        name = "a"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 11,
                                        end = 12,
                                        name = "a"
                                    },
                                    kind = PropertyKind.Init,
                                },
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    start = 14,
                                    end = 18,
                                    argument = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 17,
                                        end = 18,
                                        name = "b"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    start = 20,
                                    end = 21,
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 20,
                                        end = 21,
                                        name = "c"
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(IdentifierNode),
                                        start = 20,
                                        end = 21,
                                        name = "c"
                                    },
                                    kind = PropertyKind.Init,
                                }
                            }
                        }
                    }
                },
                start = 0,
                end = 23,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("var someObject = { someKey: { ...mapGetters([ 'some_val_1', 'some_val_2' ]) } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        start = 0,
                        end = 79,
                        declarations = new[]
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                start = 4,
                                end = 79,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 4,
                                    end = 14,
                                    name = "someObject"
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    start = 17,
                                    end = 79,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 19,
                                            end = 77,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 19,
                                                end = 26,
                                                name = "someKey"
                                            },
                                            value = new TestNode
                                            {
                                                type = typeof(ObjectExpressionNode),
                                                start = 28,
                                                end = 77,
                                                properties = new[]
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(SpreadElementNode),
                                                        start = 30,
                                                        end = 75,
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            start = 33,
                                                            end = 75,
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(IdentifierNode),
                                                                start = 33,
                                                                end = 43,
                                                                name = "mapGetters"
                                                            },
                                                            arguments = new[]
                                                            {
                                                                new TestNode
                                                                {
                                                                    type = typeof(ArrayExpressionNode),
                                                                    start = 44,
                                                                    end = 74,
                                                                    elements = new[]
                                                                    {
                                                                        new TestNode
                                                                        {
                                                                            type = typeof(LiteralNode),
                                                                            start = 46,
                                                                            end = 58,
                                                                            value = "some_val_1",
                                                                            raw = "'some_val_1'"
                                                                        },
                                                                        new TestNode
                                                                        {
                                                                            type = typeof(LiteralNode),
                                                                            start = 60,
                                                                            end = 72,
                                                                            value = "some_val_2",
                                                                            raw = "'some_val_2'"
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            kind = PropertyKind.Init,
                                        }
                                    }
                                }
                            }
                        },
                        kind = PropertyKind.Var
                    }
                },
                start = 0,
                end = 79,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("let {x, ...y} = v", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        start = 0,
                        end = 17,
                        declarations = new[]
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                start = 4,
                                end = 17,
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 4,
                                    end = 13,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 5,
                                            end = 6,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 5,
                                                end = 6,
                                                name = "x"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 5,
                                                end = 6,
                                                name = "x"
                                            }
                                        },
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            start = 8,
                                            end = 12,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 11,
                                                end = 12,
                                                name = "y"
                                            }
                                        }
                                    }
                                },
                                init = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 16,
                                    end = 17,
                                    name = "v"
                                }
                            }
                        },
                        kind = PropertyKind.Let
                    }
                },
                start = 0,
                end = 17,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("(function({x, ...y}) {})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        start = 0,
                        end = 24,
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            start = 1,
                            end = 23,
                            id = null,
                            generator = false,
                            expression = false,
                            @params = new[]
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    start = 10,
                                    end = 19,
                                    properties = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            start = 11,
                                            end = 12,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 11,
                                                end = 12,
                                                name = "x"
                                            },
                                            kind = PropertyKind.Init,
                                            value = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 11,
                                                end = 12,
                                                name = "x"
                                            }
                                        },
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            start = 14,
                                            end = 18,
                                            argument = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 17,
                                                end = 18,
                                                name = "y"
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                start = 21,
                                end = 23,
                                body = new TestNode[0]
                            }
                        }
                    }
                },
                start = 0,
                end = 24,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });
            Program.test("const fn = ({text = \"default\", ...props}) => text + props.children", new TestNode
            {
                type = typeof(ProgramNode),
                body = new[]
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        start = 0,
                        end = 66,
                        declarations = new[]
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                start = 6,
                                end = 66,
                                id = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    start = 6,
                                    end = 8,
                                    name = "fn"
                                },
                                init = new TestNode
                                {
                                    type = typeof(ArrowFunctionExpressionNode),
                                    start = 11,
                                    end = 66,
                                    id = null,
                                    generator = false,
                                    expression = true,
                                    @params = new[]
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            start = 12,
                                            end = 40,
                                            properties = new[]
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    start = 13,
                                                    end = 29,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 13,
                                                        end = 17,
                                                        name = "text"
                                                    },
                                                    kind = PropertyKind.Init,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        start = 13,
                                                        end = 29,
                                                        left = new TestNode
                                                        {
                                                            type = typeof(IdentifierNode),
                                                            start = 13,
                                                            end = 17,
                                                            name = "text"
                                                        },
                                                        right = new TestNode
                                                        {
                                                            type = typeof(LiteralNode),
                                                            start = 20,
                                                            end = 29,
                                                            value = "default",
                                                            raw = "\"default\""
                                                        }
                                                    }
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(RestElementNode),
                                                    start = 31,
                                                    end = 39,
                                                    argument = new TestNode
                                                    {
                                                        type = typeof(IdentifierNode),
                                                        start = 34,
                                                        end = 39,
                                                        name = "props"
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(BinaryExpressionNode),
                                        start = 45,
                                        end = 66,
                                        left = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            start = 45,
                                            end = 49,
                                            name = "text"
                                        },
                                        @operator = "+",
                                        right = new TestNode
                                        {
                                            type = typeof(MemberExpressionNode),
                                            start = 52,
                                            end = 66,
                                            @object = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 52,
                                                end = 57,
                                                name = "props"
                                            },
                                            property = new TestNode
                                            {
                                                type = typeof(IdentifierNode),
                                                start = 58,
                                                end = 66,
                                                name = "children"
                                            },
                                            computed = false
                                        }
                                    }
                                }
                            }
                        },
                        kind = PropertyKind.Const
                    }
                },
                start = 0,
                end = 66,
                sourceType = SourceType.Script
            }, new TestOptions
            {
                ecmaVersion = 9
            });

            Program.testFail("({get x() {}}) => {}", "Object pattern can't contain getter or setter (1:6)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...x, ...y} = {}", "Comma is not permitted after the rest element (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("({...x,}) => z", "Comma is not permitted after the rest element (1:6)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("export const { foo, ...bar } = baz;\nexport const bar = 1;\n", "Identifier 'bar' has already been declared (2:13)", new TestOptions
            {
                ecmaVersion = 9,
                sourceType = SourceType.Module
            });
            Program.testFail("function ({...x,}) { z }", "Unexpected token (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...{x, y}} = {}", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("let {...{...{x, y}}} = {}", "Unexpected token (1:8)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("0, {...rest, b} = {}", "Comma is not permitted after the rest element (1:11)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("(([a, ...b = 0]) => {})", "Rest elements cannot have a default value (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
            Program.testFail("(({a, ...b = 0}) => {})", "Rest elements cannot have a default value (1:9)", new TestOptions
            {
                ecmaVersion = 9
            });
        }
    }
}
