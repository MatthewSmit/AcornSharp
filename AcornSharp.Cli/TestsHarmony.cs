/*
  Copyright (C) 2015 Ingvar Stepanyan <me@rreverser.com>
  Copyright (C) 2012 Ariya Hidayat <ariya.hidayat@gmail.com>
  Copyright (C) 2012 Joost-Wim Boekesteijn <joost-wim@boekesteijn.nl>
  Copyright (C) 2012 Yusuke Suzuki <utatane.tea@gmail.com>
  Copyright (C) 2012 Arpad Borsos <arpad.borsos@googlemail.com>
  Copyright (C) 2011 Ariya Hidayat <ariya.hidayat@gmail.com>
  Copyright (C) 2011 Yusuke Suzuki <utatane.tea@gmail.com>
  Copyright (C) 2011 Arpad Borsos <arpad.borsos@googlemail.com>

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
  AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
  ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
  DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
  ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections.Generic;
using AcornSharp.Node;

namespace AcornSharp.Cli
{
    internal static partial class Tests
    {
        public static void TestsHarmony()
        {
            /*
              Tests below were automatically converted from https://github.com/ariya/esprima/blob/2bb17ef9a45c88e82d72c2c61b7b7af93caef028/test/harmonytest.js.
            
              Manually fixed locations for:
               - parenthesized expressions (include brackets into expression's location)
               - expression statements (excluded spaces after statement's semicolon)
               - arrow and method functions (included arguments into function's location)
               - template elements (excluded '`', '${' and '}' from element's location)
            */

            // ES6 Unicode Code Point Escape Sequence

            Program.Test("\"\\u{714E}\\u{8336}\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "煎茶",
                            raw = "\"\\u{714E}\\u{8336}\"",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "𠮷野家",
                            raw = "\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Numeric Literal

            Program.Test("00", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            raw = "00",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0o0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            raw = "0o0",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function test() {'use strict'; 0o0; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "test"},
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = "use strict",
                                        raw = "'use strict'",
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        raw = "0o0",
                                        location = new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34))
                                    },
                                    location = new SourceLocation(new Position(1, 31, 31), new Position(1, 35, 35))
                                }
                            },
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0o2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2,
                            raw = "0o2",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0o12", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 10,
                            raw = "0o12",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0O0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            raw = "0O0",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function test() {'use strict'; 0O0; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "test"},
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = "use strict",
                                        raw = "'use strict'",
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30))
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 0,
                                        raw = "0O0",
                                        location = new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34))
                                    },
                                    location = new SourceLocation(new Position(1, 31, 31), new Position(1, 35, 35))
                                }
                            },
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0O2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2,
                            raw = "0O2",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0O12", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 10,
                            raw = "0O12",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0b0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            raw = "0b0",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0b1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 1,
                            raw = "0b1",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0b10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2,
                            raw = "0b10",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0B0", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 0,
                            raw = "0B0",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0B1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 1,
                            raw = "0B1",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("0B10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 2,
                            raw = "0B10",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6 Template Strings

            Program.Test("`42`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("42", "42"),
                                    tail = true,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 3, 3))
                                }
                            },
                            expressions = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("raw`42`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            tag = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "raw"},
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                quasis = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        value = new TemplateNode("42", "42"),
                                        tail = true,
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                    }
                                },
                                expressions = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("raw`hello ${name}`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TaggedTemplateExpressionNode),
                            tag = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "raw"},
                            quasi = new TestNode
                            {
                                type = typeof(TemplateLiteralNode),
                                quasis = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        value = new TemplateNode("hello ", "hello "),
                                        tail = false,
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(TemplateElementNode),
                                        value = new TemplateNode("", ""),
                                        tail = true,
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 17, 17))
                                    }
                                },
                                expressions = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 16, 16)), name = "name"}
                                },
                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 18, 18))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`$`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("$", "$"),
                                    tail = true,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                }
                            },
                            expressions = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`\\n\\r\\b\\v\\t\\f\\\n\\\r\n`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("\\n\\r\\b\\v\\t\\f\\\n\\\n", "\n\r\b\u000b\t\f"),
                                    tail = true,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(3, 0, 18))
                                }
                            },
                            expressions = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`\n\r\n\r`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("\n\n\n", "\n\n\n"),
                                    tail = true,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(4, 0, 5))
                                }
                            },
                            expressions = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`\\u{000042}\\u0042\\x42u0\\A`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("\\u{000042}\\u0042\\x42u0\\A", "BBBu0A"),
                                    tail = true,
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25))
                                }
                            },
                            expressions = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("new raw`42`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(NewExpressionNode),
                            callee = new TestNode
                            {
                                type = typeof(TaggedTemplateExpressionNode),
                                tag = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "raw"},
                                quasi = new TestNode
                                {
                                    type = typeof(TemplateLiteralNode),
                                    quasis = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("42", "42"),
                                            tail = true,
                                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                        }
                                    },
                                    expressions = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`outer${{x: {y: 10}}}bar${`nested${function(){return 1;}}endnest`}end`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            expressions = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), name = "x"},
                                            value = new TestNode
                                            {
                                                type = typeof(ObjectExpressionNode),
                                                properties = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(PropertyNode),
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        key = new TestNode {type = typeof(IdentifierNode), name = "y"},
                                                        value = new TestNode
                                                        {
                                                            type = typeof(LiteralNode),
                                                            value = 10,
                                                            raw = "10"
                                                        },
                                                        kind = PropertyKind.Initialise
                                                    }
                                                }
                                            },
                                            kind = PropertyKind.Initialise
                                        }
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateLiteralNode),
                                    expressions = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            generator = false,
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ReturnStatementNode),
                                                        argument = new TestNode
                                                        {
                                                            type = typeof(LiteralNode),
                                                            value = 1,
                                                            raw = "1"
                                                        }
                                                    }
                                                }
                                            },
                                            expression = false
                                        }
                                    },
                                    quasis = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("nested", "nested"),
                                            tail = false
                                        },
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("endnest", "endnest"),
                                            tail = true
                                        }
                                    }
                                }
                            },
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("outer", "outer"),
                                    tail = false
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("bar", "bar"),
                                    tail = false
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    value = new TemplateNode("end", "end"),
                                    tail = true
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });


            // ES6: Switch Case Declaration

            Program.Test("switch (answer) { case 42: let t = 42; break; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(SwitchStatementNode),
                        discriminant = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), name = "answer"},
                        cases = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(SwitchCaseNode),
                                test = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    raw = "42",
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                consequent = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(VariableDeclarationNode),
                                        declarations = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(VariableDeclaratorNode),
                                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "t"},
                                                init = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    value = 42,
                                                    raw = "42",
                                                    location = new SourceLocation(new Position(1, 35, 35), new Position(1, 37, 37))
                                                },
                                                location = new SourceLocation(new Position(1, 31, 31), new Position(1, 37, 37))
                                            }
                                        },
                                        kind = VariableKind.Let,
                                        location = new SourceLocation(new Position(1, 27, 27), new Position(1, 38, 38))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(BreakStatementNode),
                                        label = null,
                                        location = new SourceLocation(new Position(1, 39, 39), new Position(1, 45, 45))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 45, 45))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Arrow Function

            Program.Test("() => \"test\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = "test",
                                raw = "\"test\"",
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("e => \"test\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = "test",
                                raw = "\"test\"",
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(e) => \"test\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = "test",
                                raw = "\"test\"",
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(a, b) => \"test\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "b"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = "test",
                                raw = "\"test\"",
                                location = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("e => { 42; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            raw = "42",
                                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                        },
                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 10, 10))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("e => ({ property: 42 })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)), name = "property"},
                                        value = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            raw = "42",
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 22, 22))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("e => { label: 42 }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(LabelledStatementNode),
                                        label = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 12, 12)), name = "label"},
                                        body = new TestNode
                                        {
                                            type = typeof(ExpressionStatementNode),
                                            expression = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                value = 42,
                                                raw = "42",
                                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                            },
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 16, 16))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 18, 18))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(a, b) => { 42; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "b"}
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 42,
                                            raw = "42",
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("([a, , b]) => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "a"},
                                        null,
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "b"}
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("([a.a]) => 42", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            Program.TestFail("() => {}()", "Unexpected token (1:8)", new Options {ecmaVersion = 6});
            Program.TestFail("(a) => {}()", "Unexpected token (1:9)", new Options {ecmaVersion = 6});
            Program.TestFail("a => {}()", "Unexpected token (1:7)", new Options {ecmaVersion = 6});
            Program.TestFail("console.log(typeof () => {});", "Unexpected token (1:20)", new Options {ecmaVersion = 6});

            Program.Test("(() => {})()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            callee = new TestNode
                            {
                                type = typeof(ArrowFunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>(),
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                },
                                generator = false,
                                expression = false,
                                location = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("((() => {}))()", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            callee = new TestNode
                            {
                                type = typeof(ArrowFunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>(),
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                generator = false,
                                expression = false,
                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                            }
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });


            Program.Test("(x=1) => x * x", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"},
                                    right = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 1,
                                        raw = "1",
                                        location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                @operator = Operator.Multiplication,
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "x"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("eval => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "eval"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("arguments => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), name = "arguments"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(a) => 00", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                raw = "00",
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(eval, a) => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), name = "eval"},
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"}
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(eval = 10) => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), name = "eval"},
                                    right = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 10,
                                        raw = "10",
                                        location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(eval, a = 10) => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), name = "eval"},
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                    right = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 10,
                                        raw = "10",
                                        location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                    },
                                    location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 42,
                                raw = "42",
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(x => x)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"}
                            },
                            body = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 7, 7))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x => y => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"}
                            },
                            body = new TestNode
                            {
                                type = typeof(ArrowFunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "y"}
                                },
                                body = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 42,
                                    raw = "42",
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                generator = false,
                                expression = true,
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(x) => ((y, z) => (x, y, z))", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "x"}
                            },
                            body = new TestNode
                            {
                                type = typeof(ArrowFunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "y"},
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "z"}
                                },
                                body = new TestNode
                                {
                                    type = typeof(SequenceExpressionNode),
                                    expressions = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "x"},
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "y"},
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "z"}
                                    },
                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                },
                                generator = false,
                                expression = true,
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("foo(() => {})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo"},
                            arguments = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ArrowFunctionExpressionNode),
                                    id = null,
                                    parameters = new List<TestNode>(),
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                    },
                                    generator = false,
                                    expression = false,
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                                }
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("foo((x, y) => {})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "foo"},
                            arguments = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ArrowFunctionExpressionNode),
                                    id = null,
                                    parameters = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "y"}
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        body = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                    },
                                    generator = false,
                                    expression = false,
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                                }
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Method Definition

            Program.Test("x = { method() { } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), name = "method"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 18, 18))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { method(test) { } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), name = "method"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)), name = "test"}
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 22, 22))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 22, 22))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { 'method'() { } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "method",
                                            raw = "'method'",
                                            location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                        },
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 20, 20))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { get() { } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), name = "get"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { set() { } }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), name = "set"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Object Literal Property Value Shorthand

            Program.Test("x = { y, z }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "y"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "y"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "z"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "z"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Destructuring

            Program.Test("[a, b] = [b, a]", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "b"}
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            right = new TestNode
                            {
                                type = typeof(ArrayExpressionNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b"},
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a"}
                                },
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });
            
            Program.Test("[a.r] = b", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)),
                                elements = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(MemberExpressionNode),
                                        location = new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4)),
                                        @object = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)),
                                            name = "a"
                                        },
                                        property = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)),
                                            name = "r"
                                        },
                                        computed = false
                                    }
                                }
                            },
                            right = new TestNode
                            {
                                type = typeof(IdentifierNode),
                                location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)),
                                name = "b"
                            }
                        }
                    }
                },
                source = SourceType.Script
            }, new Options {ecmaVersion = 6});

            Program.Test("let [a,,b] = c", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10)),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)),
                                            name = "a"
                                        },
                                        null,
                                        new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)),
                                            name = "b"
                                        }
                                    }
                                },
                                init = new TestNode
                                {
                                    type = typeof(IdentifierNode),
                                    location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)),
                                    name = "c"
                                }
                            }
                        },
                        kind = VariableKind.Let
                    }
                },
                source = SourceType.Script
            }, new Options {ecmaVersion = 6});

            Program.Test("({ responseText: text } = res)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 15, 15)), name = "responseText"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 21, 21)), name = "text"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 3, 3), new Position(1, 21, 21))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), name = "res"},
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 29, 29))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("const {a} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("const [a] = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"}
                                    },
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    elements = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("let {a} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("let [a] = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"}
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    elements = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var {a} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [a] = []", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"}
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    elements = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("const {a:b} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "b"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 10, 10))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                },
                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 16, 16))
                            }
                        },
                        kind = VariableKind.Const,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("let {a:b} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "b"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = VariableKind.Let,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var {a:b} = {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "b"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Modules

            Program.Test("export var document", new TestNode
            {
                type = typeof(ProgramNode),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), name = "document"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export var document = { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), name = "document"},
                                    init = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        properties = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 25, 25))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 25, 25))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("export var await", "The keyword 'await' is reserved (1:11)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export let document", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), name = "document"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export let document = { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), name = "document"},
                                    init = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        properties = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 25, 25))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 25, 25))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export const document = { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), name = "document"},
                                    init = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        properties = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                                    },
                                    location = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                                }
                            },
                            kind = VariableKind.Const,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export function parse() { }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)), name = "parse"},
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export class Class {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(ClassDeclarationNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)), name = "Class"},
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21))
                            },
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 21, 21))
                        },
                        specifiers = new List<TestNode>(),
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("export new Foo();", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export typeof foo;", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export default 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = 42,
                            raw = "42",
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export default function () {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29)),
                            id = null,
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export default function f() {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 30, 30)),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "f"},
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export default class {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        declaration = new TestNode
                        {
                            type = typeof(ClassDeclarationNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23)),
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export default class A {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        declaration = new TestNode
                        {
                            type = typeof(ClassDeclarationNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25)),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "A"},
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25)),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export default (class{});", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("export *", "Unexpected token (1:8)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("export * from \"crypto\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportAllDeclarationNode),
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "crypto",
                            raw = "\"crypto\"",
                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export { encrypt }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = null,
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            }
                        },
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export { encrypt, decrypt }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = null,
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "decrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "decrypt"},
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25))
                            }
                        },
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export { encrypt as default }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = null,
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), name = "default"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27))
                            }
                        },
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export { encrypt, decrypt as dec }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = null,
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), name = "dec"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "decrypt"},
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                            }
                        },
                        source = null,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("export { default } from \"other\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportNamedDeclarationNode),
                        declaration = null,
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ExportSpecifierNode),
                                exported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "default"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "default"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)),
                            value = "other",
                            raw = "\"other\""
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("export { default }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export { if }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export { default as foo }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export { if as foo }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("import \"jquery\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>(),
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "jquery",
                            raw = "\"jquery\"",
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("import $ from \"jquery\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportDefaultSpecifierNode),
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "$"},
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "jquery",
                            raw = "\"jquery\"",
                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("import { encrypt, decrypt } from \"crypto\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "decrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), name = "decrypt"},
                                location = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25))
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "crypto",
                            raw = "\"crypto\"",
                            location = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("import { encrypt as enc } from \"crypto\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), name = "enc"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 23, 23))
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "crypto",
                            raw = "\"crypto\"",
                            location = new SourceLocation(new Position(1, 31, 31), new Position(1, 39, 39))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("import crypto, { decrypt, encrypt as enc } from \"crypto\"", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56)),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportDefaultSpecifierNode),
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)),
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)), name = "crypto"}
                            },
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)), name = "decrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)), name = "decrypt"}
                            },
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                location = new SourceLocation(new Position(1, 26, 26), new Position(1, 40, 40)),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 33, 33)), name = "encrypt"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 40, 40)), name = "enc"}
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            location = new SourceLocation(new Position(1, 48, 48), new Position(1, 56, 56)),
                            value = "crypto",
                            raw = "\"crypto\""
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("import default from \"foo\"", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("import { null as nil } from \"bar\"", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportSpecifierNode),
                                imported = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "null"},
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), name = "nil"},
                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20))
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "bar",
                            raw = "\"bar\"",
                            location = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("import * as crypto from \"crypto\"", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportNamespaceSpecifierNode),
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 18, 18)),
                                local = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)), name = "crypto"}
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 32, 32)),
                            value = "crypto",
                            raw = "\"crypto\""
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("import { class } from 'foo'", "Unexpected keyword 'class' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import { class, var } from 'foo'", "Unexpected keyword 'class' (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import { a as class } from 'foo'", "Unexpected keyword 'class' (1:14)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import * as class from 'foo'", "Unexpected keyword 'class' (1:12)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import { enum } from 'foo'", "The keyword 'enum' is reserved (1:9)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import { a as enum } from 'foo'", "The keyword 'enum' is reserved (1:14)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("import * as enum from 'foo'", "The keyword 'enum' is reserved (1:12)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("() => { class a extends b { static get prototype(){} } }", "Classes may not have a static property named prototype (1:39)", new Options {ecmaVersion= 6});
            Program.TestFail("class a extends b { static set prototype(){} }", "Classes may not have a static property named prototype (1:31)", new Options {ecmaVersion= 6});
            Program.TestFail("class a { static prototype(){} }", "Classes may not have a static property named prototype (1:17)", new Options {ecmaVersion= 6});

            // Harmony: Yield Expression

            Program.Test("(function* () { yield v })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(YieldExpressionNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "v"},
                                            @delegate = false,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                        },
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 25, 25))
                            },
                            generator = true,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("(function* () { yield\nv })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(YieldExpressionNode),
                                            argument = null,
                                            @delegate = false,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                        },
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 0, 22), new Position(2, 1, 23)), name = "v"},
                                        location = new SourceLocation(new Position(2, 0, 22), new Position(2, 1, 23))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(2, 3, 25))
                            },
                            generator = true,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(2, 3, 25))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 26))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.Test("(function* () { yield *v })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(YieldExpressionNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "v"},
                                            @delegate = true,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 24, 24))
                                        },
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 24, 24))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 26, 26))
                            },
                            generator = true,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function* test () { yield *v }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "test"},
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(YieldExpressionNode),
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), name = "v"},
                                        @delegate = true,
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                    },
                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                }
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30))
                        },
                        generator = true,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var x = { *test () { yield *v } };", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)), name = "test"},
                                            value = new TestNode
                                            {
                                                type = typeof(FunctionExpressionNode),
                                                id = null,
                                                parameters = new List<TestNode>(),
                                                body = new TestNode
                                                {
                                                    type = typeof(BlockStatementNode),
                                                    body = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(ExpressionStatementNode),
                                                            expression = new TestNode
                                                            {
                                                                type = typeof(YieldExpressionNode),
                                                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "v"},
                                                                @delegate = true,
                                                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                            },
                                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31))
                                                },
                                                generator = true,
                                                expression = false,
                                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 31, 31))
                                            },
                                            kind = PropertyKind.Initialise,
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function* foo() { console.log(yield); }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "foo"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        callee = new TestNode
                                        {
                                            type = typeof(MemberExpressionNode),
                                            location = default,
                                            @object = new TestNode {type = typeof(IdentifierNode), name = "console"},
                                            property = new TestNode {type = typeof(IdentifierNode), name = "log"},
                                            computed = false
                                        },
                                        arguments = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(YieldExpressionNode),
                                                @delegate = false,
                                                argument = null
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function* t() {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "t"},
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                        },
                        generator = true,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(function* () { yield yield 10 })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(YieldExpressionNode),
                                            argument = new TestNode
                                            {
                                                type = typeof(YieldExpressionNode),
                                                argument = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    value = 10,
                                                    raw = "10",
                                                    location = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                                                },
                                                @delegate = false,
                                                location = new SourceLocation(new Position(1, 22, 22), new Position(1, 30, 30))
                                            },
                                            @delegate = false,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                        },
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                            },
                            generator = true,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("function *g() { (x = yield) => {} }", "Yield expression cannot be a default value (1:21)", new Options {ecmaVersion = 6});
            Program.TestFail("function *g() { ({x = yield}) => {} }", "Yield expression cannot be a default value (1:22)", new Options {ecmaVersion = 6});

            // Harmony: Iterators

            Program.Test("for(x of list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), name = "process"},
                                arguments = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25))
                            },
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("for (var x of list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), name = "process"},
                                arguments = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("for (var x = 42 of list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 42,
                                        raw = "42",
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                    },
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                }
                            },
                            kind = VariableKind.Var,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 23, 23)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)), name = "process"},
                                arguments = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 35, 35))
                            },
                            location = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("for (let x of list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = VariableKind.Let,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), name = "process"},
                                arguments = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Class (strawman)

            Program.Test("var A = class extends B {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "A"},
                                init = new TestNode
                                {
                                    type = typeof(ClassExpressionNode),
                                    superClass = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "B"},
                                    body = new TestNode
                                    {
                                        type = typeof(ClassBodyNode),
                                        body = new List<TestNode>(),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 26, 26))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 26, 26))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A extends class B extends C {} {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "B"},
                            superClass = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 33, 33)), name = "C"},
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                            },
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36))
                        },
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 37, 37), new Position(1, 39, 39))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {get() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "get"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static get() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), name = "get"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 25, 25))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A extends B {get foo() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "B"},
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31))
                                }
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A extends B { static get foo() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "B"},
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 37, 37), new Position(1, 39, 39))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 34, 34), new Position(1, 39, 39))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 39, 39))
                                }
                            },
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 40, 40))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {set a(v) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static set a(v) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "a"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 26, 26), new Position(1, 28, 28))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 28, 28))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {set(v) {};}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "set"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static set(v) {};}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), name = "set"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 26, 26))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {*gen(v) { yield v; }}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "gen"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(YieldExpressionNode),
                                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "v"},
                                                        @delegate = false,
                                                        location = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                                    },
                                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                        },
                                        generator = true,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 29, 29))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 29, 29))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static *gen(v) { yield v; }}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), name = "gen"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(ExpressionStatementNode),
                                                    expression = new TestNode
                                                    {
                                                        type = typeof(YieldExpressionNode),
                                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), name = "v"},
                                                        @delegate = false,
                                                        location = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                                    },
                                                    location = new SourceLocation(new Position(1, 27, 27), new Position(1, 35, 35))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 25, 25), new Position(1, 37, 37))
                                        },
                                        generator = true,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 21, 21), new Position(1, 37, 37))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 37, 37))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 38, 38))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });
            
            Program.TestFail("(class { *static x() {} })", "Unexpected token (1:17)", new Options{ecmaVersion= 6});
            Program.Test("(class { *static() {} })", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23)),
                            id = null,
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 23, 23)),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 21, 21)),
                                        computed = false,
                                        key = new TestNode
                                        {
                                            type = typeof(IdentifierNode),
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16)),
                                            name = "static"
                                        },
                                        @static = false,
                                        kind = PropertyKind.Method,
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)),
                                            id = null,
                                            generator = true,
                                            expression = false,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                location = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21)),
                                                body = new List<TestNode>()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                source = SourceType.Script
            }, new Options {ecmaVersion = 6});

            Program.Test("\"use strict\"; (class A {constructor() { super() }})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "use strict",
                            raw = "\"use strict\"",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "A"},
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        computed = false,
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35)), name = "constructor"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(SuperNode),
                                                                location = new SourceLocation(new Position(1, 40, 40), new Position(1, 45, 45))
                                                            },
                                                            arguments = new List<TestNode>(),
                                                            location = new SourceLocation(new Position(1, 40, 40), new Position(1, 47, 47))
                                                        },
                                                        location = new SourceLocation(new Position(1, 40, 40), new Position(1, 47, 47))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 38, 38), new Position(1, 49, 49))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 35, 35), new Position(1, 49, 49))
                                        },
                                        kind = PropertyKind.Constructor,
                                        @static = false,
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 49, 49))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 50, 50))
                            },
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 50, 50))
                        },
                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 51, 51))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {'constructor'() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = "constructor"
                                    },
                                    @static = false,
                                    kind = PropertyKind.Constructor,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        generator = false,
                                        expression = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("class A { constructor() {} 'constructor'() }", "Duplicate constructor in the same class (1:27)", new Options {ecmaVersion = 6});

            Program.TestFail("class A { get constructor() {} }", "Constructor can't have get/set modifier (1:14)", new Options {ecmaVersion = 6});

            Program.TestFail("class A { *constructor() {} }", "Constructor can't be a generator (1:11)", new Options {ecmaVersion = 6});

            Program.Test("class A {static foo() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 19, 19), new Position(1, 24, 24))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 24, 24))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {foo() {} static bar() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), name = "bar"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 31, 31), new Position(1, 33, 33))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 33, 33))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 34, 34))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("\"use strict\"; (class A { static constructor() { super() }})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "use strict",
                            raw = "\"use strict\"",
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "A"},
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        computed = false,
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 43, 43)), name = "constructor"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>(),
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(ExpressionStatementNode),
                                                        expression = new TestNode
                                                        {
                                                            type = typeof(CallExpressionNode),
                                                            callee = new TestNode
                                                            {
                                                                type = typeof(SuperNode),
                                                                location = new SourceLocation(new Position(1, 48, 48), new Position(1, 53, 53))
                                                            },
                                                            arguments = new List<TestNode>(),
                                                            location = new SourceLocation(new Position(1, 48, 48), new Position(1, 55, 55))
                                                        },
                                                        location = new SourceLocation(new Position(1, 48, 48), new Position(1, 55, 55))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 46, 46), new Position(1, 57, 57))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 43, 43), new Position(1, 57, 57))
                                        },
                                        kind = PropertyKind.Method,
                                        @static = true,
                                        location = new SourceLocation(new Position(1, 25, 25), new Position(1, 57, 57))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 58, 58))
                            },
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 58, 58))
                        },
                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 59, 59))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { foo() {} bar() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)), name = "bar"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27))
                                    },
                                    kind = PropertyKind.Method,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { get foo() {} set foo(v) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 22, 22))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 23, 23), new Position(1, 36, 36))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 37, 37))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static get foo() {} get foo() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 40, 40), new Position(1, 42, 42))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 37, 37), new Position(1, 42, 42))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 30, 30), new Position(1, 42, 42))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 43, 43))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 43, 43))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 43, 43))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static get foo() {} static get bar() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 44, 44)), name = "bar"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 47, 47), new Position(1, 49, 49))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 44, 44), new Position(1, 49, 49))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 30, 30), new Position(1, 49, 49))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 51, 51))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static get foo() {} static set foo(v) {} get foo() {} set foo(v) {}}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 41, 41), new Position(1, 44, 44)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 45, 45), new Position(1, 46, 46)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 48, 48), new Position(1, 50, 50))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 44, 44), new Position(1, 50, 50))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = true,
                                    location = new SourceLocation(new Position(1, 30, 30), new Position(1, 50, 50))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 55, 55), new Position(1, 58, 58)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 61, 61), new Position(1, 63, 63))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 58, 58), new Position(1, 63, 63))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 68, 68), new Position(1, 71, 71)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 72, 72), new Position(1, 73, 73)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 75, 75), new Position(1, 77, 77))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 71, 71), new Position(1, 77, 77))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 64, 64), new Position(1, 77, 77))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 78, 78))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 78, 78))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 78, 78))
            }, new Options
            {
                ecmaVersion = 6
            });


            Program.Test("class A { static [foo]() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 27, 27)),
                                    @static = true,
                                    computed = true,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), name = "foo"},
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { static get [foo]() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31)),
                                    @static = true,
                                    computed = true,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), name = "foo"},
                                    kind = PropertyKind.Get,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { set foo(v) {} get foo() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 23, 23))
                                    },
                                    kind = PropertyKind.Set,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 23, 23))
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)), name = "foo"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 31, 31), new Position(1, 36, 36))
                                    },
                                    kind = PropertyKind.Get,
                                    @static = false,
                                    location = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36))
                                }
                            },
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 38, 38))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A { foo() {} get foo() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18)),
                                    @static = false,
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)),
                                    @static = false,
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)), name = "foo"},
                                    kind = PropertyKind.Get,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class Semicolon { ; }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15)), name = "Semicolon"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Computed Properties

            Program.Test("({[x]: 10})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 10,
                                        raw = "10",
                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({[\"x\" + \"y\"]: 10})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode
                                    {
                                        type = typeof(BinaryExpressionNode),
                                        @operator = Operator.Addition,
                                        left = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "x",
                                            raw = "\"x\"",
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 6, 6))
                                        },
                                        right = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = "y",
                                            raw = "\"y\"",
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12))
                                        },
                                        location = new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12))
                                    },
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 10,
                                        raw = "10",
                                        location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({[x]: function() {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 20, 20))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({[x]: 10, y: 20})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 10,
                                        raw = "10",
                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "y"},
                                    value = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        value = 20,
                                        raw = "20",
                                        location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({get [x]() {}, set [x](v) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                                    },
                                    kind = PropertyKind.Get,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 14, 14))
                                },
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "v"}
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 23, 23), new Position(1, 29, 29))
                                    },
                                    kind = PropertyKind.Set,
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 16, 16), new Position(1, 29, 29))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({[x]() {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = true,
                                    shorthand = false,
                                    computed = true,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 11, 11))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var {[x]: y} = {y}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "y"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = true,
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                                },
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "y"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "y"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function f({[x]: y}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "x"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "y"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = false,
                                        computed = true,
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var x = {*[test]() { yield *v; }}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "x"},
                                init = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)), name = "test"},
                                            value = new TestNode
                                            {
                                                type = typeof(FunctionExpressionNode),
                                                id = null,
                                                parameters = new List<TestNode>(),
                                                body = new TestNode
                                                {
                                                    type = typeof(BlockStatementNode),
                                                    body = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(ExpressionStatementNode),
                                                            expression = new TestNode
                                                            {
                                                                type = typeof(YieldExpressionNode),
                                                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "v"},
                                                                @delegate = true,
                                                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                            },
                                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 30, 30))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 19, 19), new Position(1, 32, 32))
                                                },
                                                generator = true,
                                                expression = false,
                                                location = new SourceLocation(new Position(1, 16, 16), new Position(1, 32, 32))
                                            },
                                            kind = PropertyKind.Initialise,
                                            method = true,
                                            shorthand = false,
                                            computed = true,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 32, 32))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33))
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("class A {[x]() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)),
                                    @static = false,
                                    computed = true,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "x"},
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("({[x]})", "Unexpected token (1:5)", new Options {ecmaVersion = 6});

            // ES6: Default parameters

            Program.Test("function f([x] = [1]) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                left = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "x"}
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 1,
                                            raw = "1",
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function f([x] = [1]) { 'use strict' }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                left = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "x"}
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 1,
                                            raw = "1",
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    location = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36)),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                }
                            },
                            location = new SourceLocation(new Position(1, 22, 22), new Position(1, 38, 38))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function f({x} = {x: 10}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                left = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "x"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "x"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "x"},
                                            value = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                value = 10,
                                                raw = "10",
                                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                                            },
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 23, 23))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24))
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 26, 26), new Position(1, 28, 28))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("f = function({x} = {x: 10}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "f"},
                            right = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(AssignmentPatternNode),
                                        left = new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "x"},
                                                    value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "x"},
                                                    kind = PropertyKind.Initialise,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 16, 16))
                                        },
                                        right = new TestNode
                                        {
                                            type = typeof(ObjectExpressionNode),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "x"},
                                                    value = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        value = 10,
                                                        raw = "10",
                                                        location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                                    },
                                                    kind = PropertyKind.Initialise,
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                        },
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                                    }
                                },
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                                },
                                generator = false,
                                expression = false,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({f: function({x} = {x: 10}) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "f"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                left = new TestNode
                                                {
                                                    type = typeof(ObjectPatternNode),
                                                    properties = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(PropertyNode),
                                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "x"},
                                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "x"},
                                                            kind = PropertyKind.Initialise,
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17))
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(ObjectExpressionNode),
                                                    properties = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(PropertyNode),
                                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "x"},
                                                            value = new TestNode
                                                            {
                                                                type = typeof(LiteralNode),
                                                                value = 10,
                                                                raw = "10",
                                                                location = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                                            },
                                                            kind = PropertyKind.Initialise,
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            location = new SourceLocation(new Position(1, 21, 21), new Position(1, 26, 26))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                                },
                                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 27, 27))
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 5, 5), new Position(1, 31, 31))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 31, 31))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({f({x} = {x: 10}) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "f"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                left = new TestNode
                                                {
                                                    type = typeof(ObjectPatternNode),
                                                    properties = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(PropertyNode),
                                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                                                            kind = PropertyKind.Initialise,
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                                },
                                                right = new TestNode
                                                {
                                                    type = typeof(ObjectExpressionNode),
                                                    properties = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(PropertyNode),
                                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                                                            value = new TestNode
                                                            {
                                                                type = typeof(LiteralNode),
                                                                value = 10,
                                                                raw = "10",
                                                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                                            },
                                                            kind = PropertyKind.Initialise,
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                                                        }
                                                    },
                                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                                                },
                                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 3, 3), new Position(1, 21, 21))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 21, 21))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(class {f({x} = {x: 10}) {}})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ClassExpressionNode),
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(MethodDefinitionNode),
                                        computed = false,
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "f"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(AssignmentPatternNode),
                                                    left = new TestNode
                                                    {
                                                        type = typeof(ObjectPatternNode),
                                                        properties = new List<TestNode>
                                                        {
                                                            new TestNode
                                                            {
                                                                type = typeof(PropertyNode),
                                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                                                                kind = PropertyKind.Initialise,
                                                                method = false,
                                                                shorthand = true,
                                                                computed = false,
                                                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                                                            }
                                                        },
                                                        location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                                    },
                                                    right = new TestNode
                                                    {
                                                        type = typeof(ObjectExpressionNode),
                                                        properties = new List<TestNode>
                                                        {
                                                            new TestNode
                                                            {
                                                                type = typeof(PropertyNode),
                                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "x"},
                                                                value = new TestNode
                                                                {
                                                                    type = typeof(LiteralNode),
                                                                    value = 10,
                                                                    raw = "10",
                                                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                                                },
                                                                kind = PropertyKind.Initialise,
                                                                method = false,
                                                                shorthand = false,
                                                                computed = false,
                                                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                                            }
                                                        },
                                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                                    },
                                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 23, 23))
                                                }
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27))
                                        },
                                        kind = PropertyKind.Method,
                                        @static = false,
                                        location = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 28, 28))
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 28, 28))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(({x} = {x: 10}) => {})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(AssignmentPatternNode),
                                    left = new TestNode
                                    {
                                        type = typeof(ObjectPatternNode),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                                kind = PropertyKind.Initialise,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                                    },
                                    right = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                                                value = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    value = 10,
                                                    raw = "10",
                                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                                },
                                                kind = PropertyKind.Initialise,
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15))
                                    },
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = function(y = 1) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(FunctionExpressionNode),
                                id = null,
                                parameters = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(AssignmentPatternNode),
                                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "y"},
                                        right = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            value = 1,
                                            raw = "1",
                                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                        },
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18))
                                    }
                                },
                                body = new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>(),
                                    location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                },
                                generator = false,
                                expression = false,
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function f(a = 1) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "a"},
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1,
                                    raw = "1",
                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { f: function(a=1) {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "f"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(AssignmentPatternNode),
                                                    left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "a"},
                                                    right = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        value = 1,
                                                        raw = "1",
                                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                                                    },
                                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21))
                                                }
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 25, 25))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 25, 25))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 27, 27))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("x = { f(a=1) {} }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "x"},
                            right = new TestNode
                            {
                                type = typeof(ObjectExpressionNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "f"},
                                        value = new TestNode
                                        {
                                            type = typeof(FunctionExpressionNode),
                                            id = null,
                                            parameters = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(AssignmentPatternNode),
                                                    left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a"},
                                                    right = new TestNode
                                                    {
                                                        type = typeof(LiteralNode),
                                                        value = 1,
                                                        raw = "1",
                                                        location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                                    },
                                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                                }
                                            },
                                            body = new TestNode
                                            {
                                                type = typeof(BlockStatementNode),
                                                body = new List<TestNode>(),
                                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            expression = false,
                                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 15, 15))
                                        },
                                        kind = PropertyKind.Initialise,
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Rest parameters

            Program.Test("function f(a, ...b) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "f"},
                        parameters = new List<TestNode>
                        {
                            new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "a"},
                            new TestNode
                            {
                                type = typeof(RestElementNode),
                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "b"}
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Destructured Parameters

            Program.Test("function x([ a, b ]){}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a"},
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "b"}
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("function x({ a, b }){}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "x"},
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "a"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "b"},
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "b"},
                                        kind = PropertyKind.Initialise,
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>(),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        expression = false,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("function x(...[ a, b ]){}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});
            Program.TestFail("(([...[ a, b ]]) => {})", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){}", "Unexpected token (1:42)", new Options {ecmaVersion = 6});
            Program.TestFail("(function ({ a(){} }) {})", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            Program.Test("(function x([ a, b ]){})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "x"},
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "b"}
                                    },
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(function x({ a, b }){})", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "x"},
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                        },
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "b"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "b"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("(function x(...[ a, b ]){})", "Unexpected token (1:15)", new Options {ecmaVersion = 6});
            Program.TestFail("var a = { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});
            Program.TestFail("class a { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});

            Program.TestFail("(function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){})", "Unexpected token (1:43)", new Options {ecmaVersion = 6});

            Program.Test("({ x([ a, b ]){} })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ArrayPatternNode),
                                                elements = new List<TestNode>
                                                {
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b"}
                                                },
                                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13))
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 3, 3), new Position(1, 16, 16))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({ x(...[ a, b ]){} })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(RestElementNode),
                                                argument = new TestNode
                                                {
                                                    type = typeof(ArrayPatternNode),
                                                    elements = new List<TestNode>
                                                    {
                                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "a"},
                                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "b"}
                                                    },
                                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                                }
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 3, 3), new Position(1, 19, 19))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Program.Test("({ x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){} })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "x"},
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        id = null,
                                        parameters = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ObjectPatternNode),
                                                properties = new List<TestNode>
                                                {
                                                    new TestNode
                                                    {
                                                        type = typeof(PropertyNode),
                                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                                        value = new TestNode
                                                        {
                                                            type = typeof(ObjectPatternNode),
                                                            properties = new List<TestNode>
                                                            {
                                                                new TestNode
                                                                {
                                                                    type = typeof(PropertyNode),
                                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "w"},
                                                                    value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "w"},
                                                                    kind = PropertyKind.Initialise,
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                                                },
                                                                new TestNode
                                                                {
                                                                    type = typeof(PropertyNode),
                                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "x"},
                                                                    value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "x"},
                                                                    kind = PropertyKind.Initialise,
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                                }
                                                            },
                                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18))
                                                        },
                                                        kind = PropertyKind.Initialise,
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        location = new SourceLocation(new Position(1, 7, 7), new Position(1, 18, 18))
                                                    },
                                                    new TestNode
                                                    {
                                                        type = typeof(PropertyNode),
                                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), name = "b"},
                                                        value = new TestNode
                                                        {
                                                            type = typeof(ArrayPatternNode),
                                                            elements = new List<TestNode>
                                                            {
                                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), name = "y"},
                                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), name = "z"}
                                                            },
                                                            location = new SourceLocation(new Position(1, 23, 23), new Position(1, 29, 29))
                                                        },
                                                        kind = PropertyKind.Initialise,
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 29, 29))
                                                    }
                                                },
                                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 31, 31))
                                            },
                                            new TestNode
                                            {
                                                type = typeof(RestElementNode),
                                                argument = new TestNode
                                                {
                                                    type = typeof(ArrayPatternNode),
                                                    elements = new List<TestNode>
                                                    {
                                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), name = "a"},
                                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 40, 40), new Position(1, 41, 41)), name = "b"},
                                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 43, 43), new Position(1, 44, 44)), name = "c"}
                                                    },
                                                    location = new SourceLocation(new Position(1, 36, 36), new Position(1, 45, 45))
                                                }
                                            }
                                        },
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            body = new List<TestNode>(),
                                            location = new SourceLocation(new Position(1, 46, 46), new Position(1, 48, 48))
                                        },
                                        generator = false,
                                        expression = false,
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 48, 48))
                                    },
                                    kind = PropertyKind.Initialise,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    location = new SourceLocation(new Position(1, 3, 3), new Position(1, 48, 48))
                                }
                            },
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 50, 50))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 7
            });

            Program.Test("(...a) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(RestElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "a"}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("(a, ...b) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                new TestNode
                                {
                                    type = typeof(RestElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "b"}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({ a }) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({ a }, ...b) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                },
                                new TestNode
                                {
                                    type = typeof(RestElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b"}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("(...[a, b]) => {}", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("(a, ...[b]) => {}", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Program.Test("({ a: [a, b] }, ...c) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            value = new TestNode
                                            {
                                                type = typeof(ArrayPatternNode),
                                                elements = new List<TestNode>
                                                {
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b"}
                                                },
                                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                            },
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14))
                                },
                                new TestNode
                                {
                                    type = typeof(RestElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "c"}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({ a: b, c }, [d, e], ...f) => {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "b"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                                        },
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "c"},
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "c"},
                                            kind = PropertyKind.Initialise,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12))
                                },
                                new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "d"},
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "e"}
                                    },
                                    location = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                },
                                new TestNode
                                {
                                    type = typeof(RestElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), name = "f"}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>(),
                                location = new SourceLocation(new Position(1, 31, 31), new Position(1, 33, 33))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: SpreadElement

            Program.Test("[...a] = b", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), name = "a"},
                                        location = new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "b"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("[a, ...b] = c", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "b"},
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "c"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("[{ a, b }, ...c] = d", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ObjectPatternNode),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), name = "a"},
                                                kind = PropertyKind.Initialise,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                            },
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "b"},
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "b"},
                                                kind = PropertyKind.Initialise,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                            }
                                        },
                                        location = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                                    },
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "c"},
                                        location = new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), name = "d"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("[a, ...[b, c]] = d", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                    new TestNode
                                    {
                                        type = typeof(RestElementNode),
                                        argument = new TestNode
                                        {
                                            type = typeof(ArrayPatternNode),
                                            elements = new List<TestNode>
                                            {
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "b"},
                                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "c"}
                                            },
                                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                                        },
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), name = "d"},
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [...a] = b", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a"},
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "b"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [a, ...b] = c", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b"},
                                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), name = "c"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [{ a, b }, ...c] = d", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                                    value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), name = "a"},
                                                    kind = PropertyKind.Initialise,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    location = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                                },
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b"},
                                                    value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "b"},
                                                    kind = PropertyKind.Initialise,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                                }
                                            },
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13))
                                        },
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), name = "c"},
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), name = "d"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [a, ...[b, c]] = d", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                        new TestNode
                                        {
                                            type = typeof(RestElementNode),
                                            argument = new TestNode
                                            {
                                                type = typeof(ArrayPatternNode),
                                                elements = new List<TestNode>
                                                {
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), name = "b"},
                                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), name = "c"}
                                                },
                                                location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                                            },
                                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 17, 17))
                                        }
                                    },
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), name = "d"},
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            }
                        },
                        kind = VariableKind.Var,
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Program.Test("func(...a)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "func"},
                            arguments = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a"},
                                    location = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                }
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("func(a, ...b)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "func"},
                            arguments = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "a"},
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b"},
                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12))
                                }
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("func(...a, b)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), name = "func"},
                            arguments = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(SpreadElementNode),
                                    location = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9)),
                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), name = "a"}
                                },
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "b"}
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("/[a-z]/u", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                Pattern = "[a-z]",
                                Flags = "u"
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/u", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                Pattern = "[\\uD834\\uDF06-\\uD834\\uDF08a-z]",
                                Flags = "u"
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("do {} while (false) foo();", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(DoWhileStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 5, 5)),
                            body = new List<TestNode>()
                        },
                        test = new TestNode
                        {
                            type = typeof(LiteralNode),
                            location = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)),
                            value = false,
                            raw = "false"
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25)),
                            callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), name = "foo"},
                            arguments = new List<TestNode>()
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony Invalid syntax

            Program.TestFail("0o", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0o1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0o9", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0o18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0O", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0O1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0O9", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0O18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0b", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0b1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0b9", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0b18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0b12", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0B", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0B1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0B9", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            Program.TestFail("0B18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("0B12", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("\"\\u{110000}\"", "Code point out of bounds (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("\"\\u{}\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("\"\\u{FFFF\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("\"\\u{FFZ}\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("[v] += ary", "Assigning to rvalue (1:0)", new Options {ecmaVersion = 6});

            Program.TestFail("[2] = 42", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("({ obj:20 }) = 42", "Parenthesized pattern (1:0)", new Options {ecmaVersion = 6});

            Program.TestFail("( { get x() {} } = 0)", "Object pattern can't contain getter or setter (1:8)", new Options {ecmaVersion = 6});

            Program.TestFail("x \n is y", "Unexpected token (2:4)", new Options {ecmaVersion = 6});

            Program.TestFail("x \n isnt y", "Unexpected token (2:6)", new Options {ecmaVersion = 6});

            Program.TestFail("function default() {}", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6});

            Program.TestFail("function hello() {'use strict'; ({ i: 10, s(eval) { } }); }", "Binding eval in strict mode (1:44)", new Options {ecmaVersion = 6});

            Program.TestFail("function a() { \"use strict\"; ({ b(t, t) { } }); }", "Argument name clash (1:37)", new Options {ecmaVersion = 6});

            Program.TestFail("var super", "Unexpected keyword 'super' (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("var default", "Unexpected keyword 'default' (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("let default", "Unexpected keyword 'default' (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("const default", "Unexpected keyword 'default' (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; ({ v: eval } = obj)", "Assigning to eval in strict mode (1:20)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; ({ v: arguments } = obj)", "Assigning to arguments in strict mode (1:20)", new Options {ecmaVersion = 6});

            Program.TestFail("for (let x = 42 in list) process(x);", "Unexpected token (1:16)", new Options {ecmaVersion = 6});

            Program.TestFail("for (let x = 42 of list) process(x);", "Unexpected token (1:16)", new Options {ecmaVersion = 6});

            Program.TestFail("import foo", "Unexpected token (1:10)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("import { foo, bar }", "Unexpected token (1:19)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("import foo from bar", "Unexpected token (1:16)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("((a)) => 42", "Parenthesized pattern (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("(a, (b)) => 42", "Parenthesized pattern (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (eval = 10) => 42", "Assigning to eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; eval => 42", "Binding eval in strict mode (1:14)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; arguments => 42", "Binding arguments in strict mode (1:14)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (eval, a) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (arguments, a) => 42", "Binding arguments in strict mode (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (eval, a = 10) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("(a, a) => 42", "Argument name clash (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("function foo(a, a = 2) {}", "Argument name clash (1:16)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (a, a) => 42", "Argument name clash (1:18)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (a) => 00", "Invalid number (1:21)", new Options {ecmaVersion = 6});

            Program.TestFail("() <= 42", "Unexpected token (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("(10) => 00", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("(10, 20) => 00", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("yield v", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("yield 10", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("void { [1, 2]: 3 };", "Unexpected token (1:9)", new Options {ecmaVersion = 6});

            Program.TestFail("let [this] = [10]", "Unexpected keyword 'this' (1:5)", new Options {ecmaVersion = 6});
            Program.TestFail("let {this} = x", "Unexpected keyword 'this' (1:5)", new Options {ecmaVersion = 6});
            Program.TestFail("let [function] = [10]", "Unexpected keyword 'function' (1:5)", new Options {ecmaVersion = 6});
            Program.TestFail("let [function] = x", "Unexpected keyword 'function' (1:5)", new Options {ecmaVersion = 6});
            Program.TestFail("([function] = [10])", "Unexpected token (1:10)", new Options {ecmaVersion = 6});
            Program.TestFail("([this] = [10])", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            Program.TestFail("({this} = x)", "Unexpected keyword 'this' (1:2)", new Options {ecmaVersion = 6});
            Program.TestFail("var x = {this}", "Unexpected keyword 'this' (1:9)", new Options {ecmaVersion = 6});

            Program.Test("yield* 10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            @operator = Operator.Multiplication,
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), name = "yield"},
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 10,
                                raw = "10",
                                location = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("e => yield* 10", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>
                            {
                                new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), name = "e"}
                            },
                            body = new TestNode
                            {
                                type = typeof(BinaryExpressionNode),
                                @operator = Operator.Multiplication,
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10)), name = "yield"},
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 10,
                                    raw = "10",
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 14, 14))
                            },
                            generator = false,
                            expression = true,
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("(function () { yield 10 })", "Unexpected token (1:21)", new Options {ecmaVersion = 6});

            Program.Test("(function () { yield* 10 })", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(FunctionExpressionNode),
                            id = null,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(ExpressionStatementNode),
                                        expression = new TestNode
                                        {
                                            type = typeof(BinaryExpressionNode),
                                            @operator = Operator.Multiplication,
                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), name = "yield"},
                                            right = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                value = 10,
                                                raw = "10",
                                                location = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                            },
                                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                        },
                                        location = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                    }
                                },
                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            generator = false,
                            expression = false,
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("let + 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            left = new TestNode {type = typeof(IdentifierNode), name = "let"},
                            @operator = Operator.Addition,
                            right = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("var let = 1", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), name = "let"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 1,
                                    raw = "1"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("'use strict'; let + 1", "The keyword 'let' is reserved (1:14)", new Options {ecmaVersion = 6});

            Program.Test("var yield = 2", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode {type = typeof(IdentifierNode), name = "yield"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 2,
                                    raw = "2"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("(function() { \"use strict\"; f(yield v) })", "The keyword 'yield' is reserved (1:30)", new Options {ecmaVersion = 6});

            Program.TestFail("var obj = { *test** }", "Unexpected token (1:17)", new Options {ecmaVersion = 6});

            Program.TestFail("class A extends yield B { }", "Unexpected token (1:22)", new Options {ecmaVersion = 6});

            Program.TestFail("class default", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("`test", "Unterminated template (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("switch `test`", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Program.TestFail("`hello ${10 `test`", "Unexpected token (1:18)", new Options {ecmaVersion = 6});

            Program.TestFail("`hello ${10;test`", "Unexpected token (1:11)", new Options {ecmaVersion = 6});

            Program.TestFail("function a() 1 // expression closure is not supported", "Unexpected token (1:13)", new Options {ecmaVersion = 6});

            Program.TestFail("({ \"chance\" }) = obj", "Unexpected token (1:12)", new Options {ecmaVersion = 6});

            Program.TestFail("({ 42 }) = obj", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("function f(a, ...b, c)", "Comma is not permitted after the rest element (1:18)", new Options {ecmaVersion = 6});

            Program.TestFail("function f(a, ...b = 0)", "Unexpected token (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("function x(...{ a }){}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; function x(a, { a }){}", "Argument name clash (1:30)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; function x({ b: { a } }, [{ b: { a } }]){}", "Argument name clash (1:47)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; function x(a, ...[a]){}", "Unexpected token (1:31)", new Options {ecmaVersion = 6});

            Program.TestFail("(...a, b) => {}", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("([ 5 ]) => {}", "Assigning to rvalue (1:3)", new Options {ecmaVersion = 6});

            Program.TestFail("({ 5 }) => {}", "Unexpected token (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("(...[ 5 ]) => {}", "Unexpected token (1:6)", new Options {ecmaVersion = 7});

            Program.Test("[...{ a }] = b", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.TestFail("[...a, b] = c", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("({ t(eval) { \"use strict\"; } });", "Binding eval in strict mode (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; `${test}\\02`;", "Octal literal in strict mode (1:22)", new Options {ecmaVersion = 6});

            Program.TestFail("if (1) import \"acorn\";", "'import' and 'export' may only appear at the top level (1:7)", new Options {ecmaVersion = 6});

            Program.TestFail("[...a, ] = b", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("if (b,...a, );", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.TestFail("(b, ...a)", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("switch (cond) { case 10: let a = 20; ", "Unexpected token (1:37)", new Options {ecmaVersion = 6});

            Program.TestFail("\"use strict\"; (eval) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("(eval) => { \"use strict\"; 42 }", "Binding eval in strict mode (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("({ get test() { } }) => 42", "Object pattern can't contain getter or setter (1:7)", new Options {ecmaVersion = 6});

            /* Regression tests */

            // # https://github.com/ternjs/acorn/issues/127
            Program.Test("doSmth(`${x} + ${y} = ${x + y}`)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(CallExpressionNode),
                            callee = new TestNode {type = typeof(IdentifierNode), name = "doSmth"},
                            arguments = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateLiteralNode),
                                    expressions = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), name = "x"},
                                        new TestNode {type = typeof(IdentifierNode), name = "y"},
                                        new TestNode
                                        {
                                            type = typeof(BinaryExpressionNode),
                                            left = new TestNode {type = typeof(IdentifierNode), name = "x"},
                                            @operator = Operator.Addition,
                                            right = new TestNode {type = typeof(IdentifierNode), name = "y"}
                                        }
                                    },
                                    quasis = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("", ""),
                                            tail = false
                                        },
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode(" + ", " + "),
                                            tail = false
                                        },
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode(" = ", " = "),
                                            tail = false
                                        },
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("", ""),
                                            tail = true
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // # https://github.com/ternjs/acorn/issues/129
            Program.Test("function normal(x, y = 10) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "normal"},
                        parameters = new List<TestNode>
                        {
                            new TestNode {type = typeof(IdentifierNode), name = "x"},
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                left = new TestNode {type = typeof(IdentifierNode), name = "y"},
                                right = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        generator = false,
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>()
                        },
                        expression = false
                    }
                }
            }, new Options {ecmaVersion = 6});

            // test preserveParens option with arrow functions
            Program.Test("() => 42", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            expression = true
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, preserveParens = true});

            // https://github.com/ternjs/acorn/issues/161
            Program.Test("import foo, * as bar from 'baz';", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ImportDeclarationNode),
                        specifiers = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(ImportDefaultSpecifierNode),
                                local = new TestNode {type = typeof(IdentifierNode), name = "foo"}
                            },
                            new TestNode
                            {
                                type = typeof(ImportNamespaceSpecifierNode),
                                local = new TestNode {type = typeof(IdentifierNode), name = "bar"}
                            }
                        },
                        source = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = "baz",
                            raw = "'baz'"
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            // https://github.com/ternjs/acorn/issues/173
            Program.Test("`{${x}}`, `}`", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(SequenceExpressionNode),
                            expressions = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateLiteralNode),
                                    expressions = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), name = "x"}
                                    },
                                    quasis = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("{", "{"),
                                            tail = false
                                        },
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("}", "}"),
                                            tail = true
                                        }
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateLiteralNode),
                                    expressions = new List<TestNode>(),
                                    quasis = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(TemplateElementNode),
                                            value = new TemplateNode("}", "}"),
                                            tail = true
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/186
            Program.Test("var {get} = obj;", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), name = "get"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode {type = typeof(IdentifierNode), name = "get"}
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), name = "obj"}
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            // Destructuring defaults (https://github.com/ternjs/acorn/issues/181)

            Program.Test("var {propName: localVar = defaultValue} = obj", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 45, 45)),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 39, 39)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 38, 38)),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), name = "propName"},
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                location = new SourceLocation(new Position(1, 15, 15), new Position(1, 38, 38)),
                                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23)), name = "localVar"},
                                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 38, 38)), name = "defaultValue"}
                                            },
                                            kind = PropertyKind.Initialise
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), name = "obj"}
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var {propName = defaultValue} = obj", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 35, 35)),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 29, 29)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), name = "propName"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), name = "propName"},
                                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28)), name = "defaultValue"}
                                            }
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), name = "obj"}
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var {get = defaultValue} = obj", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30)),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 23, 23)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), name = "get"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 23, 23)),
                                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), name = "get"},
                                                right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 23, 23)), name = "defaultValue"}
                                            }
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), name = "obj"}
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var [localVar = defaultValue] = obj", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 35, 35)),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 29, 29)),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(AssignmentPatternNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), name = "localVar"},
                                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28)), name = "defaultValue"}
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), name = "obj"}
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({x = 0} = obj)", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                location = new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode
                                        {
                                            type = typeof(AssignmentPatternNode),
                                            location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                            right = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)),
                                                value = 0
                                            }
                                        }
                                    }
                                }
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), name = "obj"}
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("({x = 0}) => x", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            id = null,
                            generator = false,
                            expression = true,
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode
                                            {
                                                type = typeof(AssignmentPatternNode),
                                                location = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), name = "x"},
                                                right = new TestNode
                                                {
                                                    type = typeof(LiteralNode),
                                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)),
                                                    value = 0
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            body = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), name = "x"}
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("[a, {b: {c = 1}}] = arr", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        expression = new TestNode
                        {
                            type = typeof(AssignmentExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                            @operator = Operator.Assignment,
                            left = new TestNode
                            {
                                type = typeof(ArrayPatternNode),
                                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                elements = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), name = "a"},
                                    new TestNode
                                    {
                                        type = typeof(ObjectPatternNode),
                                        location = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15)),
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "b"},
                                                value = new TestNode
                                                {
                                                    type = typeof(ObjectPatternNode),
                                                    location = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15)),
                                                    properties = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(PropertyNode),
                                                            location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)),
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "c"},
                                                            kind = PropertyKind.Initialise,
                                                            value = new TestNode
                                                            {
                                                                type = typeof(AssignmentPatternNode),
                                                                location = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)),
                                                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), name = "c"},
                                                                right = new TestNode
                                                                {
                                                                    type = typeof(LiteralNode),
                                                                    location = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)),
                                                                    value = 1
                                                                }
                                                            }
                                                        }
                                                    }
                                                },
                                                kind = PropertyKind.Initialise
                                            }
                                        }
                                    }
                                }
                            },
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), name = "arr"}
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("for ({x = 0} in arr);", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        left = new TestNode
                        {
                            type = typeof(ObjectPatternNode),
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12)),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)),
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode
                                    {
                                        type = typeof(AssignmentPatternNode),
                                        location = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)),
                                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "x"},
                                        right = new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)),
                                            value = 0
                                        }
                                    }
                                }
                            }
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), name = "arr"},
                        body = new TestNode
                        {
                            type = typeof(EmptyStatementNode),
                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.TestFail("obj = {x = 0}", "Shorthand property assignments are valid only in destructuring patterns (1:9)", new Options {ecmaVersion = 6});

            Program.TestFail("f({x = 0})", "Shorthand property assignments are valid only in destructuring patterns (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("(localVar |= defaultValue) => {}", "Only '=' operator can be used for specifying default value. (1:9)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/191

            Program.Test("try {} catch ({message}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(TryStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        block = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)),
                            body = new List<TestNode>()
                        },
                        handler = new TestNode
                        {
                            type = typeof(CatchClauseNode),
                            location = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27)),
                            param = new TestNode
                            {
                                type = typeof(ObjectPatternNode),
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 23, 23)),
                                properties = new List<TestNode>
                                {
                                    new TestNode
                                    {
                                        type = typeof(PropertyNode),
                                        location = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), name = "message"},
                                        kind = PropertyKind.Initialise,
                                        value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), name = "message"}
                                    }
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27)),
                                body = new List<TestNode>()
                            }
                        },
                        finaliser = null
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // https://github.com/ternjs/acorn/issues/192

            Program.Test("class A { static() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 23, 23)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 21, 21)),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16)), name = "static"},
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = false,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // https://github.com/ternjs/acorn/issues/213

            Program.Test("for (const x of list) process(x);", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForOfStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), name = "x"},
                                    init = null,
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                                }
                            },
                            kind = VariableKind.Const,
                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                        },
                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 16, 16), new Position(1, 20, 20)), name = "list"},
                        body = new TestNode
                        {
                            type = typeof(ExpressionStatementNode),
                            expression = new TestNode
                            {
                                type = typeof(CallExpressionNode),
                                callee = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29)), name = "process"},
                                arguments = new List<TestNode>
                                {
                                    new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 30, 30), new Position(1, 31, 31)), name = "x"}
                                },
                                location = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                            },
                            location = new SourceLocation(new Position(1, 22, 22), new Position(1, 33, 33))
                        },
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options {ecmaVersion = 6});

            Program.Test("class A { *static() {} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 24, 24)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 22, 22)),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17)), name = "static"},
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)),
                                        id = null,
                                        parameters = new List<TestNode>(),
                                        generator = true,
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22)),
                                            body = new List<TestNode>()
                                        },
                                        expression = false
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("`${/\\d/.exec('1')[0]}`", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                        expression = new TestNode
                        {
                            type = typeof(TemplateLiteralNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                            expressions = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MemberExpressionNode),
                                    location = new SourceLocation(new Position(1, 3, 3), new Position(1, 20, 20)),
                                    @object = new TestNode
                                    {
                                        type = typeof(CallExpressionNode),
                                        location = new SourceLocation(new Position(1, 3, 3), new Position(1, 17, 17)),
                                        callee = new TestNode
                                        {
                                            type = typeof(MemberExpressionNode),
                                            location = new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12)),
                                            @object = new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                location = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)),
                                                regex = new RegexNode
                                                {
                                                    Pattern = "\\d",
                                                    Flags = ""
                                                },
                                                raw = "/\\d/"
                                            },
                                            property = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12)), name = "exec"},
                                            computed = false
                                        },
                                        arguments = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(LiteralNode),
                                                location = new SourceLocation(new Position(1, 13, 13), new Position(1, 16, 16)),
                                                value = "1",
                                                raw = "'1'"
                                            }
                                        }
                                    },
                                    property = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        location = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)),
                                        value = 0,
                                        raw = "0"
                                    },
                                    computed = true
                                }
                            },
                            quasis = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    location = new SourceLocation(new Position(1, 1, 1), new Position(1, 1, 1)),
                                    value = new TemplateNode("", ""),
                                    tail = false
                                },
                                new TestNode
                                {
                                    type = typeof(TemplateElementNode),
                                    location = new SourceLocation(new Position(1, 21, 21), new Position(1, 21, 21)),
                                    value = new TemplateNode("", ""),
                                    tail = true
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Program.Test("var _𐒦 = 10;", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "_𐒦"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("var 𫠝_ = 10;", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), name = "𫠝_"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("var _\\u{104A6} = 10;", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19)),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)), name = "_𐒦"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("let [x,] = [1]", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)),
                                id = new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"}
                                    }
                                },
                                init = new TestNode
                                {
                                    type = typeof(ArrayExpressionNode),
                                    location = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)),
                                    elements = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(LiteralNode),
                                            location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)),
                                            value = 1,
                                            raw = "1"
                                        }
                                    }
                                }
                            }
                        },
                        kind = VariableKind.Let
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("let {x} = y", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)),
                                id = new TestNode
                                {
                                    type = typeof(ObjectPatternNode),
                                    location = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), name = "x"}
                                        }
                                    }
                                },
                                init = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), name = "y"}
                            }
                        },
                        kind = VariableKind.Let
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("[x,,] = 1", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (var [name, value] in obj) {}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ForInStatementNode),
                        left = new TestNode
                        {
                            type = typeof(VariableDeclarationNode),
                            declarations = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(VariableDeclaratorNode),
                                    id = new TestNode
                                    {
                                        type = typeof(ArrayPatternNode),
                                        elements = new List<TestNode>
                                        {
                                            new TestNode {type = typeof(IdentifierNode), name = "name"},
                                            new TestNode {type = typeof(IdentifierNode), name = "value"}
                                        }
                                    },
                                    init = null
                                }
                            },
                            kind = VariableKind.Var
                        },
                        right = new TestNode {type = typeof(IdentifierNode), name = "obj"},
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("let [x]", "Complex binding patterns require an initialization value (1:7)", new Options {ecmaVersion = 6});
            Program.TestFail("var [x]", "Complex binding patterns require an initialization value (1:7)", new Options {ecmaVersion = 6});
            Program.TestFail("var _𖫵 = 11;", "Unexpected character '𖫵' (1:5)", new Options {ecmaVersion = 6});
            Program.TestFail("var 𫠞_ = 12;", "Unexpected character '𫠞' (1:4)", new Options {ecmaVersion = 6});
            Program.TestFail("var 𫠝_ = 10;", "Unexpected character '𫠝' (1:4)", new Options {ecmaVersion = 5});
            Program.TestFail("if (1) let x = 10;", "Unexpected token (1:7)", new Options {ecmaVersion = 6});
            Program.TestFail("for (;;) const x = 10;", "Unexpected token (1:9)", new Options {ecmaVersion = 6});
            Program.TestFail("while (1) function foo(){}", "Unexpected token (1:10)", new Options {ecmaVersion = 6});
            Program.TestFail("if (1) ; else class Cls {}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            Program.TestFail("'use strict'; [...eval] = arr", "Assigning to eval in strict mode (1:18)", new Options {ecmaVersion = 6});
            Program.TestFail("'use strict'; ({eval = defValue} = obj)", "Assigning to eval in strict mode (1:16)", new Options {ecmaVersion = 6});

            Program.TestFail("[...eval] = arr", "Assigning to eval in strict mode (1:4)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("function* y({yield}) {}", "Can not use 'yield' as identifier inside a generator (1:13)", new Options {ecmaVersion = 6});

            Program.Test("function foo() { new.target; }", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "foo"},
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(MetaPropertyNode),
                                        meta = new TestNode {type = typeof(IdentifierNode), name = "new"},
                                        property = new TestNode {type = typeof(IdentifierNode), name = "target"}
                                    }
                                }
                            }
                        },
                        generator = false,
                        expression = false
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("new.prop", "The only valid meta property for new is new.target (1:4)", new Options {ecmaVersion = 6});
            Program.TestFail("new.target", "new.target can only be used in functions (1:0)", new Options {ecmaVersion = 6});

            Program.Test("export default function foo() {} false", new TestNode
            {
                type = typeof(ProgramNode),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(FunctionDeclarationNode),
                            id = new TestNode {type = typeof(IdentifierNode), name = "foo"},
                            generator = false,
                            expression = false,
                            parameters = new List<TestNode>(),
                            body = new TestNode
                            {
                                type = typeof(BlockStatementNode),
                                body = new List<TestNode>()
                            }
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            value = false,
                            raw = "false"
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            // https://github.com/ternjs/acorn/issues/274

            Program.TestFail("`\\07`", "Octal literal in strict mode (1:1)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/277

            Program.TestFail("x = { method() 42 }", "Unexpected token (1:15)", new Options {ecmaVersion = 6});

            Program.TestFail("x = { get method() 42 }", "Unexpected token (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("x = { set method(val) v = val }", "Unexpected token (1:22)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/278

            //            testFail("/\\u{110000}/u", "~", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/279

            Program.TestFail("super", "'super' outside of function or class (1:0)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/275

            Program.TestFail("class A { get prop(x) {} }", "getter should have no params (1:18)", new Options {ecmaVersion = 6});
            Program.TestFail("class A { set prop() {} }", "setter should have exactly one param (1:18)", new Options {ecmaVersion = 6});
            Program.TestFail("class A { set prop(x, y) {} }", "setter should have exactly one param (1:18)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/276

            Program.TestFail("({ __proto__: 1, __proto__: 2 })", "Redefinition of __proto__ property (1:17)", new Options {ecmaVersion = 6});
            Program.TestFail("({ '__proto__': 1, __proto__: 2 })", "Redefinition of __proto__ property (1:19)", new Options {ecmaVersion = 6});
            Program.Test("({ ['__proto__']: 1, __proto__: 2 })", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Program.Test("({ __proto__() { return 1 }, __proto__: 2 })", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Program.Test("({ get __proto__() { return 1 }, __proto__: 2 })", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Program.Test("({ __proto__, __proto__: 2 })", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("export default /foo/", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("var await = 0", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(VariableDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(VariableDeclaratorNode),
                                location = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13)),
                                id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), name = "await"},
                                init = new TestNode
                                {
                                    type = typeof(LiteralNode),
                                    location = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)),
                                    value = 0,
                                    raw = "0"
                                }
                            }
                        },
                        kind = VariableKind.Var
                    }
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Script,
                allowReserved = false
            });

            Program.TestFail("var await = 0", "The keyword 'await' is reserved (1:4)", new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module,
                allowReserved = false
            });

            // https://github.com/ternjs/acorn/issues/363

            Program.Test("/[a-z]/gimuy", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(LiteralNode),
                            regex = new RegexNode
                            {
                                Pattern = "[a-z]",
                                Flags = "gimuy"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});
            Program.TestFail("/[a-z]/s", "Invalid regular expression flag (1:1)", new Options {ecmaVersion = 6});

            Program.TestFail("[...x in y] = []", "Assigning to rvalue (1:4)", new Options {ecmaVersion = 6});

            Program.TestFail("export let x = a; export function x() {}", "Identifier 'x' has already been declared (1:34)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export let [{x = 2}] = a; export {x}", "Duplicate export 'x' (1:34)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.TestFail("export default 100; export default 3", "Duplicate export 'default' (1:27)", new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("(([,]) => 0)", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(ArrowFunctionExpressionNode),
                            parameters = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ArrayPatternNode),
                                    elements = new List<TestNode> {null}
                                }
                            },
                            body = new TestNode
                            {
                                type = typeof(LiteralNode),
                                value = 0,
                                raw = "0"
                            },
                            expression = true
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // 'eval' and 'arguments' are not reserved word, but those can not be a BindingIdentifier.

            Program.Test("function foo() { return {arguments} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 37, 37)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ReturnStatementNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 35, 35)),
                                    argument = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)), name = "arguments"},
                                                kind = PropertyKind.Initialise,
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)), name = "arguments"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function foo() { return {eval} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 32, 32)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ReturnStatementNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    argument = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)), name = "eval"},
                                                kind = PropertyKind.Initialise,
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)), name = "eval"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function foo() { 'use strict'; return {arguments} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 51, 51)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(ReturnStatementNode),
                                    location = new SourceLocation(new Position(1, 31, 31), new Position(1, 49, 49)),
                                    argument = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        location = new SourceLocation(new Position(1, 38, 38), new Position(1, 49, 49)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)), name = "arguments"},
                                                kind = PropertyKind.Initialise,
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)), name = "arguments"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function foo() { 'use strict'; return {eval} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 46, 46)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        location = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(ReturnStatementNode),
                                    location = new SourceLocation(new Position(1, 31, 31), new Position(1, 44, 44)),
                                    argument = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        location = new SourceLocation(new Position(1, 38, 38), new Position(1, 44, 44)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)), name = "eval"},
                                                kind = PropertyKind.Initialise,
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)), name = "eval"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function foo() { return {yield} }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), name = "foo"},
                        generator = false,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 15, 15), new Position(1, 33, 33)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ReturnStatementNode),
                                    location = new SourceLocation(new Position(1, 17, 17), new Position(1, 31, 31)),
                                    argument = new TestNode
                                    {
                                        type = typeof(ObjectExpressionNode),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)),
                                        properties = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(PropertyNode),
                                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)), name = "yield"},
                                                kind = PropertyKind.Initialise,
                                                value = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)), name = "yield"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("function foo() { 'use strict'; return {yield} }", "The keyword 'yield' is reserved (1:39)", new Options {ecmaVersion = 6});

            Program.TestFail("function foo() { 'use strict'; var {arguments} = {} }", "Binding arguments in strict mode (1:36)", new Options {ecmaVersion = 6});
            Program.TestFail("function foo() { 'use strict'; var {eval} = {} }", "Binding eval in strict mode (1:36)", new Options {ecmaVersion = 6});
            Program.TestFail("function foo() { 'use strict'; var {arguments = 1} = {} }", "Binding arguments in strict mode (1:36)", new Options {ecmaVersion = 6});
            Program.TestFail("function foo() { 'use strict'; var {eval = 1} = {} }", "Binding eval in strict mode (1:36)", new Options {ecmaVersion = 6});

            // cannot use yield expressions in parameters.
            Program.TestFail("function* wrap() { function* foo(a = 1 + (yield)) {} }", "Yield expression cannot be a default value (1:42)", new Options {ecmaVersion = 6});
            Program.TestFail("function* wrap() { return (a = 1 + (yield)) => a }", "Yield expression cannot be a default value (1:36)", new Options {ecmaVersion = 6});

            // can use yield expressions in parameters if it's inside of a nested generator.
            Program.Test("function* foo(a = function*(b) { yield b }) { }", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 42, 42)),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                right = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 42, 42)),
                                    id = null,
                                    generator = true,
                                    expression = false,
                                    parameters = new List<TestNode>
                                    {
                                        new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), name = "b"}
                                    },
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        location = new SourceLocation(new Position(1, 31, 31), new Position(1, 42, 42)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                location = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)),
                                                expression = new TestNode
                                                {
                                                    type = typeof(YieldExpressionNode),
                                                    location = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)),
                                                    @delegate = false,
                                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 39, 39), new Position(1, 40, 40)), name = "b"}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // 'yield' as function names.

            Program.Test("function* yield() {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "yield"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("({*yield() {}})", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new TestNode
                        {
                            type = typeof(ObjectExpressionNode),
                            location = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)),
                            properties = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(PropertyNode),
                                    location = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 3, 3), new Position(1, 8, 8)), name = "yield"},
                                    kind = PropertyKind.Initialise,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("class A {*yield() {}}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ClassDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), name = "A"},
                        superClass = null,
                        body = new TestNode
                        {
                            type = typeof(ClassBodyNode),
                            location = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(MethodDefinitionNode),
                                    location = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20)),
                                    computed = false,
                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), name = "yield"},
                                    @static = false,
                                    kind = PropertyKind.Method,
                                    value = new TestNode
                                    {
                                        type = typeof(FunctionExpressionNode),
                                        location = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)),
                                        id = null,
                                        generator = true,
                                        expression = false,
                                        parameters = new List<TestNode>(),
                                        body = new TestNode
                                        {
                                            type = typeof(BlockStatementNode),
                                            location = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20)),
                                            body = new List<TestNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("(function* yield() {})", "Can not use 'yield' as identifier inside a generator (1:11)", new Options {ecmaVersion = 6});
            Program.TestFail("function* wrap() {\nfunction* yield() {}\n}", "Can not use 'yield' as identifier inside a generator (2:10)", new Options {ecmaVersion = 6});
            Program.Test("function* wrap() {\n({*yield() {}})\n}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Program.Test("function* wrap() {\nclass A {*yield() {}}\n}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            // Forbid yield expressions in default parameters:
            Program.TestFail("function* foo(a = yield b) {}", "Yield expression cannot be a default value (1:18)", new Options {ecmaVersion = 6});
            Program.TestFail("(function* foo(a = yield b) {})", "Yield expression cannot be a default value (1:19)", new Options {ecmaVersion = 6});
            Program.TestFail("({*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:11)", new Options {ecmaVersion = 6});
            Program.TestFail("(class {*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:17)", new Options {ecmaVersion = 6});
            Program.TestFail("function* foo(a = class extends (yield b) {}) {}", "Yield expression cannot be a default value (1:33)", new Options {ecmaVersion = 6});

            // Allow yield expressions inside functions in default parameters:
            Program.Test("function* foo(a = function* foo() { yield b }) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 49, 49)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 49, 49)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 45, 45)),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                right = new TestNode
                                {
                                    type = typeof(FunctionExpressionNode),
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 45, 45)),
                                    id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)), name = "foo"},
                                    generator = true,
                                    expression = false,
                                    parameters = new List<TestNode>(),
                                    body = new TestNode
                                    {
                                        type = typeof(BlockStatementNode),
                                        location = new SourceLocation(new Position(1, 34, 34), new Position(1, 45, 45)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(ExpressionStatementNode),
                                                location = new SourceLocation(new Position(1, 36, 36), new Position(1, 43, 43)),
                                                expression = new TestNode
                                                {
                                                    type = typeof(YieldExpressionNode),
                                                    location = new SourceLocation(new Position(1, 36, 36), new Position(1, 43, 43)),
                                                    @delegate = false,
                                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 42, 42), new Position(1, 43, 43)), name = "b"}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 47, 47), new Position(1, 49, 49)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function* foo(a = {*bar() { yield b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 38, 38)),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                right = new TestNode
                                {
                                    type = typeof(ObjectExpressionNode),
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38)),
                                    properties = new List<TestNode>
                                    {
                                        new TestNode
                                        {
                                            type = typeof(PropertyNode),
                                            location = new SourceLocation(new Position(1, 19, 19), new Position(1, 37, 37)),
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), name = "bar"},
                                            kind = PropertyKind.Initialise,
                                            value = new TestNode
                                            {
                                                type = typeof(FunctionExpressionNode),
                                                location = new SourceLocation(new Position(1, 23, 23), new Position(1, 37, 37)),
                                                id = null,
                                                generator = true,
                                                expression = false,
                                                parameters = new List<TestNode>(),
                                                body = new TestNode
                                                {
                                                    type = typeof(BlockStatementNode),
                                                    location = new SourceLocation(new Position(1, 26, 26), new Position(1, 37, 37)),
                                                    body = new List<TestNode>
                                                    {
                                                        new TestNode
                                                        {
                                                            type = typeof(ExpressionStatementNode),
                                                            location = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35)),
                                                            expression = new TestNode
                                                            {
                                                                type = typeof(YieldExpressionNode),
                                                                location = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35)),
                                                                @delegate = false,
                                                                argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), name = "b"}
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 40, 40), new Position(1, 42, 42)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function* foo(a = class {*bar() { yield b }}) {}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), name = "foo"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>
                        {
                            new TestNode
                            {
                                type = typeof(AssignmentPatternNode),
                                location = new SourceLocation(new Position(1, 14, 14), new Position(1, 44, 44)),
                                left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), name = "a"},
                                right = new TestNode
                                {
                                    type = typeof(ClassExpressionNode),
                                    location = new SourceLocation(new Position(1, 18, 18), new Position(1, 44, 44)),
                                    id = null,
                                    superClass = null,
                                    body = new TestNode
                                    {
                                        type = typeof(ClassBodyNode),
                                        location = new SourceLocation(new Position(1, 24, 24), new Position(1, 44, 44)),
                                        body = new List<TestNode>
                                        {
                                            new TestNode
                                            {
                                                type = typeof(MethodDefinitionNode),
                                                location = new SourceLocation(new Position(1, 25, 25), new Position(1, 43, 43)),
                                                computed = false,
                                                key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), name = "bar"},
                                                @static = false,
                                                kind = PropertyKind.Method,
                                                value = new TestNode
                                                {
                                                    type = typeof(FunctionExpressionNode),
                                                    location = new SourceLocation(new Position(1, 29, 29), new Position(1, 43, 43)),
                                                    id = null,
                                                    generator = true,
                                                    expression = false,
                                                    parameters = new List<TestNode>(),
                                                    body = new TestNode
                                                    {
                                                        type = typeof(BlockStatementNode),
                                                        location = new SourceLocation(new Position(1, 32, 32), new Position(1, 43, 43)),
                                                        body = new List<TestNode>
                                                        {
                                                            new TestNode
                                                            {
                                                                type = typeof(ExpressionStatementNode),
                                                                location = new SourceLocation(new Position(1, 34, 34), new Position(1, 41, 41)),
                                                                expression = new TestNode
                                                                {
                                                                    type = typeof(YieldExpressionNode),
                                                                    location = new SourceLocation(new Position(1, 34, 34), new Position(1, 41, 41)),
                                                                    @delegate = false,
                                                                    argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 40, 40), new Position(1, 41, 41)), name = "b"}
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 46, 46), new Position(1, 48, 48)),
                            body = new List<TestNode>()
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Program.Test("function* wrap() {\n(a = yield b)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 34)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 34)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "wrap"},
                        generator = true,
                        expression = false,
                        parameters = new List<TestNode>(),
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 34)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    location = new SourceLocation(new Position(2, 0, 19), new Position(2, 13, 32)),
                                    expression = new TestNode
                                    {
                                        type = typeof(AssignmentExpressionNode),
                                        location = new SourceLocation(new Position(2, 1, 20), new Position(2, 12, 31)),
                                        @operator = Operator.Assignment,
                                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 1, 20), new Position(2, 2, 21)), name = "a"},
                                        right = new TestNode
                                        {
                                            type = typeof(YieldExpressionNode),
                                            location = new SourceLocation(new Position(2, 5, 24), new Position(2, 12, 31)),
                                            @delegate = false,
                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 11, 30), new Position(2, 12, 31)), name = "b"}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.TestFail("function* wrap() {\n(a = yield b) => a\n}", "Yield expression cannot be a default value (2:5)", new Options {ecmaVersion = 6});

            Program.Test("function* wrap() {\n({a = yield b} = obj)\n}", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 42)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 42)),
                        id = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), name = "wrap"},
                        parameters = new List<TestNode>(),
                        generator = true,
                        expression = false,
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            location = new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 42)),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    location = new SourceLocation(new Position(2, 0, 19), new Position(2, 21, 40)),
                                    expression = new TestNode
                                    {
                                        type = typeof(AssignmentExpressionNode),
                                        location = new SourceLocation(new Position(2, 1, 20), new Position(2, 20, 39)),
                                        @operator = Operator.Assignment,
                                        left = new TestNode
                                        {
                                            type = typeof(ObjectPatternNode),
                                            location = new SourceLocation(new Position(2, 1, 20), new Position(2, 14, 33)),
                                            properties = new List<TestNode>
                                            {
                                                new TestNode
                                                {
                                                    type = typeof(PropertyNode),
                                                    location = new SourceLocation(new Position(2, 2, 21), new Position(2, 13, 32)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 21), new Position(2, 3, 22)), name = "a"},
                                                    kind = PropertyKind.Initialise,
                                                    value = new TestNode
                                                    {
                                                        type = typeof(AssignmentPatternNode),
                                                        location = new SourceLocation(new Position(2, 2, 21), new Position(2, 13, 32)),
                                                        left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 2, 21), new Position(2, 3, 22)), name = "a"},
                                                        right = new TestNode
                                                        {
                                                            type = typeof(YieldExpressionNode),
                                                            location = new SourceLocation(new Position(2, 6, 25), new Position(2, 13, 32)),
                                                            @delegate = false,
                                                            argument = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 12, 31), new Position(2, 13, 32)), name = "b"}
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(2, 17, 36), new Position(2, 20, 39)), name = "obj"}
                                    }
                                }
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("export default class Foo {}++x", new TestNode
            {
                type = typeof(ProgramNode),
                source = SourceType.Module,
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExportDefaultDeclarationNode),
                        declaration = new TestNode
                        {
                            type = typeof(ClassDeclarationNode),
                            id = new TestNode {type = typeof(IdentifierNode), name = "Foo"},
                            superClass = null,
                            body = new TestNode
                            {
                                type = typeof(ClassBodyNode),
                                body = new List<TestNode>()
                            }
                        }
                    },
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(UpdateExpressionNode),
                            @operator = Operator.Increment,
                            argument = new TestNode {type = typeof(IdentifierNode), name = "x"}
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.Test("function *f() { yield\n{}/1/g\n}", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(FunctionDeclarationNode),
                        id = new TestNode {type = typeof(IdentifierNode), name = "f"},
                        body = new TestNode
                        {
                            type = typeof(BlockStatementNode),
                            body = new List<TestNode>
                            {
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(YieldExpressionNode),
                                        argument = null,
                                        @delegate = false
                                    }
                                },
                                new TestNode
                                {
                                    type = typeof(BlockStatementNode),
                                    body = new List<TestNode>()
                                },
                                new TestNode
                                {
                                    type = typeof(ExpressionStatementNode),
                                    expression = new TestNode
                                    {
                                        type = typeof(LiteralNode),
                                        raw = "/1/g",
                                        regex = new RegexNode
                                        {
                                            Pattern = "1",
                                            Flags = "g"
                                        }
                                    }
                                }
                            }
                        },
                        generator = true
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("class B extends A { foo(a = super.foo()) { return a }}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.TestFail("function* wrap() {\n({a = yield b} = obj) => a\n}", "Yield expression cannot be a default value (2:6)", new Options {ecmaVersion = 6});

            // invalid syntax '*foo: 1'
            Program.TestFail("({*foo: 1})", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Program.Test("export { x as y } from './y.js';\nexport { x as z } from './z.js';", new TestNode {type = typeof(ProgramNode)}, new Options {sourceType = SourceType.Module, ecmaVersion = 6});

            Program.Test("export { default as y } from './y.js';\nexport default 42;", new TestNode {type = typeof(ProgramNode)}, new Options {sourceType = SourceType.Module, ecmaVersion = 6});
            
            Program.TestFail("export { default} from './y.js';\nexport default 42;", "Duplicate export 'default' (2:7)", new Options {sourceType = SourceType.Module, ecmaVersion = 6});
            Program.TestFail("export * from foo", "Unexpected token (1:14)", new Options{sourceType = SourceType.Module, ecmaVersion = 6});
            Program.TestFail("export { bar } from foo", "Unexpected token (1:20)", new Options{sourceType = SourceType.Module, ecmaVersion = 6});

            Program.TestFail("foo: class X {}", "Invalid labelled declaration (1:5)", new Options {ecmaVersion = 6});

            Program.TestFail("'use strict'; bar: function x() {}", "Invalid labelled declaration (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("({x, y}) = {}", "Parenthesized pattern (1:0)", new Options {ecmaVersion = 6});

            Program.Test("[x, (y), {z, u: (v)}] = foo", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("export default function(x) {};", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode {type = typeof(ExportDefaultDeclarationNode)},
                    new TestNode {type = typeof(EmptyStatementNode)}
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = SourceType.Module
            });

            Program.TestFail("var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            Program.TestFail("{ var foo = 1; let foo = 1; }", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("let foo = 1; var foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            Program.TestFail("let foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            Program.TestFail("var foo = 1; const foo = 1;", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("const foo = 1; var foo = 1;", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("var [foo] = [1]; let foo = 1;", "Identifier 'foo' has already been declared (1:21)", new Options {ecmaVersion = 6});

            Program.TestFail("var [{ bar: [foo] }] = x; let {foo} = 1;", "Identifier 'foo' has already been declared (1:31)", new Options {ecmaVersion = 6});

            Program.TestFail("if (x) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:24)", new Options {ecmaVersion = 6});

            Program.TestFail("if (x) {} else var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:32)", new Options {ecmaVersion = 6});

            Program.TestFail("if (x) var foo = 1; else {} let foo = 1;", "Identifier 'foo' has already been declared (1:32)", new Options {ecmaVersion = 6});

            Program.TestFail("if (x) {} else if (y) {} else var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:47)", new Options {ecmaVersion = 6});

            Program.TestFail("while (x) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:27)", new Options {ecmaVersion = 6});

            Program.TestFail("do var foo = 1; while (x) let foo = 1;", "Identifier 'foo' has already been declared (1:30)", new Options {ecmaVersion = 6});

            Program.TestFail("for (;;) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:26)", new Options {ecmaVersion = 6});

            Program.TestFail("for (const x of y) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            Program.TestFail("for (const x in y) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            Program.TestFail("label: var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:24)", new Options {ecmaVersion = 6});

            Program.TestFail("switch (x) { case 0: var foo = 1 } let foo = 1;", "Identifier 'foo' has already been declared (1:39)", new Options {ecmaVersion = 6});

            Program.TestFail("try { var foo = 1; } catch (e) {} let foo = 1;", "Identifier 'foo' has already been declared (1:38)", new Options {ecmaVersion = 6});

            Program.TestFail("function foo() {} let foo = 1;", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            Program.TestFail("{ var foo = 1; } let foo = 1;", "Identifier 'foo' has already been declared (1:21)", new Options {ecmaVersion = 6});

            Program.TestFail("let foo = 1; { var foo = 1; }", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            Program.TestFail("let foo = 1; function x(foo) {} { var foo = 1; }", "Identifier 'foo' has already been declared (1:38)", new Options {ecmaVersion = 6});

            Program.TestFail("if (x) { if (y) var foo = 1; } let foo = 1;", "Identifier 'foo' has already been declared (1:35)", new Options {ecmaVersion = 6});

            Program.TestFail("var foo = 1; function x() {} let foo = 1;", "Identifier 'foo' has already been declared (1:33)", new Options {ecmaVersion = 6});

            Program.TestFail("{ let foo = 1; { let foo = 2; } let foo = 1; }", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            Program.TestFail("for (var foo of y) {} let foo = 1;", "Identifier 'foo' has already been declared (1:26)", new Options {ecmaVersion = 6});

            Program.TestFail("function x(foo) { let foo = 1; }", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            Program.TestFail("var [...foo] = x; let foo = 1;", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            Program.TestFail("foo => { let foo; }", "Identifier 'foo' has already been declared (1:13)", new Options {ecmaVersion = 6});

            Program.TestFail("({ x(foo) { let foo; } })", "Identifier 'foo' has already been declared (1:16)", new Options {ecmaVersion = 6});

            Program.TestFail("try {} catch (foo) { let foo = 1; }", "Identifier 'foo' has already been declared (1:25)", new Options {ecmaVersion = 6});

            Program.Test("var foo = 1; var foo = 1;", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("if (x) var foo = 1; var foo = 1;", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("function x() { var foo = 1; } let foo = 1;", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("function foo() { let foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("var foo = 1; { let foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("{ let foo = 1; { let foo = 2; } }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("var foo; try {} catch (_) { let foo; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("let x = 1; function foo(x) {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (let i = 0;;); for (let i = 0;;);", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (const foo of bar); for (const foo of bar);", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (const foo in bar); for (const foo in bar);", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (let foo in bar) { let foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (let foo of bar) { let foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("class Foo { method(foo) {} method2() { let foo; } }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("() => { let foo; }; foo => {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("() => { let foo; }; () => { let foo; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("switch(x) { case 1: let foo = 1; } let foo = 1;", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("'use strict'; function foo() { let foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("let foo = 1; function x() { var foo = 1; }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("[...foo, bar = 1]", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (var a of /b/) {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (var {a} of /b/) {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("for (let {a} of /b/) {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("function* bar() { yield /re/ }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("function* bar() { yield class {} }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("() => {}\n/re/", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("(() => {}) + 2", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.TestFail("(x) => {} + 2", "Unexpected token (1:10)", new Options {ecmaVersion = 6});

            Program.Test("function *f1() { function g() { return yield / 1 } }", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("class Foo {} /regexp/", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("(class Foo {} / 2)", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});

            Program.Test("1 <!--b", new TestNode
            {
                type = typeof(ProgramNode),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            @operator = Operator.LessThan
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = SourceType.Module});

            Program.TestFail("class A extends B { constructor() { super } }", "Unexpected token (1:42)", new Options {ecmaVersion = 6});
            Program.TestFail("class A extends B { constructor() { super; } }", "Unexpected token (1:41)", new Options {ecmaVersion = 6});
            Program.TestFail("class A extends B { constructor() { (super)() } }", "Unexpected token (1:42)", new Options {ecmaVersion = 6});
            Program.TestFail("class A extends B { foo() { (super).foo } }", "Unexpected token (1:34)", new Options {ecmaVersion = 6});
            Program.Test("({super: 1})", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
            Program.Test("import {super as a} from 'a'", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.Test("export {a as super}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6, sourceType = SourceType.Module});
            Program.Test("let instanceof Foo", new TestNode
            {
                type = typeof(ProgramNode),
                location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<TestNode>
                {
                    new TestNode
                    {
                        type = typeof(ExpressionStatementNode),
                        location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        expression = new TestNode
                        {
                            type = typeof(BinaryExpressionNode),
                            location = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                            left = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), name = "let"},
                            @operator = Operator.InstanceOf,
                            right = new TestNode {type = typeof(IdentifierNode), location = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), name = "Foo"}
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Program.Test("function fn({__proto__: a, __proto__: b}) {}", new TestNode {type = typeof(ProgramNode)}, new Options {ecmaVersion = 6});
        }
    }
}
